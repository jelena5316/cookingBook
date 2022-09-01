using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MajPAbGr_project
{
    public partial class FormMain : Form
    {
        int category;
        double coefficient;
        public List<Element> recipes;
        FormMainController tb;
        CalcFunction calc;

        ComboBox combo;
        ComboBox recipe;
        ListView list;

        public FormMain()
        {
            InitializeComponent();            
            tb = new FormMainController("Recepture");
            calc = new CalcFunction();
            recipes = new List<Element>();

            combo = comboBox1;
            recipe = comboBox2;
            list = listView1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tb.setCatalog();
            fillCatalog(tb.getCatalog());
            tb.setSubCatalog(); // table Recipe
            fillSubCatalog(); // table Recipe
            checkBox1.Checked = false;
            checkBox1.Enabled = false;
            btn_insert.Enabled = false;
        }

        private List<Item> fillCatalog(List<Item> items)
        {
            //List<Item> items = tb.getCatalog(); // читает два поля, наименование и номер
            //пишет в комбинированное поле
            if (items.Count != 0)
            {
                if (combo.Items.Count > 0)
                    combo.Items.Clear();
                for (int index = 0; index < items.Count; index++)
                {
                    combo.Items.Add(items[index].name);
                }
                combo.Text = combo.Items[0].ToString();
            }
            else combo.Text = "empty";
            combo.Focus();
            //int i = combo.SelectedIndex;
            //this.Text += combo.Items[i].ToString() + " " + tb.getSelected();
            //смена выбранного индекса при записи происходит
            return items;
        }

        public void fillSubCatalog()  // recipes of recepture, combobox
        {
            //бывшая функция setRecipes()
            if (recipes.Count > 0) recipes.Clear();
            recipes = tb.readElement(2); //читает полностью, все три поля

            if (recipe.Items.Count > 0) recipe.Items.Clear();
            if (recipes.Count > 0)
            {
                for (int k = 0; k < recipes.Count; k++)
                {
                    recipe.Items.Add(recipes[k].Name);
                }
            }

            if (recipe.Items.Count > 0)
            {
                recipe.SelectedIndex = 0;
            }
            else
            {
                recipe.Text = "add recepture (g)";
                lbl_koef.Text = "koeff";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = combo.SelectedIndex;
            int selected = tb.setSelected(index);
            string info;
            tb.setSubCatalog();
            fillSubCatalog();

            category = tb.getId("id_category", selected);

            // Info about recepture
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

            List<Element> rec = tb.readElement(1); // amounts
            calc.setAmounts(rec); // сохраняет и cуммирует величины
            InputRecepture(rec);

            if (rec.Count < 1) insertIgredientsToolStripMenuItem.Enabled = true;
            else insertIgredientsToolStripMenuItem.Enabled = false;
        }             

        private void InputRecepture(List<Element> ingr)
        {
            ListViewItem items;
            list.Items.Clear();
            for (int k = 0; k < ingr.Count; k++)
            {
                items = new ListViewItem(ingr[k].Name);
                items.Tag = ingr[k].Id;
                items.SubItems.Add(ingr[k].Amounts.ToString());
                listView1.Items.Add(items);
            }
            //Сумма: счет и вывод
            double summa = calc.getTotal();

            items = new ListViewItem("Total");
            items.Tag = -1;
            items.SubItems.Add(summa.ToString());
            listView1.Items.Add(items);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 0) return;
            calc.Coefficient = recipes[comboBox2.SelectedIndex].Amounts;
            lbl_koef.Text = calc.Coefficient.ToString();
        }

        private void button1_Click(object sender, EventArgs e) // recalc recepture
        {
            if (recipes.Count < 1) return;
            double coeff = calc.Coefficient;
            if (coeff == 1) return;
            double[] arr = calc.ReCalc();
            int index;
            for (index = 0; index < arr.Length; index++)
            {
                list.Items[index].SubItems[1].Text = arr[index].ToString();
            }
            list.Items[index].SubItems[1].Text = calc.Summa(arr).ToString();
            columnHeader2.Text = "Amounts (g)";
        }

        //новый рецепт, навигация
         private void button2_Click(object sender, EventArgs e) // calc new recipe
         {
             if (string.IsNullOrEmpty(txb_coeff.Text)) return;            

             int index = 0;
             double summa = 0, amount;       
             double [] amounts = calc.getAmounts();
             string indikator = cmb_option.Text;
             string temp, t;

             if (double.TryParse(txb_coeff.Text, out amount))
             {
                 amount = double.Parse(txb_coeff.Text);
             }
             else
             {
                 t = txb_coeff.Text;
                 temp = "";
                 if (t.Contains('.'))
                 {
                     int k;
                     for (k = 0; k < t.Length; k++)
                     {
                         if (t[k] != '.')
                             temp += t[k];
                         else
                             temp += ',';
                     }
                     t = temp;
                     txb_coeff.Text = temp;
                 }

                 if (double.TryParse(t, out amount))
                 {
                     amount = double.Parse(t);
                 }
                 else
                 {
                     amount = 1;
                     txb_coeff.Text = "not number";
                     return;
                 }
             }

             // вынести в CalcFunction.cs (?)
             switch (indikator)
             {
                 case "total":
                     summa = calc.Summa();
                     coefficient = amount / summa;
                     //calc.Coefficient =  calc.calculateCoefficient(amount, calc.Summa());
                     break;

                 case "main":
                     coefficient = amount / amounts[0];
                     //calc.Coefficient = calc.calculateCoefficient(amount, amounts[0]);

                     break;

                 case "coefficient":                   
                     coefficient = amount;
                     //calc.Coefficient = amount;
                     break;
                 default: coefficient = amount / amounts[0]; ; break;                  
             }
             calc.Coefficient = coefficient;

             amounts = calc.ReCalc();

             for (index = 0; index < amounts.Length; index++)
             {
                 list.Items[index].SubItems[1].Text = amounts[index].ToString();
             }
             list.Items[index].SubItems[1].Text = calc.Summa(amounts).ToString();
            btn_insert.Enabled = true;
         }

        private void btn_insert_Click(object sender, EventArgs e)
         {
            string str_coeff;
            string count;
            RecipeController recipe = new RecipeController
                ("Recipe", comboBox2.SelectedIndex, tb.Selected);
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
            else
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
            int selected = comboBox1.SelectedIndex;

            tb.setCatalog();
            fillCatalog(tb.getCatalog());

            columnHeader2.Text = "Amounts (%)";
            comboBox1.SelectedIndex = selected;
            comboBox1.Text = comboBox1.SelectedItem.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e) // edit recepture
        {
           
            if (tb.getSelected() == 0) return;
            if (listView1.Items.Count < 1) return;
            int selected = tb.getSelected();

            //NewRecepture frm = new NewRecepture(selected, category);
            ReceptureController cntrl = new ReceptureController("Recepture", selected, category);
            NewRecepture frm = new NewRecepture(cntrl);
            frm.ShowDialog();
           
            /*
            Код вставки обработчика (копия из дизайнера):
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click_1);
            */           
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            ReceptureController cntrl = new ReceptureController("Recepture");
            NewRecepture frm = new NewRecepture(cntrl);
            frm.ShowDialog();
            */
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
            /*
            IngredientsController cntrl = new IngredientsController(opt);
            Ingredients frm = new Ingredients(cntrl);
            frm.Show();
            */
        }

        //Print to file        

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file;
            if (!string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()))
                file = comboBox1.SelectedItem.ToString();
            else file = "recipe";
            List<string> strings = PrepareOutput();

            //PrintToFile(strings, file);
            Form2 frm = new Form2(strings, file);
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

        private void PrintToFile(List<string> list, string file_name)
        {
            const string PATH = "C:\\Users\\user\\Documents\\2_diplom\\Receptures\\";
            string path, file;
            List<string> strings = list;
            file = $"{file_name}.txt";
            path = PATH + file;
            using (StreamWriter stream = new StreamWriter(path, true))
            {
                if (!File.Exists(path))
                {
                    File.CreateText(path);
                }
                for (int k = 0; k < strings.Count; k++)
                {
                    stream.WriteLine(strings[k]);
                }
                stream.Close();
            }
            string message = $"File {path} is created";
            MessageBox.Show(message);
        }

        private void recipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int lbl_info_y = lbl_info.Location.Y;
            this.lbl_info.Location = new Point(318, this.comboBox2.Location.Y);
            txb_new_recipe.Text = "";
            txb_coeff.Text = "";
            checkBox1.Enabled = true;
            checkBox1.Checked = true;            
                    
            Recipe frm = new Recipe(this, tb, calc, comboBox1.SelectedIndex);        
            frm.ShowDialog();

            this.lbl_info.Location = new Point(318, lbl_info_y);
            checkBox1.Checked = false;
            checkBox1.Enabled = false;
            button2.Enabled = true;
        }

        private void insertIgredientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            if (tb.getSelected() == 0) return;
            if (listView1.Items.Count < 1) return;
            InsertAmounts frm = new InsertAmounts(tb.getSelected());
            frm.ShowDialog();
            */
        }

        private void openDbEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditDB frm = new EditDB();
            frm.Show();
        }

        private void technologyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Technology frm;
            int selected, id_technology, count;// id of recepture and of technology;

            // проверить выбранный в списке                   
            selected = tb.getSelected();
            count = tb.SelectedCount("Recepture", "id_technology", selected);// dos recepture contain any technology

            if (count == 1)
            {
                id_technology = tb.getId("id_technology", selected);
                frm = new Technology(selected, id_technology);
                frm.Show();
            }
            else
            {
                frm = new Technology(selected);
                frm.Show();
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
    }
}
