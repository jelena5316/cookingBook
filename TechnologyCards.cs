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
    public partial class TechnologyCards : Form
    {
        int id_technology, id_cards = 1, cards = 0;
        //int id_chain;
        tbClass1 tb;
        dbController db;

        public TechnologyCards(int id_technology)
        {
            InitializeComponent();
            this.id_technology = id_technology;
            tb = new tbClass1("Technology_card");
            db = new dbController();
            tb.setCatalog();
            fillCatalog();

            string t;
            btn_remove.Enabled = false;
            //btn_edit.Enabled = false;
            btn_submit.Text = "insert";

            if (id_technology > 0)
            {
                t = db.dbReader($"select name from Technology where id = {id_technology};")[0];                
                this.Text += $" \"{t}\"";                
                string var = db.Count($"select count(*) from Technology_chain where id = {id_technology}");
                cards = int.Parse(var);

                if (int.TryParse(var, out cards))
                {
                    cards = int.Parse(var);
                }
                else cards = 0;
            }
            else
            { 
                cards = 0;
                btn_add.Enabled = false;
                t = this.Text.Substring(0, 37);
                this.Text = $"{t}edit";
            }
           
        }

        //public TechnologyCards() // for quick accesing
        //{
        //    InitializeComponent();
        //    this.id_technology = 0;
        //    tbTech = new tbClass1("Technology");
        //    db = new dbController();
        //    tbTech.setCatalog();
        //    fillCatalog();
        //}

        private List<Item> fillCatalog() //список с технологиями
        {
            ComboBox c = cmbData;
            List<Item> items = tb.getCatalog();
            lblTest.Text += $" {items.Count}";

            //пишет в комбинированное поле
            if (items.Count != 0)
            {
                if (c.Items.Count > 0)
                    c.Items.Clear();
                for (int index = 0; index < items.Count; index++)
                {
                    c.Items.Add(items[index].name);
                }
            }
            //cmbData.Text = cmbData.Items[0].ToString();
            cmbData.Focus();
            FillCards(id_technology);
            return items;
        }

        //private List<string> CardsChain()
        //{
        //    List<string> cards =  tb.SeeOtherCards(id_technology);
        //    return cards;            
        //}

        private void FillCards(int id_tech) // ярлык с прочими картами
        {
           string text = "";           
           if(id_tech > 1)
           {
            List<string> chain = tb.SeeOtherCards(id_tech);
                foreach (string cards in chain)
                {
                    text += $"{cards} \n";               
                }
            }           
            lblCardsOfTech.Text += $"\n{text}";
        }

        private void btn_submit_Click(object sender, EventArgs e) // submit changes or new card
        {
            string name, description, technology, query, ind;

            //textBox1, textBox3
            if (string.IsNullOrEmpty(textBox1.Text)) return;
            query = $"select count(*) from Technology_card where name = '{textBox1.Text}';";
            ind = db.Count(query);
            if (ind != "0")
            {
                textBox1.Text = "";
                textBox3.Text = "";
                return; // temporery                   
            } 
            if (string.IsNullOrEmpty(textBox3.Text)) return;
            if (textBox1.Text.Length > 20) // проверка длины
            {
                //string t = textBox1.Text;
                //t = t.Substring(0, 20);
                //textBox1.Text = t;
            }  
            name = textBox1.Text; 
            technology = textBox3.Text;

            //textBox2
            if (!string.IsNullOrEmpty(textBox2.Text))
            {               
                description = textBox2.Text;               
                query = $"insert into Technology_card (name, description, technology) values ('{name}', '{description}', '{technology}'); select last_insert_rowid()";
            }
            else
            {
                query = $"insert into Technology_card (name, technology) values ('{name}', '{technology}'); select last_insert_rowid()";
            }
        ind = db.Count(query); // проверка                            
        }      

        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {

           if (cmbData.SelectedIndex < tb.getCatalog().Count)
            {
                int temp = cmbData.SelectedIndex;
                tb.setSelected(temp);
                id_cards = tb.Selected;
            }

            //вывод в поля данных карты
            List<string> data;
            string query;
            string ind;

            query = $"select name from Technology_card where id = {id_cards};";
            data = db.dbReader(query);
            textBox1.Text = data[0];

            ind = db.Count($"select count (*) from Technology_card where id = { id_cards} and description not null;");

            if (ind != "0")
            {
                query = $"select description from Technology_card where id = {id_cards};";
                data = db.dbReader(query);
                textBox2.Text = data[0];
            }

            query = $"select technology from Technology_card where id = {id_cards};";
            data = db.dbReader(query);
            textBox3.Text = data[0];

            //btn_edit.Enabled = true;
            btn_remove.Enabled = true;
            btn_submit.Text = "update";
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int ind = tb.insertTechnology(id_technology, id_cards);
            MessageBox.Show($"Is inserted {ind} records");
        }
    }
}
