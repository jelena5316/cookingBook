/*
 * to manage work with recepture catalogue using form "Categories"
 * and methods of classes tbReceptureController, tbIngredientsController and tbTechnologyController
 */

using System;
using System.Collections.Generic;
using System.Linq;


namespace MajPAbGr_project
{
    
    public class RecCatalog
    {
        List<ReceptureStruct> rec_struct, full, selected, displayed;
        List<Item> categories;
        tbIngredientsController tbCat;
        bool exist_selected = false;
        int selected_rec_index = -1, selected_cat_index = -1, selected_item_index = -1;
        // 'selected_item_index' -- an index of recepture in a current list, full or selected


        public RecCatalog (List<ReceptureStruct> list)
        {
            rec_struct = list;
            full = null;
            selected = null;
            displayed = null;
            categories = null;
        }

        public RecCatalog()
        {
            full = null;
            selected = null;
            displayed = null;
            categories = null;
        }

        //Properties
        public List<ReceptureStruct> ReceptureStruct
        {
            get { return rec_struct; }
        }

        public List<Item> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        public tbIngredientsController TbCat
        {
            get { return tbCat; }
            set { tbCat = value; }
        }

        public List <ReceptureStruct> setDisplayed
        {
            get { return displayed; }
            set { displayed = value; }
        }

        public List<ReceptureStruct> Full
        {
            get { return full;  }
            set { full = value; }
        }

        public List<ReceptureStruct> SelectedRec
        {
            get { return selected; }
            set { selected = value; }
        }

        public int SelectedRecepture
        {
            get { return selected_rec_index; }
            set { selected_rec_index = value; }
        }
        public bool ExistsSelected
        {
            get { return exist_selected; }
            set { exist_selected = value; }
        }

        public int SelectedCategory
        {
            get { return selected_cat_index; }
            set 
            { 
                selected_cat_index = value;
                tbCat.setSelected(selected_cat_index);
            }
        }

        //Methods
        public void ReadCatalog(List <Item> receptures)
        {
            int id;
            ReceptureStruct rec;

            rec_struct = new List<ReceptureStruct>();
            for (int k = 0; k < receptures.Count; k++)
            {
                id = receptures[k].id;
                rec = new ReceptureStruct(id);
                rec.setData();
                rec_struct.Add(rec);
            }
        }

        public List<ReceptureStruct> selectByCategory(int index)
        {
            full = rec_struct;
            selected_cat_index = index;
            tbCat.setSelected(index);
            selected = rec_struct.FindAll(p => p.getIds()[0] == tbCat.getSelected());
            if (selected.Count < 1)
                exist_selected = false;
            return selected;
        }

        private int indexOfSelectedByCategory(tbReceptureController tb,  int index)
        {
            try
            {
                tb.Id = selected[index].getId();
                index = rec_struct.FindIndex(p => p.getId() == tb.Id);
                tb.setSelected(index);
                return index;
            }
            catch
            {
                return 0;
            }
        }

        public List<ReceptureStruct> selectByName(string name)
        {
            if (name == "")
            {
                Full = null;
                return ReceptureStruct;
            }
            else
            {
                Full = ReceptureStruct;
                selected = rec_struct.FindAll(p => p.getName().Contains(name));
                if (selected.Count < 1)
                    exist_selected = false;
                return selected;
            }
        }

        private int indexOfSelectedByName(tbReceptureController tb, int index)
        {
            //int index;
            try
            {
                int id = selected[index].getId();
                index = rec_struct.FindIndex(p => p.getId() == id);
                tb.setSelected(index);
                tb.Id = id;
                return index;
            }
            catch
            {
                return 0;
            }
        }

