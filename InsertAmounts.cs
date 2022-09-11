using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Globalization;

namespace MajPAbGr_project
{
    public partial class InsertAmounts : Form
    {
        readonly int id_recepture;
        IngredientsController tbIngred;
        AmountsController tbAmounts;        
        CalcFunction calc;
        Mode mode; //create new or edit old
        List<Element> elements; // id and name, amounts
        double[] amounts;
       // NumberFormatInfo nfi = new CultureInfo("Ru-ru", false).NumberFormat;

        public InsertAmounts(int id) // New Recepture
        {
            InitializeComponent();
            id_recepture = id;           
            tbIngred = new IngredientsController(1);
            tbIngred.setCatalog();
            tbAmounts = new AmountsController("Amounts");
            //calc = new CalcFunction();
            List<Item> receptures = tbIngred.getCatalog();
            Class1.FillCombo(receptures, ref cmbIngr); 
        }

        public InsertAmounts(Mode mode, ref AmountsController tbAmounts)
        {
            InitializeComponent();
            
            this.tbAmounts = tbAmounts;
            this.mode = mode;
            id_recepture = tbAmounts.Id_recepture;
            elements = tbAmounts.getElements();
            
            tbIngred = new IngredientsController(1);
            tbIngred.setCatalog();
            List<Item> receptures = tbIngred.getCatalog();
            Class1.FillCombo(receptures, ref cmbIngr);

            fillAmounts();
            FillAmountsView(); // listview
            showOldAmounts(); // for edit mode
        }

        private void InsertAmounts_Load(object sender, EventArgs e)
        {
            btn_recipe.Enabled = false; //insert recipe           
            btn_submit.Enabled = false; // submit ingredients
            txbAmounts.Text = "0" + "," + "0"; // "," number decimal separator
            //txbAmounts.Text = "0" + nfi.NumberDecimalSeparator + "0";

            if (mode == Mode.Edit)
            {
                listView1.Columns[1].Text = "Amounts(%) new";
                listView1.Columns[2].Text += " old";

                txbRecipe.Enabled = false;
                //btn_recipe.Visible = false;
                label1.Enabled = false;
                //label1.Visible = false;
                radioButton1.Checked = true;
            }
        }

        private void fillAmounts() //for create mode
        {
            amounts = new double[elements.Count + 1]; 
            int k;
            for (k = 0; k < amounts.Length - 1; k++)
            {
                amounts[k] = elements[k].Amounts;
            }
            calc = new CalcFunction();
            calc.setAmounts(elements); // сохраняет и cуммирует величины
            amounts[k] = calc.getTotal();
        }

        private void FillAmountsView()
        {
            //from InputRecepture(); edited
            ListView list = listView1;
            list.Items.Clear();

            ListViewItem items;
            for (int k = 0; k < elements.Count; k++)
            {
                items = new ListViewItem(elements[k].Name);
                items.Tag = elements[k].Id;
                items.SubItems.Add(elements[k].Amounts.ToString());
                items.SubItems.Add(""); // заготовка под старые величины     
                listView1.Items.Add(items);
            }
            //заготовка под сумму
            items = new ListViewItem("Total");
            items.Tag = -1;
            items.SubItems.Add("");
            items.SubItems.Add("");
            listView1.Items.Add(items);
        }

        private void showOldAmounts()
        {
            ListView list = listView1;
            for (int k = 0; k < elements.Count + 1; k++)
            {
                list.Items[k].SubItems[2].Text = amounts[k].ToString();
            }
        }
   
        private void cmbIngr_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbIngred.setSelected(cmbIngr.SelectedIndex);
            cmbIngr.Text = cmbIngr.SelectedItem.ToString();
            txbAmounts.Focus();
            if(mode == Mode.Create)
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
            if (listView1.SelectedItems.Count > 0) //proverka spiska            
            {
                items = listView1.SelectedItems[0];
                items.Selected = false;
            }

            ListViewItem item = new ListViewItem(cmbIngr.Text);
            item.SubItems.Add(txbAmounts.Text);
            item.SubItems.Add(""); // заготовка под проценты
            item.Tag = tbIngred.getSelected(); // id of ingredient       
            listView1.Items.Add(item);
            item.Selected = true;

            //добавить в список элементов
            Element el = new Element();
            el.Id = (int)item.Tag;
            el.Name = cmbIngr.Text;
            el.Amounts = num;
            elements.Insert(item.Index, el);

            txbAmounts.Text = "0"+","+"0"; //Enviroment decimal separator
            cmbIngr.Focus();
        }

        private void button2_Click(object sender, EventArgs e) // edit listview item
        {
            if (listView1.SelectedItems.Count < 0) return;
            if (listView1.SelectedIndices[0] >= elements.Count) return;
            if (btn_select.Text == "select")
            {
                if (SelectToEdit() == 0)
                {
                    btn_select.Text = "edit";
                    btn_edit.Enabled = false;
                    btn_remove.Enabled = false;
                }
                else MessageBox.Show("Please, select any ingredient to edit!");
            }
            else
            {
                if (Edit() == 0)
                {
                    btn_select.Text = "select";
                    btn_edit.Enabled = true;
                    if (radioButton1.Checked == false)
                        btn_remove.Enabled = true;
                }
                else MessageBox.Show("Please, select any ingredient to edit!");
            }            
        }

