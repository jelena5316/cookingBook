
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
        double summa;
        double[] amounts;         
        Mode mode; //create new or edit old
        string name; // name of recepture


        List<Element> elements; // id and name, for amounts
        List<Item> ingredients;        

        IngredientsController tbIngred;
        AmountsController tbAmounts;        
        CalcFunction calc;
   
        NumberFormatInfo nfi;
        string decimal_separator;

        Form2 frm = new Form2();

        public InsertAmounts(int id)
        {
            InitializeComponent();
            id_recepture = id;
            tbAmounts = new AmountsController("Amounts");
            tbAmounts.Id_recepture = id;            
            
            tbAmounts.TbRec = new FormMainController("Recepture");
            tbAmounts.tbRecSelected(id);
            tbAmounts.RefreshElements();
            elements = tbAmounts.getElements();
            tbAmounts.setSelected(0);
            
            tbIngred = new IngredientsController(1);
            tbIngred.setCatalog();
            List<Item> ingredients = tbIngred.getCatalog();
            //Class1.FillCombo(ingredients, ref cmbIngr);
            calc = new CalcFunction();

            this.mode = (elements.Count < 1) ? (Mode)0 : (Mode)1; // mode autodetector
            pragma = (mode == 0) ? 0 : 1;

            name = tbAmounts.dbReader($"select name from Recepture where id = {id_recepture}")[0];

            //FillAmountsView(); // listview
            //if (mode == (Mode)1)// for edit mode
            //{ 
            //  fillAmounts();
            //  showOldAmounts();
            //} 
            //else pragma = 0;
        }

        public InsertAmounts(ref AmountsController tbAmounts)
        {
            InitializeComponent();
            
            this.tbAmounts = tbAmounts;            
            id_recepture = tbAmounts.Id_recepture;
            elements = tbAmounts.getElements();

            this.mode = (elements.Count < 1) ? (Mode)0 : (Mode)1;
            
            tbIngred = new IngredientsController(1);
            tbIngred.setCatalog();
            ingredients = tbIngred.getCatalog();            
            calc = new CalcFunction();

            pragma = 0;
            name = tbAmounts.dbReader($"select name from Recepture where id = {id_recepture}")[0];
        }

        private void InsertAmounts_Load(object sender, EventArgs e)
        {
            
            FillAmountsView(); // listview
            if (mode == (Mode)1) // for edit mode
            {
                fillAmounts();
                toolStripStatusLabel2.Text = summa.ToString();
                showOldAmounts();
                pragma = 1;
            }
            else pragma = 0;

            btn_recipe.Enabled = false; //insert recipe           
            btn_submit.Enabled = false; // submit ingredients
            
            if (mode == Mode.Edit) //1
            {
                listView1.Columns[1].Text = "Amounts(%) new";
                listView1.Columns[2].Text += " old";

                txbRecipe.Enabled = false;
                btn_recipe.Enabled = false;                
            }

            this.Text += $" into '{name}' ";
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            nfi = CultureInfo.CurrentCulture.NumberFormat;
            decimal_separator = nfi.NumberDecimalSeparator;
            this.Text += " " + CultureInfo.CurrentCulture +
                " (decimal separator \'" + nfi.NumberDecimalSeparator + "\')";
            txbAmounts.Text = "100" + decimal_separator + "0";


            /*
             * Текстовые поля: ингредиенты и рецепты
             */
            List<string> recipes = tbAmounts.dbReader
                ($"select name from Recipe;");
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            foreach (String el in recipes) source.Add(el);
            txbRecipe.AutoCompleteCustomSource = source;
            txbRecipe.AutoCompleteMode = AutoCompleteMode.Suggest;
            txbRecipe.AutoCompleteSource = AutoCompleteSource.CustomSource;

            List<string> ingredients = tbAmounts.dbReader
                ($"select name from Ingredients;");            
            Class1.FillCombo(ingredients, ref cmbIngr);            
            AutoCompleteStringCollection source1 = new AutoCompleteStringCollection();            
            foreach (String el in recipes) source1.Add(el);            
            txbRecipe.AutoCompleteCustomSource = source;
            txbRecipe.AutoCompleteMode = AutoCompleteMode.Suggest;
            txbRecipe.AutoCompleteSource = AutoCompleteSource.CustomSource;


            // эмулятор консоли, выводит метаданные           
            frm.Show();
            frm.richTextBox1.Text = "On Load: \n";
            frm.richTextBox1.Text += this.Text + " ";
            frm.richTextBox1.Text += this.mode == Mode.Create ? "crete mode" : "edit mode";
            frm.richTextBox1.Text += "\nelements count: " + tbAmounts.Elements_count;
            frm.richTextBox1.Text += "\nid count: " + tbAmounts.Amount_id_count + "\n";
            frm.richTextBox1.Text += tbAmounts.Amount_id_count >= tbAmounts.Elements_count;
            frm.richTextBox1.Text += "\nAmounts from array \'amounts\': \n";
            
            if (mode == Mode.Edit)
            { 
                for (int k = 0; k < amounts.Length-1; k++)
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
            localizacijaToolStripMenuItem.Text = "US";      }

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
                items.SubItems.Add(""); // заготовка под старые величины или проценты  
                listView1.Items.Add(items);

                //items = new ListViewItem(elements[k].Name);
                //items.Tag = elements[k].Id;
                //t = string.Format("{0:f1}", elements[k].Amounts);
                //items.SubItems.Add(t);
                //items.SubItems.Add(""); // заготовка под старые величины или проценты  
                //listView1.Items.Add(items);
            }
        }
        
        private void showOldAmounts()
        {
            ListView list = listView1;
            for (int k = 0; k < elements.Count; k++)
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
                txbAmounts.Text = "100" + decimal_separator + "0";
            else txbAmounts.Text = "";
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
            // see more in FormMain.cs

            Element AddElement(int i)  //добавить в список элементов по индексу
            {
                Element el = new Element();
                el.Id = tbIngred.getSelected(); // id of ingredient 
                el.Name = cmbIngr.Text;
                el.Amounts = num;

                if (elements.Count > 0)
                    elements.Insert(i + 1, el);
                else
                    elements.Add(el);
                return el;
            }
  
            ListViewItem items;
            int index = 0;
            if (listView1.SelectedItems.Count > 0) //proverka spiska            
            {
                items = listView1.SelectedItems[0];
                index = items.Index;                
                items.Selected = false; // ubiraem vydelenie

                //добавить в список элементов                
                 Element el = AddElement(index);              
                
                ListViewItem item = new ListViewItem(el.Name);
                item.SubItems.Add(el.Amounts.ToString());
                item.SubItems.Add("");
                item.Tag = el.Id;
                if ((int)listView1.Items[index].Tag == -1)
                    index = -1;
                listView1.Items.Insert(index+1, item);
      
                //выделить новый
                items = listView1.Items[index+1];
                items.Selected = true;
            }
            else
            {
                //добавить в список элементов
                Element el = AddElement(index);                

                ListViewItem item = new ListViewItem(el.Name);
                item.SubItems.Add(el.Amounts.ToString());
                item.SubItems.Add(""); // заготовка под проценты
                item.Tag = el.Id; // id of ingredient       
                listView1.Items.Add(item);

                //выделить новый
                items = listView1.Items[listView1.Items.Count-1];
                items.Selected = true;
            }
            txbAmounts.Text = "100" + decimal_separator + "0";
            cmbIngr.Focus();
            btn_calc.Enabled = true;

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
            //cmbIngr.Enabled = false;
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
            //cmbIngr.Enabled = true;
            return 0;
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count < 1) return; // proverka spiska
            if (listView1.SelectedItems.Count < 1) return;

            int index = listView1.SelectedItems[0].Index;            
            int tag = (int)listView1.Items[index].Tag;

            if (tag == -1) pragma = 0;
                //return; //check tag to preserve row with sum in case when create mode

            listView1.Items[index].Selected = false;

            listView1.Items.RemoveAt(index);
            if (index < elements.Count) //отсюда годиться только для нового
                elements.RemoveAt(index);

            if (listView1.Items.Count > 0)
            {
                if (index > 0) index--;
                if (index < 0) index = 0;
                listView1.Items[index].Selected = true;
            }
            else btn_submit.Enabled = false;

            if (listView1.Items.Count == 1 && (int)listView1.SelectedItems[0].Tag == -1)
               btn_calc.Enabled = false;
      
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

            double a; // summa already exists
            double[] arr;

            a = double.Parse(listView1.Items[0].SubItems[1].Text);
            arr = AmountsFromElementsListToArray();
            summa = calc.Summa(arr); // 2 column, index 1
            arr = calc.ReCalc (100 / a, arr); // create mode or edit mode if main is changed          
            calc.Coefficient = a / 100;

            toolStripStatusLabel2.Text = summa.ToString();
           
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
                summa = calc.Summa(arr);
                toolStripStatusLabel4.Text = summa.ToString();
                //if (pragma > 0)
                //{
                //    listView1.Items[listView1.Items.Count - 1].SubItems[1].Text = summa.ToString();
                //    summa = calc.Summa(arr);
                //    listView1.Items[listView1.Items.Count - 1].SubItems[2].Text = summa.ToString();
                //}
                //else
                //    вот это и для режима редактированмя, на случай, если строку с суммой удалили
                //{
                //    ListViewItem items;
                //    items = new ListViewItem("Total");
                //    items.Tag = -1;
                //    items.SubItems.Add(summa.ToString());
                //    summa = calc.Summa(arr);
                //    items.SubItems.Add(summa.ToString());
                //    listView1.Items.Add(items);
                //    pragma = 1;
                //}

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
                    //listView1.Items[listView1.Items.Count - 1].SubItems[2].Text = summa.ToString();
                    //summa = calc.Summa(arr);
                    //listView1.Items[listView1.Items.Count - 1].SubItems[1].Text = summa.ToString();
                    //btn_calc.Enabled = false;

                    // вывод в консоль
                    frm.richTextBox1.Text += "\n Mode Edit: edit with main ingredients";
                    frm.richTextBox1.Text += "\n summa of new " + summa.ToString();
                    frm.richTextBox1.Text += "\n***";

                }
                else
                {            
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
       
        private void button4_Click(object sender, EventArgs e) // submit
        {
            int ind=0;
            if (string.IsNullOrEmpty(listView1.Items[0].SubItems[1].Text)) return;         
  
            if (mode == Mode.Edit)
            {
                ind = tbAmounts.updateRecords(ref frm);
                MessageBox.Show("Updated"+ind.ToString());
            }
            else
            {
                ind = tbAmounts.updateRecords(ref frm);
                if (ind == 0) MessageBox.Show("All amounts are inserted");
                else MessageBox.Show($"{ind} from {listView1.Items.Count} are inserted");
                btn_recipe.Enabled = true;                
                mode = Mode.Edit;
            }
                //tbAmounts.RefreshElements();
                //elements = tbAmounts.getElements();
                //fillAmounts();
                //FillAmountsView(); // listview
                //showOldAmounts(); // for edit mode                
                btn_submit.Enabled = false;
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
        
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1) return;          
            int index = listView1.SelectedItems[0].Index;
            int tag = (int)listView1.Items[index].Tag;

            if (tag > -1) //check tag to preserve row with sum in case when create mode
            {
                btn_select.Enabled = true;
                btn_remove.Enabled = true;
                btn_edit.Enabled = true;               
            }
            else
            {
                btn_select.Enabled = false;
                btn_remove.Enabled = false;
                //btn_edit.Enabled = false;
                return;
            }

            if (index == 0) // check first ingredients amounts (not equil zero)
            {
                string text;
                double num;
                    
                text = listView1.Items[index].SubItems[1].Text;
                num = double.TryParse(text, out num) ? num : 100;
                num = (num == 0) ? 100 : num;
                listView1.Items[index].SubItems[1].Text = num.ToString();
                elements[index].Amounts = num;
            }
            checked
            {
                try
                {
                    int id = tbAmounts.setSelected(index);
                    Element el = tbAmounts.getElementByIndex(index);
                    frm.richTextBox1.Text += ">>> Selected element\n";
                    int k;
                    for (k = 0; k < ingredients.Count; k++)
                    {
                        if (ingredients[k].id == el.Id)
                        {
                            cmbIngr.SelectedIndex = k;
                            frm.richTextBox1.Text += "> selected index of combo item: " + cmbIngr.SelectedIndex + "\n";
                            frm.richTextBox1.Text += "> selected item name: " + cmbIngr.Items[k].ToString() + "\n";
                        }
                    }
                    cmbIngr.Text = el.Name;
                    txbAmounts.Text = el.Amounts.ToString();
                    listView1.Focus();
                    frm.richTextBox1.Text += "> Selected ingredients (name, id): " + el.Name + " " + el.Id + "\n";
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    //throw ex;
                    return;
                }
            }

        }
    }
}
