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
        
        public Chains()
        {
            InitializeComponent();
            controller = new ChainsController();
        }

        public Chains (ChainsController controller)
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
            Class1.FillCombo(items, cmbTechn);

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
            tbReceptureController tbRec = new tbReceptureController("Recepture");
            string idStr = tbRec.dbReader($"select id_technology from {tbRec.getTable()} where id = {controller.Recepture};")[0];
            if (int.TryParse(idStr, out id) && id > 0)            {
               
                 cmbTechn.SelectedIndex = Class1.ChangeIndex(items, id); 
            }
            id = 0;
       
            items = cards.getCatalog();
            Class1.FillCombo(items, cmbData);
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
       
        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbData.SelectedIndex, selected_cards = 0;
            string description;
            List<string> technologies;
            tbTechnologyCardsController cards = controller.tbCardsController;
            tbTechnologyController techn = controller.tbTechController;
            tbChainController chains = controller.tbChainController;

            cards.setSelected(index);
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
            //toolStripStatusLabel1.Text = "id_cards " + selected_cards;
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

            listBox_cards.Items.Clear();
            cards_id = chains.CardsInTechnology(selected_techn);
            for (int k = 0; k < cards_id.Count; k++)
            {
                names = cards.dbReader($"select name from {cards.getTable()} where id = {cards_id[k]};");
                listBox_cards.Items.Add(names[0]); // а есть ли проверка на уникальность имени?
            }
            if (listBox_cards.Items.Count != 0) listBox_cards.SelectedIndex = 0;
            else listBox_cards.Text = "";

            description = techn.dbReader($"select description from {techn.getTable()} where id = {selected_techn}")[0];
            if (description.Length > 50)
            {
                string t = description.Substring(0, 50);
                description = t;
            }
            lblTechn.Text = description;
            lblInfo2.Text = chains.CardsInTechnologyCount(selected_techn).ToString();
            //toolStripStatusLabel2.Text = "id_techns " + selected_techn;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int ind = 0;
            ind = controller.ApplyToChain();
            string message = (ind == -1) ? "Is inserted no records" : $"Is inserted {ind} records";
            MessageBox.Show(message);
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            int ind = 0;
            ind = controller.RemoveFromChain();
            MessageBox.Show($"Is deleted {ind} records");
        }

        private void lblTest_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            TechnologyCardsController cntrl = new TechnologyCardsController(controller);
            TechnologyCards frm = new TechnologyCards(cntrl);

            List<Item> items = controller.tbCardsController.getCatalog();
            Class1.FillCombo(items, cmbData);
        }
    }
}
