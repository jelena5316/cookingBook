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
		bool exist_selected = false;
		int selected_recepture = -1;
		int pragma;
		List<string> list, list_of_categories;
		List<ReceptureStruct> full;
		CategoriesController controller; // включает в себя FormMainController
		tbReceptureController tbMain; // указатель на controller.TbMain

		public Categories()
		{
			InitializeComponent();
			controller = new CategoriesController();
			tbMain = controller.TbMain;
			list = new List<string>();
			for (int k = 0; k < controller.Receptures.Count; k++)
				list.Add(controller.Receptures[k].name);
			list_of_categories = new List<string>();
			for (int k = 0; k < controller.Receptures.Count; k++)
				list_of_categories.Add(controller.ReceptureStruct[k].getCategory());

			pragma = 0;
		}

		private void Categories_Load(object sender, EventArgs e)
		{
			lv_recepture.Columns.Add("Name");
			lv_recepture.Columns.Add("Category");
			lv_recepture.Columns.Add("Main");
			lv_recepture.Columns.Add("Author");
			lv_recepture.Columns.Add("Source");

			Class1.setBox(controller.Categories, cmb_categories);
			resetRecepturesList(controller.ReceptureStruct);
			if (lv_recepture.Items.Count > 0)
				lv_recepture.Items[0].Selected = true;
			cmb_categories.Text = "all";
			pragma = 1;

			//print			
			toolStripCmbPrint.SelectedIndex = 0;

			AutoCompleteStringCollection source = new AutoCompleteStringCollection();
			foreach (Item item in controller.Receptures)
				source.Add(item.name);
			textBox1.AutoCompleteCustomSource = source;
			textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
			textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
		}

		/*
		 *  Методы для обработчиков событий
		 */


		private int CheckTbMainSelected(int min)
		{
			if (tbMain.Selected == 0)
				tbMain.Selected = min;
			if (lv_recepture.SelectedItems == null)
				lv_recepture.Items[0].Selected = true;
			return tbMain.Selected;
		}

		private void openRecipesEditor()
		{
			Recipes frm = new Recipes(CheckTbMainSelected(controller.getMinIdOfReceptures()));
			frm.Show();
		}

		private void openTechnology()
		{
			int selected, id_technology, count;// id of recepture and of technology;
											   // проверить выбранный в списке  
			selected = CheckTbMainSelected(controller.getMinIdOfReceptures());
			Technology frm;

			//id_technology
			int index = selected_recepture == -1 ? 0 : selected_recepture;
			id_technology = controller.ReceptureStruct[index].getIds()[1];
			id_technology = id_technology < 0 ? 0 : id_technology;

			frm = new Technology(id_technology);
			frm.Show();

			//        count = tbMain.SelectedCount("Recepture", "id_technology", selected); // dos recepture contain any technology
			//        if (count == 1)
			//        {
			//            id_technology = int.Parse(tbMain.getById("id_technology", selected));				
			//        }
			//        else
			//        {
			//id_technology = 0;
			//        }


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
			//controller = new CategoriesController();
			//tbMain = controller.TbMain;
			//list = new List<string>();
			//for (int k = 0; k < controller.Receptures.Count; k++)
			//	list.Add(controller.Receptures[k].name);
			//pragma = 0;

			list.Clear();
			for (int k = 0; k < controller.Receptures.Count; k++)
				list.Add(controller.Receptures[k].name);
			pragma = 0;

			tbIngredientsController tbCat = controller.TbCat;
			tbCat.resetCatalog();
			controller.Categories = tbCat.getCatalog();
			Class1.setBox(controller.Categories, cmb_categories);

			controller.setReceptures();
			controller.ReceptureStruct.Clear();
			controller.setFields();
			resetRecepturesList(controller.ReceptureStruct);
			if (lv_recepture.Items.Count > 0)
				lv_recepture.Items[0].Selected = true;
			cmb_categories.Text = "all";
			pragma = 1;

			AutoCompleteStringCollection source = new AutoCompleteStringCollection();
			foreach (Item item in controller.Receptures)
				source.Add(item.name);
			textBox1.AutoCompleteCustomSource = source;
			textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
			textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
		}

		/*
		 * Обработчики событий
		 */
		private void lv_recepture_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lv_recepture.Items.Count < 0)
				exist_selected = false;

			if (lv_recepture.SelectedItems.Count > 0)
			{
				tbMain.setSelected(lv_recepture.SelectedItems[0].Index);
				selected_recepture = lv_recepture.SelectedItems[0].Index;
				exist_selected = true;
			}
			else
			{
				selected_recepture = 0;
				exist_selected = false;
			}
		}
		private void cmb_categories_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (pragma == 0) return;
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
			if (lv_recepture.Items.Count > 0)
				lv_recepture.Items[0].Selected = true;
			//pragma = 1;
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
				List<ReceptureStruct> selected = full.FindAll(p => p.getName().Contains(textBox1.Text));
				resetRecepturesList(selected);
			}
			if (lv_recepture.Items.Count > 0)
				lv_recepture.Items[0].Selected = true;
		}

		private void searchByName(string text) { }

		private void lv_recepture_DoubleClick(object sender, EventArgs e)
		{
			openReceptureEditor();
		}



		/*
		 * Others controls: buttons, menu strip items
		 */
		private void button1_Click(object sender, EventArgs e) // recipes editor
		{
			addNew();
		}

		private void openDbEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EditDB frm = new EditDB();
			frm.Show();
		}

		private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Reload();
		}

		private void recipeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openRecipesEditor();
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			SimpleTable(2);
		}

		private void ingredientsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SimpleTable(1);
		}

		private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			addNew();
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openReceptureEditor();
		}


		private void tecnologyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openTechnology();
		}

		/*
		 * Context menu
		 */
		private void recipesEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openRecipesEditor();
		}

		private void printToolStripMenuItem1_Click(object sender, EventArgs e)
		{
            if (lv_recepture.SelectedItems.Count < 1) return;

            int index = lv_recepture.SelectedItems[0].Index;
            Print frm = new Print();
            frm.Show();
            frm.richTextBox1.Lines = controller.PrintInfo(index);
        }

		private void editToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			openReceptureEditor();
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

		private void amountsOfIngredientsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (lv_recepture.SelectedItems.Count < 1) return;
			if (tbMain.Selected < 1) tbMain.setSelected(lv_recepture.SelectedItems[0].Index);
			AmountsTable();
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

        private void openReceptureEditor()
		{
			int id = 0; //id_recepture
			if (lv_recepture.SelectedItems.Count < 1) return;
			if (exist_selected)
			{
				id = controller.ReceptureStruct[lv_recepture.SelectedItems[0].Index].getId();
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

	}
}
