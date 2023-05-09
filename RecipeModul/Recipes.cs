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
        int category;       
        private List<Item> receptures;
        public List<Element> recipes;
        public List <Element> elements;
        RecipesController controller;
        tbReceptureController tb;
        tbRecipeController tbCoeff;
        CalcFunction calc;
        CalcBase calcBase = 0;

        ComboBox combo;
        ComboBox recipe;
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
            list = listView1;
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
            // list of receptures setting            
            tbCoeff.Recepture = tb.Selected;
            int index = Class1.ChangeIndex(controller.getCatalog(), tb.Selected);
            Class1.setBox(controller.getCatalog(), combo);
            combo.SelectedIndex = index;
            
            fillSubCatalog(); // table Recipe;
            AutocompleteRecipeName(); // table Recipe            

            //lokalization setting
            current = controller.Current();            
            this.Text += " " + controller.InfoLocal();
            
            
            btn_insert.Enabled = false;
            txb_new_recipe.Enabled = false;
        }

        /*
         * Lokalizacija
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
         * Konec lokalizaciji
         */

        public int fillSubCatalog()  // recipes of recepture, combobox
        {
            recipes = controller.Recipes;
            Class1.FillCombo(recipes, recipe);            
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbRecipe.Text = "";
            
            int index = combo.SelectedIndex;
            int selected = controller.getCatalog()[index].id;            
            tbCoeff.Recepture = controller.getCatalog()[index].id;
            controller.Selected = controller.getCatalog()[index].id;
            controller.setSubcatalog(index);

            int count = fillSubCatalog(); // fill the combobox2              
                     
            elements = controller.Amounts; // amounts            
            calc.setAmounts(elements); // сохраняет и cуммирует величины

            //InputRecepture(elements);            
            List<string> amounts = calc.FormatAmounts
                (calc.getAmounts(), calc.getTotal());
            Class1.FillListView(elements, amounts, listView1);

            if (count == 0)
            {
                calc.Coefficient = 1;
                lbl_koef.Text = "1";
            }
            //Info about recepture
            category = int.Parse(tb.getById("id_category", selected));            
            string info;
            info = $"{tb.getName(index)}: id {tb.getSelected()}, category ({category})\n";
            if (recipes.Count > 0)
            {
                info += $"recipes \n";
                for (int k = 0; k < recipes.Count; k++)
                {
                    info += $"  {recipes[k].Name} ({recipes[k].Amounts}) \n";
                }
            }
            lbl_info.Text = info;            

            columnHeader2.Text = "Amounts (%)";
            AutocompleteRecipeName(); // table Recipe 
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
                controller.Calc.Coefficient = recipes[index].Amounts;                
                lbl_koef.Text = string.Format("{0:f2}", controller.Calc.Coefficient);
                //txbRecipe.Text = recipes[index].Name;
            }
        }

        private void button1_Click(object sender, EventArgs e) // recalc recepture
        {
            if (recipes.Count < 1) return;
            double coeff = controller.Calc.Coefficient;
            if (coeff == 1) return;

            int index;                     
            List<string> t = controller.Calc.FormatAmounts(); //посчитали и придали вид
            for (index = 0; index < t.Count; index++)
            {
                list.Items[index].SubItems[2].Text = t[index];
            }
            columnHeader2.Text = "Amounts (g)";
            
        }

        /*****************************************************************************
         * новый рецепт, навигация        
        *******************************************************************************/
         private void button2_Click(object sender, EventArgs e) // calc new recipe
         {
             if (string.IsNullOrEmpty(txb_coeff.Text)) return;            

            int index = 0;
            List <string> amounts = controller.button1_onClick(txb_coeff.Text);
            if (amounts == null) return;            

           for (index=0; index < amounts.Count; index++)
            {
                list.Items[index].SubItems[2].Text = amounts[index];
            }
            txb_new_recipe.Enabled = true;
            btn_insert.Enabled = true;
         }

        private void btn_insert_Click(object sender, EventArgs e)
         {
            string count;
            calc = controller.Calc;

            tbRecipeController recipe = new tbRecipeController
                ("Recipe", cmbCoeff.SelectedIndex, controller.TbMain().Selected);
            count = recipe.Count
               ($"Select count (name) from Recipe where name = '{txb_new_recipe.Text}';");
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reload();
        }
        private void Reload()
        {
            int temp = comboBox1.SelectedIndex;
            tb.setCatalog();
            Class1.setBox(tb.getCatalog(), combo);
            comboBox1.SelectedIndex = temp;
            columnHeader2.Text = "Amounts (%)";            
            //comboBox1.Text = comboBox1.SelectedItem.ToString();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //tbReceptureController cntrl = new tbReceptureController("Recepture");
            //NewRecepture frm = new NewRecepture(cntrl);
            //frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e) // edit recepture
        {
            //if (tb.getSelected() == 0) return;
            //if (listView1.Items.Count < 1) return;
            //int selected = tb.getSelected();

            //tbReceptureController cntrl = new tbReceptureController("Recepture", selected, category);
            //NewRecepture frm = new NewRecepture(cntrl);
            //frm.ShowDialog();
           
            /*
            Код вставки обработчика (копия из дизайнера):
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click_1);
            */           
        }

        private void amountsEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count < 1) return;
            AmountsTable();            
        }

        private void AmountsTable()
        {
            if (tb.getSelected() == 0) return;            
            AmountsController cntrl = new AmountsController(tb);
            InsertAmounts frm = new InsertAmounts(cntrl);
            frm.ShowDialog();
            calc.Coefficient = frm.Calc.Coefficient;
            Reload();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SimpleTable(2);
        }

        private void ingredientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimpleTable(1);
        }

        private void SimpleTable(int opt)
        {
            tbIngredientsController cntrl = new tbIngredientsController(opt);
            Ingredients frm = new Ingredients(cntrl);
            frm.Show();
        }

        //Print to file  
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

            if (listView1.Items.Count > 1)
            {
                int k = 0;
                for (k = 0; k < listView1.Items.Count - 1; k++)
                {
                    output = $"{listView1.Items[k].Text} {listView1.Items[k].SubItems[1].Text}";
                    ingredients.Add(output);
                }
                output = $"-----\n {listView1.Items[k].SubItems[1].Text}";
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
        // konec 'print to file'

        private void recipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int lbl_info_y = lbl_info.Location.Y;
            this.lbl_info.Location = new Point(318, this.cmbCoeff.Location.Y);
            txb_new_recipe.Text = "";
            txb_coeff.Text = "";            
            
            RecipeEditor frm = new RecipeEditor(this, controller.TbMain(), calc, comboBox1.SelectedIndex);        
            frm.ShowDialog();

            this.lbl_info.Location = new Point(318, lbl_info_y);           
            button2.Enabled = true;
        }

        private void openDbEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditDB frm = new EditDB();
            frm.Show();
        }

        private void technologyToolStripMenuItem_Click(object sender, EventArgs e) // open chains` of technology cards editor
        {
            Chains frm;
            ChainsController controller;
            int selected, id_technology, count;// id of recepture and of technology;
            // проверить выбранный в списке                   
            selected = tb.getSelected();

            controller = new ChainsController();
            controller.Recepture = selected;

            //id_technology
            count = tb.SelectedCount("Recepture", "id_technology", selected); // dos recepture contain any technology
            if (count == 1)
            {
                id_technology = int.Parse(tb.getById("id_technology", selected));                
                controller.Technology = id_technology;                 
            }  
            
            frm = new Chains(controller);
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
                

        // edit name of coefficient
        private void btn_edit_Click(object sender, EventArgs e) 
        {
            int index, ind;
            string old_name, name, message;

            index = recipe.SelectedIndex;
            if (index < 0) return;
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (recipe.Items.Count < 1) return;
            if (recipe.SelectedIndex < 0) return;

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

        private void cmb_option_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcBase = (CalcBase)cmb_option.SelectedIndex;
            controller.CalcBase = (CalcBase)cmb_option.SelectedIndex;
        }
    }
}
