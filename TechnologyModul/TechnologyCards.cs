/*
 * to input a technologies cards
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            controller.Mode = SubmitMode.UPDATE;
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

        private string [] OutTechnologyCard()
        {
            return tb.AboutCurrent();
        }

        private string OutputTechnologyCardIntoForm(string[] info)
        {
            txbCards.Text = info[0];
            textBox2.Text = info[1];
            textBox3.Text = info[2];
            return info[0] + ": " + info[1] + "/n " + info[2];
        }


        private void cmbCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCards.SelectedIndex > -1)
            {
                tb.setSelected(cmbCards.SelectedIndex);
                id_cards = tb.Selected;
                tb.setCurrent(cmbCards.SelectedIndex);
                tb.setUsed();              
                string [] arr = OutTechnologyCard();
                OutputTechnologyCardIntoForm(arr);
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


        private void new_card_Click(object sender, EventArgs e)
        {
            id_cards = 0;
            txbCards.Clear();
            txbCards.Focus();
            cmbCards.Enabled = false;
            btn_remove.Enabled = false;
            new_card.Enabled = false;            
            btn_insert.Text = "Insert";
            controller.Mode = SubmitMode.INSERT;
        }

        private void btn_submit_Click(object sender, EventArgs e) // btn_submit
        {
            int id_temp = id_cards, new_index;
            string name, description, technology, count, report;

            //cmbCards textBox3           
            if (string.IsNullOrEmpty(txbCards.Text)) return;
            if (string.IsNullOrEmpty(textBox3.Text)) return;

            
            //name = LengthTest(txbCards.Text, 20);
            name = txbCards.Text;
            technology = textBox3.Text;
            description = textBox2.Text;

            

            switch ((int)controller.Mode)
            {
                case 0: // insert
                    report = controller.InsertNew(name, description, technology, id_temp);
                    MessageBox.Show(report);
                    cmbCards.Enabled = true;
                    btn_remove.Enabled = true;
                    new_card.Enabled = true;
                    btn_insert.Text = "Submit";
                    controller.Mode = SubmitMode.UPDATE;

                    id_temp = tb.Selected;
                    cards = FormFunction.FillCombo(tb.getCatalog(), cmbCards);
                    tb.Selected = id_temp;
                    cmbCards.SelectedIndex = FormFunction.ChangeIndex(cards, tb.Selected);
                    id_cards = tb.Selected;
                    break;
                case 1: // update
                    report = controller.UpdateExisted(name, description, technology, cmbCards.SelectedIndex);
                    MessageBox.Show(report);

                    id_temp = tb.Selected;
                    cards = FormFunction.FillCombo(tb.getCatalog(), cmbCards);
                    tb.Selected = id_temp;
                    cmbCards.SelectedIndex = FormFunction.ChangeIndex(cards, tb.Selected);
                    id_cards = tb.Selected;
                    break;
                case 2:
                    //bool ind = controller.Remove(cmbCards.SelectedIndex, out new_index);
                    break;
                default:
                    break;
            }
            
           

            //name = LengthTest(name, 20);

            //count = tb.cardsCount(name);
            //if (id_temp == 0 && count != "0")
            //{
            //   DialogResult rezult = MessageBox.Show
            //        ("Data base has {technology} technology cards with this name. Want you it update?\n" +
            //        "If you want update, than press 'Yes' and choose a technology card what you want update from list!\n" +
            //        "then press 'Submit'!\n" +
            //        "If you don't want, than press 'No' and type new name, then press 'submit'",
            //        "Quation",
            //        MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Question,
            //        MessageBoxDefaultButton.Button2);
            //    if (rezult == DialogResult.No)
            //    {
            //        txbCards.Focus();
            //        return;
            //    }
            //    else
            //    {
            //        if (cmbCards.Items.Count > 0)
            //            cmbCards.SelectedIndex = 0;
            //        return;
            //    }
            //}
            //report = controller.Submit(name, description, technology, id_temp);
            //MessageBox.Show(report);

            //id_temp = tb.Selected;
            //tb.setCatalog();           
            //cards = FormFunction.FillCombo(tb.getCatalog(), cmbCards);
            //tb.Selected = id_temp;
            //cmbCards.SelectedIndex = FormFunction.ChangeIndex(cards, tb.Selected);
            //name = "";     
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

        private void btn_remove_Click(object sender, EventArgs e) //delete
        {           
           
            bool rm;
            int index = 0, new_index, removed, ind = 0;            
            string message, using_count;

            if (cmbCards.Items.Count == 0)
                return;
            if (id_cards < 1)
                return;

            name = cmbCards.SelectedItem.ToString();
            index = cmbCards.SelectedIndex;
            
            rm = controller.Remove(index, out new_index); // is used or not, deleting if not
            if (!rm)
            {
                using_count = tb.getUsed();
                message = $"The technology's card is used in some ({using_count}) technologies chains.\nPlease, remove it from chains before deleting";
                MessageBox.Show(message);
                // in place of "some" was be a count of using case -- variable "ind" (int),
                // now "ind" store a deleted rows coun, in this case it is equel 0;
            }
            else
            {
                //removed = tb.RemoveItem();                
                 index = new_index;
                
               //rm = (removed > 0) ? true : false;

                if (rm && index > -1)
                {
                    // refreshing form
                    tb.setSelected(index);
                    int id_temp = tb.Selected;
                    tb.setCatalog();
                    //cards.Clear();
                    cards = FormFunction.FillCombo(tb.getCatalog(), cmbCards);
                    tb.Selected = id_temp;
                    //cmbCards.SelectedIndex = FormFunction.ChangeIndex(cards, tb.Selected);
                    cmbCards.SelectedIndex = new_index;

                    message = $"{name} is deleted";
                    MessageBox.Show(message);
                }
                //else
                //{
                //    if (!rm)
                //        MessageBox.Show("Nothing is deleted");
                //    else
                //        MessageBox.Show("All technologies are deleted");
                //}                               
            }
        }
    }
}
