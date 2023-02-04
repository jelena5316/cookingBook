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
        NewReceptureController controller = new NewReceptureController();


        public NewRecepture(tbReceptureController controller)
        {
            InitializeComponent();
            this.tb = controller;
            id_recepture = tb.getId();
            this.category = tb.Category;
            this.technology = tb.Technology;
            tbCat = new tbController("Categories");
            tbCat.setCatalog();
            tbTech = new tbTechnologyController("Technology");
            tbTech.setCatalog();

            List<string> data = tb.getData();
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

        public NewRecepture(NewReceptureController controller)
        {
            InitializeComponent();
            this.controller = controller;
            tb = controller.TbMain();
            tbCat = controller.TbCat();
            tbTech = controller.TbTech();

            id_recepture = tb.Id;
            category = tb.Category;
            technology = tb.Technology;
            indicator = controller.Indicator; // choose mode

            if (indicator)
            {
                indicator = true;
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
            cmbCat.SelectedIndex = Class1.ChangeIndex(items, category);            

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
                //if var = "", then textbox name get unknown! Do must add labels for boxes!
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
        }

        private void button2_Click(object sender, EventArgs e) // set / write into db (Recepture)
        {
            //if (technology == 0 || chBox_technology.Checked)
            //{
            //    MessageBox.Show("Technology is equel null");
            //    return;
            //}
            //this.Text = "!!!";
            //return;

            int num = 0;
            if (!indicator) // вводим новый рецепт
            {
                if (string.IsNullOrEmpty(txbRecepture.Text)) return;
                if (string.IsNullOrEmpty(cmbCat.SelectedItem.ToString())) return;

                name = txbRecepture.Text;
                category = tbCat.getSelected();
                if (category == 0) category = 1;
                if (tb.IfRecordIs(name)) return;
                //пресекаем попытку ввести новую запись с занятым названием
                tb.InsertNewRecord(name, category);
                // пишем в базу данных название и категория нового рецепта, получаем номер
                id_recepture = tb.getId();
                //add new recepture and get it's id
            }
            else // редактируем существующую запись
            {
                if (string.IsNullOrEmpty(txbRecepture.Text)) return;

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

            if (string.IsNullOrEmpty(txbAuthor.Text)) return;
            if (string.IsNullOrEmpty(txbSource.Text)) return;

            if (string.IsNullOrEmpty(txbDescription.Text)) return;

            source = txbSource.Text;
            author = txbAuthor.Text;
            URL = txbURL.Text;
            description = txbDescription.Text;

            num = tb.UpdateReceptureOrCards("source", source, id_recepture);
            num = tb.UpdateReceptureOrCards("author", author, id_recepture);
            Report(num, "author");
            num = tb.UpdateReceptureOrCards("description", description, id_recepture);
            Report(num, "description");

            if (string.IsNullOrEmpty(txbURL.Text)) return;
            num = tb.UpdateReceptureOrCards("URL", URL, id_recepture);
            Report(num, "URL");
            //returns only then fields is not nullable!
            
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
}

    public class NewReceptureController
    {
        bool indicator = false;
        int id_recepture, category, technology;
        string name, recepture, source, author, URL, description;
        List<Item> categories, technologies;
        ReceptureStruct info;

        tbReceptureController tb;
        tbController tbCat;
        tbTechnologyController tbTech;

        public NewReceptureController()
        {
            tb = new tbReceptureController("Recepture");
            tbCat = new tbController("Categories");
            tbTech = new tbTechnologyController("Technology");
            tbCat.setCatalog();
            categories = tbCat.getCatalog();
            tbTech.setCatalog();
            technologies = tbTech.getCatalog();
        }

        public NewReceptureController (tbReceptureController tb)
        {
            this.tb = tb;
            tbCat = new tbController("Categories");
            tbTech = new tbTechnologyController("Technology");
            tbCat.setCatalog();
            categories = tbCat.getCatalog();
            tbTech.setCatalog();
            technologies = tbTech.getCatalog();

            List<string> data = tb.getData();
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

        public string[] Data => new string[] {name, source, author, URL, description };

        public bool Indicator => indicator;
        
        public tbReceptureController TbMain() => tb;
        public tbController TbCat() => tbCat;
        public tbTechnologyController TbTech() => tbTech;

        public string [] getNames(string column)
        {
            return tb.dbReader($"select {column} from Recepture;").ToArray();
        }
    }
}
