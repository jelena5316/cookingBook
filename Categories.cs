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
	public partial class Categories : Form
	{
		bool exist_selected = false;
		int selected_recepture = 0;
		int pragma;
		List<string> list;		
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
			lv_recepture.Columns.Add("Description");

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
				string[] arr = selected[k].getData();
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
				selected_recepture = lv_recepture.SelectedItems[0].Index;
				exist_selected = true;
            }
            else
            {
				selected_recepture = 0;
				exist_selected = false;
			}
		}

		private void openReceptureEditor()
        {
			string table = "Recepture";
			int id = 0; //id_recepture
			int[] ids;
			int category = 0;
			int technology = 0;

			if (exist_selected)
			{
				id = tbMain.Selected; //id = tbMain.Receptures[lv_recepture.SelectedItems[0].Index].id; //получаем id рецептуры
			}
			else
			{
				MessageBox.Show("Please, select any recepture from list");
				return;
			}
			ids = controller.RecepturesStruct[selected_recepture].getIds();

			//category			
			category = ids[0];

			//technology
			technology = ids[1];		
	
			tbReceptureController cntrl = new tbReceptureController(table, id, category, technology);
			NewRecepture frm = new NewRecepture(cntrl);
			frm.Show();
		}

		private void lv_recepture_DoubleClick(object sender, EventArgs e)
		{
			openReceptureEditor();
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
				string[] arr = selected[k].getData();
				items = new ListViewItem(arr[0]);
				items.Tag = selected[k].getId();

				for (int q = 1; q < arr.Length; q++)
				{
					items.SubItems.Add(arr[q]);
				}
				lv_recepture.Items.Add(items);
			}
		}

		/*
		 * Others controls: buttons, menu strip items
		 */
        private void button1_Click(object sender, EventArgs e)
        {
			if (tbMain.Selected == 0)
				tbMain.Selected = int.Parse(tbMain.dbReader("select min(id) from Recepture;")[0]);
			if (lv_recepture.SelectedItems == null)
				lv_recepture.Items[0].Selected = true;
			
			Recipes frm = new Recipes(tbMain.Selected);			
			frm.Show();			
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

		private void Reload()
        {
			list.Clear();
			controller.setReceptures();
			for (int k = 0; k < controller.Receptures.Count; k++)
				list.Add(controller.Receptures[k].name);
			pragma = 0;

			tbIngredientsController tbCat = controller.TbCat;
			tbCat.resetCatalog();
			controller.Categories = tbCat.getCatalog();
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

		// categories and ingredients
		private void SimpleTable(int opt)
		{
			tbIngredientsController cntrl = new tbIngredientsController(opt);
			Ingredients frm = new Ingredients(cntrl);
			frm.Show();
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
			tbReceptureController cntrl = new tbReceptureController("Recepture");
			NewRecepture frm = new NewRecepture(cntrl);
			frm.ShowDialog();
		}

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
			openReceptureEditor();
		}
        private void tecnologyToolStripMenuItem_Click(object sender, EventArgs e)
        {
			if (tbMain.Selected == 0)
				tbMain.Selected = int.Parse(tbMain.dbReader("select min(id) from Recepture;")[0]);
			if (lv_recepture.SelectedItems == null)
				lv_recepture.Items[0].Selected = true;

			Chains frm;
			ChainController controller;
			int selected, id_technology, count;// id of recepture and of technology;
											   // проверить выбранный в списке                   
			selected = tbMain.getSelected();

			controller = new ChainController();
			controller.Recepture = selected;

			//id_technology
			count = tbMain.SelectedCount("Recepture", "id_technology", selected); // dos recepture contain any technology
			if (count == 1)
			{
				id_technology = int.Parse(tbMain.getById("id_technology", selected));
				controller.Technology = id_technology;
			}

			frm = new Chains(ref controller);
			frm.Show();
		}

		/*
		 * Context menu
		 */
		private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
			if (lv_recepture.SelectedItems.Count < 1) return;
				
			int index = lv_recepture.SelectedItems[0].Index;			
			Form2 frm = new Form2();
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
			cntrl.Info = controller.RecepturesStruct[index];
			InsertAmounts frm = new InsertAmounts(cntrl);
			frm.ShowDialog();			
			Reload();

			// проследить, чтобы передался новый (верный) коэфициент!
			// при смене элемента комбинированого поля вызывается метод заполнения списочного представлеяни,
			// а до него -- форматирования числа, которое использует внутри себя метод ReCalc()
			// с "неверным" коэфициентом,
			// причина -- в неверно выбранном объекте для вызова методов класса CalcFunction.
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
    }
}
