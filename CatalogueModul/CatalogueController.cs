/*
 * to manage catalogue cards using form "About Recepture"
 */

using FormEF_test;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

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


        public RecCatalog(List<ReceptureStruct> list)
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

        public List<ReceptureStruct> setDisplayed
        {
            get { return displayed; }
            set { displayed = value; }
        }

        public List<ReceptureStruct> Full
        {
            get { return full; }
            set { full = value; }
        }

        public List<ReceptureStruct> SelectedRec
        {
            get { return selected; }
            set { selected = value; }
        }

        public int SelectedRecIndex
        {
            get { return selected_rec_index; }
            set { selected_rec_index = value; }
        }
        public bool ExistsSelected
        {
            get { return exist_selected; }
            set { exist_selected = value; }
        }

        public int SelectedCatIndex
        {
            get { return selected_cat_index; }
            set
            {
                selected_cat_index = value;
                tbCat.setSelected(selected_cat_index);
            }
        }

        //Methods
        public void ReadCatalog(List<Item> receptures)
        {
            int id;
            ReceptureStruct rec;

            if (rec_struct == null)
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

        private int indexOfSelectedByCategory(tbReceptureController tb, int index)
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


    /*******************************************************************************************************
     * CATALOGUE MODULE
     *******************************************************************************************************/
    public class CatalogueController
    {
        int id_recepture = 0,
            category,
            technology;
        List <Item> receptures, categories, technologies;        
        List <ReceptureStruct> rec_struct;
        //List<Recepture> receptures;
        RecCatalog rec_catalog;

        ReceptureStruct info; //current
        SubmitMode mode;
        Recepture new_recepture; // for change storing and passing
        
        // tables of data base controllers
        tbReceptureController tb;
        tbController tbCat;
        tbTechnologyController tbTech;

        public CatalogueController(int version)
        {
            tb = new tbReceptureController("Recepture");
            tb.setCatalog();
            receptures = tb.getCatalog();
            setCategoriesCatalog();
            setTechnologiesCatalog();
            rec_catalog = new RecCatalog();
            rec_catalog.ReadCatalog(receptures);

            new_recepture = new Recepture();
            new_recepture.Categories = new Category();
            new_recepture.Technology = new Technology();
            new_recepture.Ingredient = new Ingredient();
        }


        public CatalogueController(tbReceptureController tb) // openReceptureEditor() in "Categories"
        {
            this.tb = tb;
            tbCat = new tbController("Categories");
            tbTech = new tbTechnologyController("Technology");
            tbCat.setCatalog();
            categories = tbCat.getCatalog();
            tbTech.setCatalog();
            technologies = tbTech.getCatalog();

            new_recepture = new Recepture();            
            new_recepture.Categories = new Category();
            new_recepture.Technology = new Technology();
            new_recepture.Ingredient = new Ingredient();
        }

        public CatalogueController() //addNew() in "Categories"
        {
            tbCat = new tbController("Categories");
            tbTech = new tbTechnologyController("Technology");
            tbCat.setCatalog();
            categories = tbCat.getCatalog();
            tbTech.setCatalog();
            technologies = tbTech.getCatalog();

            new_recepture = new Recepture();
            new_recepture.Categories = new Category();
            new_recepture.Technology = new Technology();
            new_recepture.Ingredient = new Ingredient();
        }

        public RecCatalog RecepturesCatalog => rec_catalog;

        public SubmitMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }

        public ReceptureStruct ReceptureInfo
        {
            set
            {
                info = value;
                id_recepture = info.getId();
                if (info.getId() > 0)
                {
                    int[] ids = info.getIds();
                    category = ids[0];
                    technology = ids[1];
                }
                else
                {
                    category = 0;
                    technology = 0;
                }
            }
            get { return info; }
        }

        public string[] Data => info.EditorData;

        public bool Indicator => info.getId() > 0;


        public Recepture CurrentRecepture
        {
            get { return new_recepture; }
            set { new_recepture = value; }
        }

        public tbReceptureController TbMain
        {
            set { tb = value; }
            get => tb;
        }

        public tbController TbCat
        {
            set { tbCat = value; }
            get => tbCat;
        }

        public tbTechnologyController TbTech
        {
            set { tbTech = value; }
            get => tbTech;
        } 


        //get data at start and on process, private        

        private void setTechnologiesCatalog()
        {
            tbTechnologyController tbTechnologyController = new tbTechnologyController("Technology");
            tbTechnologyController.setCatalog();
            technologies = tbTechnologyController.getCatalog();
        }

        private void setCategoriesCatalog()
        {
            tbController tbCategory = new tbController("Categories");
            tbCategory.setCatalog();
            categories = tbCategory.getCatalog();
        }
        
        //CRUD
        public bool setIndicator(int id) => id > 0;

        public void CompletData
            (
            string name,
            string author,
            string source,
            string url,
            string description
            )
        {
            CurrentRecepture.Name = name;
            CurrentRecepture.Author = author;
            CurrentRecepture.Source = source;
            CurrentRecepture.Path = url;
            CurrentRecepture.Description = description;
        }

        public int InsertNew(ReceptureStruct rec)
        {
            string name;
            
            ReceptureInfo = rec;
            name = rec.getName();
            category = rec.getIds()[0];

            //tb.InsertNewRecord(name, category);
            tb.InsertNewRecord(rec);
            id_recepture = tb.Id;

            ReceptureInfo = new ReceptureStruct(id_recepture, name, category);
            return id_recepture;
        }

        public int InsertNew()
        {
            int result;
            
            ReceptureStruct rec = new ReceptureStruct
            (
                id_recepture,
                CurrentRecepture.Name,
                CurrentRecepture.Source,
                CurrentRecepture.Author,
                CurrentRecepture.Description,
                CurrentRecepture.Path,
                CurrentRecepture.CategoriesId,
                CurrentRecepture.Technology.Id,
                CurrentRecepture.Ingredient.Id
            );

            rec.setDataStrings
            (
                CurrentRecepture.Categories.Name,
                CurrentRecepture.Technology.Name,
                CurrentRecepture.Ingredient.Name
            );
           
            result = tb.InsertNewRecord(rec);
            id_recepture = tb.Id;

            if(result < 1)
            {
                return result;
            }


            ReceptureInfo = new ReceptureStruct                
            (
                id_recepture,
                CurrentRecepture.Name,
                CurrentRecepture.Source,
                CurrentRecepture.Author,
                CurrentRecepture.Description,
                CurrentRecepture.Path,
                CurrentRecepture.CategoriesId,
                CurrentRecepture.Technology.Id,
                CurrentRecepture.Ingredient.Id
            );

            info.setDataStrings
            (
                CurrentRecepture.Categories.Name,
                CurrentRecepture.Technology.Name,
                ""
            );
            
            return id_recepture;
        }
    
        public int UpdateExisited()
        {          
            return tb.UpdateReceptureOrCards(CurrentRecepture);
        }

    }
}
