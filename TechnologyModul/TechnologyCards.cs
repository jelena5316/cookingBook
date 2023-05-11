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
        int id_technology=0, id_cards = 0, cards_count = 0, output_cards_id = 0; // id_technology -- id of tehnology, not of card!
        int counter = 0;
        string name = "";
    

        List <Item> cards;
        tbTechnologyCardsController tb;
        TechnologyCardsController controller;


        public TechnologyCards (TechnologyCardsController cntrl)
        {
            InitializeComponent();
            controller = cntrl;
            this.tb = cntrl.getTbController();
            cards = cntrl.Cards;
            id_cards = tb.Selected;
            label5.Visible = false;
        }

        public TechnologyCards(int card)
                {
                    InitializeComponent();

                    controller = new TechnologyCardsController(card);
                    this.tb = controller.getTbController();
                    cards = controller.Cards;
                    id_cards = tb.Selected;
                }

        public TechnologyCards() // for quick accesing
        {
            //InitializeComponent();
            //this.id_technology = 0;
            //tb = new tbTechnologyCardsController("Technology_card");
            ////db = new dbController();
            //tb.setCatalog();
            //cards = Class1.FillCombo(tb.getCatalog(), cmbData);

            //cards_count = 0;

            //string t;
            //t = this.Text.Substring(0, 37);
            //this.Text = $"{t}edit";
            //lblTest.Text = $"count {cards.Count}";
            //btn_remove.Enabled = false;
            //btn_insert.Text = "insert";
            //btn_add.Enabled = false;
        }

        private void TechnologyCards_Load(object sender, EventArgs e)
        {
            // вывод в комбо-поле списка карт, установка в него выбранной карты
            // и возврат значения выбранной карты
            int temp = tb.Selected;
            Class1.FillCombo(cards, cmbCards);           
            cmbCards.SelectedIndex = Class1.ChangeIndex(cards, temp);

            //автозаполнения
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            foreach (Item i in cards) source.Add(i.name);
            cmbCards.AutoCompleteCustomSource = source;
            cmbCards.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbCards.AutoCompleteSource = AutoCompleteSource.CustomSource;           

            txbCards.AutoCompleteCustomSource = source;
            txbCards.AutoCompleteMode = AutoCompleteMode.Suggest;
            txbCards.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public int Cards
        { 
            set
            { 
                id_cards = value;
                cmbCards.SelectedIndex = ChangeSelectedIndex(id_cards);
            }
        } // for quick accessing

        public int Technology { set { id_technology = value; } } //for quick accessing

        public void activdApplyButton() //for quick accessing
        {
            //btn_add.Enabled = true;
        }

        private string OutTechnology()
        {
            tb.setFields(); //заполняем поля
            string[] arr = tb.getFields(); // читаем из полей
            txbCards.Text = arr[0];            
            textBox2.Text = arr[1];
            textBox3.Text = arr[2];
            counter++;            
            return arr[0] + ": " + arr[1] + "/n " + arr[2];
        }

        private void cmbCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCards.SelectedIndex > -1)
            {
                tb.setSelected(cmbCards.SelectedIndex);
                id_cards = tb.Selected;
                OutTechnology();
            }
            else
            {
                tb.Selected = 0;
                id_cards = tb.Selected;
            }   
        }

        private int ChangeSelectedIndex(int test)
        {
            int index, num=0;
            for (index = 0; index < cards.Count; index++)
            {
                if (test == cards[index].id)
                {
                    num = index;
                    break;
                }
            }
            return num;
        }

        private void cmbData_SelectedIndexChanged(object sender, EventArgs e) //для карт, теперь cmbCards
        {
           //string ind;

           // if (tb.Selected < 1 && cmbData.Items.Count > 0)
           // {
           //     cmbData.SelectedIndex = 1;
           //     tb.setSelected(1);
           // }

           // if (cmbData.SelectedIndex < tb.getCatalog().Count)
           // {
           //     int temp = cmbData.SelectedIndex;
           //     tb.setSelected(temp); // находим по номеру в списке номер карты в таблице
           //     id_cards = tb.Selected;
           // }

           // if (id_cards < 1)
           // {
           //     cmbData.Text = "no selection";
           //     return;
           // }

           // // заполняем поля
           // tb.setFields();

           // tb.Name = tb.getName(cmbData.SelectedIndex);
           // tb.Description = tb.getById("description", tb.Selected);
           // tb.Card = tb.getById("technology", tb.Selected);

           // //вывод в поля данных карты (из полей объекта)            
           // ind = tb.cardsCount(id_cards);                        
           // cmbCards.Text = tb.Name;
           // if (ind != "0")
           //     textBox2.Text = tb.Description; 
           // textBox3.Text = tb.Card;

           // output_cards_id = id_cards;
            
           // btn_remove.Enabled = true;
           // //btn_update.Enabled = true;
        }

        /****************************************************************************************
        * Buttons' and label5's click's handlers
        ******************************************************************************************/
        private void btn_submit_Click(object sender, EventArgs e) // btn_submit
        {
            int id_temp = id_cards;
            string name, description, technology, count, report;

            //cmbCards textBox3           
            if (string.IsNullOrEmpty(txbCards.Text)) return;
            if (string.IsNullOrEmpty(textBox3.Text)) return;

            name = txbCards.Text;
            technology = textBox3.Text;
            description = textBox2.Text;

            name = LengthTest(name, 20); // проверка длины

            count = tb.cardsCount(name);
            if (id_temp == 0 && count != "0")
            {
                //Form2 frm = new Form2();
                //string [] t = tb.dbReader($"select name from {tb.getTable()} where name = '{name}';").ToArray();
                //frm.Show();
                //frm.richTextBox1.Lines = t;

                DialogResult rezult = MessageBox.Show
                    ("Data base has {technology} technology cards with this name. Want you it update?\n" +
                    "If you want update, than press 'Yes' and choose a technology card what you want update from list!\n" +
                    "then press 'Submit'!\n" +
                    "If you don't want, than press 'No' and type new name, then press 'submit'",
                    "Quation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (rezult == DialogResult.No)
                {
                    txbCards.Focus();
                    return;
                }
                else
                {
                    if (cmbCards.Items.Count > 0)
                        cmbCards.SelectedIndex = 0;
                    return;
                }
            }
            report = controller.Submit(name, description, technology, id_temp);
            MessageBox.Show(report);

            id_temp = tb.Selected;
            tb.setCatalog();           
            cards = Class1.FillCombo(tb.getCatalog(), cmbCards);
            tb.Selected = id_temp;
            cmbCards.SelectedIndex = Class1.ChangeIndex(cards, tb.Selected);
            name = "";
            //cmbCards.SelectedIndex = ChangeSelectedIndex(tb.Selected);
            
            //OutTechnology();    
            //lblTest.Text = $"count {cards.Count}";        
        }

        private string LengthTest(string text, int length)
        {
            if (text.Length > length)
                return text.Substring(0, length);
            else
                return text;
        }

        private int ChangeIndex(List<Item> items, int test)  // 'load', 'submit', 'delete'        
        {
            int index = -1, temp_id = test;
            if (items.Count != 0)
            {
                //comboBox2.SelectedIndex = 0;
                if (temp_id > 0)
                {
                    for (index = 0; index < items.Count; index++)
                    {
                        if (items[index].id == temp_id)
                        {
                            //comboBox2.SelectedIndex = index;                          
                            break;
                        }
                    }
                }
                return index;
            }
            else return index;
        }

        private void btn_new_Click(object sender, EventArgs e) // clear
        {
            txbCards.Clear();
            textBox2.Clear();
            textBox3.Clear();
            id_cards = 0;
        }

        private void label5_Click(object sender, EventArgs e) // out to form2
        {
            string [] arr = controller.getFullInfo();
            PrintInfo(arr);
        }

        private void PrintInfo(string [] arr)
        {
            Print frm = new Print();
            frm.Show();
            frm.richTextBox1.Lines = arr;
        }

        private void PrintInfo()
        {
            // вывод в консоль
            string ind;
            Print frm = new Print();
            frm.Show();
            frm.richTextBox1.Text = $"Id of technology is {id_technology} \n";

            //if (cmbData.SelectedIndex < tb.getCatalog().Count)
            //{
            //    id_cards = tb.Selected;
            //    frm.richTextBox1.Text += $"Id of card is {id_cards.ToString()} \n";
            //}
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private string FillCards(int id_tech) // ярлык с прочими картами
        {
            string text = "";
            if (id_tech > 1)
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

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (cmbCards.Items.Count == 0) return;
            if (id_cards < 1) { return; }

           
            bool rm;
            int index = 0, removed;
            string message;
            name = cmbCards.SelectedItem.ToString();

            //is used or not
            tbChainController chains = controller.Chains.tbChainController;
            int ind = chains.TechnologiesWithSelectedCardCount(id_cards);
            if (ind > 0)
            {
                message = $"The technology's card is used in {ind} Receptures.\nPlease, remove it before deleting";
                MessageBox.Show(message);            
               
            }
            else
            {
                removed =  tb.RemoveItem();
                index = cmbCards.SelectedIndex;
                if(ind > 0)
                {
                    if (index == tb.getCatalog().Count - 1) index--;
                    if (index == 0) index++;
                }
                
               rm = (removed > 0) ? true : false;

                if (rm && index > -1)
                {
                    //обновляем форму
                    tb.setSelected(index);
                    int id_temp = tb.Selected;
                    tb.setCatalog();
                    cards.Clear();
                    cards = Class1.FillCombo(tb.getCatalog(), cmbCards);
                    tb.Selected = id_temp;
                    cmbCards.SelectedIndex = Class1.ChangeIndex(cards, tb.Selected);
                    
                    //выводим сообщение                    
                    MessageBox.Show($"{name} is deleted");
                }
                else
                {
                    if (!rm)
                        MessageBox.Show("Nothing is deleted");
                    else
                        MessageBox.Show("All technologies are deleted");
                }                               
            }
        }  

        private void btn_add_Click(object sender, EventArgs e)
        {
            int ind = tb.insertCardsIntoChain(id_technology, id_cards);
            MessageBox.Show($"Is inserted {ind} records");
        }
    }
}
