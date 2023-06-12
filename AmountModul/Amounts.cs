/*
 * to input ingredients of recipe (formulation) and insert ones into DB
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;

namespace MajPAbGr_project
{
    public partial class InsertAmounts : Form
    {
        double summa;
        double[] amounts;
        Mode mode; //0 - Create, 1 - Edit, 2 - EditNewMain
        List<Element> elements; //list of recipe ingredients
        List<Item> ingredients;

        AmountsController controller;
        tbIngredientsController tbIngred;
        tbAmountsController tbAmount;
        CalcFunction calc;

        NumberFormatInfo nfi;
        string decimal_separator;        

        public InsertAmounts(AmountsController controller)
        {
            InitializeComponent();
            this.controller = controller;
            tbIngred = controller.TbIngred;
            tbAmount = controller.TbAmount;
            calc = controller.Calc;
            elements = controller.Elements;
            ingredients = controller.Ingredients;
            mode = controller.getMode;
            summa = 0.0;
        }

        private void InsertAmounts_Load(object sender, EventArgs e)
        {
            /*
            * Lokalization
            */
            CultureInfo.CurrentCulture = new CultureInfo("us-US");
            localizacijaToolStripMenuItem.Text = $"US (\'{decimal_separator}\')";
            nfi = CultureInfo.CurrentCulture.NumberFormat;
            decimal_separator = nfi.NumberDecimalSeparator;

            /*
             * Textbox: ingredients and formulations
             */
            cmbIngr.AutoCompleteCustomSource = AutoCompleteNames
                (FormFunction.setBox(ingredients, cmbIngr));
            cmbIngr.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbIngr.AutoCompleteSource = AutoCompleteSource.CustomSource;

            txbRecipe.AutoCompleteCustomSource = AutoCompleteNames
                (controller.getRecipesNames());
            txbRecipe.AutoCompleteMode = AutoCompleteMode.Suggest;
            txbRecipe.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection AutoCompleteNames(List<string> list)
            {
                AutoCompleteStringCollection source = new AutoCompleteStringCollection();
                if (list.Count > 0)
                {                
                    foreach (String el in list) source.Add(el);                   
                }
                else
                {
                    source.Add("none");
                }
                return source;
            }

            /*
             *  set elements
             */

            lblName.Text = controller.Info.getName(); 
            
            FillAmountsView(); // listview
            if (mode == (Mode)1) // for edit mode
            {
                fillAmounts();                
                showOldAmounts();
                lvRecipe.Columns[1].Text = "Amounts(%) new";
                lvRecipe.Columns[2].Text += " old";
            }
            
             //a main ingredient, coefficients
            lblMain.Text = controller.Main;
            lblRecipe.Text = string.Format("{0:f2}", controller.Recipe);
            lblCoef.Text = string.Format("{0:f2}", controller.Calc.Coefficient);
            
            // save a coefficient (recipe)
            if (mode == Mode.Create)           
                txbRecipe.Enabled = true;                            
            else                          
                txbRecipe.Enabled = false;
        }

        /*
         * Lokalization setting
         */
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CultureInfo.CurrentCulture = new CultureInfo("us-US");
            nfi = CultureInfo.CurrentCulture.NumberFormat;
            decimal_separator = nfi.NumberDecimalSeparator;           
            localizacijaToolStripMenuItem.Text = $"US (\'{decimal_separator}\')";           
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CultureInfo.CurrentCulture = new CultureInfo("lv-LV");
            nfi = CultureInfo.CurrentCulture.NumberFormat;
            decimal_separator = nfi.NumberDecimalSeparator;            
            localizacijaToolStripMenuItem.Text = $"LV (\'{decimal_separator}\')";         
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            nfi = CultureInfo.CurrentCulture.NumberFormat;
            decimal_separator = nfi.NumberDecimalSeparator;            
            localizacijaToolStripMenuItem.Text = $"RU (\'{decimal_separator}\')";
        }

        /*
         * end of lokalization setting
         */

        private void fillAmounts()
        {
            amounts = new double[elements.Count + 1];
            int k;
            for (k = 0; k < amounts.Length - 1; k++)
            {
                amounts[k] = elements[k].Amounts;
            }

            calc.setAmounts(elements); // save and sum values
            amounts[k] = calc.getTotal();
            summa = calc.getTotal();          
        }

        private void FillAmountsView()
        {             
            ListView list = lvRecipe;
            list.Items.Clear();

            ListViewItem items;
            for (int k = 0; k < elements.Count; k++)
            {
                string t;
                items = new ListViewItem(elements[k].Name);
                items.Tag = elements[k].Id;
                t = string.Format("{0:f2}", elements[k].Amounts);
                items.SubItems.Add(t);
                items.SubItems.Add(""); // for calculated values
                lvRecipe.Items.Add(items);
            }
        }

        private void showOldAmounts()
        {
            string t;
            ListView list = lvRecipe;
            for (int k = 0; k < elements.Count; k++)
            {
                t = string.Format("{0:f2}", amounts[k]);
                list.Items[k].SubItems[2].Text = t;
            }
        }

        private void cmbIngr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ingredients.Count < 1) return;
            
            tbIngred.setSelected(cmbIngr.SelectedIndex);               
            txbAmounts.Text = "";            
            txbAmounts.Focus();
        }

        private void btn_edit_onClick() // add an ingredient
        {
            if (cmbIngr.SelectedIndex == -1) return;            
            if (string.IsNullOrEmpty(txbAmounts.Text)) return;            
        
            int index, index_ingr, new_index;
            double amount;
            string item_text = "";
            Element el;
            ListViewItem item;

            if (double.TryParse(txbAmounts.Text, out amount))
                amount = double.Parse(txbAmounts.Text);
            else return;
                                    
            // getting index            
            if (lvRecipe.SelectedItems.Count > 0)
            {
                index = lvRecipe.SelectedItems[0].Index;
            }
            else
            {
                if (lvRecipe.Items.Count > 0)
                    index = lvRecipe.Items.Count - 1;
                else
                    index = -1;
            }            
            index_ingr = cmbIngr.SelectedIndex;         
            
            //writing into mirror of table
            new_index = controller.SetMain(amount, index);
            el = controller.AddElement(index_ingr, index);
            controller.SetAmounts(amount, el);

            //inserting in listview new item           
            item = new ListViewItem(el.Name);
            mode = controller.getMode;
            if (mode == Mode.Edit)
                item_text = string.Format("{0:f2}", el.Amounts);
            else
                item_text = string.Format("{0:f2}", amount);           
            item.SubItems.Add(item_text);
            item_text = string.Format("{0:f2}", el.Amounts);
            item.SubItems.Add(item_text);
            item.Tag = el.Id;
            if (lvRecipe.SelectedItems.Count > 0)               
                lvRecipe.SelectedItems[0].Selected = false;            
                
            try
            {               
                item = lvRecipe.Items.Insert(new_index, item);                          
                item.Selected = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                lvRecipe.Items.Add(item);                
                new_index = lvRecipe.Items.Count - 1;
                lvRecipe.Items[new_index].Selected = true;
            }
        }

        private void btn_edit_Click(object sender, EventArgs e) // add an ingredient
        {
            btn_edit_onClick();

            //main ingredient, coefficients
            lblMain.Text = controller.Main;
            lblRecipe.Text = string.Format("{0:f2}", controller.Recipe);
            lblCoef.Text = string.Format("{0:f2}", controller.Calc.Coefficient);
        }

        private void btn_remove_onClick()
        {
            if (lvRecipe.Items.Count < 1) return;
            if (lvRecipe.SelectedItems.Count < 1) return;
            if (lvRecipe.SelectedItems[0].Index < 0) return;

            int index, result;

            double coef = controller.Calc.Coefficient;
            double recipe = controller.Recipe;            

            // deleting value
            index = lvRecipe.SelectedItems[0].Index;
            lvRecipe.Items[index].Selected = false;           

            result = controller.RemoveElement(index);

            if (result > 0)
            {
                lvRecipe.Items.RemoveAt(index);
                lvRecipe.Items[result].Selected = true;
            }
            else
            {
                if (result == 0)
                {
                    lvRecipe.Items.RemoveAt(index);
                    if (lvRecipe.Items.Count > result)
                        lvRecipe.Items[result].Selected = true;

                    if (index == 0)
                    {
                        double amount = 0.0;
                        controller.RemoveMain(); // remove data about a main ingredient                
                        if (controller.Elements.Count > 0 && lvRecipe.Items.Count > 0)
                            amount = double.Parse
                                (lvRecipe.Items[0].SubItems[1].Text);
                        if (amount > 0.0)
                        {
                            if (controller.ResetMain(amount)) // set new data of a main ingredient
                            {
                                for (int k = 1; k < lvRecipe.Items.Count; k++)
                                {
                                    double num;
                                    if (double.TryParse
                                        (lvRecipe.Items[k].SubItems[1].Text, out num))
                                    {
                                        num = double.Parse
                                            (lvRecipe.Items[k].SubItems[1].Text);
                                    }
                                    else num = 0;
                                    controller.Elements[k].Amounts = num;
                                }
                                elements = controller.ResetAmounts();  // amounts of ingredients will be recalculated          
                                for (int k = 0; k < elements.Count; k++)
                                {
                                    double new_amount = elements[k].Amounts;
                                    lvRecipe.Items[k].SubItems[2].Text
                                        = string.Format("{0:f2}", new_amount);
                                }                        
                            }
                        }                   
                        else
                        {
                            if (lvRecipe.Items.Count > 0)
                                lvRecipe.Items[0].Selected = true;                                
                        }
                        mode = controller.getMode; // reload mode value from controller, because it may be changed there
                    }
                }        
            }
    }
        private void btn_remove_Click(object sender, EventArgs e)
        {
            btn_remove_onClick();

            //main ingredient, coefficients
            lblMain.Text = controller.Main;
            lblRecipe.Text = string.Format("{0:f2}", controller.Recipe);
            lblCoef.Text = string.Format("{0:f2}", controller.Calc.Coefficient);
        }

        /*
         * Writting into DB
         */
        private void btn_submit_Click(object sender, EventArgs e) // submit
        {
            int ind = 0;
            mode = controller.getMode;

            ind = tbAmount.updateRecords();            
            if (mode == Mode.Create)
            {
                if (ind == elements.Count)
                    MessageBox.Show("All amounts are inserted");
                else
                {
                    if (ind > 0)
                        MessageBox.Show($"{ind} from {elements.Count} are inserted");
                    else
                    {
                        MessageBox.Show("Sorry! Nothing is inserted!");                   
                    }
                }                   
                ind = 0;                
                {
                    ind = controller.UpdateMain();
                    switch (ind)
                    {
                        case -1:
                            MessageBox.Show("Unknown recepture");
                            break;
                        case -2:                           
                            MessageBox.Show("Sorry! Something goes wrong!");
                            break;
                    }
                }      
                
                ind = 0;
                    if (!string.IsNullOrEmpty(txbRecipe.Text))
                    {
                        ind = controller.saveRecipe(txbRecipe.Text);
                        if (ind < 0)
                            MessageBox.Show($"Recipe \'{txbRecipe.Text}\' does not saved");
                    }
                             
            }
            else
            {
                int test = tbAmount.Amount_id_count > elements.Count ? tbAmount.Amount_id_count : elements.Count;
                if (ind > 0)
                {
                    if (ind == test)
                    {
                        MessageBox.Show("Changes are succeful saved");
                    }
                    else
                    {
                        MessageBox.Show($"{ind} from {test} are changed");
                    }
                }
                else
                {
                    if (ind > -1)
                    {
                        if (test > 1)
                            MessageBox.Show("Sorry! Something goes wrong!");                                                 
                    }
                    else
                    {
                        MessageBox.Show("Something does not changed");  
                    }                
                }

                ind = 0;
                {                    
                    ind = controller.UpdateMain();
                    switch (ind)
                    {
                        case -1:
                            MessageBox.Show("Unknown recepture");
                            break;
                        case -2:
                            MessageBox.Show("Sorry! Something goes wrong!");
                            break;
                    }
                }
            }
            this.Close();
            this.Dispose();
        }

        /*
         * ListView and Reload
        */
        private void lvRecipe_onIndexChanged()
        {
            if (lvRecipe.SelectedItems.Count < 1) return;
            int index = lvRecipe.SelectedItems[0].Index;

            checked
            {
                try
                {
                    int id;
                    string amount;
                    Element el;

                    el = controller.changeSelectedElement(index);
                    id = el.Id;
                    amount = string.Format("{0:f2}", el.Amounts);

                    cmbIngr.SelectedIndex = FormFunction.ChangeIndex(ingredients, id);
                    txbAmounts.Text = amount;
                    lvRecipe.Focus();
                }
                catch (ArgumentOutOfRangeException)
                {
                    return;
                }
            }
        }

        private void lvRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvRecipe_onIndexChanged();
        }

        private void lblReload_Click(object sender, EventArgs e)
        {
            Refresh();            
            fillAmounts();
            FillAmountsView(); // listview            
            showOldAmounts(); // for edit mode
            if (lvRecipe.Items.Count > 0)
                lvRecipe.Items[0].Selected = true;


            lblMain.Text = controller.Main;
            lblRecipe.Text = "none";
            lblCoef.Text = "none";                        
        }

        private new void Refresh()
        {
            if (controller.getMode == (Mode).0) return;            
            controller.RefreshElements();
            mode = controller.getMode;
            elements = controller.Elements;
        }
    }
}
