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
        readonly int id_recepture; //для ввода рецепта (коэфициента) к расчитанной из него рецептуре
        readonly CalcBase calcBase = CalcBase.Main;

        private int pragma = 0; // for create mode

        private int main_ingredient_id;
        bool set_main = false,
            reset_main = false;
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
            {
                checkBox1.Enabled = true;
                checkBox1.Checked = false;
                txbRecipe.Enabled = false;
            }
            else
            {
                checkBox1.Visible = false;
                txbRecipe.Visible = false;
            }

            FillAmountsView(); // listview
            if (mode == (Mode)1) // for edit mode
            {
                fillAmounts();
                toolStripStatusLabel2.Text = summa.ToString();
                showOldAmounts();
                pragma = 1;
            }
            else pragma = 0;


            btn_submit.Enabled = false; // submit ingredients

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
                t = string.Format("{0:f1}", elements[k].Amounts);
                items.SubItems.Add(t);
                items.SubItems.Add(""); // заготовка под старые величины или проценты  
                listView1.Items.Add(items);
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
            frm.richTextBox1.Text += ">>> Selected item (*cmbIngr)\n";
            tbIngred.setSelected(cmbIngr.SelectedIndex);
            frm.richTextBox1.Text += "> id of selected ingredient: " + tbIngred.Selected.ToString() + "\n";
            //cmbIngr.Text = cmbIngr.SelectedItem.ToString();
            txbAmounts.Focus();
            if (mode == Mode.Create)
                txbAmounts.Text = "100" + decimal_separator + "0";
            else txbAmounts.Text = "";

            
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
                index = listView1.SelectedIndices[0];          
            index_ingr = cmbIngr.SelectedIndex;         
            
            //записываем в слепок с таблицы
            new_index = controller.SetMain(amount, index);
            el = controller.AddElement(index_ingr, index);
            new_amount = controller.SetAmounts(amount, el);            
            //tbAmount.setSelected(new_index);

            // создаём новую единицу списочного представления
            mode = controller.getMode;
            item = new ListViewItem(el.Name);            
            if (mode == Mode.Edit)
                item.SubItems.Add(el.Amounts.ToString());
            else
                item.SubItems.Add(amount.ToString());
            item.SubItems.Add(el.Amounts.ToString());
            item.Tag = el.Id;

            // убираем выделение
            if (listView1.Items.Count > 0)
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
            
            //if (cmbIngr.SelectedIndex == -1) return;
            //if (string.IsNullOrEmpty(cmbIngr.Text)) return;
            //if (string.IsNullOrEmpty(txbAmounts.Text)) return;

            //double num;
            //if (double.TryParse(txbAmounts.Text, out num))
            //    num = double.Parse(txbAmounts.Text);
            //else return;
            //// see more in FormMain.cs

            //Element AddElement(int i)  //добавить в список элементов по индексу
            //{
            //    Element el = new Element();
            //    el.Id = tbIngred.getSelected(); // id of ingredient 
            //    el.Name = cmbIngr.Text;
            //    el.Amounts = num;

            //    if (elements.Count > 0)
            //        elements.Insert(i + 1, el);
            //    else
            //        elements.Add(el);
            //    return el;
            //}

            //ListViewItem items;
            //int index = 0;
            //if (listView1.SelectedItems.Count > 0) //proverka spiska            
            //{
            //    items = listView1.SelectedItems[0];
            //    index = items.Index;
            //    items.Selected = false; // ubiraem vydelenie

            //    //добавить в список элементов                
            //    Element el = AddElement(index);

            //    ListViewItem item = new ListViewItem(el.Name);
            //    item.SubItems.Add(el.Amounts.ToString());
            //    item.SubItems.Add("");
            //    item.Tag = el.Id;
            //    if ((int)listView1.Items[index].Tag == -1)
            //        index = -1;
            //    listView1.Items.Insert(index + 1, item);

            //    //выделить новый
            //    items = listView1.Items[index + 1];
            //    items.Selected = true;

            //    //пересчитать
            //    double koef = calc.Coefficient;
            //    items.SubItems[2].Text = (el.Amounts * koef).ToString();
            //}
            //else
            //{
            //    //добавить в список элементов
            //    Element el = AddElement(index);

            //    ListViewItem item = new ListViewItem(el.Name);
            //    item.SubItems.Add(el.Amounts.ToString());
            //    item.SubItems.Add(""); // заготовка под проценты
            //    item.Tag = el.Id; // id of ingredient       
            //    listView1.Items.Add(item);

            //    //выделить новый
            //    items = listView1.Items[listView1.Items.Count - 1];
            //    items.Selected = true;

            //    //определить главный вид сырья
            //    main_ingredient_id = el.Id;
            //    toolStripStatusLabel6.Text = main_ingredient_id.ToString();
            //    calc.Coefficient = 100 / el.Amounts;
            //    listView1.Items[0].SubItems[2].Text = "100";
            //}
            //txbAmounts.Text = "100" + decimal_separator + "0";
            //cmbIngr.Focus();
            //btn_calc.Enabled = true;

            ////вывод в консоль
            //frm.richTextBox1.Text += "On btn_edit click, new item added\n";
            //frm.richTextBox1.Text += "records in list view count: " + (listView1.Items.Count - 1).ToString();
            //frm.richTextBox1.Text += "\nrecords in \'elements\' count: " + this.elements.Count;
            //frm.richTextBox1.Text += "\nrecords in controller \'elements\' count: " + tbAmount.Elements_count;
            //frm.richTextBox1.Text += "\nid count: " + tbAmount.Amount_id_count + "\n";
            //frm.richTextBox1.Text += tbAmount.Amount_id_count >= tbAmount.Elements_count;
            //frm.richTextBox1.Text += "\n***\n";
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

            Element el = elements[listView1.SelectedIndices[0]];
            el.Amounts = num;
            el.Name = (string)cmbIngr.SelectedItem;
            el.Id = (int)item.Tag;

            txbAmounts.Text = "0" + decimal_separator + "0";
            return 0;
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
                        label1.Text = calc.Coefficient.ToString();
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

        private void AmountsFromArrayToElementsList(double [] arr)
        {
            int index, length;

            if(arr.Length < elements.Count)
                length = arr.Length;         
            else
                length = elements.Count;           

            for (index = 0; index < length; index++)
            {
                elements[index].Amounts = arr[index];
            }
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

            toolStripStatusLabel2.Text = string.Format("{0:f1}", summa);
           
            // вывод в консоль
            frm.richTextBox1.Text += "On Calc";
            frm.richTextBox1.Text += "\nsumma " + summa.ToString();
            frm.richTextBox1.Text += "\ncoeficient " + calc.Coefficient.ToString();

            
            int index;
            if  (mode == Mode.Create)
            {
                summa = calc.Summa(arr);
                List<string> formated = calc.FormatAmounts(arr, summa); //format number
                for (index = 0; index < arr.Length; index++)
                {
                    listView1.Items[index].SubItems[2].Text = formated[index];                    
                }                
                toolStripStatusLabel4.Text = formated[index];              

                btn_submit.Enabled = true;                

                // вывод в консоль
                frm.richTextBox1.Text += "\n Mode Create";
                }
            else
            {
                //int index;
                index = 0;
                if (a != 100.0)
                {
                    double summa1 = calc.Summa(arr);
                    List<string> formated = calc.FormatAmounts(arr, summa1); //format number

                    //recalculating a recepture
                    for (index = 0; index < arr.Length; index++)
                    {
                        ListViewItem item = listView1.Items[index];
                        item.SubItems[2].Text = item.SubItems[1].Text;                        
                        item.SubItems[1].Text = formated[index];
                    }
                    toolStripStatusLabel4.Text = formated[index];                    

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
            AmountsFromArrayToElementsList(arr);            
        }

        /*************************************************************************
         * Запись в базу данных
         *************************************************************************/

        private void btn_test_Click(object sender, EventArgs e)
        {           
            frm.richTextBox1.Text = "";
            if (mode == Mode.Create)
            {
                string text = "Create mode";                
                for (int k=0; k > elements.Count; k++)
                {
                    Element el = elements[k];
                    text += el.Name + " " + el.Amounts.ToString();
                    text += "\n";
                }
                frm.richTextBox1.Text = text;
            }
        }

        private void button4_Click(object sender, EventArgs e) // submit
        {
            //int ind = 0;
            //if (string.IsNullOrEmpty(listView1.Items[0].SubItems[1].Text)) return;

            //if (mode == Mode.Edit)
            //{
            //    ind = tbAmount.updateRecords(ref frm);
            //    MessageBox.Show("Updated" + ind.ToString());
            //}
            //else
            //{
            //    ind = tbAmount.updateRecords(ref frm);
            //    if (ind == 0) MessageBox.Show("All amounts are inserted");
            //    else MessageBox.Show($"{ind} from {listView1.Items.Count} are inserted");
            //    mode = Mode.Edit;
            //}
            //// забираю в обработчик Обновления (полоса меню)

            //if (checkBox1.Checked && !string.IsNullOrEmpty(txbRecipe.Text))
            //{
            //    saveRecipe();
            //}
        }
       
        public void saveRecipe()
        {
            int ind;
            double coefficient = calc.Coefficient;
            tbRecipeController tb = new tbRecipeController("Recipe");
            tb.Selected = id_recepture;

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

       private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txbRecipe.Enabled = true;
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string[]> lv = new List<string[]>();
            foreach (ListViewItem item in listView1.Items)
            {
                string[] arr = new string[3];
                for (int k = 0; k < 3; k++)
                    arr[k] = item.SubItems[k].Text;
                lv.Add(arr);
            }
            frm.richTextBox1.Clear();           
            frm.richTextBox1.Lines = controller.PrintAmount(lv);
        }        
    }
}
