/*
 * to input a head of technological chain
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MajPAbGr_project
{
	public partial class TechnologyForm : Form
	{
		int id_technology, // tooken id, will be changed updating and creating
			selected_tech; // id of selected from list technology, is used for updating and deleting
		List<Item> technologies, receptures;
		tbTechnologyController tb;
		TechnologyController controller;
		tbController tbRec;

		public TechnologyForm(int technology)
		{
			InitializeComponent();

			controller = new TechnologyController(technology);
			tb = controller.getTbController();
			tbRec = controller.getRecTbController();			

			if (technology < 0) technology = 0;
			id_technology = technology;
			tb.setCatalog();
			technologies = tb.getCatalog();

			selected_tech = technology;
			tb.Selected = technology;			
		}

		private void Technology_Load(object sender, EventArgs e)
		{
			int index = FormFunction.ChangeIndex(technologies, tb.Selected);
			FormFunction.FillCombo(technologies, comboBox2);
			if (comboBox2.Items.Count > 0)
            {
				if (index > -1)
					comboBox2.SelectedIndex = index;
                else
                {
					comboBox2.Text = comboBox2.Items[0].ToString();
                }				
            }
			
			

			// autocomlpete
			AutoCompleteStringCollection source = new AutoCompleteStringCollection();
			foreach (Item i in technologies) source.Add(i.name);
			textBox1.AutoCompleteCustomSource = source;
			textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
			textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
		}		

		private void fillCatalogRec() // recepture
		{
			receptures = controller.setReceptures(); // set subcatalog
			if (listBox_rec.Items.Count > 0)
				listBox_rec.Items.Clear();
			if(receptures.Count > 0)
            {
				for (int k = 0; k < receptures.Count; k++)
					listBox_rec.Items.Add(receptures[k]);
            }
		}

		private string OutTechnology() //outputting into textbox
		{
			string[] arr = controller.getTbController().OutTechnology();
			textBox1.Text = arr[0];
			textBox3.Text = arr[1];
			return arr[0] + ": " + arr[1];
		}

		private int fillCatalogCards(int selected) // cards subcatalog
        {
			int count = 0;
			List<Item> cards_id;
			//List<string> cards_id, names;

			if (listBox_cards.Items.Count > 0)
			{
				listBox_cards.Items.Clear();
			}	

			cards_id = controller.Cards(selected, out count); //controller.Cards

			if (cards_id != null)
			{
				int k;
				for (k = 0; k < cards_id.Count; k++)
				{
					listBox_cards.Items.Add(cards_id[k].name);
				}
			}
			return count;
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // tech
		{
			if (comboBox2.SelectedIndex > -1)
                {
				int count = 0,
					index = comboBox2.SelectedIndex,
					selected = tb.setSelected(index);
				controller.getTbController().setCurrent();
				selected_tech = selected;
				id_technology = selected;
			
				OutTechnology();
				fillCatalogRec();
				tb.setUsed();
				lbl_rec.Text = $"Is used in {receptures.Count} recipes";

				count = fillCatalogCards(selected);
				lbl_steps.Text = $"Use {count} steps";				
			}			
		}

		private void button1_Click(object sender, EventArgs e) // submit inserting or updating (editing)
		{
			int id_temp = id_technology;
			string name, description, count, report = "";

			if (string.IsNullOrEmpty(textBox1.Text)) return;
			if (string.IsNullOrEmpty(textBox3.Text)) return;

			name = textBox1.Text;
			description = textBox3.Text;

			count = tb.technologiesCount(name); //count of technology with this name
			if (id_temp == 0 && count != "0")
			{
				DialogResult rezult = MessageBox.Show
					("Data base has {technology} technologies with this name. Want you it update?\n" +
					"If you want update, than press 'Yes' then press 'Submit'!\n" +
					"If you don't want, than press 'No' and type new name, then press 'submit'",
					"Quation",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2);
				if (rezult == DialogResult.No)
				{
					textBox1.Focus();
					return;
				}
				else
				{
					id_temp = tb.getTechnologiesIdsByName(name)[0].id;				
				}
			}
			report = controller.Submit(name, description, id_temp);
			MessageBox.Show(report);

			id_temp = tb.Selected;
			technologies = FormFunction.FillCombo(tb.getCatalog(), comboBox2);
			tb.Selected = id_temp;
			comboBox2.SelectedIndex = FormFunction.ChangeIndex(technologies, tb.Selected);			
		}

		private void button3_Click(object sender, EventArgs e) // clear
		{
			textBox1.Clear();
			textBox3.Clear();
			textBox1.Focus();
			id_technology = 0;
		}        

        private void label3_Click(object sender, EventArgs e)
        {
			openChainEditor();			
		}
        
		private void openChainEditor()
        {
            Chains frm;
            ChainsController cntrl;  
			cntrl = new ChainsController();					
			cntrl.Technology = tb.Selected;
            frm = new Chains(cntrl);
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e) //delete technology from data base
		{
			if (comboBox2.SelectedIndex == -1) return;

			int index = 0;
			bool ind;
			string name = textBox1.Text;

			string used = tb.getUsed();
			if (used == "0")
			{
				//deleting
				ind = controller.Remove(comboBox2.SelectedIndex, out index);

				if (ind && index > -1)
				{
					//refresh form
					technologies = FormFunction.FillCombo(tb.getCatalog(), comboBox2);
					comboBox2.SelectedIndex = index;
					textBox1.Focus();
					MessageBox.Show($"{name} is deleted");
				}
				else
				{
					if (!ind)
						MessageBox.Show("Nothing is deleted");
					else
						MessageBox.Show("All technologies are deleted");
				}
			}
            else
            {
				string message = $"The technology is used in {used} Receptures.\nPlease, remove it before deleting";
				MessageBox.Show(message);
            }				
		}	
	}
}
