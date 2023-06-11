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
	public partial class Technology : Form
	{
		int id_technology, id_recepture, id, selected_tech/*, selected_rec*/;
		List<Item> technologies, receptures;
		tbTechnologyController tb;
		TechnologyController controller;
		tbController tbRec;

		/*		 
		 * id_technology -- переданный номер, для редактора; меняется и при правке, и при записи нового
		 * selected_tech -- номер выбранной из списка, равен c tb.Selected; по нему удаляем  и правим;
		 * id - возможно, больше не нужен, (уже не) используется в  OutTechnology(), с прежних конструкторов на случай,
		 * если не технология не передавалась;
		 * submit new, setStatusLabel3, comboBox2_SelectedIndexChanged;
		 * id_recepture - используется в fillCatalogRec();
		 * selected_rec (убрала) - только для вывода в статусную полоску, определяется только при очистке полей редактора.
		 */

		public Technology(int technology)
		{
			InitializeComponent();

			controller = new TechnologyController(technology);
			tb = controller.getTbController();
			tbRec = controller.getRecTbController();
			id_recepture = 0;

			if (technology < 0) technology = 0;
			id_technology = technology;
			tb.setCatalog();
			technologies = tb.getCatalog();

			selected_tech = technology;
			tb.Selected = technology;
			id = technology;
		}

		private string OutTechnology() //into textbox
		{
			string[] arr = controller.OutTechnology(selected_tech);
			textBox1.Text = arr[0];
			textBox3.Text = arr[1];
			return arr[0] + ": " + arr[1];
		}

		private void Technology_Load(object sender, EventArgs e)
		{
			int index = FormFunction.ChangeIndex(technologies, tb.Selected);
			FormFunction.FillCombo(technologies, comboBox2);
			comboBox2.SelectedIndex = index;
			//ChangeIndex(technologies);
			//OutTechnology();
			fillCatalogRec();

			//toolStripStatusLabel1.Text = $"Recepture {id_recepture}, technology {id_technology}";
			//toolStripStatusLabel2.Text = $"Selected: recepture {id_recepture} technology {selected_tech}";
			setStatusLabel3(id_recepture);

			// автодополнение
			AutoCompleteStringCollection source = new AutoCompleteStringCollection();
			foreach (Item i in technologies) source.Add(i.name);
			textBox1.AutoCompleteCustomSource = source;
			textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
			textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
		}

		//private int ChangeIndex(List<Item> items, int test)  // 'load', 'submit', 'delete'        
		//{
		//	int index = 0, temp_id = test;
		//	if (items.Count != 0)
		//	{
		//		//comboBox2.SelectedIndex = 0;
		//		if (temp_id > 0)
		//		{
		//			for (index = 0; index < items.Count; index++)
		//			{
		//				if (items[index].id == temp_id)
		//				{
		//					//comboBox2.SelectedIndex = index;                          
		//					break;
		//				}
		//			}
		//		}
		//		return index;
		//	}
		//	else return index;
		//}

		private void fillCatalogRec() // recepture
		{
			receptures = controller.setReceptures();
			if (listBox_rec.Items.Count > 0)
				listBox_rec.Items.Clear();
			if(receptures.Count > 0)
            {
				for (int k = 0; k < receptures.Count; k++)
					listBox_rec.Items.Add(receptures[k]);
            }
		}		

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // tech
		{
			if (comboBox2.SelectedIndex > -1)
                {
				int count = 0, index = comboBox2.SelectedIndex, selected = tb.setSelected(index);
				List<string> cards_id;
				tbChainController chains;

				selected_tech = selected;
				id_technology = selected;
				//toolStripStatusLabel2.Text = $"Selected: Recepture {id_recepture} technology {selected_tech}";

				// output in textbox				
				OutTechnology();

				//output receptures				
				fillCatalogRec();
				tb.setUsed();
				lbl_rec.Text = $"Is used in {receptures.Count} recipes";
				
				//output cards, if ones exists				
				chains = new tbChainController("Technology_chain");
				count = chains.CardsInTechnologyCount(selected);
				cards_id = chains.CardsInTechnology(selected);				
				
				if (listBox_cards.Items.Count > 0)
                    {
						listBox_cards.Items.Clear();
                    }

				List <string> Names (List<string> ids)
				{
					int k;
					string range = "";
					for (k = 0; k < cards_id.Count - 1; k++)
						range += $"{cards_id[k]}, ";
					range += cards_id[k];
					return tb.dbReader($"select name from Technology_card where id in ({range})");
				}
               
                if (cards_id.Count > 0)
                {
					List<string> names = Names(cards_id);
					for (int k =0; k < cards_id.Count; k++)
                    {
						listBox_cards.Items.Add(names[k]);
                    }
                }
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

			count = tb.technologiesCount(name); //select count(*) from Technology where name = '{name}';	
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

			//FormFunction.FillCombo(.., ..) меняет tb.Selected, selected_tech, меняет в setSelected(int index),
			//где index = 0;
			id_temp = tb.Selected;
			technologies = FormFunction.FillCombo(tb.getCatalog(), comboBox2);
			tb.Selected = id_temp;
			comboBox2.SelectedIndex = FormFunction.ChangeIndex(technologies, tb.Selected);

			//OutTechnology();
			//toolStripStatusLabel2.Text = $"Selected: recepture {id_recepture} technology {selected_tech}";
		}

		private void button3_Click(object sender, EventArgs e) // clear
		{
			textBox1.Clear();
			textBox3.Clear();
			textBox1.Focus();
			id_technology = 0;
		}

        private void goToChainsToolStripMenuItem_Click(object sender, EventArgs e)
        {

			//int selected, id_technology, count;// id of recepture and of technology;
			//								   // проверить выбранный в списке  
			//selected = CheckTbMainSelected(controller.getMinIdOfReceptures());
			//Chains frm;
			//ChainsController cntrl;

			//cntrl = new ChainsController();
			//cntrl.Recepture = selected;

			////id_technology
			//count = tbMain.SelectedCount("Recepture", "id_technology", selected); // dos recepture contain any technology
			//if (count == 1)
			//{
			//	id_technology = int.Parse(tbMain.getById("id_technology", selected));
			//	cntrl.Technology = id_technology;
			//}

			//frm = new Chains(ref cntrl);
			//frm.Show();
		}

        private void label3_Click(object sender, EventArgs e)
        {

			openChainEditor();
			//int selected, id_technology, count;// id of recepture and of technology;
			//								   // проверить выбранный в списке  
			//selected = CheckTbMainSelected(controller.getMinIdOfReceptures());
			//Chains frm;
			//ChainsController cntrl;

			//cntrl = new ChainsController();
			//cntrl.Recepture = selected;

			////id_technology
			//count = tbMain.SelectedCount("Recepture", "id_technology", selected); // dos recepture contain any technology
			//if (count == 1)
			//{
			//	id_technology = int.Parse(tbMain.getById("id_technology", selected));
			//	cntrl.Technology = id_technology;
			//}

			//frm = new Chains(ref cntrl);
			//frm.Show();
		}
        
		private void openChainEditor()
        {
            Chains frm;
            ChainsController cntrl;  
			cntrl = new ChainsController();
			if (receptures.Count > 0)
				cntrl.Recepture = receptures[0].id;
			else
				cntrl.Recepture = 0;			
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
				//удаляем
				ind = controller.Remove(comboBox2.SelectedIndex, out index);

				if (ind && index > -1)
				{
					//обновляем форму
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
		

		private void PrintInfo(string[] arr)
		{
			Print frm = new Print();
			frm.Show();
			frm.richTextBox1.Lines = arr;
		}

		public void PrintTechnology()
        {
			string[] arr = controller.getFullInfo();
			PrintInfo(arr);
        }

		private void printToolStripMenuItem_Click(object sender, EventArgs e) //print
		{
			

			//int id_rec, id_tech;
			//string rec, tech, category;
			//RichTextBox frm_textbox;
			//Form2 frm;

			//frm = new Form2();
			//frm.Show();
			//frm_textbox = frm.richTextBox1;
			//frm_textbox.Text = "";

			////выведет выбранную технологию и описание технологии
			//id_tech = technologies[comboBox2.SelectedIndex].id;
			//tech = technologies[comboBox2.SelectedIndex].name;
			//frm_textbox.Text += $"name: {tech}, id {id_tech}\n";
			//frm_textbox.Text += $"description: {textBox3.Text}\n";

			////выведет название рецепта
			//if (tbRec.Selected > 0 || receptures.Count > 0)
			//{
			//	id_rec = tbRec.Selected;
			//	frm_textbox.Text += $"Technology is used in {receptures.Count.ToString()} receptures:\n";
			//	int k = 0;
			//	for (k = 0; k < receptures.Count; k++)
			//	{
			//		category = controller.SeeRecepturesCategory(k);
			//		rec = receptures[k].name;
			//		frm_textbox.Text += $"{k + 1}. name: {rec}, id {tbRec.Selected}, {category}\n";
			//	}
			//	tbRec.Selected = id_rec;

			//}
			//else
			//{
			//	frm_textbox.Text += "Technology is not used in any recepture\n";
			//}

			////выведет на печать технологию с цепочкой карт
			//int temp = 0;
			//string text = "***\n";
			//List<string> chain;

			//tbTechnologyCardsController tbCards;
			//if (id_technology == 0)
			//{
			//	//return;
			//	string id = tb.dbReader($"select id_technology from Recepture where id ={tbRec.Selected};")[0];
			//	if (id == null)
			//		return;
			//	else
			//		temp = int.Parse(id);
			//	if (temp > 1)
			//	{
			//		tbCards = new tbTechnologyCardsController("Technology_card");
			//		chain = tbCards.SeeOtherCards(temp);
			//		foreach (string card in chain)
			//		{
			//			text += $"{card} \n";
			//		}
			//	}
			//	frm_textbox.Text += text;
			//}
			//else
			//{
			//	tbCards = new tbTechnologyCardsController("Technology_card");
			//	chain = tbCards.SeeOtherCards(id_technology);
			//	foreach (string card in chain)
			//	{
			//		text += $"{card} \n";
			//	}
			//	frm_textbox.Text += text;
			//}
		}	

		private void setStatusLabel3(int selected) //toolstrips` labels with technology name of selected recepture
		{
			//without method getReceptureByName()
			string tech_of_rec = null, name = "no", data = null;
			if (selected == 0) selected = 1;
			data = tbRec.getById("id_technology", selected);
			//$"select id_technology from Recepture where id={selected};";            
   
			if (!string.IsNullOrEmpty(tech_of_rec))
			{
				tech_of_rec = data;                
				data = technologies.Find(d => d.id == id).name;
				if (data != null)
				{
					name = data;
				}
				//toolStripStatusLabel3.Text = $"R {id_recepture} contains a T {name}";
			}
			//else toolStripStatusLabel3.Text = $"R {id_recepture} contains {name} T";
		}
			
	}
}
