using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    class CategoriesController
    {
        int category, recepture;
        List<int> receptures_id;
        List<Item> categories, receptures;
        IngredientsController tbCat;
        FormMainController tb;

        public CategoriesController()        
        {
            tbCat = new IngredientsController(2);
            tbCat.setCatalog();            
            categories = tbCat.getCatalog();
            category = 0;

            tb = new FormMainController("Recepture");
            tb.setCatalog();
            receptures = tb.getCatalog();
            
            receptures_id = new List<int>();
            for (int k = 0; k < receptures.Count; k++)
            {
                receptures_id.Add(receptures[k].id);
            }
            recepture = 0;
         }

        public List<Item> Categories
        {
            get { return categories; }
        }

        public List<Item> Receptures
        {
            get { return receptures; }
        }

        public void SelectedRecepture(int id)
        {
            string query;
            query = $"select id, name from Recepture where id_category = {id};";            
            receptures = tb.Catalog(query);
        }

        public void setReceptures()
        {           
            tb.setCatalog();
            receptures = tb.getCatalog();
        }
    }
}
