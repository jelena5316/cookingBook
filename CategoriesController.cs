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
            rec_struct = new List<ReceptureStruct>();
            setFields();
        }

        public List<ReceptureStruct> RecepturesStruct
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

        public void SelectedByCategoryRecepture(int id)
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

        public void setFields()
        {
            int id;            
            List<Item> receptures = this.receptures;
            ReceptureStruct rec;

            tbTech.Receptures = receptures;
            for (int k = 0; k < receptures.Count; k++)
            {
                id = receptures[k].id;
                rec = new ReceptureStruct(id);
                rec.setData();               
                rec_struct.Add(rec);
            }
        }

        public void setListView(ref ListView lv)
        {
            int id;
            List<Item> receptures = this.Receptures;
            ListViewItem items;

            tbTech.Receptures = this.Receptures;
            for (int k = 0; k < receptures.Count; k++)
            {
                id = receptures[k].id;
                items = new ListViewItem(receptures[k].name);
                items.Tag = receptures[k].id;

                items.SubItems.Add(rec_struct[k].getData()[1]);
                items.SubItems.Add(rec_struct[k].getData()[2]);
                items.SubItems.Add(rec_struct[k].getData()[3]);
                items.SubItems.Add(rec_struct[k].getData()[4]);
                items.SubItems.Add(rec_struct[k].getData()[5]);
                items.SubItems.Add(rec_struct[k].getData()[6]);             

                lv.Items.Add(items);
            }
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
    }
}
