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
			int index = ChangeIndex(technologies, tb.Selected);
			Class1.FillCombo(technologies, ref comboBox2);
			comboBox2.SelectedIndex = index;
			//ChangeIndex(technologies);
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

		private int ChangeIndex(List<Item> items, int test)  // 'load', 'submit', 'delete'        
		{
			int index = -1, temp_id = test;
			if (items.Count != 0)
			{
				//comboBox2.SelectedIndex = 0;
				if (temp_id > 0)
				{
					for (index = 0; index < items.Count; index++)
					{
						if (items[index].id == temp_id)
						{
							//comboBox2.SelectedIndex = index;                          
							break;
						}
					}
				}
				return index;
			}
			else return index;
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
				toolStripStatusLabel2.Text = $"Selected: Recepture {id_recepture} technology {selected_tech}";

				// output in textbox				
				OutTechnology();

				//output receptures				
				fillCatalogRec();
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
					"If you want update, than press 'Yes' and choose a technology what you want update from list!\n" +
					"then press 'Submit'!\n" +
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
					if (comboBox1.Items.Count > 0)
						comboBox1.SelectedIndex = 0;
					return;
				}
			}
			report = controller.Submit(name, description, id_temp);
			MessageBox.Show(report);

			//Class1.FillCombo(.., ..) меняет tb.Selected, selected_tech, меняет в setSelected(int index),
			//где index = 0;
			id_temp = tb.Selected;
			technologies = Class1.FillCombo(tb.getCatalog(), ref comboBox2);
			tb.Selected = id_temp;
			comboBox2.SelectedIndex = ChangeIndex(technologies, tb.Selected);

			OutTechnology();
			toolStripStatusLabel2.Text = $"Selected: recepture {id_recepture} technology {selected_tech}";
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
			if (comboBox2.SelectedIndex == -1) return;

			int index = 0;
			bool ind;

			string message = controller.IsUsed();//проверка, используется ли
			message += "\nPlease, remove it before deleting";
			if (message == "")
			{
				//удаляем
				ind = controller.Remove(comboBox2.SelectedIndex, out index);

				if (ind && index > -1)
				{
					//обновляем форму
					technologies = Class1.FillCombo(tb.getCatalog(), ref comboBox2);
					comboBox2.SelectedIndex = index;
					textBox1.Focus();
					toolStripStatusLabel2.Text =
						$"Selected: recepture {id_recepture} technology {tb.Selected}";
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
				MessageBox.Show(message);
		}

		private void PrintInfo(string[] arr)
		{
			Print frm = new Print();
			frm.Show();
			frm.richTextBox1.Lines = arr;
		}

		private void printToolStripMenuItem_Click(object sender, EventArgs e) //print
		{
			string[] arr = controller.getFullInfo();
			PrintInfo(arr);

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
				toolStripStatusLabel3.Text = $"R {id_recepture} contains a T {name}";
			}
			else toolStripStatusLabel3.Text = $"R {id_recepture} contains {name} T";
		}
			
	}
}
