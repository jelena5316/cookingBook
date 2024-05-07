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
		bool exist_selected = false, reload_cat_mode = true, reload_rec_mode = true;
		int selected_recepture = -1;
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

			FormFunction.setBox(controller.Categories, cmb_categories); //categories` list			
			cmb_categories.SelectedIndex = 0;			
			//cmb_categories.SelectedIndex = -1; // when index is equel -1, then the text is empty string
			cmb_categories.Text = "all";
			reload_cat_mode = false;

            resetRecepturesList(controller.ReceptureStruct); //receptures` list
            reload_rec_mode = false;
            if (lv_recepture.Items.Count > 0)
                lv_recepture.Items[0].Selected = true;
            AutoCompleteRecepture(controller.Receptures);

            if (tbMain.Err_code > 0) //database error handling
			{
				MessageBox.Show($"{tbMain.Err_message}");
				tbMain.ResetErr_info();
			}
		}

		private void Reload()
		{
			controller.ReloadData();

			reload_cat_mode = true;
			FormFunction.setBox(controller.Categories, cmb_categories);
			reload_cat_mode = false;
			seeAll();
			AutoCompleteRecepture(controller.Receptures);
		}

		private void AutoCompleteRecepture(List<Item> rec) // for form load both reload
		{
			AutoCompleteStringCollection source = new AutoCompleteStringCollection();
			foreach (Item item in rec)
				source.Add(item.name);
			textBox1.AutoCompleteCustomSource = source;
			textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
			textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
		}

		private void resetRecepturesList(List<ReceptureStruct> list) //for loading both reloading list of receptures
		{
			reload_rec_mode = true;
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
			reload_rec_mode = false;
		}
		
		private void seeAll() //for reloading list of receptures, used in method 'reload()' 
		{
			cmb_categories.SelectedIndex = 0;
			cmb_categories.Text = "all";
			textBox1.Text = "";

			List<ReceptureStruct> full = controller.DisplayAll;
			resetRecepturesList(full);

			if (lv_recepture.Items.Count > 0)
				lv_recepture.Items[0].Selected = true;
		}


		/*
		 *  For items from lists selecting and filtring: 'recepture' and 'categories' -- by user
		 */

		//combo box with lists of categories
		private void cmb_categories_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (reload_cat_mode)
				return;
			if (cmb_categories.Items.Count < 1)
				return;
            if (cmb_categories.SelectedIndex == -1)
                return;
			
            int index = cmb_categories.SelectedIndex;
            resetRecepturesList(controller.SearchByCategory(index));

            if (lv_recepture.Items.Count > 0)
                lv_recepture.Items[0].Selected = true;

			reload_rec_mode = true;
			textBox1.Text = "";
			reload_rec_mode = false;
        }

		//text box for pattern of receptures name input
		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			if (reload_rec_mode)
				return;
			resetRecepturesList(controller.SearchByName(textBox1.Text));

			if (lv_recepture.Items.Count > 0)
				lv_recepture.Items[0].Selected = true;			
		}

		//list view for list of receptures
		private void lv_recepture_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (reload_rec_mode)
                return;

            controller.SelectedRecepture = -1;
			controller.ExistsSelected = false;

            //if (lv_recepture.Items.Count < 0)
            //    return;

            if (lv_recepture.SelectedItems.Count < 1)
                return;

            controller.SelectRecepture(
                lv_recepture.SelectedItems[0].Index,
                textBox1.Text
                );
        }

		/*
		 * Methods for works with recepture card
		 */

		//for editing selected item
		private void openReceptureEditor()
		{
			bool result = controller.editRec();
			if (!result)
			{
				MessageBox.Show("Please, select any recepture from list");
				return;
			}
			Reload();
		}

		//private void aboutReceptureToolStripMenuItem_Click(object sender, EventArgs e)
		//{
		//	openReceptureEditor();
		//	//при закрытие формы вылетает ошибка, так как  CategoriesController.categories.Count = 0
		//}

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

		//for adding new item
		private void addNew()
		{
			controller.addNewRec();
			Reload();
		}


		/*
		 * Methods for other events handlers
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

		private void SimpleTable(int opt)
		{
			controller.openFormToSimpleTable(opt);
		}

		private void AmountsTable()
		{
			int index = lv_recepture.SelectedItems[0].Index;

			controller.openAmountsForm(index);
			Reload();
		}


		/*
		 * Events handlers. Others controls: buttons, menu strip items
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

		//private void lbl_add_Click(object sender, EventArgs e) // add category
		//{
		//	int rec_index = lv_recepture.SelectedItems[0].Index, num;
		//	string new_category = "", t = lbl_add.Text;

		//	if (t == "add new")
  //          {
		//		//change GUI for input of new category item
		//		cmb_categories.DropDownStyle = ComboBoxStyle.Simple;
		//		cmb_categories.Text = "";
		//		label3.Text = "Type a name";
		//		lbl_add.Text = "insert";
		//	}
  //          else
  //          {
		//		if (t == "insert")
  //              {
		//			DialogResult result1 = MessageBox.Show
		//				(
		//				"Want you insert new category?",
		//				"Quetion",
		//				MessageBoxButtons.YesNo,
		//				0
		//				);

		//			if (cmb_categories.Text != "" && result1 == DialogResult.Yes)
  //                  {
  //                      new_category = cmb_categories.Text;
  //                      num = controller.TbCat.AddItem(new_category);					
						
		//				FormFunction.setBox(controller.Categories, cmb_categories);
		//				//cmb_categories.Text = "all";
		//				seeAll();
		//				lv_recepture.Items[lv_recepture.SelectedItems[0].Index].Selected = false;
		//				lv_recepture.Items[rec_index].Selected = true;

		//				DialogResult result2 = MessageBox.Show
		//				(
		//				"Want you change category of selected recipe?",
		//				"Quetion",
		//				MessageBoxButtons.YesNo,
		//				0
		//				);

		//				//id of new category
		//				int temp = controller.TbCat.getCatalog().
		//					FindIndex(n => n.name == new_category);						
		//				controller.TbCat.Selected = controller.TbCat.getCatalog()[temp].id;

		//				if (result2 == DialogResult.Yes)
		//				{
		//					num = controller.changeCategoryToAdded(controller.TbCat.Selected);							
		//					if (num != 0)
		//					{
		//						MessageBox.Show("Category is changed");
		//					}
		//					else
		//					{
		//						MessageBox.Show("Category is NOT changed");
		//					}
		//					Reload();
		//				}
		//			}					
                    
		//			//change GUI for input of new category item
  //                  cmb_categories.DropDownStyle = ComboBoxStyle.DropDown;
		//			label3.Text = "Categories` list";
		//			lbl_add.Text = "add_new";
		//			seeAll();
		//		}
  //          }
		//}

		private void ingredientsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SimpleTable(1);
		}

		private void tecnologyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			controller.openTechnologyForm();
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
			//при закрытие формы вылетает ошибка, так как  CategoriesController.categories.Count = 0
		}

		private void printToolStripMenuItem_Click(object sender, EventArgs e)
		{
            int option;
            option = toolStripCmbPrint.SelectedIndex;            
            controller.PrintInto(option);
		}

		private void helpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			controller.openManual();
		}

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Reload();
        }

        private void helpOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
			controller.openManualOnline();
		}

        private void onlineCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
			controller.openOnlineCalculator();			
		}
    }
}