        private int SelectToEdit()
        {
            if (listView1.SelectedItems.Count < 1) return -1;
            ListViewItem item = listView1.SelectedItems[0];

            for (int index = 0; index < cmbIngr.Items.Count; index++)
            {
                if (item.SubItems[0].Text == cmbIngr.Items[index].ToString())
                {
                    cmbIngr.SelectedIndex = index;                   
                    cmbIngr.Text = cmbIngr.SelectedItem.ToString();
                    break;
                }
            }
            txbAmounts.Text = item.SubItems[1].Text;
            cmbIngr.Text = item.SubItems[0].Text;
            return 0;
        }

        private int Edit()
        {
            if (listView1.SelectedItems.Count < 1) return -1;            
            ListViewItem item = listView1.SelectedItems[0];

            double num;
            if (double.TryParse(txbAmounts.Text, out num))
                num = double.Parse(txbAmounts.Text);
            else return -2;

            item.SubItems[1].Text = txbAmounts.Text;
            item.SubItems[0].Text = cmbIngr.SelectedItem.ToString();
            item.Tag = tbIngred.getSelected();

            if (listView1.Items[0].SubItems[2].Text != "" && item.Index == 0)
            {
                btn_submit.Enabled = false; // submit
                btn_calc.Focus(); // calc
            }

            elements[listView1.SelectedIndices[0]].Amounts = num;
            elements[listView1.SelectedIndices[0]].Name = (string)cmbIngr.SelectedItem;
            elements[listView1.SelectedIndices[0]].Id = (int)item.Tag;

            txbAmounts.Text = "0"+","+"0"; //Enviroment decimal separator
            return 0;
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            ListViewItem items;
            int index = listView1.SelectedItems[0].Index;
            if (listView1.Items.Count > 0) // proverka spiska
            {            
                listView1.Items.RemoveAt(index);
                if (listView1.Items.Count > 0)
                {
                    items = listView1.Items[listView1.Items.Count - 1];
                    items.Selected = true;
                }
                else btn_submit.Enabled = false;
            }
            if (mode == Mode.Edit)
            {
                elements.RemoveAt(index);
                MessageBox.Show($"Are deleted {listView1.SelectedItems[0].Index}");
            }
        }

        private void button3_Click(object sender, EventArgs e) // calculation
        {
           
            if (listView1.Items.Count < 1) return; 
             calc = new CalcFunction();

            if  (mode == Mode.Create)
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
                //btn_submit.Enabled = true;

            }
            else
            {
                //getting new total summ of ingredients amounts
                int index;
                ListView lv = listView1;
                List<double> values = new List<double>();
                for (index = 0; index < lv.Items.Count-1; index++)
                {
                     string amount = lv.Items[index].SubItems[1].Text;                     
                     double value = double.Parse(amount);
                    values.Add(value);
                }                
                calc.setAmounts(values);
                double summa = calc.getTotal();

                ListViewItem items = listView1.Items[listView1.Items.Count - 1];
                items.SubItems[1].Text = summa.ToString();
            }
            btn_submit.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e) // submit
        {
            int ind=0;
            if (string.IsNullOrEmpty(listView1.Items[0].SubItems[1].Text)) return;
            
            if (mode == Mode.Edit)
            {
                int count = elements.Count, k;
                List<string> amounts = tbAmounts.setAmountsIdList(id_recepture);               

                for (k = 0; k < count; k++)
                {
                    ind += tbAmounts.UpdateReceptureOrCards("id_ingredients", elements[k].Id.ToString(), int.Parse(amounts[k]));
                    ind += tbAmounts.UpdateReceptureOrCards("amount", elements[k].Amounts.ToString(), int.Parse(amounts[k]));
                }
                MessageBox.Show("Updated"+ind.ToString());
                tbAmounts.RefreshElements();
                elements = tbAmounts.getElements();

                fillAmounts();
                FillAmountsView(); // listview
                showOldAmounts(); // for edit mode

            }
            else
            {
                ind = tbAmounts.InsertAmounts(ref listView1, id_recepture);
                if (ind == 0) MessageBox.Show("All amounts are inserted");
                else MessageBox.Show($"{ind} from {listView1.Items.Count} are inserted");

                btn_recipe.Enabled = true;
                btn_submit.Enabled = false;
                mode = Mode.Edit;
            }
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = radioButton1;
            if(r.Checked == true)
            {
                radioButton2.Enabled = false;
                r.Enabled = false;
                //пока проверяю один из подрежимов

                btn_remove.Enabled = false;
                btn_edit.Enabled = false;

                //MessageBox.Show("radio button is checked");
            }
            else
            {
                //MessageBox.Show("radio button is not checked");
            }
        }
    }
}
