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

        private void btn_add_Click(object sender, EventArgs e)
        {
            int id_technology=0, id_cards = 0;

            int ind = controller.tbCardsController.insertCardsIntoChain(id_technology, id_cards);
            //int ind = 0;
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
