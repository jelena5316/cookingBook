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
        ChainsController controller;
        tbTechnologyCardsController cards;
        tbTechnologyController techn;
        tbChainController chains;

        public Chains (ChainsController controller)
        {
            InitializeComponent();
            this.controller = controller;
            cards = controller.tbCardsController;
            techn = controller.tbTechController;
            chains = controller.tbChainController;
        }

        private void Chains_Load(object sender, EventArgs e)
        {
            int id = 0, k;
            List<Item> items;
            
            items = techn.getCatalog();
            FormFunction.FillCombo(items, cmbTechn);
            if (controller.Technology > 0)            
                cmbTechn.SelectedIndex = FormFunction.ChangeIndex(items, controller.Technology);           
       
            items = cards.getCatalog();
            FormFunction.FillCombo(items, cmbData);
            id = controller.Card; 
            if (id !=0)
                cmbData.SelectedIndex = FormFunction.ChangeIndex(items, id);            
        }        
       
        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbData.SelectedIndex;   
            cards.setSelected(index);
            setListBoxTechnology();
        }

        private void setListBoxTechnology()
        {
            int index = cmbData.SelectedIndex, selected_cards = 0;
            string description;
            List<string> technologies;           
            selected_cards = cards.Selected;
            listBox_tech.Items.Clear();
            technologies = chains.getNames(chains.TechnologiesWithSelectedCard(selected_cards));
            if (technologies != null)
            {
                for (int k = 0; k < technologies.Count; k++)
                    listBox_tech.Items.Add(technologies[k]); // а есть ли проверка на уникальность имени? 
            }
            if (listBox_tech.Items.Count != 0) listBox_tech.SelectedIndex = 0;
            else listBox_tech.Text = "";
            description = cards.getById("description", selected_cards);
            lblInfo.Text = chains.TechnologiesWithSelectedCardCount(selected_cards).ToString();
            lblCards.Text = description;           
        }

        private void cmbTechn_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbTechn.SelectedIndex;                   
            techn.setSelected(index);
            setListBoxCards();
        }

        private void setListBoxCards()
        {
            string description;
            List<string> names;            
            listBox_cards.Items.Clear();            
            names = controller.Names(chains.CardsInTechnology(techn.Selected));
            for (int k = 0; k < names.Count; k++)
                listBox_cards.Items.Add(names[k]); // а есть ли проверка на уникальность имени?           
            if (listBox_cards.Items.Count != 0) listBox_cards.SelectedIndex = 0;
            else listBox_cards.Text = "";

            description = techn.getDescription();
            if (description.Length > 50)
            {
                string t = description.Substring(0, 50);
                description = t;
            }
            lblTechn.Text = description;
            lblInfo2.Text = chains.CardsInTechnologyCount(techn.Selected).ToString();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int ind = 0;
            string message = "";
            ind = controller.ApplyToChain();
            message = (ind == -1) ? "Is inserted no records" : $"Is inserted {ind} records";
            MessageBox.Show(message);
            setListBoxCards();
            setListBoxTechnology();
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            int ind = 0;
            ind = controller.RemoveFromChain();
            MessageBox.Show($"Is deleted {ind} records");
            setListBoxCards();
            setListBoxTechnology();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            TechnologyCardsController cntrl = new TechnologyCardsController(controller);
            TechnologyCards frm = new TechnologyCards(cntrl);
            frm.Show();
        }
    }
}
