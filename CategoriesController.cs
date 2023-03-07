using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MajPAbGr_project
{
    class CategoriesController
    {
        int category, recepture;
        List<int> receptures_id;
        List<Item> categories, receptures;
        List<ReceptureStruct> rec_struct;  
        
        
        tbReceptureController tb;
        tbIngredientsController tbCat;   
        TechnologyController tbTech;

        public CategoriesController()        
        {
            tbCat = new tbIngredientsController(2);
            tbCat.setCatalog();            
            categories = tbCat.getCatalog();
            category = 0;

            tb = new tbReceptureController("Recepture");
            tb.setCatalog();
            receptures = tb.getCatalog();
            
            receptures_id = new List<int>();
            for (int k = 0; k < receptures.Count; k++)
            {
                receptures_id.Add(receptures[k].id);
            }
            recepture = 0;
            
            tbTech = new TechnologyController(1);
            tbTech.Receptures = this.receptures;
            rec_struct = new List<ReceptureStruct>();
            setFields();
        }

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

        public int getMinIdOfReceptures()
            => int.Parse(tb.dbReader("select min(id) from Recepture;")[0]);
        

        //public void SelectedByCategoryRecepture(int id)
        //{
        //    string query;
        //    query = $"select id, name from Recepture where id_category = {id};";            
        //    receptures = tb.Catalog(query);
        //}

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

        public void setListView(ListView lv)
        {
            ListViewItem items;
            for (int k = 0; k < receptures.Count; k++)
            {                
                items = new ListViewItem(receptures[k].name);
                items.Tag = receptures[k].id;                
                string[] arr = rec_struct[k].getData();

                for (int q = 1; q < 5; q++)
                {
                    items.SubItems.Add(arr[q]);
                }
                lv.Items.Add(items);               
            }
            lv.Items[0].Selected = true;
        }

        public string[] PrintInfo(int index)
        {
            string[] arr = new string[]
            {
                $"Recepture: {rec_struct[index].getData()[1]}\n",
                $"Category: {rec_struct[index].getData()[1]}\n",
                $"Source: {rec_struct[index].getData()[2]}\n",
                $"Author: {rec_struct[index].getData()[3]}\n",
                $"Technology (name): {rec_struct[index].getData()[4]}\n",
                $"Main_ingredient: {rec_struct[index].getData()[5]}\n",
                $"Description: {rec_struct[index].getData()[6]}"

                //name, category, source, author, technology, ingredient, description
            };
            return arr;
        }
    
         public List<ReceptureStruct> selectByCategory(int index)
         {
            List<ReceptureStruct> selected;        
            selected = rec_struct.FindAll(p => p.getCategory() == categories[index].name);
            return selected;
         }
    }   
}
