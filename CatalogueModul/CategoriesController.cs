/*
 * to manage work with recepture catalogue using form "Categories"
 * and methods of classes tbReceptureController, tbIngredientsController and tbTechnologyController
 */

using System;
using System.Collections.Generic;
using System.Linq;


namespace MajPAbGr_project
{
    public class CategoriesController
    {
        List<int> receptures_id;
        List<Item> categories, receptures;
        List<ReceptureStruct> rec_struct;

        tbReceptureController tb;
        tbIngredientsController tbCat;   
        TechnologyController tbTech;

        //from class `Categories`
        bool exist_selected = false;
        int selected_recepture = -1;
        List<ReceptureStruct> full;

        public CategoriesController()        
        {
            tbCat = new tbIngredientsController(2);
            tbCat.setCatalog();            
            categories = tbCat.getCatalog();            

            tb = new tbReceptureController("Recepture");
            tb.setCatalog();
            receptures = tb.getCatalog();
            
            receptures_id = new List<int>();
            for (int k = 0; k < receptures.Count; k++)
            {
                receptures_id.Add(receptures[k].id);
            }
            
            tbTech = new TechnologyController(1);
            tbTech.Receptures = this.receptures;
            rec_struct = new List<ReceptureStruct>();
            setFields();
        }

        /*
         * Properties
         */
        public List<ReceptureStruct> ReceptureStruct
        {
            get { return rec_struct; }
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
            get {return exist_selected; }
            set { exist_selected = value; }
        }

        public int SelectedRecepture
        {
            get { return selected_recepture; }
            set { selected_recepture = value; }
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
            int id;            
            ReceptureStruct rec;
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
            List<ReceptureStruct> selected;            
            selected = rec_struct.FindAll(p => p.getCategory() == categories[index].name);
            tbCat.setSelected(index);
            return selected;
         }

        public int indexOfSelectedByCategory(int index, int category)
        {
            try
            {
                string name;
                List<ReceptureStruct> selected;

                name = tbCat.getName(category);
                selected = rec_struct.FindAll(p => p.getCategory() == name);
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
            List<ReceptureStruct> selected;
            selected = rec_struct.FindAll(p => p.getName().Contains(name));
            return selected;
        }

        public int indexOfSelectedByName(string name)
        {
            int index;
            try
            {
                tb.Id = rec_struct.Find(p => p.getName().Contains(name)).getId();
                index = rec_struct.FindIndex(p => p.getName().Contains(name));           
                tb.setSelected(index);
                return index;
            }
            catch
            {
                return 0;
            }
        }

        public int changeCategoryToAdded(int category)
        {
            return tb.UpdateReceptureOrCards("id_category", category.ToString(), tb.Selected);
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

        public void OpenRecipesForm()
        {
            if (!exist_selected) return;
            int id = CheckTbSelected(getMinIdOfReceptures());
            if (id == 0) return;
            Recipes frm = new Recipes(tb.Selected);
            frm.Show();
        }

        public void openTechnologyForm()
        {
            int selected, id_technology;

            // check selected item of list
            selected = CheckTbSelected(getMinIdOfReceptures());
            Technology frm;

            //id_technology
            int index = selected_recepture == -1 ? 0 : selected_recepture;

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
            if (!exist_selected)
                return false;
            int id = CheckTbSelected(getMinIdOfReceptures());
            if (id == 0)
                return false;

            id = ReceptureStruct[selected_recepture].getId();

            if (tb.Selected != id)
            {
                tb.Selected = id;
            }

            tb.Id = id;
            NewReceptureController rec = new NewReceptureController(tb);
            rec.ReceptureInfo = ReceptureStruct[selected_recepture];
            NewRecepture frm = new NewRecepture(rec);
            frm.ShowDialog();
            return true;
        }
    }   
}
