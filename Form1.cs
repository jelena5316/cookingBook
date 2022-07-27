using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MajPAbGr_project
{
    public partial class Form1 : Form
    {
        int category;
        double coefficient;
        List<Element> recipes;
        tbClass1 tb;
        CalcFunction calc;

        ComboBox combo;
        ComboBox recipe;
        ListView list;

        public Form1()
        {
            InitializeComponent();
            tb = new tbClass1("Recepture");
            calc = new CalcFunction();
            recipes = new List<Element>();

            combo = comboBox1;
            recipe = comboBox2;
            list = listView1;
                       
            tb.setCatalog();
            fillCatalog(tb.getCatalog());
            tb.setSubCatalog();
            fillSubCatalog();
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

        private void fillSubCatalog()  // recipes of recepture, combobox
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
            //tbClass1 tbCat = new tbClass1(2);
            //List<Item> cat = tbCat.getCatalog();

            //Item item = new Item();
            //item = cat.Find(it => it.id == category);
            //category_name = item.name;
            //Name of category: wrong!

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
            // Сумма: счет и вывод
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

        private void button1_Click(object sender, EventArgs e) // calc
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
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txb_new_recipe.Text))
                return;  
            string str_coeff = calc.ColonToPoint(coefficient.ToString());
            tb.insertNewRecipe(txb_new_recipe.Text, str_coeff);
            fillSubCatalog();
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimpleTable(2);
        }

        private void ingredientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimpleTable(1);
        }

        private void SimpleTable(int opt)
        {
            Ingredients frm = new Ingredients(opt);
            frm.Show();
        }

        private void recipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Recipe frm = new Recipe();
            //frm.Show();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewRecepture frm = new NewRecepture();
            frm.ShowDialog();
        }

        private void insertIgredientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tb.getSelected() == 0) return;
            if (listView1.Items.Count < 1) return;
            InsertAmounts frm = new InsertAmounts(tb.getSelected());
            frm.ShowDialog();
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

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tb.getSelected() == 0) return;
            if (listView1.Items.Count < 1) return;
            int selected = tb.getSelected();
            NewRecepture frm = new NewRecepture(selected, category);
            frm.ShowDialog();
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

        //private string ColonToPoint(string text)
        //{
        //    string number;
        //    if (text.Contains(","))
        //    {
        //        int k;
        //        number = "";
        //        for (k = 0; k < text.Length; k++)
        //        {
        //            if (text[k] != ',')
        //                number += text[k];
        //            else
        //                number += '.';
        //        }
        //    }
        //    else
        //    {
        //        number = text;
        //    }
        //    return number;
        //}

    }
}
