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
    public partial class InsertAmounts : Form
    {
        int id_recepture;
        tbClass1 tbIngred;
        CalcFunction calc;

        public InsertAmounts(int id)
        {
            InitializeComponent();
            id_recepture = id;
            tbIngred = new tbClass1("Ingredients");            
            tbIngred.setCatalog();
            calc = new CalcFunction();            
            FillCatalog();
            btn_recipe.Enabled = false; //insert recipe
            btn_submit.Enabled = false; // submit ingredients           
        }

        private void FillCatalog()
        {
            List<Item> ingr = tbIngred.getCatalog();
            if (ingr.Count != 0)
            {
                if (cmbIngr.Items.Count > 0)
                {
                    cmbIngr.Items.Clear();
                }
                for (int index = 0; index < ingr.Count; index++)
                {
                    cmbIngr.Items.Add(ingr[index].name);
                }
            }
            cmbIngr.Text = cmbIngr.Items[0].ToString();
        }

        private void cmbIngr_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbIngred.setSelected(cmbIngr.SelectedIndex);
            cmbIngr.Text = cmbIngr.SelectedItem.ToString();
        }

        private void btn_edit_Click(object sender, EventArgs e) // add an ingredient
        {
            if (cmbIngr.SelectedIndex == -1) return;
            if (string.IsNullOrEmpty(cmbIngr.Text)) return;
            if (string.IsNullOrEmpty(txbAmounts.Text)) return;
            double num;
            if (double.TryParse(txbAmounts.Text, out num))
                num = double.Parse(txbAmounts.Text);
            else return;

            //ubiraem vydelenie
            ListViewItem items;
            if (listView1.Items.Count > 0) // proverka spiska
            {
                items = listView1.SelectedItems[0];
                items.Selected = false;
            }

            ListViewItem item = new ListViewItem(cmbIngr.Text);
            item.SubItems.Add(txbAmounts.Text);
            item.SubItems.Add(""); // заготовка под проценты
            item.Tag = tbIngred.getSelected();            
            listView1.Items.Add(item);
            item.Selected = true;            
        }

        private void button2_Click(object sender, EventArgs e) // edit listview item
        {
            if (listView1.SelectedItems.Count < 0) return;
            if (btn_select.Text == "select")
            {
                SelectToEdit();
                btn_select.Text = "edit";
                btn_edit.Enabled = false;
                btn_remove.Enabled = false;
            }
            else
            {
                Edit();
                btn_select.Text = "select";
                btn_edit.Enabled = true;
                btn_remove.Enabled = true;
            }
        }

        private void SelectToEdit()
        {
            int id;
            if (listView1.SelectedItems.Count < 1) return;
            ListViewItem item = listView1.SelectedItems[0];

            id = tbIngred.getSelected();
            for (int index = 0; index < cmbIngr.Items.Count; index++)
            {
                if (item.SubItems[0].Text == cmbIngr.Items[index].ToString())
                {
                    cmbIngr.Select(index, 1); // parametrs: start, length
                    cmbIngr.Text = cmbIngr.SelectedItem.ToString();
                    break;
                }
            }
            txbAmounts.Text = item.SubItems[1].Text;
            cmbIngr.Text = item.SubItems[0].Text;
        }

        private void Edit()
        {
            ListViewItem item = listView1.SelectedItems[0];

            item.SubItems[1].Text = txbAmounts.Text;
            item.SubItems[0].Text = cmbIngr.SelectedItem.ToString();
            item.Tag = tbIngred.getSelected();

            if (listView1.Items[0].SubItems[2].Text != "" && item.Index == 0)
            {
                btn_submit.Enabled = false; // submit
                btn_calc.Focus(); // calc
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            ListViewItem items;
            if (listView1.Items.Count > 0) // proverka spiska
            {
                Remove(ref listView1);
                if (listView1.Items.Count > 0)
                {
                    items = listView1.Items[listView1.Items.Count - 1];
                    items.Selected = true;
                }
                else btn_submit.Enabled = false;
            }  
        }

        private void Remove(ref ListView lv) // peredacha po ssylke
        {
            ListViewItem items;
            int i;
            if (lv.Items.Count > 0) // proverka spiska
            {
                items = lv.SelectedItems[0];
                i = items.Index;
                lv.Items.RemoveAt(i);
            }

            //ListViewItem items;
            //int i;
            //if (listView1.Items.Count > 0) // proverka spiska
            //{
            //    items = listView1.SelectedItems[0];
            //    i = items.Index;
            //    listView1.Items.RemoveAt(i);

            //    if (listView1.Items.Count > 0)
            //    {
            //        items = listView1.Items[listView1.Items.Count - 1];
            //        items.Selected = true;
            //    }
            //    else btn_submit.Enabled = false;
            //}
        }

        private void button3_Click(object sender, EventArgs e) // calculation
        {
            if (listView1.Items.Count > 0)
            {
                double a = double.Parse(listView1.Items[0].SubItems[1].Text);
                double[] arr = new double[listView1.Items.Count];
                for (int index = 0; index < arr.Length; index++)
                {
                    arr[index] = double.Parse(listView1.Items[index].SubItems[1].Text);
                }
                arr = calc.ReCalc(100 / a, arr);

                for (int index = 0; index < arr.Length; index++)
                {
                   listView1.Items[index].SubItems[2].Text = arr[index].ToString();
                }
                calc.Coefficient = a / 100;
                btn_submit.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e) // submit
        {
            int ind;
            if (string.IsNullOrEmpty(listView1.Items[0].SubItems[1].Text)) return;
            ind = Submit(ref listView1);
            btn_recipe.Enabled = true;
            btn_submit.Enabled = false;
        }

        private int Submit (ref ListView lv)
        {
            dbController db = new dbController();
            int index = 0, ind = 0, sum;
            string amount, id_ingr;
            //string query;

            id_ingr = lv.Items[0].Tag.ToString();

            ind = tbIngred.UpdateReceptureOrCards("id_main", id_ingr, id_recepture); // Recepture

            if (ind == 0) return 1;

            for (index = lv.Items.Count - 1, sum = 0; index > -1; index--)
            {
                id_ingr = lv.Items[index].Tag.ToString();
                amount = lv.Items[index].SubItems[2].Text;
                amount = ColonToPoint(amount);

                ind = tbIngred.insertAmounts(id_recepture, id_ingr, amount);
                sum += index; // proverka zapisi
            }
            if (sum == lv.Items.Count) return 0;// vse zapisalosj
            else return sum; 
        }

        private void button1_Click(object sender, EventArgs e) // btn_recipe: insert recipe
        {
            int ind;
            double coefficient = calc.Coefficient;
            tbClass1 tb = new tbClass1("Recipe");
            tb.Selected = id_recepture;

            if (string.IsNullOrEmpty(txbRecipe.Text)) return;
            if (coefficient != 0)
            {
                string coeff = ColonToPoint(coefficient.ToString());
                ind = tb.insertNewRecipe(txbRecipe.Text, coeff);
                btn_recipe.Enabled = false;
            }
        }

        private string ColonToPoint(string text)
        {
            string number;
            if (text.Contains(","))
            {
                int k;
                number = "";
                for (k = 0; k < text.Length; k++)
                {
                    if (text[k] != ',')
                        number += text[k];
                    else
                        number += '.';
                }
            }
            else
            {
                number = text;
            }
            return number;
        }
    }
}