        public void SelectRecepture(tbReceptureController tb, int index, string text)
        {
            selected_item_index = index;
            if (full == null)
            {
                tb.setSelected(index);
                selected_rec_index = index;
            }
            else
            {
                if (text != "")
                {
                    selected_rec_index = indexOfSelectedByName(tb, index);
                }
                else
                {
                    if (categories.Count > 0)
                    {
                        selected_rec_index = indexOfSelectedByCategory(tb, index);
                    }
                    else
                        return;
                }
            }
            exist_selected = true;
        }

        public List<ReceptureStruct> SeeAll()
        {
            full = null;
            selected = null;            
            return ReceptureStruct;
        }
    }
    
    
    public class CategoriesController
    {
        List<int> receptures_id;
        List<Item> categories, receptures;
        List<ReceptureStruct> rec_struct, displaied_rec;

        tbReceptureController tb;
        tbIngredientsController tbCat;   
        TechnologyController tbTech;

        //new fields
        private RecCatalog rec_catalog;

        public CategoriesController()        
        {
            tb = new tbReceptureController("Recepture");
            tb.setCatalog();
            receptures = tb.getCatalog();            
            
            //with new field 'RecCatalog'
            rec_catalog = new RecCatalog();
            setFields();            
            rec_struct = ReceptureStruct;            

            //for new field RecCatalog
            tbCat = new tbIngredientsController(2);
            rec_catalog.TbCat = tbCat;
            tbCat.setCatalog(); 
            rec_catalog.Categories = tbCat.getCatalog();
            categories = rec_catalog.Categories;            

            //others
            receptures_id = new List<int>();
            for (int k = 0; k < receptures.Count; k++)
            {
                receptures_id.Add(receptures[k].id);
            }
            
            tbTech = new TechnologyController(1);
            tbTech.Receptures = this.receptures;
        }

        /*
         * Properties
         */
        public List<ReceptureStruct> ReceptureStruct
        {
            //get { return rec_struct; }
            get { return rec_catalog.ReceptureStruct; }
        }

        public List<Item> Categories
        {
            set { categories = value; }
            get { return categories; }
        }

        public List<Item> Receptures
        {
            get { return receptures; }
        }

        public tbReceptureController TbMain
        {
            get { return tb; }
        }

        public tbIngredientsController TbCat
        {
            get { return tbCat; }
        }

        public bool ExistsSelected
        {
            get {return rec_catalog.ExistsSelected; }
            set { rec_catalog.ExistsSelected = value; }
        }

        public int SelectedRecepture
        {
            get { return rec_catalog.SelectedRecepture; }
            set { rec_catalog.SelectedRecepture = value; }
        }

        public int SelectedCategory
        {
            get { return rec_catalog.SelectedCategory; }
            set { rec_catalog.SelectedCategory = value; }
        }

        /*
         * Methods
         */
        public int getMinIdOfReceptures()
        {
            string minid =  tb.dbReader("select min(id) from Recepture;")[0];
            if (minid == "")
                return 0;
            else
                return int.Parse(minid);
        }

        public void setReceptures()
        {           
            tb.setCatalog();
            receptures = tb.getCatalog();
        }

        public void setFields()
        {
            rec_catalog.ReadCatalog(receptures);            
        }


        /*
         * Print to file
         */
        public void Print(int index, int option)
        {
            string file = "file";
            string[] arr;
            List<string> output = new List<string>();           

            if (index < 0) option = 1;
            PrintController print; 

            switch (option)
            {
                case 0:
                    file = rec_struct[index].getName();
                    print = new PrintController(file);
                    print.Info = PrintInfo(index).ToList();
                    print.Ingredients = new List<string>() {PrintRecepture()};
                    print.Technology = PrintTechnology(index).ToList();
                    print.Cards = PrintCards(index).ToList();
                    print.PrepareRecipeToOutput();
                    print.PrintRecipe();
                    break;
                case 1:
                    string rec, tech, cards, cat, ingr;
                    rec = tb.Statistic_common;
                    rec += $" (without technology: {tb.Statistic_formula})";                   
                    tech = tbTech.getTbController().Statistic;
                    cards = tbTech.getTbController().Statisic_cards;
                    cat = tbCat.Statistic;
                    ingr = tbCat.getStatistic(1);                    
                    arr = new string[]
                    {
                        "Statistic: ",
                        $"\trecipes: {rec}",
                        $"\ttechnologies: {tech}",
                        $"\ttechnologies cards: {cards}",
                         $"\trecipes categories: {cat}",
                        $"\tingredients: {ingr}"
                    };
                    output.AddRange(arr);
                    file = "report";
                    print = new PrintController(file);
                    print.Strings = output;
                    print.PrintRecipe();
                    break;
                default:
                    arr = new string[] { "Home e-cooking book is apps to store and manage recipes` and technologies` collections" };
                    output.AddRange(arr);
                    file = "about";
                    break;
            }
        }

