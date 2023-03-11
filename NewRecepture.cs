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
    public partial class NewRecepture : Form
    {
        int id_recepture, category, technology;
        bool indicator; // choose mode
        string name, recepture, source, author, URL, description;

        tbReceptureController tb;
        tbController tbCat;
        tbTechnologyController tbTech;
        NewReceptureController controller;// = new NewReceptureController();


        public NewRecepture(tbReceptureController tbMain, NewReceptureController controller)
        {
            InitializeComponent();           
            this.controller = controller;
            controller.TbMain = tbMain;
            this.tb = controller.TbMain;           
            id_recepture = controller.ReceptureInfo.getId();
            this.category = 0;
            this.technology = 0;

            tbCat = new tbController("Categories");
            tbCat.setCatalog();
            tbTech = new tbTechnologyController("Technology");
            tbTech.setCatalog();

            //indicator = id_recepture > 0 ? true : false;
            indicator = controller.Indicator;
        }

        public NewRecepture(NewReceptureController controller)
        {
            InitializeComponent();
            this.controller = controller;
            tb = controller.TbMain;
            tbCat = controller.TbCat();
            tbTech = controller.TbTech();

            ReceptureStruct rec = controller.ReceptureInfo;
            int[] ids = rec.getIds();
            id_recepture = rec.getId();
            category = ids[0];
            technology = ids[1];
            indicator = controller.Indicator; // choose mode

            if (indicator)
            {                
                name = controller.Data[0];
                source = controller.Data[1];
                author = controller.Data[2];
                URL = controller.Data[3];
                description = controller.Data[4];
            }
        }

        private void SetForm()
        {
            int temp;
            List<Item> items;          

            temp = category;
            items = tbCat.getCatalog();
            Class1.FillCombo(items, ref cmbCat);
            category = temp;
            if (category > 0)
                cmbCat.SelectedIndex = Class1.ChangeIndex(items, category);
            else
                cmbCat.SelectedIndex = 0;

            temp = technology;            
            items = tbTech.getCatalog();
            Class1.FillCombo(items, ref cmbTech);
            technology = temp;
            if (technology > 0)
            {
                cmbTech.SelectedIndex = Class1.ChangeIndex(items, technology);                
            }
            else
            {
                cmbTech.Text = "choose any technology";
            }

            if (indicator)
            {
                txbRecepture.Text = name;
                txbSource.Text = source;
                txbAuthor.Text = author;
                txbURL.Text = URL;
                txbDescription.Text = description;                
            }
        }

        private void cmbTech_SelectedIndexChanged(object sender, EventArgs e)
        {
            technology = tbTech.setSelected(cmbTech.SelectedIndex);
        }
        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbCat.setSelected(cmbCat.SelectedIndex);
        }

        private void chBox_technology_CheckedChanged(object sender, EventArgs e)
        {
            if (chBox_technology.Checked)
            {
                cmbTech.Enabled = false;
                technology = 0;
            }
            else
            {
                tbTechnologyController tbTech = new tbTechnologyController("Technology");
                tbTech.setCatalog();
                cmbTech.Enabled = true;
                technology = tbTech.setSelected(0);
            }
        }

        private void button1_Click(object sender, EventArgs e) //remove
        {
            int selected = tb.Selected;
            if (selected == 0)
            {
                MessageBox.Show($"Recipe do not recorded into data base!");
                this.Dispose();
                this.Close();
                return;
            }
            int count = tb.RemoveItem();
            MessageBox.Show($"Is removed {count} records");
            this.Dispose();
            this.Close();
        }

       

        private void button2_Click(object sender, EventArgs e) // set / write into db (Recepture)
        {
            int result = WriteIntoDataBase(); // -1: no data, -2: name not unieqe 
            string message = "Recepture is updated (inserted)";

            if (result > 0)
            {
                message = "Recepture is updated (inserted)";
                MessageBox.Show(message);
                if (!indicator)
                    {
                     this.Dispose();
                     this.Close();                      
                    }       
            }
            else
            {
                if (result == -1)
                {
                    message = "Please, check name and category be inputed and then try again!";
                    MessageBox.Show(message);
                    return;
                }                
                if (result == -2)
                {
                    message = "Recepture with such name already exists. Please, type new name and try again";
                    MessageBox.Show(message);
                    return;
                }
            }
        }

        private int WriteIntoDataBase()
        {
            int num = 0;
            if (!indicator) // вводим новый рецепт
            {
                if (string.IsNullOrEmpty(txbRecepture.Text)) return -1;
                if (string.IsNullOrEmpty(cmbCat.SelectedItem.ToString())) return -1;

                name = txbRecepture.Text;
                category = tbCat.getSelected();
                if (category == 0) category = 1;
                if (tb.IfRecordIs(name)) return -2;
                //пресекаем попытку ввести новую запись с занятым названием
                tb.InsertNewRecord(name, category);
                // пишем в базу данных название и категория нового рецепта, получаем номер
                id_recepture = tb.getId();
                //add new recepture and get it's id     
            }
            else // редактируем существующую запись
            {
                if (string.IsNullOrEmpty(txbRecepture.Text)) return -1;

                name = txbRecepture.Text;
                category = tbCat.getSelected();

                num = tb.UpdateReceptureOrCards("name", name, id_recepture);
                num = tb.UpdateReceptureOrCards("id_category", category.ToString(), id_recepture);
                if (technology == 0 || chBox_technology.Checked)
                {
                    num = tb.UpdateReceptureOrCards("id_technology", technology.ToString(), id_recepture);
                    //num = tb.Edit($"update Recepture set id_technology = {technology} where id = {id_recepture};");
                }
            }

            if (string.IsNullOrEmpty(txbAuthor.Text)) return 0;
                author = txbAuthor.Text;
                num = tb.UpdateReceptureOrCards("author", author, id_recepture);
                Report(num, "author");

            if (string.IsNullOrEmpty(txbSource.Text)) return 0;
                source = txbSource.Text;
                num = tb.UpdateReceptureOrCards("source", source, id_recepture);

            if (string.IsNullOrEmpty(txbDescription.Text)) return 0;
                description = txbDescription.Text;
                num = tb.UpdateReceptureOrCards("description", description, id_recepture);
                Report(num, "description");
            
           
            if (string.IsNullOrEmpty(txbURL.Text)) return 0;
                URL = txbURL.Text;
                num = tb.UpdateReceptureOrCards("URL", URL, id_recepture);
                Report(num, "URL");
                //returns only then fields is not nullable!

                return id_recepture;
            // сделать перезагрузку изменных данных в котроллер формы!
        }

        private void Report(int num, string variable) //developer mode
        {
            if (num == 0) this.Text += $" {variable} not writted";
            else this.Text += $" {variable} is writted";
        }

        private void NewRecepture_Load(object sender, EventArgs e)
        {
            int ind;
            ind = TextBoxAutocomplet("name", txbRecepture);
            ind = TextBoxAutocomplet("source", txbSource);
            ind = TextBoxAutocomplet("author", txbAuthor);
            ind = TextBoxAutocomplet("URL", txbURL);
            SetForm();            
        }

        private int TextBoxAutocomplet(string column, TextBox box)
        {
            int length = 0;
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
           
            if (tb.IfRecordIs())
            {
                source.AddRange(controller.getNames(column));
                box.AutoCompleteCustomSource = source;
                box.AutoCompleteMode = AutoCompleteMode.Suggest;
                box.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            else
            {
                source.Add("empty");
            }
            return length;
    } 


        //private void ChangeInfo()
        //{
        //    // добваить в РецептуруСтрукт методы для записи данных
        //    if (string.IsNullOrEmpty(txbRecepture.Text)) return;
        //    if (tb.IfRecordIs(txbRecepture.Text)) return;
        //    name = txbRecepture.Text;

        //    if (cmbCat.SelectedIndex < 0) return;
        //    category = tbCat.setSelected(cmbCat.SelectedIndex);
        //    if (!chBox_technology.Checked && cmbCat.SelectedIndex > -1)
        //        technology = tbTech.setSelected(cmbTech.SelectedIndex);

        //    if (string.IsNullOrEmpty(txbDescription.Text)) return;
        //    description = txbDescription.Text;
        //    if (string.IsNullOrEmpty(txbURL.Text)) return;
        //    URL = txbURL.Text;

        //    source = txbSource.Text;
        //    author = txbAuthor.Text;
        //}
}

    public class NewReceptureController
    {
        int id_recepture = 0, category, technology;        
        string name, recepture, source, author, URL, description;        
        List<Item> categories, technologies;
        ReceptureStruct info;

        tbReceptureController tb;
        tbController tbCat;
        tbTechnologyController tbTech;

        //public NewReceptureController()
        //{
        //    tb = new tbReceptureController("Recepture");
        //    tbCat = new tbController("Categories");
        //    tbTech = new tbTechnologyController("Technology");
        //    tbCat.setCatalog();
        //    categories = tbCat.getCatalog();
        //    tbTech.setCatalog();
        //    technologies = tbTech.getCatalog();
        //}

        public NewReceptureController (tbReceptureController tb)
        {
            this.tb = tb;
            tbCat = new tbController("Categories");
            tbTech = new tbTechnologyController("Technology");
            tbCat.setCatalog();
            categories = tbCat.getCatalog();
            tbTech.setCatalog();
            technologies = tbTech.getCatalog();
        }

        public NewReceptureController() // верный
        {
            tbCat = new tbController("Categories");
            tbTech = new tbTechnologyController("Technology");
            tbCat.setCatalog();
            categories = tbCat.getCatalog();
            tbTech.setCatalog();
            technologies = tbTech.getCatalog(); 
        }

        public ReceptureStruct ReceptureInfo
        {
            set
            { 
                info = value;
                id_recepture = info.getId();
                if (info.getId() > 0)
                {
                    int[] ids = info.getIds();                
                    category = ids[0];
                    technology = ids[1];
                }
                else
                {
                    category = 0;
                    technology = 0;
                }
            }
           get { return info; }
        }

        public string[] Data => info.EditorData;

        public bool Indicator => info.getId() > 0;

        public bool setIndicator(int id) => id > 0;   
        
        public tbReceptureController TbMain
        {
            set { tb = value; }
            get => tb;
        }
        public tbController TbCat() => tbCat;
        public tbTechnologyController TbTech() => tbTech;

        public string [] getNames(string column)
        {
            return tb.dbReader($"select {column} from Recepture;").ToArray();
        }

        //public void ReloadFromDataBase(int id)
       /*{
            string[] columns = { "id_category", "id_technology" };
            string Query(string column) => 
                $"select {column} from {tb.getTable()} where id = {id}";
            
            List<string> data = tb.getData();
            category = int.Parse(tb.dbReader(Query(columns[0]))[0]);           
            technology = int.Parse(tb.dbReader(Query(columns[1]))[0]);                     
            // запустить setData()
            if (data != null)
            {
                indicator = true;
                name = data[0];
                source = data[1];
                author = data[2];
                URL = data[3];
                description = data[4];
            }
        }
        */
    }
}
