using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace MajPAbGr_project
{
    public partial class InsertAmounts : Form
    {
        readonly int id_recepture; // formulation's id
       
       
        double summa = 0;
        double[] amounts;

        Mode mode; //create new or edit old
        string recepture_name; // name of recepture, for assigning this.Text when form is loading

        List<Element> elements; // id and name, for amounts
        List<Item> ingredients;

        AmountsController controller;
        tbIngredientsController tbIngred;
        tbAmountsController tbAmount;
        CalcFunction calc;

        NumberFormatInfo nfi;
        string decimal_separator;
        Print frm = new Print();

        public InsertAmounts(AmountsController controller) // this is main and it have to be one
        {
            InitializeComponent();
            this.controller = controller;
            tbIngred = controller.TbIngred;
            tbAmount = controller.TbAmount;
            calc = controller.Calc;
            elements = controller.Elements;
            ingredients = controller.Ingredients;

            id_recepture = tbAmount.Id_recepture;
            recepture_name = tbAmount.dbReader($"select name from Recepture where id = {id_recepture}")[0]; // for this.Text           
            
            mode = controller.getMode;
        }

        private void InsertAmounts_Load(object sender, EventArgs e)
        {
            if (mode == Mode.Create)           
                txbRecipe.Enabled = true;                            
            else                          
                txbRecipe.Enabled = false;
           

            FillAmountsView(); // listview
            if (mode == (Mode)1) // for edit mode
            {
                fillAmounts();                
                showOldAmounts();           
            }

            if (mode == Mode.Edit) //1
            {
                listView1.Columns[1].Text = "Amounts(%) new";
                listView1.Columns[2].Text += " old";
            }

            label8.Text = recepture_name;
            txbAmounts.Text = "100" + decimal_separator + "0";

            lblMain.Text = controller.Main;
            lblRecipe.Text = string.Format("{0:f2}", controller.Recipe);
            lblCoef.Text = string.Format("{0:f2}", controller.Calc.Coefficient);


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
                (Class1.setBox(ingredients, cmbIngr));
            cmbIngr.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbIngr.AutoCompleteSource = AutoCompleteSource.CustomSource;

            txbRecipe.AutoCompleteCustomSource = AutoCompleteNames
                (tbAmount.dbReader($"select name from Recipe;"));
            txbRecipe.AutoCompleteMode = AutoCompleteMode.Suggest;
            txbRecipe.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection AutoCompleteNames(List<string> list)
            {
                AutoCompleteStringCollection source = new AutoCompleteStringCollection();
                foreach (String el in list) source.Add(el);
                return source;
            }
        }

        /*
         * Lokalization
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
         * end
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
            ListView list = listView1;
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
                listView1.Items.Add(items);
            }
        }

        private void showOldAmounts()
        {
            string t;
            ListView list = listView1;
            for (int k = 0; k < elements.Count; k++)
            {
                t = string.Format("{0:f2}", amounts[k]);
                list.Items[k].SubItems[2].Text = t;
            }
        }

        private void cmbIngr_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbIngred.setSelected(cmbIngr.SelectedIndex);           
           
            txbAmounts.Focus();
            if (mode == Mode.Create)
                txbAmounts.Text = "100" + decimal_separator + "0";
            else txbAmounts.Text = "";       
        }

        private void btn_edit_onClick()
        {
            if (cmbIngr.SelectedIndex == -1) return;            
            if (string.IsNullOrEmpty(txbAmounts.Text)) return;            
        
            int index, index_ingr, new_index;
            double amount, new_amount;
            Element el;
            ListViewItem item;

            if (double.TryParse(txbAmounts.Text, out amount))
                amount = double.Parse(txbAmounts.Text);
            else return;

            if (listView1.SelectedItems.Count < 1)
            {
                if (listView1.Items.Count > 0)
                    index = listView1.Items.Count - 1;
                else index = -1;
            }
            else
                index = listView1.SelectedItems[0].Index;         
            index_ingr = cmbIngr.SelectedIndex;         
            
            //writting into double of table
            new_index = controller.SetMain(amount, index);
            el = controller.AddElement(index_ingr, index);
            new_amount = controller.SetAmounts(amount, el);              

            // creating new item of listview
            mode = controller.getMode;
            item = new ListViewItem(el.Name);

            string t = "";
            if (mode == Mode.Edit)
                t = string.Format("{0:f2}", el.Amounts);
            else
                t = string.Format("{0:f2}", amount);           
            item.SubItems.Add(t);
            t = string.Format("{0:f2}", el.Amounts);
            item.SubItems.Add(t);
            item.Tag = el.Id;

            // remove selecting
            if (listView1.SelectedItems.Count > 0)               
                listView1.SelectedItems[0].Selected = false;            
                

            // inserting in listview new item
            try
            {
                //inserting new
                item = listView1.Items.Insert(new_index, item);

                //selecting new                
                item.Selected = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                // adding new to list
                listView1.Items.Add(item);

                //selecting new
                new_index = listView1.Items.Count - 1;
                listView1.Items[new_index].Selected = true;
            }
        }

        private void btn_edit_Click(object sender, EventArgs e) // add an ingredient
        {
            btn_edit_onClick();

            // inserting data: main ingredient, coefficients
            lblMain.Text = controller.Main;
            lblRecipe.Text = string.Format("{0:f2}", controller.Recipe);
            lblCoef.Text = string.Format("{0:f2}", controller.Calc.Coefficient);
        }

        private void btn_remove_onClick()
        {
            if (listView1.Items.Count < 1) return; // checking list
            if (listView1.SelectedItems.Count < 1) return;
            if (listView1.SelectedItems[0].Index < 0) return;

            int index, result;

            double coef = controller.Calc.Coefficient;
            double recipe = controller.Recipe;            

            // deleting value
            index = listView1.SelectedItems[0].Index;
            listView1.Items[index].Selected = false;           

            result = controller.RemoveElement(index);

            if (result > 0)
            {
                listView1.Items.RemoveAt(index);
                listView1.Items[result].Selected = true;
            }
            else
            {
                if (result == 0)
                {
                    listView1.Items.RemoveAt(index);
                    if (listView1.Items.Count > result)
                        listView1.Items[result].Selected = true;

                    if (index == 0)
                    {
                        double amount = 0.0;
                        controller.RemoveMain();                       
                        if (controller.Elements.Count > 0 && listView1.Items.Count > 0)
                            amount = double.Parse(listView1.Items[0].SubItems[1].Text);
                        if (amount > 0.0)
                        {
                            if (controller.ResetMain(amount))
                            {
                                for (int k = 1; k < listView1.Items.Count; k++)
                                {
                                    double num;
                                    if (double.TryParse(listView1.Items[k].SubItems[1].Text, out num))
                                    {
                                        num = double.Parse(listView1.Items[k].SubItems[1].Text);
                                    }
                                    else num = 0;
                                    controller.Elements[k].Amounts = num;
                                }
                                elements = controller.ResetAmounts();                    
                                for (int k = 0; k < elements.Count; k++)
                                {
                                    double new_amount = elements[k].Amounts;
                                    listView1.Items[k].SubItems[2].Text = string.Format("{0:f2}", new_amount);
                                }                        
                            }
                        }                   
                        else
                        {
                            if (listView1.Items.Count > 0)
                                listView1.Items[0].Selected = true;                                
                        }
                        mode = controller.getMode; // reload mode value from controller, because it may be changed there
                    }
                }        
            }
    }
        private void btn_remove_Click(object sender, EventArgs e)
        {
            btn_remove_onClick();
            lblMain.Text = controller.Main;
            lblRecipe.Text = string.Format("{0:f2}", controller.Recipe);
            lblCoef.Text = string.Format("{0:f2}", controller.Calc.Coefficient);
        }

        /*************************************************************************
         * Writting into DB
         *************************************************************************/
        private void button4_Click(object sender, EventArgs e) // submit
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

        /****************************************************************************
         * ListView and Reload
         ***************************************************************************/
        
        private void onIndexChanged()
        {   
            if (listView1.SelectedItems.Count < 1) return;            
            int index = listView1.SelectedItems[0].Index;            
            
            checked
            {
                try
                {
                    int id; // id of selected element
                    Element el = tbAmount.getElementByIndex(index);
                    tbAmount.Selected = el.Id;
                    id = tbAmount.Selected;
                    cmbIngr.SelectedIndex = Class1.ChangeIndex(ingredients, id);
                    string t = string.Format("{0:f1}", el.Amounts);                   
                    listView1.Focus();                    
                }
                catch (ArgumentOutOfRangeException)
                {                    
                    return;
                }
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            onIndexChanged();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Refresh();            
            fillAmounts();
            FillAmountsView(); // listview            
            showOldAmounts(); // for edit mode
            if (listView1.Items.Count > 0)
                listView1.Items[0].Selected = true;


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
