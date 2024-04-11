/*
 * to manage recipes` (formulations`) catalogue: read data from DB and show it
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MajPAbGr_project
{
	public partial class Categories : Form
	{
		bool exist_selected = false;
		int selected_recepture = -1;		
		List<ReceptureStruct> full;
		CategoriesController controller;
		tbReceptureController tbMain; // pointer at controller.TbMain

		public Categories()
		{
			InitializeComponent();
			controller = new CategoriesController();			
			tbMain = controller.TbMain;			
		}

		public Categories(CategoriesController cntrl)
		{
			InitializeComponent();
			controller = cntrl;
			tbMain = controller.TbMain;
		}

		private void Categories_Load(object sender, EventArgs e)
		{
			lv_recepture.Columns.Add("Name");
			lv_recepture.Columns[0].Width = 250;
			lv_recepture.Columns.Add("Category");
			lv_recepture.Columns[1].Width = 100;
			lv_recepture.Columns.Add("Main");
			lv_recepture.Columns[2].Width = 120;
			lv_recepture.Columns.Add("Author");
			lv_recepture.Columns[3].Width = 250;
			lv_recepture.Columns.Add("Source");
			lv_recepture.Columns[4].Width = 250;

			toolStripCmbPrint.SelectedIndex = 0;//print	
			FormFunction.setBox(controller.Categories, cmb_categories);
			cmb_categories.Text = "all";
			resetRecepturesList(controller.ReceptureStruct);			
			full = null;
			if (lv_recepture.Items.Count > 0)
				lv_recepture.Items[0].Selected = true;
			AutoCompleteRecepture(controller.Receptures);

			if(tbMain.Err_code > 0)
			{
				MessageBox.Show($"{tbMain.Err_message}");
				tbMain.ResetErr_info();
			}
		}

		private void AutoCompleteRecepture(List<Item> rec)
		{
			AutoCompleteStringCollection source = new AutoCompleteStringCollection();
			foreach (Item item in rec)
				source.Add(item.name);
			textBox1.AutoCompleteCustomSource = source;
			textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
			textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
		}

		/*
		 * Methods for events handlers
		 */

		private int CheckTbMainSelected(int min)
		{
			if (tbMain.Selected > 0)
			{ 
				return tbMain.Selected;
			}				
			else
			{
				tbMain.Selected = min;
				return min;
			}	
		}

		private void openRecipesEditor()
		{
			controller.OpenRecipesForm();			
		}

		private void openTechnology()
		{
			int selected, id_technology;

			// check selected item of list
			selected = CheckTbMainSelected(controller.getMinIdOfReceptures());
			Technology frm;

			//id_technology
			int index = selected_recepture == -1 ? 0 : selected_recepture;

			if (controller.ReceptureStruct.Count < 1)
				id_technology = 0;
			else
				id_technology = controller.ReceptureStruct[index].getIds()[1];
			id_technology = id_technology < 0 ? 0 : id_technology;

			frm = new Technology(id_technology);
			frm.Show();
		}

		private void addNew()
		{
			NewReceptureController rec = new NewReceptureController();
			ReceptureStruct info = new ReceptureStruct(0);
			rec.ReceptureInfo = info;
			NewRecepture frm = new NewRecepture(tbMain, rec);
			frm.ShowDialog();
			Reload();
		}

		private void SimpleTable(int opt)
		{
			tbIngredientsController cntrl = new tbIngredientsController(opt);
			Ingredients frm = new Ingredients(cntrl);
			frm.Show();
		}

		private void Reload()
		{
			tbIngredientsController tbCat = controller.TbCat;
			tbCat.resetCatalog();
			controller.Categories = tbCat.getCatalog();
			FormFunction.setBox(controller.Categories, cmb_categories);

			controller.setReceptures();
			controller.ReceptureStruct.Clear();
			controller.setFields();

			seeAll();
			AutoCompleteRecepture(controller.Receptures);		
		}

		/*
		 * Events handlers
		 */
		private void lv_recepture_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lv_recepture.Items.Count < 0)
				controller.ExistsSelected = false;
				//exist_selected = false;
			if (lv_recepture.SelectedItems.Count < 1)
			{
				selected_recepture = 0;
				controller.ExistsSelected = false;
				//exist_selected = false;
				return;
			}

			if (full == null)
			{
				tbMain.setSelected(lv_recepture.SelectedItems[0].Index);
				selected_recepture = lv_recepture.SelectedItems[0].Index;
			}
			else
			{
				if (textBox1.Text != "")
				{
					string name = textBox1.Text;
					selected_recepture = controller.indexOfSelectedByName(name);
				}
				else
				{
					int index = lv_recepture.SelectedItems[0].Index;
					if (controller.Categories.Count > 0)
					{
						selected_recepture = controller.indexOfSelectedByCategory(index, cmb_categories.SelectedIndex);
					}
					else
						return;

				}
			}
			controller.ExistsSelected = true;
			//exist_selected = true;
		}

		private void cmb_categories_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmb_categories.Items.Count < 1) return;
			textBox1.Text = "";
			if (cmb_categories.SelectedIndex == -1) return;
			int index = cmb_categories.SelectedIndex, id = controller.Categories[index].id;
			List<ReceptureStruct> selected = new List<ReceptureStruct>();

			full = controller.ReceptureStruct;
			selected = controller.selectByCategory(index);

			resetRecepturesList(selected);
			if (lv_recepture.Items.Count > 0)
				lv_recepture.Items[0].Selected = true;
			else
				exist_selected = false;
		}

		private void resetRecepturesList(List<ReceptureStruct> list)
		{
			if (list == null) return;
			lv_recepture.Items.Clear();
			ListViewItem items;
			for (int k = 0; k < list.Count; k++)
			{
				string[] arr = list[k].getData();
				items = new ListViewItem(arr[0]);
				items.Tag = list[k].getId();

				for (int q = 1; q < arr.Length; q++)
				{
					items.SubItems.Add(arr[q]);
				}
				lv_recepture.Items.Add(items);
			}
		}

		private void seeAll()
		{
			cmb_categories.SelectedIndex = 0;
			resetRecepturesList(full);
			full = null;
			cmb_categories.Text = "all";
			textBox1.Text = "";
			if (lv_recepture.Items.Count > 0)
				lv_recepture.Items[0].Selected = true;			
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

			if (textBox1.Text == "")
			{
				resetRecepturesList(full);
				full = null;
			}
			else
			{
				full = controller.ReceptureStruct;
				List<ReceptureStruct> selected = controller.selectByName(textBox1.Text);	
				resetRecepturesList(selected);
			}
			if (lv_recepture.Items.Count > 0)
				lv_recepture.Items[0].Selected = true;			
		}

		/*
		 * Others controls: buttons, menu strip items
		 */
		private void button1_Click(object sender, EventArgs e) // recipes editor
		{
			addNew();
		}


		private void recipeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openRecipesEditor();
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			SimpleTable(2);			
		}

		private void lbl_add_Click(object sender, EventArgs e) // add category
		{
			int rec_index = lv_recepture.SelectedItems[0].Index, num;
			string new_category = "", t = lbl_add.Text;

			if (t == "add new")
            {
				//change GUI for input of new category item
				cmb_categories.DropDownStyle = ComboBoxStyle.Simple;
				cmb_categories.Text = "";
				label3.Text = "Type a name";
				lbl_add.Text = "insert";
			}
            else
            {
				if (t == "insert")
                {
					DialogResult result1 = MessageBox.Show
						(
						"Want you insert new category?",
						"Quetion",
						MessageBoxButtons.YesNo,
						0
						);

					if (cmb_categories.Text != "" && result1 == DialogResult.Yes)
                    {
                        new_category = cmb_categories.Text;
                        num = controller.TbCat.AddItem(new_category);					
						
						FormFunction.setBox(controller.Categories, cmb_categories);
						//cmb_categories.Text = "all";
						seeAll();
						lv_recepture.Items[lv_recepture.SelectedItems[0].Index].Selected = false;
						lv_recepture.Items[rec_index].Selected = true;

						DialogResult result2 = MessageBox.Show
						(
						"Want you change category of selected recipe?",
						"Quetion",
						MessageBoxButtons.YesNo,
						0
						);

						//id of new category
						int temp = controller.TbCat.getCatalog().
							FindIndex(n => n.name == new_category);						
						controller.TbCat.Selected = controller.TbCat.getCatalog()[temp].id;

						if (result2 == DialogResult.Yes)
						{
							num = controller.changeCategoryToAdded(controller.TbCat.Selected);							
							if (num != 0)
							{
								MessageBox.Show("Category is changed");
							}
							else
							{
								MessageBox.Show("Category is NOT changed");
							}
							Reload();
						}
					}					
                    
					//change GUI for input of new category item
                    cmb_categories.DropDownStyle = ComboBoxStyle.DropDown;
					label3.Text = "Categories` list";
					lbl_add.Text = "add_new";
					seeAll();
				}
            }
		}

		private void ingredientsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SimpleTable(1);
		}

		private void tecnologyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openTechnology();
		}

		private void AmountsTable()
		{
			if (tbMain.getSelected() == 0) return;
			int index = lv_recepture.SelectedItems[0].Index;
			AmountsController cntrl = new AmountsController(tbMain);
			cntrl.Info = controller.ReceptureStruct[index];
			InsertAmounts frm = new InsertAmounts(cntrl);
			frm.ShowDialog();
			Reload();
		}

		private void amountsEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (lv_recepture.SelectedItems.Count < 1) return;
			AmountsTable();
		}

		private void seeAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			seeAll();
		}

		private void aboutReceptureToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openReceptureEditor();
		}

		private void printToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int option, index;
			option = toolStripCmbPrint.SelectedIndex;
			index = -1;
			
			if (option == 0)
			{
				if (lv_recepture.Items.Count > 0)
				{
					index = lv_recepture.SelectedItems[0].Index;
					if (index < 0) option = 1;
				}
				else
					option = 1;
			}
			controller.Print(index, option);
		}

		private void helpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Print frm = new Print();
			frm.Show();

			const string PATH = "man\\user_manul_en.txt";
			frm.OpenFile1(PATH, "user_manual");
			frm.Button3_Enabled_status(false);
		}

		private void openReceptureEditor()
		{
			int id = 0; //id_recepture			

			if (lv_recepture.SelectedItems.Count < 1) return;
			if (exist_selected)
			{
				id = controller.ReceptureStruct[selected_recepture].getId();				
				if (tbMain.Selected != id)
				{
					tbMain.Selected = id;
				}
			}
			else
			{
				MessageBox.Show("Please, select any recepture from list");
				return;
			}
			tbMain.Id = id;
			NewReceptureController rec = new NewReceptureController(tbMain);
			rec.ReceptureInfo = controller.ReceptureStruct[selected_recepture];
			NewRecepture frm = new NewRecepture(rec);

			frm.ShowDialog();
			Reload();
		}

		private void openReceptureEditor(int temp)
		{
			int id = 0; //id_recepture			

			if (lv_recepture.SelectedItems.Count < 1) return;
			if (exist_selected)
			{
				id = controller.ReceptureStruct[selected_recepture].getId();
				if (tbMain.Selected != id)
				{
					tbMain.Selected = id;
				}
			}
			else
			{
				MessageBox.Show("Please, select any recepture from list");
				return;
			}
			tbMain.Id = id;
			NewReceptureController rec = new NewReceptureController(tbMain);
			rec.ReceptureInfo = controller.ReceptureStruct[selected_recepture];
			NewRecepture frm = new NewRecepture(rec);		
			
			frm.Show();
			frm.cmbCat_IndexChange(temp);			
			Reload();
		}

        private void helpOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
			string path = "https://github.com/jelena5316/cookingBook/blob/master/CookingBook/man/user_manul_en.txt";			
			System.Diagnostics.Process.Start(path);
		}

        private void onlineCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
			//string path = "C:\\Users\\user\\Documents\\instalacija.odt";
			string path = "https://www.thecalculatorsite.com/conversions/massandweight.php";
			System.Diagnostics.Process.Start(path);
		}
    }
}
