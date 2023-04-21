﻿using System;
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
        readonly int id_recepture; //для ввода рецепта (коэфициента) к расчитанной из него рецептуре
        //readonly CalcBase calcBase = CalcBase.Main;
        
        private int pragma = 0; // for create mode

       // private int main_ingredient_id;
        //bool set_main = false,
            //reset_main = false;
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

            this.mode = (elements.Count < 1) ? (Mode)0 : (Mode)1; // mode autodetector
            pragma = (mode == 0) ? 0 : 1;
        }

        public CalcFunction Calc
        {
            set { calc = value; }
            get { return calc; }
        }

        private void InsertAmounts_Load(object sender, EventArgs e)
        {
            if (mode == Mode.Create)                          
                txbRecipe.Enabled = true;            
            else                          
                txbRecipe.Visible = false;
           

            FillAmountsView(); // listview
            if (mode == (Mode)1) // for edit mode
            {
                fillAmounts();
                toolStripStatusLabel2.Text = summa.ToString();
                showOldAmounts();
                pragma = 1;
            }
            else pragma = 0;


            //btn_submit.Enabled = false; // submit ingredients

            if (mode == Mode.Edit) //1
            {
                listView1.Columns[1].Text = "Amounts(%) new";
                listView1.Columns[2].Text += " old";
            }

            this.Text += $" into '{recepture_name}' ";
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            nfi = CultureInfo.CurrentCulture.NumberFormat;
            decimal_separator = nfi.NumberDecimalSeparator;
            this.Text += " " + CultureInfo.CurrentCulture +
                " (decimal separator \'" + nfi.NumberDecimalSeparator + "\')";
            txbAmounts.Text = "100" + decimal_separator + "0";


            /*
             * Текстовые поля: ингредиенты и рецепты
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

            // эмулятор консоли, выводит метаданные           
            frm.Show();
            frm.richTextBox1.Text = "On Load: \n";
            frm.richTextBox1.Text += this.Text + " ";
            frm.richTextBox1.Text += this.mode == Mode.Create ? "crete mode" : "edit mode";
            frm.richTextBox1.Text += "\nelements count: " + tbAmount.Elements_count;
            frm.richTextBox1.Text += "\nid count: " + tbAmount.Amount_id_count + "\n";
            frm.richTextBox1.Text += tbAmount.Amount_id_count >= tbAmount.Elements_count;
            frm.richTextBox1.Text += "\nAmounts from array \'amounts\': \n";

            if (mode == Mode.Edit)
            {
                for (int k = 0; k < amounts.Length - 1; k++)
                {
                    frm.richTextBox1.Text += amounts[k] + " ";
                }
            }

            frm.richTextBox1.Text += "\n****\n";
        }

        /*
         * Lokalizacija
         */
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CultureInfo.CurrentCulture = new CultureInfo("us-US");
            nfi = CultureInfo.CurrentCulture.NumberFormat;
            decimal_separator = nfi.NumberDecimalSeparator;
            this.Text = "Receptures " + CultureInfo.CurrentCulture +
                " (decimal separator \'" + nfi.NumberDecimalSeparator + "\')";
            localizacijaToolStripMenuItem.Text = "US"; }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CultureInfo.CurrentCulture = new CultureInfo("lv-LV");
            nfi = CultureInfo.CurrentCulture.NumberFormat;
            decimal_separator = nfi.NumberDecimalSeparator;
            this.Text = "Receptures " + CultureInfo.CurrentCulture +
                " (decimal separator \'" + nfi.NumberDecimalSeparator + "\')";
            localizacijaToolStripMenuItem.Text = "LV";
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            nfi = CultureInfo.CurrentCulture.NumberFormat;
            decimal_separator = nfi.NumberDecimalSeparator;
            this.Text = "Receptures " + CultureInfo.CurrentCulture +
                " (decimal separator \'" + nfi.NumberDecimalSeparator + "\')";
            localizacijaToolStripMenuItem.Text = "RU";
        }

        /*
         * Konec lokalizaciji
         */

        private void fillAmounts()
        {
            amounts = new double[elements.Count + 1];
            int k;
            for (k = 0; k < amounts.Length - 1; k++)
            {
                amounts[k] = elements[k].Amounts;
            }

            calc.setAmounts(elements); // сохраняет и cуммирует величины
            amounts[k] = calc.getTotal();
            summa = calc.getTotal();

            //вывод в консоль
            frm.richTextBox1.Text += "On fillAmounts (write ingredients amounts from this elements)\n";
            for (k = 0; k < amounts.Length - 1; k++)
            {
                frm.richTextBox1.Text += amounts[k] + " ";
            }
        }

        private void FillAmountsView()
        {
            //from InputRecepture(); edited            
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
                items.SubItems.Add(""); // заготовка под старые величины или проценты  
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
            //cmbIngr.Text = cmbIngr.SelectedItem.ToString();
            txbAmounts.Focus();
            if (mode == Mode.Create)
                txbAmounts.Text = "100" + decimal_separator + "0";
            else txbAmounts.Text = "";

            frm.richTextBox1.Text += ">>> Selected item (*cmbIngr)\n";
            frm.richTextBox1.Text += "> id of selected ingredient: " + tbIngred.Selected.ToString() + "\n";
            frm.richTextBox1.Text += "> *selected index of combo: " + cmbIngr.SelectedIndex + "\n";
            frm.richTextBox1.Text += "> *selected item name: " + tbIngred.getName(cmbIngr.SelectedIndex) + "\n";            
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
            
            //записываем в слепок с таблицы
            new_index = controller.SetMain(amount, index);
            el = controller.AddElement(index_ingr, index);
            new_amount = controller.SetAmounts(amount, el);

            //toolStripStatusLabel7.Text = controller.Calc.Coefficient.ToString();     

            // создаём новую единицу списочного представления
            mode = controller.getMode;
            item = new ListViewItem(el.Name);

            string t = "";
            if (mode == Mode.Edit)
                t = string.Format("{0:f2}", el.Amounts);
            else
                t = string.Format("{0:f2}", amount);
            // item.SubItems.Add(el.Amounts.ToString());
            item.SubItems.Add(t);
            t = string.Format("{0:f2}", el.Amounts);
            item.SubItems.Add(t);
            item.Tag = el.Id;

            // убираем выделение
            if (listView1.SelectedItems.Count > 0)               
                listView1.SelectedItems[0].Selected = false;            
                

            // вставляем созданную выше единицу
            try
            {
                //вводим в список после выбранного
                item = listView1.Items.Insert(new_index, item);

                //выбираем новое
                //listView1.Items[new_index].Selected = true;
                item.Selected = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                // добавляем в список
                listView1.Items.Add(item);

                //выбираем новое
                new_index = listView1.Items.Count - 1;
                listView1.Items[new_index].Selected = true;
            }
        }

        private void btn_edit_Click(object sender, EventArgs e) // add an ingredient
        {
            btn_edit_onClick();
        }

        private void btn_remove_onClick()
        {
            if (listView1.Items.Count < 1) return; // proverka spiska
            if (listView1.SelectedItems.Count < 1) return;
            if (listView1.SelectedItems[0].Index < 0) return;

            int index, result;

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
                    controller.RemoveMain();
                    listView1.Items.RemoveAt(index);         
                    
                    if (controller.ResetMain())
                    {
                        elements = controller.ResetAmounts();                    
                        for (int k = 0; k < elements.Count; k++)
                        {
                            double new_amount = elements[k].Amounts;
                            listView1.Items[k].SubItems[2].Text = new_amount.ToString();
                            //label1.Text += calc.Coefficient.ToString() + " ";
                        }
                        listView1.Items[result].Selected = true;                       
                        //toolStripStatusLabel6.Text = calc.Coefficient.ToString();
                    }
                    else
                    {
                        if(listView1.Items.Count > 0)
                            listView1.Items[index].Selected = true;
                    }
                }        
            }
    }
        private void btn_remove_Click(object sender, EventArgs e)
        {
            btn_remove_onClick();            
        }

        /*************************************************************************
         * Запись в базу данных
         *************************************************************************/
        private void button4_Click(object sender, EventArgs e) // submit
        {
            int ind = 0;          
            //if (string.IsNullOrEmpty(listView1.Items[0].SubItems[1].Text)) return;
            
            ind = tbAmount.updateRecords(ref frm);
            
            if (mode == Mode.Create)
            {                
                if (ind == 0) MessageBox.Show("All amounts are inserted");
                else MessageBox.Show($"{ind} from {listView1.Items.Count} are inserted");
                mode = Mode.Edit;               
            }
            else
            {
                MessageBox.Show("Updated " + ind.ToString());                
            }

            // забираю в обработчик Обновления (полоса меню)
            if (!string.IsNullOrEmpty(txbRecipe.Text))
            {
                saveRecipe();
            }
        }
       
        public void saveRecipe()
        {
            int ind;
            double coefficient = calc.Coefficient;
            tbRecipeController tb = new tbRecipeController("Recipe");
            tb.Selected = id_recepture;
            tb.Recepture = id_recepture;

            if (string.IsNullOrEmpty(txbRecipe.Text)) return;
            if (coefficient != 0)
            {
                string coeff = calc.ColonToPoint(coefficient.ToString());
                ind = tb.insertNewRecipe(txbRecipe.Text, coeff);
            }
        }
        /****************************************************************************
         * Подрежимы редактирования
         ***************************************************************************/
        
        private void onIndexChanged()
        {   
            if (listView1.SelectedItems.Count < 1) return;            
            int index = listView1.SelectedItems[0].Index;            
            
            checked
            {
                try
                {
                    int id; // номер выбранного элемента
                    Element el = tbAmount.getElementByIndex(index);
                    tbAmount.Selected = el.Id;
                    id = tbAmount.Selected;
                    frm.richTextBox1.Text += ">>> Selected element\n";
                    int k;
                    for (k = 0; k < ingredients.Count; k++)
                    {
                        if (ingredients[k].id == id)
                        {
                            cmbIngr.SelectedIndex = k;
                            frm.richTextBox1.Text += "> selected index of combo item: " + cmbIngr.SelectedIndex + "\n";
                            frm.richTextBox1.Text += "> selected item name: " + cmbIngr.Items[k].ToString() + "\n";
                        }
                    }
                    string t = string.Format("{0:f1}", el.Amounts);
                    //txbAmounts.Text = el.Amounts.ToString();
                    listView1.Focus();
                    frm.richTextBox1.Text += "> Selected ingredients (name, id): " + el.Name + " " + el.Id + "\n";
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

            //if (listView1.SelectedItems.Count < 1) return;          
            //int index = listView1.SelectedItems[0].Index;
            //int tag = (int)listView1.Items[index].Tag;

            //if (tag > -1) //check tag to preserve row with sum in case when create mode
            //{
            //    btn_select.Enabled = true;
            //    btn_remove.Enabled = true;
            //    btn_edit.Enabled = true;               
            //}
            //else
            //{
            //    btn_select.Enabled = false;
            //    btn_remove.Enabled = false;
            //    //btn_edit.Enabled = false;
            //    return;
            //}

            //if (index == 0) // check first ingredients amounts (not equil zero)
            //{
            //    string text;
            //    double num;
                    
            //    text = listView1.Items[index].SubItems[1].Text;
            //    num = double.TryParse(text, out num) ? num : 100;
            //    num = (num == 0) ? 100 : num;
            //    listView1.Items[index].SubItems[1].Text = num.ToString();
            //    elements[index].Amounts = num;
            //}

            //checked
            //{
            //    try
            //    {
            //        int id; // номер выбранного элемента
            //        Element el = tbAmount.getElementByIndex(index);
            //        tbAmount.Selected = el.Id;
            //        id = tbAmount.Selected;
            //        frm.richTextBox1.Text += ">>> Selected element\n";
            //        int k;
            //        for (k = 0; k < ingredients.Count; k++)
            //        {
            //            if (ingredients[k].id == id)
            //            {
            //                cmbIngr.SelectedIndex = k;
            //                frm.richTextBox1.Text += "> selected index of combo item: " + cmbIngr.SelectedIndex + "\n";
            //                frm.richTextBox1.Text += "> selected item name: " + cmbIngr.Items[k].ToString() + "\n";
            //            }
            //        }
            //        string t = string.Format("{0:f1}", el.Amounts);
            //        //txbAmounts.Text = el.Amounts.ToString();
            //        listView1.Focus();
            //        frm.richTextBox1.Text += "> Selected ingredients (name, id): " + el.Name + " " + el.Id + "\n";
            //    }
            //    catch (ArgumentOutOfRangeException ex)
            //    {
            //        //throw ex;
            //        return;
            //    }
            //}

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private new void Refresh()
        {
            tbAmount.RefreshElements();
            elements = tbAmount.getElements();
            fillAmounts();
            FillAmountsView(); // listview
            showOldAmounts(); // for edit mode                              
            btn_submit.Enabled = false;
        }

    }
}
