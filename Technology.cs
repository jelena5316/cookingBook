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
        dbController db;
        TechnologyController tb = new TechnologyController("Technology");
        tbClass1 tbRec = new tbClass1("Recepture");
        int id_technology, id_recepture=0, id, selected_tech, selected_rec;
        List<Item> technologies, receptures;

        public Technology (int technology)
        {
            InitializeComponent();

            db = new dbController();

            if (technology < 0) technology = 0;
            id_technology = technology;
            tb.setCatalog();
            technologies = tb.getCatalog();            
            
            tb.Selected = technology;
            id = technology;
        }

        private string OutTechnology() //into textbox
        {
            string query = $"select name, description from Technology where id = {id};";
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
            toolStripStatusLabel2.Text = $"Selected: recepture {selected_rec} technology {selected_tech}";
            setStatusLabel3(selected_rec);

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
                    if (items[index].id == id_recepture)
                    {
                        recepture = index;
                    }
                }
                index--;
                comboBox1.Text = comboBox1.Items[index].ToString();
                if (recepture > -1)
                {
                    comboBox1.SelectedIndex = recepture;
                    comboBox1.Text = comboBox1.Items[recepture].ToString();
                }
            }
            else
            {
                comboBox1.Items.Clear();
                comboBox1.Text = "";
                txbRec.Text = "empty";
            }
            //comboBox1.Focus();
            return items;
        }

        private void button1_Click(object sender, EventArgs e) // submit new
        {
            string name, description, query, technology;

            if (string.IsNullOrEmpty(textBox1.Text)) return;
            if (string.IsNullOrEmpty(textBox3.Text)) return;

            name = textBox1.Text;
            description = textBox3.Text;

            
            technology = tb.technologiesCount(name);
            //query = $"select count(*) from Technology where name = '{name}';";
            // see `cardCount (string):in` t in TecnologyCardController;
            

            if (technology != "0")
            {
                // if dialog rezult is ok, then update records

                //query = $"select id from Technology where name = '{name}';";
                //id_technology = int.Parse(tb.dbReader(query)[0]);

                id_technology = tb.technologiesIdByName(name);
                int ind = tb.UpdateReceptureOrCards("description", description, id_technology);               
            }
            else
            {
                query = tb.insertTechnology(name, description);
                    //$"insert into Technology (name, description) values ('{name}', '{description}'); select last_insert_rowid()";
                technology = db.Count(query);                
                if (int.TryParse(technology, out id_technology))
                {
                    id = int.Parse(technology);
                }
                else return;
            }            
            MessageBox.Show($"Technology {name} (id {technology}) is inserted or updated");
            tb.setCatalog();
            technologies = Class1.FillCombo(tb.getCatalog(), ref comboBox2);
            ChangeIndex(technologies);
        }

        private void button3_Click(object sender, EventArgs e) // clear
        {
             textBox1.Clear();
             textBox3.Clear();
             textBox1.Focus();
             selected_rec = 0;
             selected_tech = 0;
             toolStripStatusLabel2.Text =
                 $"Selected: recepture {selected_rec} technology {selected_tech}";
        }

        private void button4_Click(object sender, EventArgs e) //delete technology from db
        {
            //проверка, используется ли
            //string query = $"select count (*) from Recepture count where id_technology = {selected_tech};";
            int ind = tb.recepturesCount(selected_tech); //int.Parse(tb.Count(query));
            // а что делать, если selected_tech = id_technology?
        
            if (ind > 0)
            {
                MessageBox.Show($"The technology is used in {ind} Receptures. \nPlease, remove it before deleting");
                return;
            }

            //удаляем
            //string query = $"delete from Technology where id = {selected_tech};";
            //ind = db.Edit(query);            
            ind = tb.RemoveItem();

            //обновляем форму
            if (ind > 0)
            {
                tb.setCatalog();
                technologies = Class1.FillCombo(tb.getCatalog(), ref comboBox2);
                ChangeIndex(technologies);
                textBox1.Focus();
                selected_tech = tb.Selected;
                toolStripStatusLabel2.Text =
                    $"Selected: recepture {selected_rec} technology {selected_tech}";
            }
            else MessageBox.Show("Nothing is deleted");
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e) //print
        {
            int id_rec, id_tech;
            string rec, tech;
            RichTextBox frm_textbox;

            Form2 frm;
            TechnologyCardsController tbCards;


            frm = new Form2();
            frm.Show();
            frm_textbox = frm.richTextBox1;
            frm_textbox.Text = "";

            id_rec = receptures[comboBox1.SelectedIndex].id;
            rec = receptures[comboBox1.SelectedIndex].name;
            id_tech = technologies[comboBox2.SelectedIndex].id;
            tech = technologies[comboBox2.SelectedIndex].name;


            //выведет название рецепта
            frm_textbox.Text += id_rec + " " + rec + "\n";

            //выведет выбранную технологию и описание технологии
            frm_textbox.Text += id_tech + " " + tech + "\n";
            frm_textbox.Text += textBox3.Text + "\n";

            //выведет на печать технологию с цепочкой карт

            int temp=0;
            if (id_technology == 0)
            {  
                string id = tb.dbReader($"select id_technology from Recepture where id ={tbRec.Selected};")[0];
                if (id == null)
                    return;
                else
                    temp = int.Parse(id);
            }
            
                string text = "***\n";
                if (temp > 1)
                {
                tbCards = new TechnologyCardsController("Technology_card");
                List<string> chain = tbCards.SeeOtherCards(temp);
                    foreach (string card in chain)
                    {
                        text += $"{card} \n";
                    }
                }
            frm_textbox.Text += text;
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
                toolStripStatusLabel3.Text = $"R {selected_rec} contains a T {name}";
            }
            else toolStripStatusLabel3.Text = $"R {selected_rec} contains {name} T";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // rec
        {
           int index, selected, probe, count, num;
           // string id, query;
          
           index = comboBox1.SelectedIndex;
           txbRec.Text = comboBox1.Items[index].ToString();

           //selected = tbRec.setSelected(index);           
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
                //id_technology = selected; //заменить на строку ниже
                selected_tech = selected;
                //label5.Text = selected.ToString();
                toolStripStatusLabel2.Text = $"Selected: recepture {selected_rec} technology {selected_tech}";

                // output in textbox
                id = selected;
                OutTechnology();

                //output receptures
                receptures = tb.setSubCatalog("Recepture", "id_technology");
                fillCatalogRec(receptures);
                if (receptures.Count > 0)               
                    tbRec.Selected = receptures[0].id;
                else
                {
                    tbRec.Selected = 0; // разобраться, где используется!                    
                }
                        
            }
        }

            
    }
}
