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
        int id_technology, id_cards = 0, cards = 0, output_cards_id = 0;
        //int id_chain;

        List <Item> catalog;
        TechnologyCardsController tb;
        dbController db;

        public TechnologyCards() // for quick accesing
        {
            InitializeComponent();
            this.id_technology = 0;
            tb = new TechnologyCardsController("Technology_card");
            db = new dbController();
            tb.setCatalog();
            catalog = fillCatalog();

            string t;
            btn_remove.Enabled = false;
            //btn_edit.Enabled = false;
            btn_insert.Text = "insert";
            cards = 0;
            btn_add.Enabled = false;
            t = this.Text.Substring(0, 37);
            this.Text = $"{t}edit";
        }

        public TechnologyCards(int id_technology)
        {
            InitializeComponent();
            this.id_technology = id_technology;
            tb = new TechnologyCardsController("Technology_card");
            tb.Selected = id_technology;
            db = new dbController();
            tb.setCatalog();
            catalog = fillCatalog();

            //set text of form one and buttons
            setTextAndButtons();

            //set field `cards`
            setCards();
        }

        private void setTextAndButtons()
        {
            //set text of form one and buttons
            string t;
            btn_remove.Enabled = false;
            //btn_edit.Enabled = false;
            btn_insert.Text = "insert";

            if (id_technology > 0)
            {
                t = tb.dbReader($"select name from Technology where id = {id_technology};")[0];
                this.Text += $" \"{t}\"";
            }
            else
            {
                btn_add.Enabled = false;
                t = this.Text.Substring(0, 37);
                this.Text = $"{t}edit";
            }
        }
        
        private void setCards()
        {
            //set field `cards`
            if (id_technology > 0)
            {
                string var = tb.Count($"select count(*) from Technology_chain where id = {id_technology}");
                if (int.TryParse(var, out cards))
                {
                    cards = int.Parse(var);
                }
                else cards = 0;
            }
            else
            {
                cards = 0;
            }
        }

        private void TechnologyCards_Load(object sender, EventArgs e)
        {
            //авозаполнения для поля "Наименование"
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            foreach (Item i in catalog) source.Add(i.name);
            textBox1.AutoCompleteCustomSource = source;
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            // на случай, если не передаётся выбранная технология! 
            if (id_technology == 0)
            {
                ComboBox cboTechnology = new ComboBox();
                cboTechnology.Location = new Point(378, 399);
                cboTechnology.Text = "Technology";
                this.Controls.Add(cboTechnology);
                //cboTechnology.DataSource = new string[] { "a", "B", "!" };

                //get data for combo from data base
                tbClass1 tbSub = new tbClass1("Technology");
                tbSub.setCatalog();                
                List<Item> items = tb.getCatalog(); // ??? tbSub.getCatalog()

                //put data into combo
                ComboBox c = cboTechnology;
                if (items.Count != 0)
                {
                    if (c.Items.Count > 0)
                        c.Items.Clear();
                    for (int index = 0; index < items.Count; index++)
                    {
                        c.Items.Add(items[index].name);
                    }
                }              
            }
        }

        private List<Item> fillCatalog() //список с технологиями
        {
            ComboBox c = cmbData;
            List<Item> items = tb.getCatalog();
            lblTest.Text = $"id {items.Count}";

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
           
            return items;
        }

        private List<string> CardsChain() // a test for method FillCards(int)
        {
            List<string> cards = tb.SeeOtherCards(id_technology);
            return cards;
        }

        private void FillCards(int id_tech) // ярлык с прочими картами
        {
           string text = "";           
           if(id_tech > 1)
           {
            List<string> chain = tb.SeeOtherCards(id_tech);
                foreach (string card in chain)
                {
                    text += $"{card} \n";               
                }
            }           
            lblCardsOfTech.Text += $"\n{text}";            
        }

        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {

           if (cmbData.SelectedIndex < tb.getCatalog().Count)
            {
                int temp = cmbData.SelectedIndex;
                tb.setSelected(temp);
                id_cards = tb.Selected;
            }

            if (id_cards < 1) { cmbData.Text = "no selection"; return; }

            //вывод в поля данных карты
            int data;
            string ind = tb.cardsCount(id_cards).ToString();

            data = tb.getById("name", id_cards);
            textBox1.Text = data.ToString();

            if (ind != "0")
            {
                data = tb.getById("description", id_cards);
                textBox2.Text = data.ToString();
            }

            data = tb.getById("technology", id_cards);
            textBox3.Text = data.ToString();

            output_cards_id = id_cards;
            //btn_edit.Enabled = true;
            btn_remove.Enabled = true;
            btn_update.Enabled = true;
        }

        //buttons
        private void btn_submit_Click(object sender, EventArgs e) // btn_insert
        {
            string name, description, technology, query, ind;

            //textBox1, textBox3
            if (string.IsNullOrEmpty(textBox1.Text)) return;
            query = $"select count(*) from Technology_card where name = '{textBox1.Text}';";

            ind = tb.Count(query); // не писать с одинаковым названием
            if (ind != "0")
            {
                textBox1.Text = "";
                textBox2.Text = "";
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
            tb.setCatalog();
            fillCatalog();
        }

        private void btn_new_Click(object sender, EventArgs e) // clear
        {
            output_cards_id = 0;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            btn_update.Enabled = false;
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {

            if (id_cards < 1) { return; }
            tb.RemoveItem();
            fillCatalog();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (output_cards_id > 0)
            {

                int ind = 0;
                ind += tb.UpdateReceptureOrCards("name", textBox1.Text, output_cards_id);
                ind+= tb.UpdateReceptureOrCards("description", textBox2.Text, output_cards_id);
                ind += tb.UpdateReceptureOrCards("technology", textBox3.Text, output_cards_id);
                output_cards_id = 0;
            } 
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int ind = tb.insertTechnology(id_technology, id_cards);
            MessageBox.Show($"Is inserted {ind} records");
        }
    }
}
