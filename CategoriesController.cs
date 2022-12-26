using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MajPAbGr_project
{

    public struct ReceptureStruct
    {
        int id;
        string name, category, source, author, technology, ingredient;
        public ReceptureStruct(int id)
        {
            this.id = id;
            name = "unknown";
            category = "unknown";
            source = "unknown";
            author = "unknown";
            technology = "unknown";
            ingredient = "unknown";
        }

        public void setFields
            (string name, string category, string source,
            string author, string technology, string ingredient )
        {
            this.name = name;
            this.category = category;
            this.source = source;
            this.author = author;
            this.technology = technology;
            this.ingredient = ingredient;
        }

        public string [] getFields()
        {
            string[] arr = new string[] { name, category, source, author, technology, ingredient };
            return arr;
        }

        public int getId()
        {
            return id;
        }

        public string getName()
        {
            return name;
        }
    };

    public class CategoriesController
    {
        string name, category;
        List<ReceptureStruct> @struct;
        tbCategoriesController tbMain;
        TechnologyController tbTech;
        
        public CategoriesController()
        {
            tbMain = new tbCategoriesController();
            tbTech = new TechnologyController(1);
            @struct = new List<ReceptureStruct>();
        }

        public List<ReceptureStruct> Receptures
        {
            get { return @struct; }
        }
        
        public void setListView(ref ListView lv)
        {
            int id;
            string query, source;
            List<Item> receptures = tbMain.Receptures;
            ListViewItem items;
            ReceptureStruct rec;

            string getInfo()
            {
                source = "unknown";
                if (tbMain.TbMain.dbReader(query).Count > 0)
                        source = tbMain.TbMain.dbReader(query)[0] == "" ? source: tbMain.TbMain.dbReader(query)[0];
                return source;
            }

            lv.Columns.Add("Name");
            lv.Columns.Add("Category");
            lv.Columns.Add("Source");
            lv.Columns.Add("Author");
            lv.Columns.Add("Technology");
            lv.Columns.Add("Main_ingredient");
            
            tbTech.Receptures = tbMain.Receptures;           
            for (int k = 0; k < receptures.Count; k++)
            {
                id = receptures[k].id;
                items = new ListViewItem(receptures[k].name);
                items.Tag = receptures[k].id;
                rec = new ReceptureStruct(id);
                string[] arr = new string[6];                                

                items.SubItems.Add(tbTech.SeeRecepturesCategory(k));                
                query = $"select source from Recepture where id = {id};";
                items.SubItems.Add(getInfo());                
                query = $"select author from Recepture where id = {id}";
                items.SubItems.Add(getInfo());               
                query = $"select name from Technology where id = (select id_technology from Recepture where id = {id});";
                items.SubItems.Add(getInfo());
                query = $"select name from Ingredients where id = (select id_main from Recepture where id = {id});";
                items.SubItems.Add(getInfo());
                lv.Items.Add(items);

                for (int q = 0; q < items.SubItems.Count; q++)
                {
                    arr[q] = items.SubItems[q].Text;
                }
                rec.setFields(arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);
                @struct.Add(rec);               
            }
        }

    }
}
