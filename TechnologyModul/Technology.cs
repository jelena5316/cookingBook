/*
 * to input a head of technological chain
 */

using System;
using System.Collections.Generic;
using System.Reflection;
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
			tbRec = controller.getTbRecController();			

			if (technology < 0) technology = 0;
			id_technology = technology;					
			technologies = tb.getCatalog();

			selected_tech = technology;
			tb.Selected = technology;			
		}

        public TechnologyForm (List<ReceptureStruct> ReceptureStruct, int index)
        {
            InitializeComponent();

			int technology = 0;
			if (ReceptureStruct.Count < 1)
                technology = 0;
            else
                technology = ReceptureStruct[index].getIds()[1];
			selected_tech = technology; 

            controller = new TechnologyController(technology);
			controller.setTbController(index);
				
			tbRec = controller.getTbRecController(); // tbController
			tb = controller.getTbController();
            technologies = tb.getCatalog(); // T: Item    
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
			//receptures = controller.setReceptures(); // set subcatalog
			receptures = tb.RecepturesOfTechnology;
			if (listBox_rec.Items.Count > 0)
				listBox_rec.Items.Clear();
			if(receptures.Count > 0)
            {
				for (int k = 0; k < receptures.Count; k++)
					listBox_rec.Items.Add(receptures[k]);
            }
		}

		private string OutputTechnologyIntoForm( string[] info) //outputting into textbox
		{
			
			textBox1.Text = info[0];
			textBox3.Text = info[1];
			return info[0] + ": " + info[1];
		}

		private int fillCatalogCards(int selected) // cards subcatalog
        {
			int count = 0;
			List<Item> cards_items;			

			if (listBox_cards.Items.Count > 0)
			{
				listBox_cards.Items.Clear();
			}	

			cards_items = controller.Cards(selected, out count);

			if (cards_items != null)
			{
				int k;
				for (k = 0; k < cards_items.Count; k++)
				{
					listBox_cards.Items.Add(cards_items[k].name);
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
				tb.setCurrent(index);
				selected_tech = selected;
				id_technology = selected;

                string[] arr = controller.OutTechnology();
                OutputTechnologyIntoForm(arr);
				fillCatalogRec();
				//tb.setUsed();
				lbl_rec.Text = $"Is used in {receptures.Count} recipes";

				count = fillCatalogCards(selected);
				lbl_steps.Text = $"Use {count} steps";				
			}			
		}

		private void button1_Click(object sender, EventArgs e) // submit inserting or updating (editing)
		{
			bool count;
			int id_temp = id_technology;
			string name, description, report = "";

			if (string.IsNullOrEmpty(textBox1.Text)) return;
			if (string.IsNullOrEmpty(textBox3.Text)) return;

			name = textBox1.Text;
			description = textBox3.Text;

			count = controller.NotUnique(name);
			if (id_temp == 0 && count) // if name not unique -- will do nothing! No dialog!
            {
				MessageBox.Show
					("Data base has {technology} technologies with this name. Want you it update?\n" +
					"If you want update, than press 'Yes' then press 'Submit'!\n" +
					"If you don't want, than press 'No' and type new name, then press 'submit'",
					"Quation");
				return;				
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

        private void new_tech_Click(object sender, EventArgs e)
        {
			textBox1.Clear();
			textBox1.Focus();


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