        public string[] PrintInfo(int index)
        {
           string[] data = rec_struct[index].getData();
           return new string[]
            {
                $"Name: {data[0]}\n",
                $"Category: {data[1]}\n",
                $"Source: {data[4]}\n",
                $"Author: {data[3]}\n",
                $"Technology (name): {data[5]}\n",
                $"Main_ingredient: {data[2]}\n",
                $"Description: {data[6]}"               
                // name, category, ingredient, author, source, technology, description
            };           
        }

        public string[] PrintTechnology(int index)
        {
            int id_technology;
            TechnologyController tehn;
            string[] arr;

            id_technology = ReceptureStruct[index].getIds()[1];

            if (id_technology < 1)
                return new string[] { "has no tehnology" };
            else
            {
                tehn = new TechnologyController(id_technology);
                tehn.setReceptures();                
                arr = tehn.OutTechnology(id_technology);
                return arr;
            }
        }

        public string[] PrintCards(int index)
        {
            int id_technology, k=0;       
            string[] arr;
            List<string> steps;
            tbTechnologyCardsController cards;

            id_technology = ReceptureStruct[index].getIds()[1];

            if (id_technology < 0)
                return new string[] { "has no cards" };
            else
            {
                cards = new tbTechnologyCardsController("Technology_card");
            }

            steps = cards.SeeOtherCardsFull(id_technology);
            arr = new string[steps.Count];
            for (k = 0; k < steps.Count(); k++)
            {
                arr[k] = $"{k + 1} {steps[k]}\n";
            }
            return arr;
        }

        public string PrintRecepture()
        {
            int k = 0;
            string rec, amount;
            List<Element> el;

            el = tb.readElement(1);
            rec = "";

            if (el.Count == 0) return "no formulation";
            if (el.Count == 1)
            {
                amount = string.Format("{0:f2}", el[k].Amounts);
                rec += $"{el[k].Name} {amount}.";
            }
            else
            {
                for (k = 0; k < el.Count - 1; k++)
                {
                    amount = string.Format("{0:f2}", el[k].Amounts);
                    rec += $"{el[k].Name} {amount}, ";
                }
                amount = string.Format("{0:f2}", el[k].Amounts);
                rec += $"{el[k].Name} {amount}.";
            }          
            return rec;
        }

        public void PrintInto(int pr_option)
        {
            int option, index;
            option = pr_option;
            index = -1;
            
            if (option < 0 || option > 1) return;
            // option {0, 1}
                     
            //exist_selected = false;
            //selected_rec_index = -1;
            //receptures.Clear();
            //receptures = null;
            //// for testing 
            
            if (option == 0) // recepture`s printing
            {
                if (receptures == null)
                {
                    option = 1; // statistic outputing
                }   
                else
                {
                    if (receptures.Count == 0 || ExistsSelected == false)
                        option = 1; //statistic outputing
                    else
                        index = (SelectedRecepture > -1) ? index = SelectedRecepture : 0;
     
                }
            }
            Print(index, option);
        }


