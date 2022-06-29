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
