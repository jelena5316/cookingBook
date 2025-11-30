/*
 * to input a technologies cards
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MajPAbGr_project
{
    public partial class TechnologyCards : Form
    {
        int id_cards = 0;
        string name = "";
    

        List <Item> cards;
        tbCardsController tb;
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

        private void TechnologyCards_Load(object sender, EventArgs e)
        {
            int temp = tb.Selected;
            FormFunction.FillCombo(cards, cmbCards);           
            cmbCards.SelectedIndex = FormFunction.ChangeIndex(cards, temp);
            
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            foreach (Item i in cards) source.Add(i.name);
            cmbCards.AutoCompleteCustomSource = source;
            cmbCards.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbCards.AutoCompleteSource = AutoCompleteSource.CustomSource;           

            txbCards.AutoCompleteCustomSource = source;
            txbCards.AutoCompleteMode = AutoCompleteMode.Suggest;
            txbCards.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private string OutTechnology()
        {
            tb.setFields(); // to fill fields
            string[] arr = tb.getFields(); // to read field
            txbCards.Text = arr[0];            
            textBox2.Text = arr[1];
            textBox3.Text = arr[2];                     
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

        /*
        * Buttons' and label5's click's handlers
        */
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

            name = LengthTest(name, 20);

            count = tb.cardsCount(name);
            if (id_temp == 0 && count != "0")
            {
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
            cards = FormFunction.FillCombo(tb.getCatalog(), cmbCards);
            tb.Selected = id_temp;
            cmbCards.SelectedIndex = FormFunction.ChangeIndex(cards, tb.Selected);
            name = "";     
        }

        private string LengthTest(string text, int length)
        {
            if (text.Length > length)
                return text.Substring(0, length);
            else
                return text;
        }

        private void btn_new_Click(object sender, EventArgs e) // clear
        {
            txbCards.Clear();
            textBox2.Clear();
            textBox3.Clear();
            id_cards = 0;
        }

        private void label5_Click(object sender, EventArgs e) // out to 'Print to file'
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
                    // refreshing form
                    tb.setSelected(index);
                    int id_temp = tb.Selected;
                    tb.setCatalog();
                    cards.Clear();
                    cards = FormFunction.FillCombo(tb.getCatalog(), cmbCards);
                    tb.Selected = id_temp;
                    cmbCards.SelectedIndex = FormFunction.ChangeIndex(cards, tb.Selected);
                    
                    //outputting message                  
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
    }
}
