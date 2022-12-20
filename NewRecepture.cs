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

        ReceptureController tb;
        tbClass1 tbCat;

        //public NewRecepture()
        //{
        //    InitializeComponent();
        //    id_recepture = 0;
        //    SetForm();
        //}

        //public NewRecepture(int id, int category)
        //{
        //    InitializeComponent();
        //    id_recepture = id;
        //    this.category = category;

        //    /* this.controller = controller;
        //        id_recepture = controller.getIdRec();
        //        this.category = controller.getCategory();*/

        //    SetForm();
        //}

        public NewRecepture (ReceptureController controller)
        {
            InitializeComponent();
            this.tb = controller;
            id_recepture = tb.getId();
            this.category = tb.getCategory();
            this.technology = tb.getTechnology();
            tbCat = new tbClass1("Categories");

            List<string> data = tb.getData();
            if (data!=null)
            {
                indicator = true;
                name = data[0];
                source = data[1];
                author = data[2];
                URL = data[3];
                description = data[4];              
            }
        }

        private void SetForm()
        {
            //tbCat = new tbClass1("Categories");
            tbCat.setCatalog(); // we can check catalog on null
            FillCatalog(tbCat.getCatalog());

            tbTechnologyController tbTech = new tbTechnologyController("Technology");
            tbTech.setCatalog();
            List<Item> technologies = tbTech.getCatalog();
            Class1.FillCombo(technologies, ref cmbTech);
            if(technology > 0)
            {
                cmbTech.Text = "the used technology";
                //TechnologyController controller = new TechnologyController(technology);
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
        private void FillCatalog(List<Item> cat)
        {
            //fill catalog
            if (cat.Count != 0)
            {
                if (cmbCat.Items.Count > 0)
                {
                    cmbCat.Items.Clear();
                }
                for (int index = 0; index < cat.Count; index++)
                {
                    cmbCat.Items.Add(cat[index].name);
                }
            }
            if (cmbCat.Items.Count > 0)
            {
                cmbCat.SelectedIndex = 0;
                cmbCat.Text = cmbCat.SelectedItem.ToString();
                //cmbCat.Text = cmbCat.Items[0].ToString();
            }

            //index, name (edit mode)
            if (indicator)
            {
                int index;
                Item item = new Item();

                item = cat.Find(it => it.id == category);
                index = cat.FindIndex(it => it.id == category);

                cmbCat.Text = item.name;
                cmbCat.SelectedIndex = index; // setSelected(int) внутри

                //test
                this.Text += " " + index + " " + tbCat.getSelected();
            }
        }

        private void label1_Click_1(object sender, EventArgs e) // ввод рецепта
        {
            if (id_recepture == 0) return;
            InsertAmounts frm = new InsertAmounts(id_recepture);
            frm.ShowDialog();
            //this.Dispose();
        }

        private void cmbTech_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbTechnologyController tbTech = new tbTechnologyController("Technology");
            tbTech.setCatalog();
            //List<Item> technologies = tbTech.getCatalog();
            //string name = technologies[cmbTech.SelectedIndex].name;
            int selected = tbTech.setSelected(cmbTech.SelectedIndex);            
            technology = tbTech.Selected;
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
         
        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
           tbCat.setSelected(cmbCat.SelectedIndex);
        }

        private void button2_Click(object sender, EventArgs e) // set / write into db (Recepture)
        {
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
                if (id_recepture > 0)
                {                    
                    label1.Enabled = true;
                }
                //add new recepture and get it's id
            }
            else // редактируем существуещую запись
            {
                if (string.IsNullOrEmpty(txbRecepture.Text)) return;

                name = txbRecepture.Text;
                category = tbCat.getSelected();
              
                num = tb.UpdateReceptureOrCards("name", name, id_recepture);
                num = tb.UpdateReceptureOrCards("id_category", category.ToString(), id_recepture);
                num = tb.UpdateReceptureOrCards("id_technology", technology.ToString(), id_recepture);                
                //num = tb.Edit($"update Recepture set id_technology = {technology} where id = {id_recepture};");
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
        }
  
        private void Report (int num, string variable) //developer mode
        {
            if (num == 0) this.Text += $" {variable} not writted";
            else this.Text += $" {variable} is writted";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //if (id_recepture == 0) return;
            //InsertAmounts frm = new InsertAmounts(id_recepture);
            //frm.ShowDialog();
        }

        private void NewRecepture_Load(object sender, EventArgs e)
        {
            int ind;
            AutoCompleteStringCollection source; // recepture in commented code
            dbController db = new dbController();

            source = new AutoCompleteStringCollection();
            ind = TextBoxAutocomplet(db, "name", source, ref txbRecepture);
            source.Clear();
            ind = TextBoxAutocomplet(db, "source", source, ref txbSource);
            source.Clear();
            ind = TextBoxAutocomplet(db, "URL", source, ref txbURL);
            source.Clear();
            ind = TextBoxAutocomplet(db, "author", source, ref txbAuthor);
            source.Clear();
            SetForm();

            //int length;       
            //length = int.Parse(db.Count("select count (name) from Recepture;"));
            //if (length != 0)
            //{
            //    string [] receptures = new string[length];          
            //    receptures = db.dbReader("select name from Recepture;").ToArray();
            //    recepture = new AutoCompleteStringCollection();
            //    recepture.AddRange(receptures);            
            //    txbRecepture.AutoCompleteCustomSource = recepture;
            //    txbRecepture.AutoCompleteMode = AutoCompleteMode.Suggest;
            //    txbRecepture.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //}

        }

        private int TextBoxAutocomplet(dbController db, string column, AutoCompleteStringCollection source, ref TextBox box )
        {
            int length;
            if ((length = int.Parse(db.Count("select count (name) from Recepture;"))) == 0)
            {   
                //оставить здесь, в конроллер вынести проверку
                source = new AutoCompleteStringCollection();
                source.Add("empty");
                return 0;
            }
            else
            {
                //можно вынести в контроллер
                string[] receptures = new string[length];
                receptures = db.dbReader($"select {column} from Recepture;").ToArray();

                //оставить здесь
                source = new AutoCompleteStringCollection();
                source.AddRange(receptures);
                box.AutoCompleteCustomSource = source;
                box.AutoCompleteMode = AutoCompleteMode.Suggest;
                box.AutoCompleteSource = AutoCompleteSource.CustomSource;
                return length;
            }
           
        }
    }
}