        /*
         * Open other forms
         */
        private int CheckTbSelected(int min)
        {
            if (tb.Selected > 0)
            {
                return tb.Selected;
            }
            else
            {
                tb.Selected = min;
                return min;
            }
        }

        private bool CheckTbSelected()
        {
            if (tb.Selected > 0)
                return true;
            else
                return false;  
            
        }

        private bool CheckCatalogSelected()
        {
            /*
             * 1. Проверить, выбран ли какой-либо рецепт
             * 2. Проверить, попадает ли его индекс он в диапазон
             * 3. Проверить, получен ли его номер (id) в базе
             * 4. Проверить, есть ли в базе запись с таким номером (id)
             */

            /*1. Проверить, выбран ли какой - либо рецепт*/
            if (!rec_catalog.ExistsSelected)
                return false;

            /*2. Проверить, попадает ли его индекс в диапазон*/
            if (rec_catalog.SelectedRecepture < 0 || rec_catalog.SelectedRecepture > rec_catalog.ReceptureStruct.Count - 1)
            {
                //int index = rec_catalog.SelectedRecepture == -1 ? 0 : rec_catalog.SelectedRecepture;
                return false;
            }

            /* 3. Проверить, получен ли его номер (id) в базе*/
            int id1, id2;
            bool ind = CheckTbSelected();

            /* 4. Проверить, есть ли в базе запись с таким номером (id)*/
            if (!ind)
            {
                id1 = ReceptureStruct[SelectedRecepture].getId();
                id2 = tb.getCatalog()[SelectedRecepture].id;
                if (id1 > 0 && tb.IfRecordIs(id1))
                {
                    tb.Selected = id1;
                    return true;
                }
                else if (id2 > 0 && tb.IfRecordIs(id2))
                    return true;

                /* 5. Проверить, есть ли в базе запись с таким наименованием*/
                string id = tb.Count($"select id, name from {tb.getTable()} where name = '{tb.getCatalog()[SelectedRecepture].name}';");
                if (id == "0" || id == "")
                    return false;             
                return false;
            }
                          

            //int id = rec_catalog.ReceptureStruct[rec_catalog.SelectedRecepture].getId();
            //    if (tb.Selected != id)
            //    {
            //        tb.Selected = id;
            //    }
            //if (tb.getSelected() < 1)
            //    return false;

            /* 4. Проверить, есть ли в базе запись с таким номером (id)*/
            //if (!tb.IfRecordIs(tb.getSelected()))
            //{
            //    id = CheckTbSelected(getMinIdOfReceptures());
            //    if (id == 0)
            //        return false;
            //}
            return true;
        }

        public void OpenRecipesForm()
        {
            //if (!ExistsSelected) return;
            //int id = CheckTbSelected(getMinIdOfReceptures());
            //if (id == 0) return;

            /*1. Проверить, выбран ли какой-либо рецепт*/
            //ExistsSelected = false;

            /*2. Проверить, попадает ли его индекс в диапазон*/
            //SelectedRecepture = -1;
            //SelectedRecepture = rec_catalog.ReceptureStruct.Count;

            /* 3. Проверить, получен ли его номер (id) в базе*/
            tb.Selected = 0;
            
            bool ind = CheckCatalogSelected();

            if (!ind)
                System.Windows.Forms.MessageBox.Show("Yohoo");
            else
            {
                Recipes frm = new Recipes(tb.Selected); // id is not corect anyever
                frm.Text += " *SelectedRecepture = -1";
                frm.Show();
            }
            
        }

        public void openTechnologyForm()
        {
            int id, id_technology;

            // check selected item of list
            id = CheckTbSelected(getMinIdOfReceptures());
            Technology frm;

            //id_technology
            // а может, просто проверить и если нет выбранной рецептуры,
            // то не открывать?
            int index = SelectedRecepture == -1 ? 0 : SelectedRecepture;

            if (ReceptureStruct.Count < 1)
                id_technology = 0;
            else
                id_technology = ReceptureStruct[index].getIds()[1];
            id_technology = id_technology < 0 ? 0 : id_technology;

            frm = new Technology(id_technology);
            frm.Show();
        }

