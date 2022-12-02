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
            TechnologyCardsController cards = controller.tbCardsController;           

            cards.setSelected(index);
            selected_cards = cards.Selected;

            toolStripStatusLabel1.Text = "id_cards " + selected_cards;
        }

        private void cmbTechn_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbTechn.SelectedIndex, selected_techn = 0;
            TechnologyController techn = controller.tbTechController;

            techn.setSelected(index);
            selected_techn = techn.Selected;

            toolStripStatusLabel2.Text = "id_techns " + selected_techn;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int ind = 0;
            ind = controller.CreateChain();
            MessageBox.Show($"Is inserted {ind} records");
        } 
        
        private void label3_Click(object sender, EventArgs e)
        {
            //
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            //
        }
    }
}
