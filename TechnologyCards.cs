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


        // * id_technology -- идентификатор технологии.
        // По ошибке использовала как идентификатор карты!

        //* output_cards_id нужен для правки записи в базе данных, запоминает идентификатор
        //выбранного элемента списка из текстового поля, обнудяется при очитски поля ("clear")

        List <Item> catalog;
        TechnologyCardsController tb;
        //dbController db; // заменить на TechnologyController?  

        public TechnologyCards() // for quick accesing
        {
            InitializeComponent();
            this.id_technology = 0;            
            tb = new TechnologyCardsController("Technology_card");
            //db = new dbController();
            tb.setCatalog();
            catalog = Class1.FillCombo(tb.getCatalog(), ref cmbData);

            cards = 0;

            string t;            
            t = this.Text.Substring(0, 37);
            this.Text = $"{t}edit";
            lblTest.Text = $"count {catalog.Count}";
            btn_remove.Enabled = false;           
            btn_insert.Text = "insert";            
            btn_add.Enabled = false; 
        }

        public TechnologyCards(int id_technology)
        {
            InitializeComponent();
            this.id_technology = id_technology;
            tb = new TechnologyCardsController("Technology_card");
            //db = new dbController();         
            tb.setCatalog();
            catalog = Class1.FillCombo(tb.getCatalog(), ref cmbData);
            //tb.Selected = id_technology; // не раньше! иначе изменится при записи в поле (строка выше)
            //не та таблица!

            //set field `cards`
            setCards();

            //set text of form one and buttons
            setTextAndButtons();  
        }

        private void setTextAndButtons()
        {
            //set text of form one and buttons
            string t;
            TechnologyController tbTechn = new TechnologyController("Technology");            
            if (id_technology > 0)
            {
                t = tbTechn.getById("name", id_technology);
                //t = tb.dbReader($"select name from Technology where id = {id_technology};")[0];                
                this.Text += $" \"{t}\"";
            }
            else
            {
                btn_add.Enabled = false;
                t = this.Text.Substring(0, 37);
                this.Text = $"{t}edit";
            }
            lblTest.Text = $"count {catalog.Count}";
            btn_remove.Enabled = false;
            btn_insert.Text = "insert";
        }
        
        private void setCards()
        {
            //set field `cards`
            if (id_technology > 0)
            {
                string var = tb.cardsCountInChain(id_technology);
                //string var = tb.Count("select count(*) from Technology_chain where id = {id_technology}");

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
            // выставить нужную технологическую карту
            //ChangeSelectedIndex(tb.Selected);
            int selected = tb.Selected;
            for (int index = 0; index < catalog.Count; index++)
            {
                if (selected == catalog[index].id)
                {
                    cmbData.SelectedIndex = index;
                    break;
                }
            }

            //автозаполнения для поля "Наименование"
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
                List<Item> items = tbSub.getCatalog(); //

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
        
        private void ChangeSelectedIndex(int selected)
        {
            for (int index = 0; index < catalog.Count; index++)
            {
                if (selected == catalog[index].id)
                {
                    cmbData.SelectedIndex = index;
                    break;
                }
            }
        }

        private List<string> CardsChain() // a test for method FillCards(int)
        {
            List<string> cards = tb.SeeOtherCards(id_technology);
            return cards;
        }

        private string FillCards(int id_tech) // ярлык с прочими картами
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
            //lblCardsOfTech.Text += $"\n{text}";
            return text;
        }

        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {
           string ind;

            if (tb.Selected < 1 && cmbData.Items.Count > 0)
            {
                cmbData.SelectedIndex = 1;
                tb.setSelected(1);
            }            
            
            if (cmbData.SelectedIndex < tb.getCatalog().Count)
            {
                int temp = cmbData.SelectedIndex;
                tb.setSelected(temp); // находим по номеру в списке номер карты в таблице
                id_cards = tb.Selected;
            }

            if (id_cards < 1)
            { 
                cmbData.Text = "no selection";
                return;
            }

            // заполняем поля
            tb.setFields(); 

            //tb.Name = tb.getName(cmbData.SelectedIndex);
            //tb.Description = tb.getById("description", tb.Selected);
            //tb.Card = tb.getById("technology", tb.Selected);

            //вывод в поля данных карты (из полей объекта)            
            ind = tb.cardsCount(id_cards);                        
            textBox1.Text = tb.Name;
            if (ind != "0")
                textBox2.Text = tb.Description; 
            textBox3.Text = tb.Card;

            output_cards_id = id_cards;
            
            btn_remove.Enabled = true;
            btn_update.Enabled = true;
        }

        //buttons
        private void btn_submit_Click(object sender, EventArgs e) // btn_insert
        {
            string name, description, technology, query, ind;

            //textBox1, textBox3
            if (string.IsNullOrEmpty(textBox1.Text)) return;

            //query = $"select count(*) from Technology_card where name = '{textBox1.Text}';";
            //ind = tb.Count(query); 
            
            // не писать с одинаковым названием
            ind = tb.cardsCount(textBox1.Text);
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
                query = tb.insertCards(name, description, technology);                    
            }
            else
            {
                query = tb.insertCards(name, technology);
            }
            ind = tb.Count(query); // проверка
            MessageBox.Show($"{ind} of recorded card");
            tb.setCatalog();
            catalog.Clear();
            catalog = Class1.FillCombo(tb.getCatalog(), ref cmbData);
            lblTest.Text = $"count {catalog.Count}";

            tb.Selected = int.Parse(ind);
            ChangeSelectedIndex(tb.Selected);
        }

        private void btn_new_Click(object sender, EventArgs e) // clear
        {
            output_cards_id = 0;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            btn_update.Enabled = false;
        }

        private void label5_Click(object sender, EventArgs e) // out to form2
        {
            PrintInfo();
        }

        private void PrintInfo()
        {
            // вывод в консоль
            string ind;
            Form2 frm = new Form2();
            frm.Show();
            frm.richTextBox1.Text = $"Id of technology is {id_technology} \n";

            if (cmbData.SelectedIndex < tb.getCatalog().Count)
            {
                id_cards = tb.Selected;
                frm.richTextBox1.Text += $"Id of card is {id_cards.ToString()} \n";
            }
            if (id_cards > 0)
            {
                //вывод в поля данных консоли                
                ind = tb.cardsCount(id_cards); // id
                tb.setFields();               
                frm.richTextBox1.Text += tb.Name + "\n";
                if (ind != "0")
                {
                    frm.richTextBox1.Text += tb.Description + "\n"; ;
                }
                frm.richTextBox1.Text += tb.Card;
            }
            else
                frm.richTextBox1.Text = "pusto!";
            frm.richTextBox1.Text += "\n********\n";

            frm.richTextBox1.Text += FillCards(2);           
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {

            if (id_cards < 1) { return; }
            tb.RemoveItem();
            tb.setCatalog();
            catalog.Clear();
            catalog = Class1.FillCombo(tb.getCatalog(), ref cmbData);
            lblTest.Text = $"count {catalog.Count}";
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (output_cards_id > 0)
            {
                int ind = 0;
                ind += tb.UpdateReceptureOrCards("name", textBox1.Text, output_cards_id);
                ind += tb.UpdateReceptureOrCards("description", textBox2.Text, output_cards_id);
                ind += tb.UpdateReceptureOrCards("technology", textBox3.Text, output_cards_id);
                output_cards_id = 0;
            } 
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int ind = tb.insertCardsIntoChain(id_technology, id_cards);
            MessageBox.Show($"Is inserted {ind} records");
        }
    }
}