        public void openFormToSimpleTable(int opt)
        {
            tbIngredientsController cntrl = new tbIngredientsController(opt);
            Ingredients frm = new Ingredients(cntrl);
            frm.Show();
        }

        public void openAmountsForm(int index)
        {
            if (tb.getSelected() == 0) return;
            //int index = lv_recepture.SelectedItems[0].Index;
            AmountsController cntrl = new AmountsController(tb);
            cntrl.Info = ReceptureStruct[index];
            InsertAmounts frm = new InsertAmounts(cntrl);
            frm.ShowDialog();            
        }

        public void ReloadData()
        {
            tbCat.resetCatalog();
            Categories = tbCat.getCatalog();
            rec_catalog.Categories = Categories;

            setReceptures();
            ReceptureStruct.Clear();
            setFields();
        }

        public void openManual()
        {
            Print frm = new Print();
            frm.Show();

            const string PATH = "man\\user_manul_en.txt";
            frm.OpenFile1(PATH, "user_manual");
            frm.Button3_Enabled_status(false);
        }

        public void addNewRec()
        {
            NewReceptureController rec = new NewReceptureController();
            ReceptureStruct info = new ReceptureStruct(0);
            rec.ReceptureInfo = info;
            NewRecepture frm = new NewRecepture(tb, rec);
            frm.ShowDialog();         
        }

        public bool editRec()
        {
            if (!ExistsSelected)
                return false;
            int id = CheckTbSelected(getMinIdOfReceptures());
            if (id == 0)
                return false;

            id = ReceptureStruct[SelectedRecepture].getId();

            if (tb.Selected != id)
            {
                tb.Selected = id;
            }

            //проверить, есть ли запись с таким номером
            tb.Id = id;
            NewReceptureController rec = new NewReceptureController(tb);
            rec.ReceptureInfo = ReceptureStruct[SelectedRecepture];
            NewRecepture frm = new NewRecepture(rec);
            frm.ShowDialog();
            return true;
        }

        public void openOnlineCalculator()
        {
            //string path = "C:\\Users\\user\\Documents\\instalacija.odt";
            string path = "https://www.thecalculatorsite.com/conversions/massandweight.php";
            System.Diagnostics.Process.Start(path);
        }

        public void openManualOnline()
        {
            string path = "https://github.com/jelena5316/cookingBook/blob/master/CookingBook/man/user_manul_en.txt";
            System.Diagnostics.Process.Start(path);
        }

        /*
         * Select recepture
         */
        
        // for event 'lv_recepture_SelectedIndexChanged' handler
        public void SelectRecepture(int index, string name)
        {
            rec_catalog.SelectRecepture(tb, index, name);
        }

        public void setDisplayid()
        {
            if (rec_catalog.Full == null)
            {
                displaied_rec = rec_catalog.SelectedRec;
            }
            else
            {
                displaied_rec = ReceptureStruct;
            }
        }

        /*
         *  Search by name or category:
         *  -- select items by name or categories
         *  -- display selected items
         *  -- dislplay again all items  
         */

        // for event 'textBox1_TextChanged' handler 
        public List<ReceptureStruct> SearchByName(string textbox_text)
        {
            return rec_catalog.selectByName(textbox_text);
        }

        // for event 'cmb_categories_SelectedIndexChanged' handler
        public List<ReceptureStruct> SearchByCategory(int index)
        {
            // do to checking do has list 'categories' values or not
            return rec_catalog.selectByCategory(index);
        }

        // additing a new category
        public int changeCategoryToAdded(int category)
        {
            return tb.UpdateReceptureOrCards("id_category", category.ToString(), tb.Selected);
        }

        public List<ReceptureStruct> DisplayAll
        {
            get {return rec_catalog.SeeAll(); }
        }
    }
}
