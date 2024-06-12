/*
 * to calculate recipe and manage coefficients` catalogue
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Globalization;


namespace MajPAbGr_project
{
	public partial class Recipes : Form
	{
		bool load_mode;
		
		private List<Item> receptures; // formulation
		public List<Element> recipes;  // recipes (coeifficients)
		public List <Element> elements;
		RecipesController controller;
		tbReceptureController tb;
		tbRecipeController tbCoeff;
		CalcFunction calc;       

		ComboBox combo;
		ComboBox recipe;
		ComboBox categories;
		ListView list;

		CultureInfo current;

		public Recipes(int id)
		{
			InitializeComponent();
			controller = new RecipesController(id);
			tb = controller.TbMain();
			tbCoeff = controller.TbCoeff();
			calc = controller.Calc;
			receptures = controller.getCatalog();

			combo = comboBox1;
			recipe = cmbCoeff;
			categories = cmbCat;
			list = listView1;
		}

		public Recipes()
        {
			InitializeComponent();
		}

		public RecipesController Controller
        {
			get { return controller; }
        }

		private void AutocompleteRecipeName()
		{
			AutoCompleteStringCollection source = new AutoCompleteStringCollection();
			foreach (Element el in recipes) source.Add(el.Name);
			txb_new_recipe.AutoCompleteCustomSource = source;
			txb_new_recipe.AutoCompleteMode = AutoCompleteMode.Suggest;
			txb_new_recipe.AutoCompleteSource = AutoCompleteSource.CustomSource;
		}

		private void Form1_Load(object sender, EventArgs e) 
		{
			load_mode = true;
			// list of receptures setting            
			tbCoeff.Recepture = tb.Selected;			
			int index = FormFunction.ChangeIndex(controller.getCatalog(), tb.Selected);
			FormFunction.setBox(controller.getCatalog(), combo);

            // list of categories	
            List<Item> category = controller.CategoriesController.TbCat.getCatalog();
            FormFunction.setBox(category, categories);
			load_mode = false;			
			combo.SelectedIndex = index;
			controller.CategoriesController.ExistsSelected = true;

			bool result = setSelectedIndex(load_mode, index);
			if (result == true)
				return;
			combo_onReceptureSelected(index);


			//lokalization setting
			current = controller.Current();            
			this.Text += " " + controller.InfoLocal();            

			btn_insert.Enabled = false;
			txb_new_recipe.Enabled = false;

			//information about connection
			int code = tb.Error_code;
			if (code > 0)
			{
				MessageBox.Show(tb.Error_message);
			}
		}

		/*
		 * Lokalization
		 */

		private void changeLocale(string locale)
		{
			controller.setNFI(locale);
			current = controller.Current();
			this.Text = "Recepture " + controller.InfoLocal();
			localizacijaToolStripMenuItem.Text = controller.CurrentName();            
		}

		private void uSToolStripMenuItem_Click(object sender, EventArgs e)
		{
			changeLocale("us-US");            
		}

		private void lVToolStripMenuItem_Click(object sender, EventArgs e)
		{
			changeLocale("lv-LV");
		}

		private void rUToolStripMenuItem_Click(object sender, EventArgs e)
		{
			changeLocale("ru_RU");            
		}

		/*
		 * Categories of receptures
		 */

		private void cmbCat_SelectedIndexChanged(object sender, EventArgs e) // categories of recepture selecting
		{
			controller.cmbCat_onSelectedIndexChanged(cmbCat.SelectedIndex);
			//controller.CategoriesController.Catalog.SelectedCatIndex = cmbCat.SelectedIndex;
			//this.Text = controller.CategoriesController.Catalog.SelectedCatIndex.ToString();			
		}

		private void lbl_info_Click(object sender, EventArgs e) // new form with info about selected recepture
		{
			int index = combo.SelectedIndex;
			//controller.CategoriesController.SelectedRecepture = index;
			bool result = controller.AboutRecepture();
			if (!result)
				MessageBox.Show("Error!");
		}


		/*
		 * Recepture's selecting: index checking, data about recepture and formulation showing
		 */
		private bool setSelectedIndex(bool mode, int rec_index) // improving index in case list is not full
		{
			if (mode == true)
				return mode;

			rec_index = combo.SelectedIndex;

			if (lbl_SeeAll.Text == "all")
				rec_index = controller.ImpoveIndex(rec_index);
			else
			{
				controller.CategoriesController.ExistsSelected = true;
				controller.CategoriesController.SelectedRecepture = rec_index;
			}
			return mode;
		}

		public int fillSubCatalog()  // recipes of recepture, combobox 'cmbCoef'
		{
			recipes = controller.Recipes;
			FormFunction.FillCombo(recipes, recipe);
			if (recipe.Items.Count > 0)
			{
				recipe.SelectedIndex = 0;				
			}
			else
			{
				recipe.Text = "add recepture (g)";
				lbl_koef.Text = calc.Coefficient.ToString();
			}
			return recipes.Count;
		}

		private string OutputIngredients(int rec_index) // ingredients: formulation
		{
			int count;
			string text = "";
			List<string> amounts;

			amounts = controller.changeSubcatalog(rec_index);
			elements = controller.Amounts; // amounts

			//Output recepture and recipes;
			FormFunction.FillListView(elements, amounts, listView1);
			count = fillSubCatalog(); // fill the 'cmbCoef'
			if (count == 0)
			{
				calc.Coefficient = 1;
				text = "1";
			}
			columnHeader2.Text = "Amounts (%)";
			return text;
		}

		private void OutputInfoAboutRecepture(int rec_index)
		{
			//Output info about recepture
			txbRecipe.Text = "";
			lbl_info.Text = controller.AboutRecepture(rec_index);
			int cat_index = controller.cmbCatIndex(rec_index);
			if (cmbCat.Items.Count > cat_index)
				cmbCat.SelectedIndex = cat_index;
		}

		private void combo_onReceptureSelected(int index) // used for form loading and reloading too
		{
			index = controller.CategoriesController.SelectedRecepture;
			lbl_koef.Text = OutputIngredients(index);
			OutputInfoAboutRecepture(index);
			AutocompleteRecipeName(); // table Recipe 
		}


		/*
		 * Receptures (formulation) and recipes of selected receptures selecting using comboboxes
		 */
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool result = true;
			int index = -1;

			result = setSelectedIndex(load_mode, index);
			if (result == true)
				return;

			combo_onReceptureSelected(index);
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbCoeff.SelectedIndex < 0) 
			{
				lbl_koef.Text = "not number";
				return;
			}
			else
			{
				int index = recipe.SelectedIndex;
				calc.Coefficient = recipes[index].Amounts;                
				lbl_koef.Text = string.Format("{0:f2}", calc.Coefficient);                
			}
		}


		/*
		 * Works with selected data
		 */

		private void button1_Click(object sender, EventArgs e) // recalc recepture
		{
			if (recipes.Count < 1) return;
			double coeff = calc.Coefficient;
			if (coeff == 1) return;

			int index;                     
			List<string> t = calc.FormatAmounts();
			for (index = 0; index < t.Count; index++)
			{
				list.Items[index].SubItems[2].Text = t[index];
			}
			columnHeader2.Text = "Amounts (g)";
			
		}

		/*
		 * new recipe (coefficient)
		*/
		 private void button2_Click(object sender, EventArgs e) // calc new recipe
		 {
			 if (string.IsNullOrEmpty(txb_coeff.Text)) return;             

			int index = 0;
			List <string> amounts = controller.button2_onClick(txb_coeff.Text);
			if (amounts == null)
			{
				txb_coeff.Text = "";
				return;
			}   

		   for (index=0; index < amounts.Count; index++)
			{
				list.Items[index].SubItems[2].Text = amounts[index];
			}
			txb_new_recipe.Enabled = true;
			btn_insert.Enabled = true;
		 }

		private void btn_insert_Click(object sender, EventArgs e)
		 {
			string count, str_coeff;        

			count = tbCoeff.Count
			   ($"Select count (name) from Recipe where name = '{txb_new_recipe.Text}';");

			if (string.IsNullOrEmpty(txb_new_recipe.Text))
				return;
			if (count == "0")
			{
				str_coeff = calc.Coefficient.ToString();                
				tbCoeff.insertNewRecipe(txb_new_recipe.Text, str_coeff);
				fillSubCatalog();
				btn_insert.Enabled = false;
			}
			else
			{
				MessageBox.Show($"Recipe {txb_new_recipe.Text} already exists");
			}
		}

		private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Reload();
		}

		private void Reload()
		{
			/*Прочитать из базы данных обновленные данные (вместе со старыми)*/
			// 1. из таблицы рецептур: номера и наименования;
			// номер и наименования с номерами категорий, технологий, главного вида сырья,
			// наименования автора и источника, адреса в сети, описание каждой рецептуре
			// 2. из прочих таблиц -- наименования категории, технологии, вида сырья
			// 3. из таблиц категорий рецептур -- их номера и наименования

			/*Сохранить в соответствующих переменных, структурах и объектах*/
			// 1. Списки рецептур и их категорий -- структура Item: номера и наименования
			// 2. Список структур ReceptureStruct в объектах класса RecStruct -- см. п. 1 в первом абзаце.

			/*Переменные и методы или указатели на них в классе Recipes*/
			// 1. tb, controller.TbMain -- содержит методы и поля для чтения из базы данных -- setCatalog() и resetCatalog() -- 
			// и хранения прочитанных данных -- поле catalog, свойство getCatalog(), структуры Item
			// 2.controller.CategoriesController.TbCat -- доступ к методам контроллера таблицы категорий
			// 3. controller.CategoriesController.Catalog -- содержит методы и поля для работы со списком
			// одноименных структур в объекте класса RecCatalog и свойство для доступа к объекту, методы
			// 4. ReceptureStruct, ReadCatalog () -- свойство со списком структур ReceptureStruct
			// и метод для чтения из данных из базы данных
			// 5. controller.CategoriesController.Catalog.Categories  -- свойство,
			// помогает сохранить обновлённый список категорий в поле класса RecCatalog

			/*
			 * Загрузить обновленные данные
			 * в комбинированный поля в режиме load_mode
			 * и  выбрать нужный пункт;
			 * обновить прочие данные, выводимые в форме, если такове есть
			 */

			/*
			 * Разобраться, что делать, если отображалась выборка
			 * и если число категорий или рецептур изменилось
			 */

			List<Item> rec, cat;
			int temp = combo.SelectedIndex; // will be checked range in case recepture would be deleted

			
			rec = controller.ReloadData();
			cat = controller.CategoriesController.TbCat.getCatalog();

			load_mode = true;
			FormFunction.setBox(rec, combo); // getting data from table controller, outputing
			FormFunction.setBox(cat, cmbCat);			
			load_mode = false;
            // output into comboboxes

            if (temp < combo.Items.Count)
                combo.SelectedIndex = temp;
            else if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
            // select recepture after range checking

            columnHeader2.Text = "Amounts (%)"; // refreshing other form data

            bool result = setSelectedIndex(load_mode, temp);
            if (result == true)
                return;
            combo_onReceptureSelected(temp);
        }

		/*
		 * Print to file 
		 */
		private void printToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string file, name, output = "", mesuare;

			List<string> info,
				ingredients;

			info  = new List<string>();
			ingredients = new List<string>();           

			name = comboBox1.Text;
			if (columnHeader2.Text == "Amounts (%)")
			{
				mesuare = "(in %)";              
			}
			else
			{
				mesuare = $" (on {cmbCoeff.SelectedItem.ToString()})";               
			}

			if (!string.IsNullOrEmpty(comboBox1.Text))
			{
				output = $"{name} {mesuare}";
				info.Add(output);
			}

			if (list.Items.Count > 1)
			{
				int k = 0;
				for (k = 0; k < list.Items.Count - 1; k++)
				{
					output = string.Format("{0, -20}\t{1, -8}\t{2, -8}", list.Items[k].Text, list.Items[k].SubItems[1].Text, list.Items[k].SubItems[2].Text);                   
					ingredients.Add(output);
				}
				output = $"-----\n {list.Items[k].SubItems[1].Text}\t({list.Items[k].SubItems[2].Text})";
				ingredients.Add(output);
			}
			else
			{
				output = "Ingredient amounts are unknown";
				ingredients.Add(output);
			}

			if (!string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()))
				file = comboBox1.SelectedItem.ToString();
			else file = "recipe";

			PrintController print = new PrintController(file);
			print.Info = info;
			print.Ingredients = ingredients;
			print.PrepareRecipeIngredientsOutput();
			print.PrintRecipe();                   
		}
		/*
		 * end of printing
		 */

		private void openDbEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EditDB frm = new EditDB(new EditDBController());
			frm.Show();
		}             

		private void label2_Click(object sender, EventArgs e)
		{
			txb_coeff.Text = "";                     
		}

		private void label1_Click(object sender, EventArgs e)
		{
			txb_new_recipe.Text = "";   
		}
				

		/*
		 * edit name of coefficient
		 */

		private void btn_edit_Click(object sender, EventArgs e) 
		{
			int index, ind;
			string old_name, name, message;

			index = recipe.SelectedIndex;
			if (index < 0) return;
			if (recipes.Count < 1) return;

			old_name = recipe.Text;
			name = txbRecipe.Text;
			ind = controller.btn_edit_onClick(name, index);
			 switch (ind)
			 {
				case 0:
					message =$"No change";
					break;
				case -1:
					message = "Data base error";
					break;
				default:
					message = $"Recipe's name is changed from '{old_name}' to '{name}'";
					Reload();
					recipe.SelectedIndex = index;
					break;
			 }
			MessageBox.Show(message);
			fillSubCatalog();
			AutocompleteRecipeName();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (recipe.Items.Count < 1) return;
			if (recipe.SelectedIndex < 0) return;
			if (recipes.Count > 1) return;

			DialogResult result = MessageBox.Show(
					"Do delete recipe?", "",
					MessageBoxButtons.OKCancel);

			if (result == DialogResult.OK)
			{
				int id = 0, ind = 0, index = recipe.SelectedIndex;             
			   
				ind = controller.btn_remove_onClick(
					recipe.SelectedIndex,
					combo.SelectedIndex
					);
				if (ind > 0)
				{
					id = tbCoeff.Selected;
					MessageBox.Show($"Recipe {id} is deleted");                
					Reload();
				}
				else
				{
					MessageBox.Show("Nothing is deleted");
				}                           
			}
			else
			{
				MessageBox.Show("Ok");
			}
		}

		// set calcualtion base
		private void cmb_option_SelectedIndexChanged(object sender, EventArgs e)
		{
		   controller.CalcBase = (CalcBase)cmb_option.SelectedIndex;
		}

        private void label4_Click(object sender, EventArgs e)
        {
			//
        }

        private void lbl_SeeAll_Click(object sender, EventArgs e)
        {
			if (lbl_SeeAll.Text == "select")
            {
				lbl_SeeAll.Text = "all";
				controller.SelectByCategory(cmbCat.SelectedIndex);
				List<Item> list = controller.CategoriesController.TbCat.Subcatalog;
				FormFunction.FillCombo(list, combo);
				if (combo.Items.Count > 0)
					combo.SelectedIndex = 0;
				cmbCat.Enabled = false;
            }
            else
            {
				lbl_SeeAll.Text = "select";
				controller.UnDoSelectingByCategory();
				FormFunction.FillCombo(controller.getCatalog(), combo);
				if (combo.Items.Count > 0)
					combo.SelectedIndex = 0;
				cmbCat.Enabled = true;
			}
        }

    }
}
