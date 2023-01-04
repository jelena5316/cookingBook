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
		int pragma;		
		List<string> list;
		//CategoriesController controller; // включает в себя tbCategoriesController
		tbCategoriesController tbMain; // включает в себя FormMainController
		
		public Categories()
		{
			InitializeComponent();
			tbMain = new tbCategoriesController();
			//controller = new CategoriesController();
			list = new List<string>();
			for (int k = 0; k < tbMain.Receptures.Count; k++)			
				list.Add(tbMain.Receptures[k].name);			
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

			Class1.setBox(tbMain.Categories, ref cmb_categories);
			//Class1.FillListView(categoriesController.Receptures, ref lv_recepture);
			tbMain.setListView(ref lv_recepture);			
			cmb_categories.Text = "all";
			pragma = 1;
						
			AutoCompleteStringCollection source = new AutoCompleteStringCollection();
			foreach (Item item in tbMain.Receptures)
				source.Add(item.name);		
			textBox1.AutoCompleteCustomSource = source;
			textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
			textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
		}

		private void cmb_categories_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (pragma == 0) return;
			int index = cmb_categories.SelectedIndex;            
			int id = tbMain.Categories[index].id;
			//tbMain.SelectedByCategoryRecepture(id);
			//Class1.FillListView(tbMain.Receptures, ref lv_recepture);

			List<ReceptureStruct> full = tbMain.RecepturesStruct;
			List<ReceptureStruct> selected
				= full.FindAll(p => p.getCategory() == ("category: " +tbMain.Categories[index].name));
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
			tbMain.setReceptures();
			lv_recepture.Items.Clear();
			//controller.setFields();
			tbMain.setListView(ref lv_recepture);
			cmb_categories.Text = "all";
		}

		private void lv_recepture_SelectedIndexChanged(object sender, EventArgs e)
		{
			int id = 0; //id_recepture
			int category = 0;
			int technology = 0;
			//id = tbMain.Receptures[lv_recepture.SelectedItems[0].Index].id;
			//category = tbMain.Categories[lv_recepture.SelectedItems[0].Index].id;
			//technology = int.Parse(controller.Receptures[0].getFields()[5]);
		}

		private void lv_recepture_DoubleClick(object sender, EventArgs e)
		{
			string table = "Recepture", t;
			int id = 0; //id_recepture
			int category = 0;
			int technology = 0;

			id = tbMain.Receptures[lv_recepture.SelectedItems[0].Index].id;
			
			//category
			t = tbMain.TbMain.dbReader($"select id_category from Recepture where id = {id};")[0];
			if (int.TryParse(t, out category))
				category = int.Parse(t);			
			else
				category = 0;		

			//technology
			t = tbMain.TbMain.dbReader($"select id_technology from Recepture where id = {id};")[0];
			if (int.TryParse(t, out technology))
				technology = int.Parse(t);
			else
				technology = 0;

			ReceptureController cntrl = new ReceptureController(table, id, category, technology);
			NewRecepture frm = new NewRecepture(cntrl);
			frm.Show();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
        {
			if (textBox1.Text == "") return;

			List<ReceptureStruct> full = tbMain.RecepturesStruct;
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
