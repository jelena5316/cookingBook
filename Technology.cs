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
	public partial class Technology : Form
	{
		int id_technology, id_recepture, id, selected_tech/*, selected_rec*/;
		List<Item> technologies, receptures;        
		tbTechnologyController tb;
		TechnologyController controller;
		tbClass1 tbRec;

		/*		 
		 * id_technology -- переданный номер, для редактора; меняется и при правке, и при записи нового
		 * selected_tech -- номер выбранной из списка, равен c tb.Selected; по нему удаляем  и правим;
		 * id - возможно, больше не нужен, (уже не) используется в  OutTechnology(), с прежних конструкторов на случай,
		 * если не технология не передавалась;
		 * submit new, setStatusLabel3, comboBox2_SelectedIndexChanged;
		 * id_recepture - используется в fillCatalogRec();
		 * selected_rec (убрала) - только для вывода в статусную полоску, определяется только при очистке полей редактора.
		 */

		public Technology (int technology)
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
			return arr[0] + ": " + arr[1];;
		}

		private void Technology_Load(object sender, EventArgs e)
		{
			Class1.FillCombo(technologies, ref comboBox2);
			ChangeIndex(technologies);
			OutTechnology();
            fillCatalogRec();            

            toolStripStatusLabel1.Text = $"Recepture {id_recepture}, technology {id_technology}";
			toolStripStatusLabel2.Text = $"Selected: recepture {id_recepture} technology {selected_tech}";
			setStatusLabel3(id_recepture);

			// автодополнение
			AutoCompleteStringCollection source = new AutoCompleteStringCollection();
			foreach (Item i in technologies) source.Add(i.name);
			textBox1.AutoCompleteCustomSource = source;
			textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
			textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
		}

		private int ChangeIndex(List<Item> items)  // 'load', 'submit', 'delete'        
		{   
			int index = -1, temp_id = selected_tech;
			if (items.Count != 0)
			{
				comboBox2.SelectedIndex = 0;
				if (temp_id > 0)
				{
					for (index = 0; index < items.Count; index++)
					{
						if (items[index].id == temp_id)
						{
							comboBox2.SelectedIndex = index;                          
							break;
						}
					}                  
				}
				return index;
			}
			else return -2;
		}

		private void fillCatalogRec() // recepture
		{
            receptures = controller.setReceptures();
            Class1.FillCombo(receptures, ref comboBox1);            
            if (receptures.Count > 0)
            {
                tbRec.Selected = receptures[0].id;//// вывод на печать
				id_recepture = tbRec.Selected;
            }
            else
            {
                tbRec.Selected = 0; // вывод на печать
                label5.Text = "no category";
				txbRec.Text = "empty";

				comboBox1.Items.Clear();
                comboBox1.Text = "";
                //посмотреть, как в FormFunktion отвязать от подаваемого списка! 
             }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // rec
		{
			int index = comboBox1.SelectedIndex;
		    txbRec.Text = comboBox1.Items[index].ToString();			
			label5.Text = controller.SeeRecepturesCategory(index);

			id_recepture = tbRec.Selected;
			toolStripStatusLabel2.Text = $"Selected: recepture {tbRec.Selected} technology {selected_tech}";
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // tech
		{
			{
				int index = comboBox2.SelectedIndex;
				int selected = tb.setSelected(index);				
				selected_tech = selected;
				id_technology = selected;
				toolStripStatusLabel2.Text = $"Selected: recepture {id_recepture} technology {selected_tech}";				

				// output in textbox				
				OutTechnology();

                //output receptures				
                fillCatalogRec();                
            }
		}

		private void button1_Click(object sender, EventArgs e) // submit inserting or updating (editing)
		{
			int ind = 0, id_temp = id_technology;
			string name, description, query, technology, report = "";

			if (string.IsNullOrEmpty(textBox1.Text)) return;
			if (string.IsNullOrEmpty(textBox3.Text)) return;

			name = textBox1.Text;
			description = textBox3.Text;

			technology = tb.technologiesCount(name); //select count(*) from Technology where name = '{name}';	
            if (id_temp == 0 && technology != "0")
            {
                DialogResult rezult =  MessageBox.Show($"Data base has {technology} technologies with this name. Want you it update?", "Quation", MessageBoxButtons.YesNo);
				if (rezult == DialogResult.Yes)
                {
					//id_temp = -1;
					MessageBox.Show("Choose a recepture, what you want update! Don't clear -- only submit!");					
					return;
                }
            }
			report = controller.Submit(name, description, id_temp);
			//tb.setCatalog(); --в 'Submit(string, string, int)'

			selected_tech = tb.Selected;
			id_technology = tb.Selected;

			technologies = Class1.FillCombo(tb.getCatalog(), ref comboBox2);
			ChangeIndex(technologies);
			OutTechnology();
			MessageBox.Show($"Technology {name} (id {tb.Selected}) is {report}");
			//setStatusLabel3(id_recepture);

			//MessageBox.Show($"id from controller {report}, this selected  {tb.Selected}");
		}

		private void button3_Click(object sender, EventArgs e) // clear
		{
			textBox1.Clear();
			textBox3.Clear();
			textBox1.Focus();
			id_technology = 0;			 
		}

		private void button4_Click(object sender, EventArgs e) //delete technology from data base
		{
			int count=0;
			if (technologies.Count > 0) count++;            
			if(selected_tech == id_technology) count++;

			//проверка, используется ли			
			int ind = tb.recepturesCount(selected_tech); //int.Parse(tb.Count(query));
														 //		
			if (ind > 0)
			{
				MessageBox.Show($"The technology is used in {ind} Receptures. \nPlease, remove it before deleting");
				return;
			}

			//удаляем
			tb.Selected = selected_tech;		          
			ind = tb.RemoveItem();

			//обновляем форму
			if (ind > 0)
			{
				tb.setCatalog();
				technologies = Class1.FillCombo(tb.getCatalog(), ref comboBox2);
				ChangeIndex(technologies);
				textBox1.Focus();
				selected_tech = tb.Selected; //??
				toolStripStatusLabel2.Text =
					$"Selected: recepture {id_recepture} technology {selected_tech}";
			}
			else MessageBox.Show("Nothing is deleted");
		}

		private void printToolStripMenuItem_Click(object sender, EventArgs e) //print
		{
			int id_rec, id_tech;
			string rec, tech;
			RichTextBox frm_textbox;
			Form2 frm;
			
			frm = new Form2();
			frm.Show();
			frm_textbox = frm.richTextBox1;
			frm_textbox.Text = "";

			//выведет выбранную технологию и описание технологии
			id_tech = technologies[comboBox2.SelectedIndex].id;
			tech = technologies[comboBox2.SelectedIndex].name;
			frm_textbox.Text += $"name: {tech}, id {id_tech}\n";			
			frm_textbox.Text += $"description: {textBox3.Text}\n";

			//выведет название рецепта
			if (tbRec.Selected > 0 || receptures.Count > 0)
			{
				frm_textbox.Text += $"Technology is used in {receptures.Count.ToString()} receptures:\n";
				int k = 0;
				for (k = 0; k < receptures.Count; k++)
				{
					id_rec = receptures[k].id;
					rec = receptures[k].name;					
					string id_category = tbRec.dbReader($"select id_category from Recepture where id = {id_rec};")[0];
					string category = tbRec.dbReader($"select name from Categories where id = {id_category};")[0];					
					frm_textbox.Text += $"{k+1}. name: {rec}, id {id_rec}, category: {category}\n";
				}
				
			}
			else
			{
				frm_textbox.Text += "Technology is not used in any recepture\n";
			}

			////выведет на печать технологию с цепочкой карт
			// 
			//int temp = 0;
			//TechnologyCardsController tbCards;
			//if (id_technology == 0)
			//{
			//	string id = tb.dbReader($"select id_technology from Recepture where id ={tbRec.Selected};")[0];
			//	if (id == null)
			//		return;
			//	else
			//		temp = int.Parse(id);
			//}

			//	string text = "***\n";
			//	if (temp > 1)
			//	{
			//	tbCards = new TechnologyCardsController("Technology_card");
			//	List<string> chain = tbCards.SeeOtherCards(temp);
			//		foreach (string card in chain)
			//		{
			//			text += $"{card} \n";
			//		}
			//	}
			//frm_textbox.Text += text;
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
				toolStripStatusLabel3.Text = $"R {id_recepture} contains a T {name}";
			}
			else toolStripStatusLabel3.Text = $"R {id_recepture} contains {name} T";
		}

		

			
	}
}
