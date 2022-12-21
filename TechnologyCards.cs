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
        int id_technology=0, id_cards = 0, cards_count = 0, output_cards_id = 0;
    
        // * id_technology -- идентификатор технологии.
        // По ошибке использовала как идентификатор карты!
        //* output_cards_id нужен для правки записи в базе данных, запоминает идентификатор
        //выбранного элемента списка из текстового поля, обнуляется при очистке поля ("clear")

        List <Item> cards;
        tbTechnologyCardsController tb;
        TechnologyCardsController controller;


        public TechnologyCards(int card)
        {
            InitializeComponent();

            controller = new TechnologyCardsController(card);
            this.tb = controller.getTbController();
            tb.setCatalog();
            cards = Class1.FillCombo(tb.getCatalog(), ref cmbCards);
            tb.Selected = card; // не раньше! иначе изменится при записи в поле (строка выше)
            //не та таблица!
        }

        public TechnologyCards() // for quick accesing
        {
            InitializeComponent();
            this.id_technology = 0;
            tb = new tbTechnologyCardsController("Technology_card");
            //db = new dbController();
            tb.setCatalog();
            cards = Class1.FillCombo(tb.getCatalog(), ref cmbData);

            cards_count = 0;

            string t;
            t = this.Text.Substring(0, 37);
            this.Text = $"{t}edit";
            lblTest.Text = $"count {cards.Count}";
            btn_remove.Enabled = false;
            btn_insert.Text = "insert";
            btn_add.Enabled = false;
        }

        private void TechnologyCards_Load(object sender, EventArgs e)
        {
            //set field `cards` (from setCards()) ???
            if (id_technology > 0)
            {
                string var = tb.cardsCountInChain(id_technology);
                //string var = tb.Count("select count(*) from Technology_chain where id = {id_technology}");

                if (int.TryParse(var, out cards_count))
                {
                    cards_count = int.Parse(var);
                }
                else cards_count = 0;
            }
            else
            {
                cards_count = 0;
            }

            //set text of form one and buttons (from setTextAndButtons())
            string t;
            tbTechnologyController tbTechn = new tbTechnologyController("Technology");
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
            lblTest.Text = $"count {cards.Count}";
            btn_remove.Enabled = false;
            btn_insert.Text = "insert";

            int index = ChangeSelectedIndex(tb.Selected);

            // заполняем поля
            tb.setFields();           

            //вывод в поля данных карты (из полей объекта)            
            string ind = tb.cardsCount(id_cards);
            cmbCards.SelectedIndex = index;
            if (ind != "0")
                textBox2.Text = tb.Description;
            textBox3.Text = tb.Card;

            //автозаполнения для поля "Наименование"
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            foreach (Item i in cards) source.Add(i.name);
            cmbCards.AutoCompleteCustomSource = source;
            cmbCards.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbCards.AutoCompleteSource = AutoCompleteSource.CustomSource;            
        }

        public int Cards
        { 
            set
            { 
                id_cards = value;
                ChangeSelectedIndex(id_cards);
            }
        } // for quick accessing

        public int Technology { set { id_technology = value; } } //for quick accessing

        public void activdApplyButton() //for quick accessing
        {
            btn_add.Enabled = true;
        }

        private void cmbCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb.setSelected(cmbCards.SelectedIndex);
            id_cards = tb.Selected;
            tb.setFields(); //заполняем поля
       
            string ind = tb.cardsCount(id_cards);
            if (ind != "0")
                textBox2.Text = tb.Description;
            textBox3.Text = tb.Card;
            //вывод в поля данных карты (из полей объекта)  
        }

        private int ChangeSelectedIndex(int selected)
        {
            int index;
            for (index = 0; index < cards.Count; index++)
            {
                if (selected == cards[index].id)
                {
                    cmbCards.SelectedIndex = index;
                    break;
                }
            }
            return index;
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

        private void cmbData_SelectedIndexChanged(object sender, EventArgs e) //для карт, теперь cmbCards
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
            cmbCards.Text = tb.Name;
            if (ind != "0")
                textBox2.Text = tb.Description; 
            textBox3.Text = tb.Card;

            output_cards_id = id_cards;
            
            btn_remove.Enabled = true;
            btn_update.Enabled = true;
        }

        /****************************************************************************************
        * Buttons' and label5's click's handlers
        ******************************************************************************************/
        private void btn_submit_Click(object sender, EventArgs e) // btn_insert
        {
            string name, description, technology, query, ind;

            //cmbCards textBox3
            if (string.IsNullOrEmpty(cmbCards.Text)) return;

            //query = $"select count(*) from Technology_card where name = '{cmbCards.Text}';";
            //ind = tb.Count(query); 
            
            // не писать с одинаковым названием
            ind = tb.cardsCount(cmbCards.Text);
            if (ind != "0")
            {
                cmbCards.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                return; // temporery                   
            }

            if (string.IsNullOrEmpty(textBox3.Text)) return;
            if (cmbCards.Text.Length > 20) // проверка длины
            {
                //string t = cmbCards.Text;
                //t = t.Substring(0, 20);
                //cmbCards.Text = t;
            }

            name = cmbCards.Text;
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
            cards.Clear();
            cards = Class1.FillCombo(tb.getCatalog(), ref cmbData);
            lblTest.Text = $"count {cards.Count}";

            tb.Selected = int.Parse(ind);
            ChangeSelectedIndex(tb.Selected);
        }

        private void btn_new_Click(object sender, EventArgs e) // clear
        {
            output_cards_id = 0;
            //cmbCards.Clear();
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
            if (cmbCards.Items.Count == 0) return;
            if (id_cards < 1) { return; }
            tb.RemoveItem();
            tb.setCatalog();
            cards.Clear();
            cards = Class1.FillCombo(tb.getCatalog(), ref cmbData);
            lblTest.Text = $"count {cards.Count}";
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (output_cards_id > 0)
            {
                int ind = 0;
                ind += tb.UpdateReceptureOrCards("name", cmbCards.Text, output_cards_id);
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
