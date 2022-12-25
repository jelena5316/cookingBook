﻿using System;
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

        public Chains (ref ChainController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void Chains_Load(object sender, EventArgs e)
        {
            int id, k;
            List<Item> items;
            tbTechnologyCardsController cards = controller.tbCardsController;
            tbTechnologyController techn = controller.tbTechController;

            items = techn.getCatalog();
            Class1.FillCombo(items, ref cmbTechn);

            if (controller.Technology > 0)
            {
                for (k = 0; k < items.Count; k++)
                {
                    if (items[k].id == controller.Technology)
                    {
                        cmbTechn.SelectedIndex = k;
                        break;
                    }
                }
            }
            ReceptureController tbRec = new ReceptureController("Recepture");
            string idStr = tbRec.dbReader($"select id_technology from {tbRec.getTable()} where id = {controller.Recepture};")[0];
            if (int.TryParse(idStr, out id) && id > 0)            {
               
                 cmbTechn.SelectedIndex = Class1.ChangeIndex(items, id); 
            }
            id = 0;
       
            items = cards.getCatalog();
            Class1.FillCombo(items, ref cmbData);
            id = controller.Card;
            if (id != 0)
            {
                for (k = 0; k < items.Count; k++)
                {
                    if (items[k].id == id)
                    {
                        cmbData.SelectedIndex = k;
                    }
                }
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Technology frm; // разобраться с конструкторами!           
            int technology = controller.tbTechController.Selected;
            int recepture = controller.Recepture;
            frm = new Technology(technology);
            frm.Show();

            //int selected, id_technology, count;// id of recepture and of technology;

            //// проверить выбранный в списке                   
            //selected = tb.getSelected();
            //count = tb.SelectedCount("Recepture", "id_technology", selected);// dos recepture contain any technology

            //if (count == 1)
            //{
            //    id_technology = int.Parse(tb.getById("id_technology", selected));
            //    frm = new Technology(selected, id_technology);
            //    frm.Show();
            //}
            //else
            //{
            //    frm = new Technology(selected);
            //    frm.Show();
            //}
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            TechnologyCards frm = new TechnologyCards(controller.tbCardsController.Selected);
            //frm.Cards = controller.tbCardsController.Selected;
            frm.Show();            
        }

        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbData.SelectedIndex, selected_cards = 0;
            string description;
            List<string> technologies, names;            
            tbTechnologyCardsController cards = controller.tbCardsController;
            tbTechnologyController techn = controller.tbTechController;
            tbChainController chains = controller.tbChainController;            

            cards.setSelected(index);
            selected_cards = cards.Selected;
            
            cmbUsedIn.Items.Clear();
            technologies = chains.TechnologiesWithSelectedCard(selected_cards);
            for (int k = 0; k < technologies.Count; k++)
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
            tbTechnologyCardsController cards = controller.tbCardsController;
            tbTechnologyController techn = controller.tbTechController;
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

        private void btn_add_Click(object sender, EventArgs e)
        {
            int ind = 0;
            ind = controller.ApplyToChain();
            MessageBox.Show($"Is inserted {ind} records");
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            int ind = 0;
            ind = controller.RemoveFromChain();
            MessageBox.Show($"Is deleted {ind} records");
        }
    }
}
