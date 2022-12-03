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
    public partial class Chains : Form
    {
        ChainController controller;
        
        public Chains()
        {
            InitializeComponent();
            controller = new ChainController();
        }

        private void Chains_Load(object sender, EventArgs e)
        {
            List<Item> items;
            TechnologyCardsController cards = controller.tbCardsController;
            TechnologyController techn = controller.tbTechController;

            items = cards.getCatalog();
            Class1.FillCombo(items, ref cmbData);
            items = techn.getCatalog();
            Class1.FillCombo(items, ref cmbTechn);
        }

        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbData.SelectedIndex, selected_cards = 0;
            string description;
            List<string> technologies, names;
            TechnologyCardsController cards = controller.tbCardsController;
            TechnologyController techn = controller.tbTechController;
            tbChainController chains = controller.tbChainController;            

            cards.setSelected(index);
            selected_cards = cards.Selected;
            
            cmbUsedIn.Items.Clear();
            technologies = chains.TechnologiesWithSelectedCard(selected_cards);
            for (int k = 0; k< technologies.Count; k++)
            {
                names = techn.dbReader($"select name from {techn.getTable()} where id = {technologies[k]};");
                cmbUsedIn.Items.Add(names[0]); // а есть ли проверка на уникальность имени?
            }
            if (cmbUsedIn.Items.Count != 0) cmbUsedIn.SelectedIndex = 0;
            else cmbUsedIn.Text = "";

            description = cards.dbReader($"select description from {cards.getTable()} where id = {selected_cards}")[0];
 
            lblInfo.Text = chains.TechnologiesWithSelectedCardCount(selected_cards).ToString();
            lblCards.Text = description;
            toolStripStatusLabel1.Text = "id_cards " + selected_cards;
        }

        private void cmbTechn_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbTechn.SelectedIndex, selected_techn = 0;
            string description;
            List<string> cards_id, names;
            TechnologyCardsController cards = controller.tbCardsController;
            TechnologyController techn = controller.tbTechController;
            tbChainController chains = controller.tbChainController;

            techn.setSelected(index);
            selected_techn = techn.Selected;

            cmbHasCards.Items.Clear();
            cards_id = chains.CardsInTechnology(selected_techn);
            for (int k = 0; k < cards_id.Count; k++)
            {
                names = cards.dbReader($"select name from {cards.getTable()} where id = {cards_id[k]};");
                cmbHasCards.Items.Add(names[0]); // а есть ли проверка на уникальность имени?
            }
            if (cmbHasCards.Items.Count != 0) cmbHasCards.SelectedIndex = 0;
            else cmbHasCards.Text = "";

            description = techn.dbReader($"select description from {techn.getTable()} where id = {selected_techn}")[0];
            if (description.Length > 50)
            {
                string t = description.Substring(0, 50);
                description = t;
            }

            lblTechn.Text = description;
            lblInfo2.Text = chains.CardsInTechnologyCount(selected_techn).ToString();
            toolStripStatusLabel2.Text = "id_techns " + selected_techn;
        }

        private void cmbUsedIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string text = cmbUsedIn.SelectedItem.ToString();
            //cmbUsedIn.Text = text;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int ind = 0;
            ind = controller.CreateChain();
            MessageBox.Show($"Is inserted {ind} records");
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            int ind = 0;
            ind = controller.RemoveFromChain();
            MessageBox.Show($"Is deleted {ind} records");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            //
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //
        }
    }
}
