using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace MajPAbGr_project
{
    public partial class FormMain : Form
    {
        int category;
        double coefficient;
        public List<Element> recipes;
        public List <Element> elements;
        FormMainController controller;
        tbReceptureController tb;
        CalcFunction calc;

        ComboBox combo;
        ComboBox recipe;
        ListView list;

        CultureInfo current;       

        public FormMain(int id)
        {
            InitializeComponent();
            controller = new FormMainController(id);            
            recipes = new List<Element>();

            tb = controller.TbMain();            
            calc = controller.Calc;

            combo = comboBox1;
            recipe = comboBox2;
            list = listView1;
        }

        private void AutocompleteRecipeName()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            foreach (Element el in recipes) source.Add(el.Name);
            txb_new_recipe.AutoCompleteCustomSource = source;
            txb_new_recipe.AutoCompleteMode = AutoCompleteMode.Suggest;
            txb_new_recipe.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            int temp_id = tb.Selected;
            Class1.setBox(controller.getCatalog(), ref combo);
            combo.SelectedIndex = Class1.ChangeIndex(controller.getCatalog(), temp_id);           
            //fillSubCatalog(); // table Recipe;
            //AutocompleteRecipeName(); // table Recipe            

            current = controller.Current();            
            this.Text += " " + controller.InfoLocal();
            
            checkBox1.Checked = false;
            //checkBox1.Enabled = false;
            btn_insert.Enabled = false;
        }

        /*
         * Lokalizacija
         */

        private void changeLocale(string locale)
        {
            controller.setNFI(locale);
            current = controller.Current();
            this.Text = "Recepture " + controller.InfoLocal();
            localizacijaToolStripMenuItem.Text = controller.CurrentName();            
        }

        private void uSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLocale("us-US");            
        }

        private void lVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLocale("lv-LV");
        }

        private void rUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLocale("ru_RU");            
        }

        /*
         * Konec lokalizaciji
         */

        public int fillSubCatalog()  // recipes of recepture, combobox
        {
            recipes = controller.Recipes;
            Class1.FillCombo(recipes, ref recipe);            
            if (recipe.Items.Count > 0)
            {
                recipe.SelectedIndex = 0;                
            }
            else
            {
                recipe.Text = "add recepture (g)";
                lbl_koef.Text = calc.Coefficient.ToString();
            }
            return recipes.Count;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = combo.SelectedIndex;
            controller.setSubcatalog(index);
            int selected = tb.Selected;                      
            int count = fillSubCatalog(); // fill the combobox2
            AutocompleteRecipeName(); // table Recipe      

            //used more one time in `InsertAmounts`, mode:edit           
            elements = controller.Amounts; // amounts            
            calc.setAmounts(elements); // сохраняет и cуммирует величины

            //InputRecepture(elements);            
            List<string> amounts = calc.FormatAmounts
                (calc.getAmounts(), calc.getTotal());
            Class1.FillListView(elements, amounts, ref listView1);

            if (count == 0)
            {
                calc.Coefficient = 1;
                lbl_koef.Text = "1";
            }

            //Info about recepture
            category = int.Parse(tb.getById("id_category", selected));            
            string info;
            info = $"  {tb.getName(index)}: id {tb.getSelected()}, category ({category})\n";
            if (recipes.Count > 0)
            {
                info += $"recipes\n";
                for (int k = 0; k < recipes.Count; k++)
                {
                    info += $"{recipes[k].Name} ({recipes[k].Amounts}) \n";
                }
            }
            lbl_info.Text = info;
        }

        private void InputRecepture(List<Element> ingr)
        {
            string t;
            ListViewItem items;
            list.Items.Clear();
            for (int k = 0; k < ingr.Count; k++)
            {
                items = new ListViewItem(ingr[k].Name);
                items.Tag = ingr[k].Id;
                t = string.Format("{0:f2}", ingr[k].Amounts);
                items.SubItems.Add(t);
                listView1.Items.Add(items);
                t = "";
            }
            //Сумма: счет и вывод
            double summa = calc.getTotal();
            t = string.Format("{0:f2}", summa);

            items = new ListViewItem("Total");
            items.Tag = -1;
            items.SubItems.Add(t);
            listView1.Items.Add(items);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 0) 
            {
                lbl_koef.Text = "1";
                return;
            }
            else
            {
                controller.Calc.Coefficient = recipes[comboBox2.SelectedIndex].Amounts;
                lbl_koef.Text = controller.Calc.Coefficient.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e) // recalc recepture
        {
            if (recipes.Count < 1) return;
            double coeff = controller.Calc.Coefficient;
            if (coeff == 1) return;

            int index;                     
            List<string> t = controller.Calc.FormatAmounts(); //посчитали и придали вид
            for (index = 0; index < t.Count; index++)
            {
                list.Items[index].SubItems[1].Text = t[index];
            }
            columnHeader2.Text = "Amounts (g)";
            //записали в списковид

            //for (index = 0; index < controller.Calc.getAmounts().Length; index++)
            //{
            //    elements[index].Amounts = controller.Calc.getAmounts()[index];
            //}

        }

        /*****************************************************************************
         * новый рецепт, навигация        
        *******************************************************************************/
         private void button2_Click(object sender, EventArgs e) // calc new recipe
         {
             if (string.IsNullOrEmpty(txb_coeff.Text)) return;            

            int index = 0;
            string indikator = cmb_option.Text;
            double [] amounts = controller.button1_onClick(txb_coeff.Text, indikator);

             for (index = 0; index < amounts.Length; index++)
             {
                 list.Items[index].SubItems[1].Text = amounts[index].ToString();
             }
             list.Items[index].SubItems[1].Text = controller.Calc.Summa(amounts).ToString();
            btn_insert.Enabled = true;
         }

        private void btn_insert_Click(object sender, EventArgs e)
         {
            string str_coeff;
            string count;

            calc = controller.Calc;

            tbRecipeController recipe = new tbRecipeController
                ("Recipe", comboBox2.SelectedIndex, controller.TbMain().Selected);
            count = recipe.Count
               ($"Select count (name) from Recipe where name = '{txb_new_recipe.Text}';");

            if (!checkBox1.Checked)
            {
                if (string.IsNullOrEmpty(txb_new_recipe.Text))
                 return;
                if (count == "0")
                {
                     str_coeff = calc.ColonToPoint(coefficient.ToString());
                     recipe.insertNewRecipe(txb_new_recipe.Text, str_coeff);
                     fillSubCatalog();
                     btn_insert.Enabled = false;
                }
                else
                {
                    MessageBox.Show($"Recipe {txb_new_recipe.Text} already exists");
                }
            }
            else //Recipe.cs
            {
                if (txb_coeff.Text == "" || txb_coeff.Text == " ")
                {
                    str_coeff = calc.ColonToPoint(coefficient.ToString());
                }
                else
                {
                    str_coeff = calc.ColonToPoint(txb_coeff.Text);                    
                }                   
                if (txb_new_recipe.Text == "" || txb_new_recipe.Text == " ")
                {
                    txb_new_recipe.Text = comboBox2.SelectedItem.ToString();
                }
                DialogResult result = MessageBox.Show(
                     $"New name = {txb_new_recipe.Text}, new coefficient = {str_coeff}", "",
                     MessageBoxButtons.OKCancel);
                if(result == DialogResult.OK)
                {
                    recipe.UpdateReceptureOrCards
                        ("name", txb_new_recipe.Text, recipe.Selected);
                    recipe.UpdateReceptureOrCards
                        ("Coefficient", str_coeff, recipe.Selected);
                }
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reload();
        }
        private void Reload()
        {
            int selected = comboBox1.SelectedIndex;

            tb.setCatalog();
            Class1.setBox(tb.getCatalog(), ref combo);

            columnHeader2.Text = "Amounts (%)";
            comboBox1.SelectedIndex = selected;
            comboBox1.Text = comboBox1.SelectedItem.ToString();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbReceptureController cntrl = new tbReceptureController("Recepture");
            NewRecepture frm = new NewRecepture(cntrl);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e) // edit recepture
        {
            if (tb.getSelected() == 0) return;
            if (listView1.Items.Count < 1) return;
            int selected = tb.getSelected();

            tbReceptureController cntrl = new tbReceptureController("Recepture", selected, category);
            NewRecepture frm = new NewRecepture(cntrl);
            frm.ShowDialog();
           
            /*
            Код вставки обработчика (копия из дизайнера):
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click_1);
            */           
        }

        private void amountsEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count < 1) return;
            AmountsTable();            
        }

        private void AmountsTable()
        {
            if (tb.getSelected() == 0) return;
            tbAmountsController cntrl = new tbAmountsController("Amounts", ref tb);
            InsertAmounts frm = new InsertAmounts(ref cntrl);
            frm.ShowDialog();
            calc.Coefficient = frm.Calc.Coefficient;
            Reload();
            // проследить, чтобы передался новый (верный) коэфициент!
            // при смене эелемента комбинированого поля вызывается метод заполнения списочного представлеяни,
            // а до него -- форматирования числа, которое использует внутри себя метод ReCalc()
            // с "неверным" коэфициентом,
            // причина -- в неверно выбранном объекте для вызова методов класса CalcFunction.

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SimpleTable(2);
        }

        private void ingredientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimpleTable(1);
        }

        private void SimpleTable(int opt)
        {
            IngredientsController cntrl = new IngredientsController(opt);
            Ingredients frm = new Ingredients(cntrl);
            frm.Show();
        }

        //Print to file  
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file;
            if (!string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()))
                file = comboBox1.SelectedItem.ToString();
            else file = "recipe";
            List<string> strings = PrepareOutput();            
            Form2 frm = new Form2(strings, file); // method PrintToFile is a Form2 method
            frm.Show();
        }        
        private List<string> PrepareOutput()
        {
            string name, output, mesuare, info;
            List<string> strings = new List<string>();
            name = comboBox1.Text;
            if (columnHeader2.Text == "Amounts (%)")
            {
                mesuare = "%";
                name += $" ({mesuare})";
                info = $"Info: {lbl_info.Text}";
            }
            else
            {
                mesuare = "g";
                name += $" ({mesuare}) {comboBox2.SelectedItem.ToString()}";
                info = $"Info:  {tb.getName(comboBox1.SelectedIndex)}: id {tb.getSelected()}, category ({category})\n"; ;
            }

            output = "Recipe name: ";
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                output += $"{name} \n";
                strings.Add(output);
            }

            if (listView1.Items.Count > 0)
            {
                for (int k = 0; k < listView1.Items.Count; k++)
                {
                    output = $"{listView1.Items[k].Text} {listView1.Items[k].SubItems[1].Text}";
                    strings.Add(output);
                }
            }
            else
            {
                output = "Ingredient amounts are unknown";
                strings.Add(output);
            }

            strings.Add(info);
            return strings;
        }
        // konec 'print to file'

        private void recipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int lbl_info_y = lbl_info.Location.Y;
            this.lbl_info.Location = new Point(318, this.comboBox2.Location.Y);
            txb_new_recipe.Text = "";
            txb_coeff.Text = "";
            checkBox1.Enabled = true;
            checkBox1.Checked = true;
            
            Recipe frm = new Recipe(this, controller.TbMain(), calc, comboBox1.SelectedIndex);        
            frm.ShowDialog();

            this.lbl_info.Location = new Point(318, lbl_info_y);
            checkBox1.Checked = false;
            checkBox1.Enabled = false;
            button2.Enabled = true;
        }

        private void openDbEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditDB frm = new EditDB();
            frm.Show();
        }

        private void technologyToolStripMenuItem_Click(object sender, EventArgs e) // open chains` of technology cards editor
        {
            Chains frm;
            ChainController controller;
            int selected, id_technology, count;// id of recepture and of technology;
            // проверить выбранный в списке                   
            selected = tb.getSelected();

            controller = new ChainController();
            controller.Recepture = selected;

            //id_technology
            count = tb.SelectedCount("Recepture", "id_technology", selected); // dos recepture contain any technology
            if (count == 1)
            {
                id_technology = int.Parse(tb.getById("id_technology", selected));                
                controller.Technology = id_technology;                 
            }  
            
            frm = new Chains(ref controller);
            frm.Show();

            { 
            //Technology frm;
            //int selected, id_technology, count;// id of recepture and of technology;

            //// проверить выбранный в списке                   
            //selected = tb.getSelected();
            //count = tb.SelectedCount("Recepture", "id_technology", selected);// dos recepture contain any technology

            //if (count == 1)
            //{
            //    id_technology = int.Parse(tb.getById("id_technology", selected));
            //    frm = new Technology(selected, id_technology);
            //    frm.Show();
            //}
            //else
            //{
            //    frm = new Technology(selected);
            //    frm.Show();
            //}
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           //
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked) // edit mode on
            {
                btn_insert.Enabled = true;
                button2.Enabled = false;
                btn_insert.Text = "edit";
                cmb_option.SelectedItem = cmb_option.Items[2];
                //cmb_option.DropDownStyle = ComboBoxStyle.Simple;
            }
            else // edit mode off
            {
                btn_insert.Enabled = false;
                button2.Enabled = true;
                btn_insert.Text = "btn_insert";
                cmb_option.SelectedItem = cmb_option.Items[0];
                //cmb_option.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            txb_coeff.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            txb_new_recipe.Text = "";
        }




        //private void executeViewToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    string file;
        //    if (!string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()))
        //        file = comboBox1.SelectedItem.ToString();
        //    else file = "recipe";
        //    List<string> strings = PrintViewRezult();

        //    //PrintToFile(strings, file);
        //    Form2 frm = new Form2(strings, file);
        //    frm.Show();
        //}

        //private List<string> PrintViewRezult()
        //{
        //    string query = "SELECT (SELECT name FROM Recepture WHERE id = 8) AS recepture, " +
        //    "name, Coefficient AS coefficient, amount*Coefficient AS amount " +
        //   "FROM Recipe AS r " +
        //   "JOIN " +
        //   "Amounts AS a ON r.id_recepture = a.id_recepture WHERE r.id_recepture = 8;";   
        //    return tb.dbReader(query);
        //}
    }
}
