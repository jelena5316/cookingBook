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
		TechnologyController tb;
		tbClass1 tbRec;

		/*		 
		 * id_technology -- переданный номер, выставляет с поле имя из списка в поле технологий;  номер исправленной технологии (теперь и новой);
		 * selected_tech -- номер выбранной из списка в поле, равен c tb.Selected; обнуляется при их очистке; по нему удаляем  и правим;
		 * id - возможно, больше не нужен, (уже не) используется в  OutTechnology(), с прежних конструкторов на случай,
		 * если не технология не передавалась;
		 * submit new, setStatusLabel3, comboBox2_SelectedIndexChanged;
		 * id_recepture - используется в fillCatalogRec();
		 * selected_rec (убрала) - только для вывода в статусную полоску, определяется только при очистке полей редактора.
		 */

		public Technology (int technology)
		{
			InitializeComponent();

			tb = new TechnologyController("Technology");
			tbRec = new tbClass1("Recepture");
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
			string query = $"select name, description from Technology where id ={selected_tech};"; //id
			string technology = "";
			technology = tb.dbReadTechnology(query)[0];            
			string[] arr = null;
			arr = technology.Split('*');
			technology = arr[0] + ": " + arr[1];
			textBox1.Text = arr[0];
			textBox3.Text = arr[1];            
			return technology;
		}

		private void Technology_Load(object sender, EventArgs e)
		{
			Class1.FillCombo(technologies, ref comboBox2);
			ChangeIndex(technologies);			

			receptures = tb.setSubCatalog("Recepture", "id_technology");
			fillCatalogRec(receptures);
			if (receptures.Count > 0)
				tbRec.Selected = receptures[0].id;
			else
				tbRec.Selected = 0; // разобраться, где используется!            

			OutTechnology();

			toolStripStatusLabel1.Text = $"Recepture {id_recepture}, technology {id_technology}";
			toolStripStatusLabel2.Text = $"Selected: recepture {id_recepture} technology {selected_tech}";
			setStatusLabel3(id_recepture);

			AutoCompleteStringCollection source = new AutoCompleteStringCollection();
			foreach (Item i in technologies) source.Add(i.name);
			textBox1.AutoCompleteCustomSource = source;
			textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
			textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
		}

		private int ChangeIndex(List<Item> items)  // 'load', 'submit', 'delete'        
		{   
			int index = -1, temp_id = id_technology;
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

		private List<Item> fillCatalogRec(List<Item> items) // recepture
		{
			if (items.Count != 0)
			{
				int recepture = -1, index;

				if (comboBox1.Items.Count > 0)
					comboBox1.Items.Clear();
				for (index = 0; index < items.Count; index++)
				{
					comboBox1.Items.Add(items[index].name);
					if (items[index].id == id_recepture) // уже нет смысла!
					{
						recepture = index;
					}
				}
				index--;
				comboBox1.SelectedIndex = 0;
				//comboBox1.Text = comboBox1.Items[index].ToString();
				//if (recepture > -1)
				//{
				//	comboBox1.SelectedIndex = recepture;
				//	comboBox1.Text = comboBox1.Items[recepture].ToString();
				//}
			}
			else
			{
				// что делать с выбранным номером в списке рецептур? Обнулять ли?
				comboBox1.Items.Clear();
				comboBox1.Text = "";
				txbRec.Text = "empty";
			}
			//comboBox1.Focus();
			return items;
		}

		private void button1_Click(object sender, EventArgs e) // submit new
		{
			int ind=0, id_temp = id_technology; // id_technology = 0!
			string name, description, query, technology, report = "";

			if (string.IsNullOrEmpty(textBox1.Text)) return;
			if (string.IsNullOrEmpty(textBox3.Text)) return;
			
			name = textBox1.Text;
			description = textBox3.Text;
			
			technology = tb.technologiesCount(name);
			//query = $"select count(*) from Technology where name = '{name}';";
			// see `cardCount (string):in` t in TecnologyCardController;			

			void Insert()
			{
				int num;
				query = tb.insertTechnology(name, description);				
				technology = tb.Count(query);
				if (int.TryParse(technology, out num))
				{
					id_technology = num;
				}
                else
                {
					id_technology = id;
					ind--;
                }
			}
			
			if (technology != "0")
			{
				MessageBox.Show($"Data base has {technology} technologies with this name. Want you update?", "Quation", MessageBoxButtons.YesNo);
				if (DialogResult == DialogResult.Yes)
				{
					id_technology = tb.technologiesIdByName(name);
					ind = tb.UpdateReceptureOrCards("description", description, id_technology);					
				}   
			}
			else
				Insert();
			
			report = id_temp == id_technology ? "not inserted" : "inserted";
			report = ind > 0 ? "updated" : report;
			MessageBox.Show($"Technology {name} (id {id_technology}) is {report}");
			setStatusLabel3(id_recepture);

			if (ind < 0)
				selected_tech = id; //  пока selected_tech  обнуляется при очистке!
	
			tb.setCatalog();
			technologies = Class1.FillCombo(tb.getCatalog(), ref comboBox2);
			ChangeIndex(technologies);
			OutTechnology();
		}

		private void button3_Click(object sender, EventArgs e) // clear
		{
			 textBox1.Clear();
			 textBox3.Clear();
			 textBox1.Focus();			 
			 selected_tech = 0;
			 id = id_recepture; // пока не ясно с selected_tech
			 id_recepture = 0;
			 toolStripStatusLabel2.Text =
				 $"Selected: recepture {id_recepture} technology {selected_tech}";
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

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // rec
		{
		   int index, selected, probe, count, num;
			// string id, query;
		   
		   index = comboBox1.SelectedIndex;
		   txbRec.Text = comboBox1.Items[index].ToString();			
			if (receptures.Count > 0)
			{
				selected = receptures[index].id;
				tbRec.Selected = receptures[index].id;
				string id_category = tbRec.dbReader($"select id_category from Recepture where id = {selected};")[0];
				string category = tbRec.dbReader($"select name from Categories where id = {id_category};")[0];
				label5.Text = category;				
			}			
	 
			//         
			//selected_rec = selected;
			//query = $"select id_technology from Recepture where id ={tbRec.Selected};";
			//id = tb.dbReader(query)[0]; 

			//if (id == "")
			//     id = "0";            
			//probe = int.Parse(id);
			//count = (technologies.Count < receptures.Count) ? technologies.Count : receptures.Count;

			// ChangeIndex(technologies); // работает иначе
			// for (num = 0; num < count; num++)
			// {
			//     if (technologies[num].id == probe)
			//     {
			//         comboBox2.SelectedIndex = num;
			//         break;
			//     }
			//     id_technology = probe;
			// }

			// toolStripStatusLabel2.Text = $"Selected: recepture {selected_rec} technology {selected_tech}";
			// setStatusLabel3(selected);
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // tech
		{
			{
				int index = comboBox2.SelectedIndex;
				int selected = tb.setSelected(index);				
				selected_tech = selected;// вместо id_technology = selected;				
				toolStripStatusLabel2.Text = $"Selected: recepture {id_recepture} technology {selected_tech}";				

				// output in textbox
				id = selected;
				OutTechnology();

				//output receptures				
				receptures = tb.setSubCatalog("Recepture", "id_technology");
				fillCatalogRec(receptures);
				if (receptures.Count > 0)
				{
					tbRec.Selected = receptures[0].id;					
				}
				else
				{
					tbRec.Selected = 0; // разобраться, где используется!
					label5.Text = "no category";					
				}		
			}
		}

			
	}
}
