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
        readonly int id_recepture;
        AmountsController tbIngred;
        CalcFunction calc;

        public InsertAmounts(int id)
        {
            InitializeComponent();
            id_recepture = id;
            tbIngred = new AmountsController("Ingredients");            
            tbIngred.setCatalog();
            calc = new CalcFunction();            
            FillCatalog();
            btn_recipe.Enabled = false; //insert recipe
            btn_submit.Enabled = false; // submit ingredients
            txbAmounts.Text = "amounts";
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
            txbAmounts.Focus();
            txbAmounts.Text = "";
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

            txbAmounts.Text = "amounts";
            cmbIngr.Focus();
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
            ind = tbIngred.Submit(ref listView1, id_recepture);

            if (ind == 0) MessageBox.Show("All amounts are inserted");            
            else MessageBox.Show($"{ind} from {listView1.Items.Count} are inserted");
           
            btn_recipe.Enabled = true;
            btn_submit.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e) // btn_recipe: insert recipe
        {
            int ind;
            double coefficient = calc.Coefficient;
            RecipeController tb = new RecipeController("Recipe");
            tb.Selected = id_recepture;

            if (string.IsNullOrEmpty(txbRecipe.Text)) return;
            if (coefficient != 0)
            {
                string coeff = calc.ColonToPoint(coefficient.ToString());
                ind = tb.insertNewRecipe(txbRecipe.Text, coeff);
                btn_recipe.Enabled = false;
            }
        }
    }
}
