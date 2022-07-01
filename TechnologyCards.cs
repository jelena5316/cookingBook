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
        int id_technology, id_cards=1, id_chain;
        tbClass1 tb;
        dbController db = new dbController();

        public TechnologyCards(int id_technology)
        {
            InitializeComponent();
            this.id_technology = id_technology;
            tb = new tbClass1("Technology_card");
            tb.setCatalog();
            fillCatalog();           
        }

        private List<Item> fillCatalog()
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
            FillCards();
            return items;
        }

        private List<string> CardsChain()
        {
            List<string> cards =  tb.SeeOtherCards(id_technology);
            return cards;            
        }

        private void FillCards()
        {
            string text;
            text = "";
           
            List<string> chain = CardsChain();
            foreach (string cards in chain)
            {
                text += $"{cards} \n";               
            }
            lblCardsOfTech.Text += $"\n{text}";
        }

        private void btn_submit_Click(object sender, EventArgs e) // submit changes or new card
        {
            //int id = 0;
            string name, description, technology, query, ind;

            if (string.IsNullOrEmpty(textBox1.Text)) return;
            if (string.IsNullOrEmpty(textBox3.Text)) return;
            name = textBox1.Text;               
            technology = textBox3.Text;

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                query = $"select count(*) from Technology_card where name = '{name}';";
                ind = db.Count(query);
                if (ind != "0")
                {
                    return; // temporery
                    //int last = (int)name[name.Length - 1];
                    //if (last > 47 || last < 57)
                    //{
                    //    last++;
                    //    int length = name.Length - 1;
                    //    if (last > 10) length--;
                    //    name.Substring(0, length);
                    //    name += last.ToString();
        // доделать на случай, если уже двухзначный номер,
        // вводить перед номером, например, знак "_" (95)
                    //} 
                } 
                query = $"insert into Technology_card (name, technology) values ('{name}', '{technology}'); select last_insert_rowid()";
            }
            else
            {   
                //if (textBox2.Text.Length > 21)
            //    {
            //        string t = textBox2.Text;
            //        t = t.Substring(0, 20);
            //        textBox2.Text = t;
            //    }
                
                name = textBox1.Text;
                description = textBox2.Text;
                technology = textBox3.Text;

                query = $"select count(*) from Technology_card where name = '{name}';";
                ind = db.Count(query);
                if (ind != "0") return;

                query = $"insert into Technology_card (name, description, technology) values ('{name}', '{description}', '{technology}'); select last_insert_rowid()";
               
            }
                ind = db.Count(query); // проверка

                //if (int.TryParse(technology, out id_technology))
                //{
                //    id = int.Parse(technology);
                //}
                //else return;                
        }

        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox c = cmbData;
            if (c.SelectedIndex < tb.getCatalog().Count)
            {
                int temp = c.SelectedIndex;
                tb.setSelected(temp);
                id_cards = tb.Selected;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int ind = tb.insertTechnology(id_technology, id_cards);
            MessageBox.Show($"Is inserted {ind} records");
        }
    }
}
