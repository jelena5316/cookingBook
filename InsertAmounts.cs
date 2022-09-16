
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
        readonly int id_recepture;
        private int pragma; // for create mode
        double[] amounts;
        Mode mode; //create new or edit old
        List<Element> elements; // id and name, amounts

        IngredientsController tbIngred;
        AmountsController tbAmounts;        
        CalcFunction calc;
   
        NumberFormatInfo nfi;
        string decimal_separator;

        Form2 frm = new Form2();

        public InsertAmounts(int id) // New Recepture
        {
            InitializeComponent();
            id_recepture = id;           
            tbIngred = new IngredientsController(1);
            tbIngred.setCatalog();
            tbAmounts = new AmountsController("Amounts");
            calc = new CalcFunction();
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
            calc = new CalcFunction();

            fillAmounts();
            FillAmountsView(); // listview

            if (mode == (Mode)2) showOldAmounts(); // for edit mode
            else pragma = 0;
        }

        private void InsertAmounts_Load(object sender, EventArgs e)
        {
            btn_recipe.Enabled = false; //insert recipe           
            btn_submit.Enabled = false; // submit ingredients
            txbAmounts.Text = "0" + ","+ "0";

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

            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            nfi = CultureInfo.CurrentCulture.NumberFormat;
            decimal_separator = nfi.NumberDecimalSeparator;
            this.Text += " " + CultureInfo.CurrentCulture +
                " (decimal separator \'" + nfi.NumberDecimalSeparator + "\')";
            txbAmounts.Text = "0" + decimal_separator + "0";

            // эмулятор консоли, выводит метаданные           
            frm.Show();
            frm.richTextBox1.Text = "On Load: \n";
            frm.richTextBox1.Text += this.Text + " ";
            frm.richTextBox1.Text += this.mode == Mode.Create ? "crete mode" : "edit mode";
            frm.richTextBox1.Text += "\nelements count: " + tbAmounts.Elements_count;
            frm.richTextBox1.Text += "\nid count: " + tbAmounts.Amount_id_count + "\n";
            frm.richTextBox1.Text += tbAmounts.Amount_id_count >= tbAmounts.Elements_count;
            frm.richTextBox1.Text += "\nAmounts from array \'amounts\': \n";
            for (int k = 0; k < amounts.Length-1; k++)
            {
                frm.richTextBox1.Text += amounts[k] + " ";
            }
            frm.richTextBox1.Text += "\n****\n";
        }

        private void fillAmounts() //for create mode
        {
            amounts = new double[elements.Count + 1]; 
            int k;
            for (k = 0; k < amounts.Length - 1; k++)
            {
                amounts[k] = elements[k].Amounts;
            }
            
            calc.setAmounts(elements); // сохраняет и cуммирует величины
            amounts[k] = calc.getTotal();

            //вывод в консоль
            frm.richTextBox1.Text += "On fillAmounts (write ingredients amounts from this elements)\n";
            for (k = 0; k < amounts.Length-1; k++)
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
                items = new ListViewItem(elements[k].Name);
                items.Tag = elements[k].Id;
                items.SubItems.Add(elements[k].Amounts.ToString());
                items.SubItems.Add(""); // заготовка под старые величины     
                listView1.Items.Add(items);
            }
            if (mode == Mode.Edit)
            {
                //заготовка под сумму
                items = new ListViewItem("Total");
                items.Tag = -1;
                items.SubItems.Add("");
                items.SubItems.Add("");
                listView1.Items.Add(items);
            }
            
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
            //txbAmounts.Focus();
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

            ListViewItem items;
            int index = 0;
            if (listView1.SelectedItems.Count > 0) //proverka spiska            
            {
                items = listView1.SelectedItems[0];
                index = items.Index;
                items.Selected = false; // ubiraem vydelenie

                ListViewItem item = new ListViewItem(cmbIngr.Text);
                item.SubItems.Add(txbAmounts.Text);
                item.SubItems.Add("");
                item.Tag = tbIngred.getSelected(); // id of ingredient                                                 
                listView1.Items.Insert(index+1, item);                

                //добавить в список элементов
                Element el = new Element();
                el.Id = (int)item.Tag;
                el.Name = cmbIngr.Text;
                el.Amounts = num;
                elements.Insert(index+1, el);

                //выделить новый
                items = listView1.Items[index+1];
                items.Selected = true;
            }
            else
            {
                ListViewItem item = new ListViewItem(cmbIngr.Text);
                item.SubItems.Add(txbAmounts.Text);
                item.SubItems.Add(""); // заготовка под проценты
                item.Tag = tbIngred.getSelected(); // id of ingredient       
                listView1.Items.Add(item);
            
                //добавить в список элементов
                Element el = new Element();
                el.Id = (int)item.Tag;
                el.Name = cmbIngr.Text;
                el.Amounts = num;
                elements.Add(el);

                //выделить новый
                items = listView1.Items[listView1.Items.Count-1];
                items.Selected = true;
            }
            txbAmounts.Text = "0" + decimal_separator + "0";
            cmbIngr.Focus();

            //вывод в консоль
            frm.richTextBox1.Text += "On btn_edit click, new item added\n";
            frm.richTextBox1.Text += "records in list view count: " + (listView1.Items.Count - 1).ToString();
            frm.richTextBox1.Text += "\nrecords in \'elements\' count: " + this.elements.Count;
            frm.richTextBox1.Text += "\nrecords in controller \'elements\' count: " + tbAmounts.Elements_count;
            frm.richTextBox1.Text += "\nid count: " + tbAmounts.Amount_id_count + "\n";
            frm.richTextBox1.Text += tbAmounts.Amount_id_count >= tbAmounts.Elements_count;
            frm.richTextBox1.Text += "\n***\n";
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
                btn_remove.Enabled = true;
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
            cmbIngr.Enabled = false;
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

            txbAmounts.Text = "0" + decimal_separator + "0";
            cmbIngr.Enabled = true;
            return 0;
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count < 1) return; // proverka spiska
            if (listView1.SelectedItems.Count < 1) return;

            int index = listView1.SelectedItems[0].Index;
            listView1.Items[index].Selected = false;

            listView1.Items.RemoveAt(index);
            if (index < elements.Count)
                elements.RemoveAt(index);

            if (listView1.Items.Count > 0)
            {
                if (index > 0) index--;
                if (index < 0) index = 0;
                listView1.Items[index].Selected = true;
            }
            else btn_submit.Enabled = false;
    
            //вывод в консоль
            frm.richTextBox1.Text += "On btn_remove click, an item deleted\n";
            frm.richTextBox1.Text += "records in list view count: " + (listView1.Items.Count - 1).ToString();
            frm.richTextBox1.Text += "\nrecords in \'elements\' count: " + this.elements.Count;
            frm.richTextBox1.Text += "\nrecords in controller \'elements\' count: " + tbAmounts.Elements_count;
            frm.richTextBox1.Text += "\nid count: " + tbAmounts.Amount_id_count + "\n";
            frm.richTextBox1.Text += tbAmounts.Amount_id_count >= tbAmounts.Elements_count;
            frm.richTextBox1.Text += "\n***\n";
        }

        
        /******************************************************************
         * Калькуляция рецептуры и суммарного веса
         ******************************************************************/
        
        private double[] AmountsFromElementsListToArray()
        {
            double[] arr = new double[elements.Count];          
            for (int index = 0; index < arr.Length; index++)
            {
                arr[index] = elements[index].Amounts;
            }
            return arr;
        }

        private void button3_Click(object sender, EventArgs e) // calculation
        {
            if (listView1.Items.Count < 1) return;

            double summa, a;
            double[] arr;

            a = double.Parse(listView1.Items[0].SubItems[1].Text);
            arr = AmountsFromElementsListToArray();
            summa = calc.Summa(arr); // 2 column, index 1
            arr = calc.ReCalc (100 / a, arr); // create mode or edit mode if main is changed          
            calc.Coefficient = a / 100;

            // вывод в консоль
            frm.richTextBox1.Text += "On Calc";
            frm.richTextBox1.Text += "\nsumma " + summa.ToString();
            frm.richTextBox1.Text += "\ncoeficient " + calc.Coefficient.ToString();

            if  (mode == Mode.Create)
            {
                for (int index = 0; index < arr.Length; index++)
                {
                   listView1.Items[index].SubItems[2].Text = arr[index].ToString();
                }
                if (pragma > 0)
                {
                    listView1.Items[listView1.Items.Count - 1].SubItems[1].Text = summa.ToString();
                    summa = calc.Summa(arr);
                    listView1.Items[listView1.Items.Count - 1].SubItems[2].Text = summa.ToString();
                }
                else
                {
                    ListViewItem items;
                    items = new ListViewItem("Total");
                    items.Tag = -1;
                    items.SubItems.Add(summa.ToString());
                    summa = calc.Summa(arr);
                    items.SubItems.Add(summa.ToString());
                    listView1.Items.Add(items);
                    pragma = 1;
                }

                btn_submit.Enabled = true;                

                // вывод в консоль
                frm.richTextBox1.Text += "\n Mode Create";
                }
            else
            {
                int index;
                if (a != 100.0)
                {
                    //recalculating a recepture
                    for (index = 0; index < arr.Length; index++)
                    {
                        ListViewItem item = listView1.Items[index];
                        item.SubItems.Insert(2, new ListViewItem.ListViewSubItem());
                        item.SubItems[2].Text = item.SubItems[1].Text;
                        item.SubItems[1].Text = arr[index].ToString();
                    }
                    listView1.Columns.Insert(2, "Amounts*(g)");                    
                    listView1.Items[listView1.Items.Count - 1].SubItems[2].Text = summa.ToString();
                    summa = calc.Summa(arr);
                    listView1.Items[listView1.Items.Count - 1].SubItems[1].Text = summa.ToString();
                    btn_calc.Enabled = false;

                    // вывод в консоль
                    frm.richTextBox1.Text += "\n Mode Edit: edit with main ingredients";
                    frm.richTextBox1.Text += "\n summa of new " + summa.ToString();
                    frm.richTextBox1.Text += "\n***";

                }
                else
                {            
                    ListViewItem items = listView1.Items[listView1.Items.Count - 1];
                    items.SubItems[1].Text = summa.ToString();

                    // вывод в консоль
                    frm.richTextBox1.Text += "\n Mode Edit: edit without main ingredients";
                    frm.richTextBox1.Text += "\n summa of new " + summa.ToString();
                    frm.richTextBox1.Text += "\n***\n";
                }
            }
            btn_submit.Enabled = true;
        }

        /*************************************************************************
         * Запись в базу данных
         *************************************************************************/
        private int updateRecords()
        {
            int element_count = tbAmounts.Elements_count, k, amount_count, ind = 0;
            List<string> amounts = tbAmounts.getAmountsIdList;
            amount_count = tbAmounts.Amount_id_count;
            //bool diff = amount_count >= element_count;
            
            if(amount_count >= element_count)
            {
                frm.richTextBox1.Text += "\n***\nUpdating db\n";
                for (k = 0; k < element_count; k++)
                {
                    ind += tbAmounts.UpdateReceptureOrCards("id_ingredients", elements[k].Id.ToString(), int.Parse(amounts[k]));
                    string amount = calc.ColonToPoint(elements[k].Amounts.ToString());
                    ind += tbAmounts.UpdateReceptureOrCards("amount", amount, int.Parse(amounts[k]));
                    frm.richTextBox1.Text += ind/2 + "th records, where id " + amounts[k]+"\n";
                }
                if(amount_count - element_count > 0)
                {
                    //удаляем лишнее
                    frm.richTextBox1.Text += "\n***\nDeleting from db\n";
                    for (int q = k; q < amount_count; q++)
                    {
                        string query = $"delete from Amounts where id = {amounts[q]};";
                        int rezult = tbAmounts.Edit(query);
                        frm.richTextBox1.Text += rezult + " records, where id " + amounts[q]+"\n";
                    }
                }
            }
            else
            {
                frm.richTextBox1.Text += "\n***\nUpdating db\n";
                for (k = 0; k < amount_count; k++)
                {
                    ind += tbAmounts.UpdateReceptureOrCards("id_ingredients", elements[k].Id.ToString(), int.Parse(amounts[k]));
                    string amount = calc.ColonToPoint(elements[k].Amounts.ToString());
                    ind += tbAmounts.UpdateReceptureOrCards("amount", amount, int.Parse(amounts[k]));
                    frm.richTextBox1.Text += ind/2 + "th records, where row id " + amounts[k] + "\n";
                }
                frm.richTextBox1.Text += "\n***\nInserting into db\n";
                for (int q = k; q < element_count; q++)
                {
                    // дописывает недостающее и получаем номера                    
                    string amount = calc.ColonToPoint(elements[q].Amounts.ToString());
                    string query = "insert into Amounts (id_recepture, id_ingredients, amount) " +
                    $"values ({id_recepture}, {elements[q].Id.ToString()}, {amount}); select last_insert_rowid();";
                    string id = tbAmounts.Count(query);
                    frm.richTextBox1.Text += "last insert records id "+  id + ", where ingredients id " + elements[q].Id.ToString() + "\n";

                    // вносим номера в список номеров в контроллере
                    amounts.Add(id);
                }
                frm.richTextBox1.Text += "records are " + amounts.Count + "\n";
            }
            return ind;
        }

        private void button4_Click(object sender, EventArgs e) // submit
        {
            int ind=0;
            if (string.IsNullOrEmpty(listView1.Items[0].SubItems[1].Text)) return;
            
            if (mode == Mode.Edit)
            {
                ind = updateRecords();
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
        /********************************************************************************
         * Ввод рецепта (коэфициента) к расчитанной из него рецептуре
         *******************************************************************************/
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
        /****************************************************************************
         * Подрежимы редактирования
         ***************************************************************************/
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = radioButton1;
            if(r.Checked == true)
            {
                radioButton2.Enabled = false;
                r.Enabled = false;
                //пока проверяю один из подрежимов

                //btn_remove.Enabled = false;
                //btn_edit.Enabled = false;

                //MessageBox.Show("radio button is checked");
            }
            else
            {
                //MessageBox.Show("radio button is not checked");
            }
        }
        
    }
}
