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
    public partial class Technology : Form
    {
        dbController db;
        tbClass1 tb = new tbClass1("Technology");
        tbClass1 tbRec = new tbClass1("Recepture");
        int id_technology, id_recepture, id, selected_tech, selected_rec;

        public Technology()
        {
            InitializeComponent();
            db = new dbController();
            id_technology = 0;
            id = 0;
            this.Text += ": no technology";
        }

        public Technology(int recepture)
        {
            InitializeComponent();
            db = new dbController();
            
            id_recepture = recepture;
            tbRec.setCatalog();
            fillCatalogRec();
            tbRec.Selected = recepture;           
            selected_rec = recepture;
            
            id_technology = 0;
            tb.setCatalog();            
            fillCatalog();
            selected_tech = 1;
            id = 1;

            this.Text += $": recepture {id_recepture}, no technology ({id_technology})";
            OutTechnology();
        }

        public Technology(int recepture, int technology)
        {
            InitializeComponent();

            db = new dbController();

            id_recepture = recepture;
            tbRec.setCatalog();
            fillCatalogRec();
            tbRec.Selected = recepture;            
            selected_rec = recepture;

            id_technology = technology;
            tb.setCatalog();
            fillCatalog();
            tb.Selected = technology;            
            id = technology;

            this.Text += $": recepture {id_recepture}, technology {id_technology}";
            OutTechnology();
        }

        private string OutTechnology() //into textbox
        {
           string query = $"select name, description from Technology where id = {id};";
            string technology = "";
            technology = db.dbReadTechnology(query)[0];
            txbOutput.Text = technology;
            return technology;
        }

        private List<Item> fillCatalog() //  technology
        {
            List<Item> items = tb.getCatalog(); // читает два поля, наименование и номер

            //пишет в комбинированное поле
            if (items.Count != 0)
            {
                int tech_of_recipe = -1, index;

                if (comboBox2.Items.Count > 0)
                    comboBox2.Items.Clear();

                for (index = 0; index < items.Count; index++)
                {
                    if (items[index].id == id_technology)
                    {
                        tech_of_recipe = index;
                        //label6.Text += " tech "+tech_of_recipe;
                    }
                    comboBox2.Items.Add(items[index].name);
                }
                index--;
               
                if (id_technology == 0)
                {
                    comboBox2.Text = comboBox2.Items[0].ToString();                    
                }
                else
                {
                    comboBox2.SelectedIndex = index;                                       
                    comboBox2.Text = comboBox2.Items[index].ToString();
                    if (tech_of_recipe > -1)
                    {
                        comboBox2.SelectedIndex = tech_of_recipe;
                        comboBox2.Text = comboBox2.Items[tech_of_recipe].ToString();
                    }
                }  
            }
            else comboBox2.Text = "empty";
            comboBox2.Focus();
            return items;
        }

        private List<Item> fillCatalogRec() // recepture
        {
            
            List<Item> items = tbRec.getCatalog(); // читает два поля, наименование и номер
            //пишет в комбинированное поле
            if (items.Count != 0)
            {
                int recepture = -1, index;

                if (comboBox1.Items.Count > 0)
                    comboBox1.Items.Clear();
                for (index = 0; index < items.Count; index++)
                {
                    comboBox1.Items.Add(items[index].name);
                    if (items[index].id == id_recepture)
                    {
                        recepture = index;
                    }
                }
                index--;         
                comboBox1.Text = comboBox1.Items[index].ToString();
                if (recepture > -1)
                {
                    comboBox1.SelectedIndex = recepture;
                    comboBox1.Text = comboBox1.Items[recepture].ToString();
                }
            }
            else comboBox1.Text = "empty";
            //comboBox1.Focus();
            return items;
        }


        private void button1_Click(object sender, EventArgs e) // submit new
        {
            string name, description, query, technology;

            if (string.IsNullOrEmpty(textBox1.Text)) return;
            if (string.IsNullOrEmpty(textBox3.Text)) return;

            name = textBox1.Text;
            description = textBox3.Text;
            query = $"select count(*) from Technology where name = '{name}';";
            technology = db.Count(query);

            if (technology != "0")
            {
                // if dialog rezult is ok, then update records
                query = $"select id from Technology where name = '{name}';";
                id_technology = int.Parse(db.dbReader(query)[0]);
                int ind = tb.UpdateReceptureOrCards("description", description, id_technology);               
            }
            else
            {
                query = $"insert into Technology (name, description) values ('{name}', '{description}'); select last_insert_rowid()";
                technology = db.Count(query);
                if (int.TryParse(technology, out id_technology))
                {
                    id = int.Parse(technology);
                }
                else return;
            }
            OutTechnology();
            //код, легший в основу функции выше
            //query = $"select name, description from Technology where id = {id_technology};";
            //technology = "";
            //technology = db.dbReadTechnology(query)[0];
            //txbOutput.Text = technology;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dbController db = new dbController();
            db.Edit("update Recepture set id_technology = null where id = 2;");
            MessageBox.Show("У рецептуры больше нет технологии");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TechnologyCards frm = new TechnologyCards(selected_tech);
            frm.Show();

        }

        private void btn_technology_Click(object sender, EventArgs e) // apply technology to recepture
        {
            int ind;
            string query;
            //id_technology = tb.getSelected();           

            // проверка, есть ли уже у рецептуры технология   
            query = $"select count(*) from Recepture where id = '{selected_rec}' and id_technology = '{selected_tech}';";
            string indStr = db.Count(query);
            if (indStr != "0") { this.Text += "*"; return; }

            //замена с отчетом в тексте окна
            //query = $"update Recepture set id_technology = {id_technology} where id = {id_recepture};";
            query =  $"update Recepture set id_technology = {selected_tech} where id = {selected_rec};";
            ind = db.Edit(query);

            if (ind != 0)
            {
                //string t = $"Technology {id_recepture} {id_technology}";
                string t = $"Technology {id_recepture} {selected_tech}";
                this.Text = t;
            }

            //возврат значений
            tb.Selected = id_technology;
            selected_tech = tb.Selected;
            tbRec.Selected = id_recepture;
            selected_rec = tbRec.Selected;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // rec
        {
            int index = comboBox1.SelectedIndex;
            int selected = tbRec.setSelected(index);
            //id_recepture = selected;
            selected_rec = selected;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // tech
        {
            {
                int index = comboBox2.SelectedIndex;
                int selected = tb.setSelected(index);
                //id_technology = selected; //заменить на строку ниже
                selected_tech = selected;
                label5.Text = selected.ToString();

                // output in textbox
                id = selected;
                OutTechnology();
            }
        }





            private void Technology_Load(object sender, EventArgs e)
        {
           // OutTechnology();
            //private string OutTechnology()
            //{
            //    string query = $"select name, description from Technology where id = {id};";
            //    string technology = "";
            //    technology = db.dbReadTechnology(query)[0];
            //    txbOutput.Text = technology;
            //    return technology;
            //}
        }
    }
}
