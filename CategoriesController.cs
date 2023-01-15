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
        IngredientsController tbCat;   
        TechnologyController tbTech;

        public CategoriesController()        
        {
            tbCat = new IngredientsController(2);
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

        public IngredientsController TbCat
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
            string query, source;
            List<Item> receptures = this.receptures;
            ReceptureStruct rec;

            string getInfo()
            {
                source = "unknown";
                if (tb.dbReader(query).Count > 0)
                    source = tb.dbReader(query)[0] == "" ? source : tb.dbReader(query)[0];
                return source;
            }

            tbTech.Receptures = receptures;
            for (int k = 0; k < receptures.Count; k++)
            {
                id = receptures[k].id;
                rec = new ReceptureStruct(id);
                //rec.setFields();
                string[] arr = new string[7];

                arr[0] = receptures[k].name;
                arr[1] = tbTech.SeeRecepturesCategory(k);
                query = $"select source from Recepture where id = {id};";
                arr[2] = getInfo();
                query = $"select author from Recepture where id = {id}";
                arr[3] = getInfo();
                query = $"select name from Technology where id = (select id_technology from Recepture where id = {id});";
                arr[4] = getInfo();
                query = $"select name from Ingredients where id = (select id_main from Recepture where id = {id});";
                arr[5] = getInfo();
                query = $"select description from Recepture where id = {id}";
                arr[6] = getInfo();

                rec.setFields(arr);
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

                items.SubItems.Add(rec_struct[k].getFields()[1]);
                items.SubItems.Add(rec_struct[k].getFields()[2]);
                items.SubItems.Add(rec_struct[k].getFields()[3]);
                items.SubItems.Add(rec_struct[k].getFields()[4]);
                items.SubItems.Add(rec_struct[k].getFields()[5]);
                items.SubItems.Add(rec_struct[k].getFields()[6]);             

                lv.Items.Add(items);
            }
        }

        public string[] PrintInfo(int index)
        {
            string[] arr = new string[]
            {
                $"Recepture: {rec_struct[index].getFields()[1]}\n",
                $"Category: {rec_struct[index].getFields()[1]}\n",
                $"Source: {rec_struct[index].getFields()[2]}\n",
                $"Author: {rec_struct[index].getFields()[3]}\n",
                $"Technology (name): {rec_struct[index].getFields()[4]}\n",
                $"Main_ingredient: {rec_struct[index].getFields()[5]}\n",
                $"Description: {rec_struct[index].getFields()[6]}"

                //name, category, source, author, technology, ingredient, description
            };
            return arr;
        }
    }
}
