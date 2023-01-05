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
	public partial class Categories : Form
	{
		bool selected_rec = false;
		int pragma; 	
		List<string> list;		
		CategoriesController controller; // включает в себя FormMainController
		tbFormMainController tbMain; // указатель на controller.TbMain
		
		public Categories()
		{
			InitializeComponent();
			controller = new CategoriesController();
			tbMain = controller.TbMain;
			list = new List<string>();
			for (int k = 0; k < controller.Receptures.Count; k++)			
				list.Add(controller.Receptures[k].name);			
			pragma = 0;
		}

		private void Categories_Load(object sender, EventArgs e)
		{
			lv_recepture.Columns.Add("Name");
			lv_recepture.Columns.Add("Category");
			lv_recepture.Columns.Add("Source");
			lv_recepture.Columns.Add("Author");
			lv_recepture.Columns.Add("Technology");
			lv_recepture.Columns.Add("Main_ingredient");

			Class1.setBox(controller.Categories, ref cmb_categories);			
			controller.setListView(ref lv_recepture);			
			cmb_categories.Text = "all";
			pragma = 1;
						
			AutoCompleteStringCollection source = new AutoCompleteStringCollection();
			foreach (Item item in controller.Receptures)
				source.Add(item.name);		
			textBox1.AutoCompleteCustomSource = source;
			textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
			textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
		}

		private void cmb_categories_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (pragma == 0) return;
			int index = cmb_categories.SelectedIndex;            
			int id = controller.Categories[index].id;
			//tbMain.SelectedByCategoryRecepture(id);
			//Class1.FillListView(tbMain.Receptures, ref lv_recepture);

			List<ReceptureStruct> full = controller.RecepturesStruct;
			List<ReceptureStruct> selected
				= full.FindAll(p => p.getCategory() == ("category: " +controller.Categories[index].name));
			lv_recepture.Items.Clear();

			ListViewItem items;
			for (int k = 0; k < selected.Count; k++)
			{
				string[] arr = selected[k].getFields();
				items = new ListViewItem(arr[0]);
				items.Tag = selected[k].getId();

				for (int q = 1; q < arr.Length; q++)
				{
					items.SubItems.Add(arr[q]);
				}
				lv_recepture.Items.Add(items);
			}
		}

		private void label1_Click(object sender, EventArgs e)
		{
			cmb_categories.SelectedIndex = 0;
			controller.setReceptures();
			lv_recepture.Items.Clear();
			controller.setFields();
			controller.setListView(ref lv_recepture);
			cmb_categories.Text = "all";
			lv_recepture.Items[0].Selected = true;
		}

		private void lv_recepture_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lv_recepture.SelectedItems.Count > 0)
            {
				tbMain.setSelected(lv_recepture.SelectedItems[0].Index);
				selected_rec = true;
            }
            else
            {
				selected_rec = false;
			}
		}

		private void lv_recepture_DoubleClick(object sender, EventArgs e)
		{
			string table = "Recepture", t;
			int id = 0; //id_recepture
			int category = 0;
			int technology = 0;

			if (selected_rec)
            {
				id = tbMain.Selected; //id = tbMain.Receptures[lv_recepture.SelectedItems[0].Index].id; //получаем id рецептуры
            }			
            else
            {
				MessageBox.Show("Please, select any recepture from list");
				return;				
			}


			//category			
			t = tbMain.readCategory();
			if (int.TryParse(t, out category))
				category = int.Parse(t);			
			else
				category = 0;

			//technology			
			t = tbMain.readTechnology();
			if (int.TryParse(t, out technology))
				technology = int.Parse(t);
			else
				technology = 0;

            tbFormMainController cntrl = new tbFormMainController(table, id, category, technology);
            NewRecepture frm = new NewRecepture(cntrl);
            frm.Show();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
        {
			if (textBox1.Text == "") return;

			List<ReceptureStruct> full = controller.RecepturesStruct;
			List<ReceptureStruct> selected = full.FindAll(p => p.getName().Contains(textBox1.Text));
			lv_recepture.Items.Clear();

			ListViewItem items;
			for (int k = 0; k < selected.Count; k++)
			{
				string[] arr = selected[k].getFields();
				items = new ListViewItem(arr[0]);
				items.Tag = selected[k].getId();

				for (int q = 1; q < arr.Length; q++)
				{
					items.SubItems.Add(arr[q]);
				}
				lv_recepture.Items.Add(items);
			}
		}

        private void button1_Click(object sender, EventArgs e)
        {
			FormMain frm = new FormMain();			
			frm.Show();			
        }

        
    }
}
