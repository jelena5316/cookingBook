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

        public void setFields (string [] fields)
        {
            this.name = fields[0];
            this.category = fields[1];
            this.source = fields[2];
            this.author = fields[3];
            this.technology = fields[4];
            this.ingredient = fields[5];
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

        public string getCategory()
        {
            return category;
        }
    };

    public class CategoriesController1
    {
        List<ReceptureStruct> rec_struct;
        CategoriesController tbMain;
        TechnologyController tbTech;        
        
        public CategoriesController1()
        {
            tbMain = new CategoriesController();
            tbTech = new TechnologyController(1);            
            rec_struct = new List<ReceptureStruct>();
            //setFields();
        }

        public List<ReceptureStruct> RecepturesStruct
        {
            get { return rec_struct; }
        }

        public void setFields()
        {
            int id;
            string query, source;
            List<Item> receptures = tbMain.Receptures;           
            ReceptureStruct rec;

            string getInfo()
            {
                source = "unknown";
                if (tbMain.TbMain.dbReader(query).Count > 0)
                    source = tbMain.TbMain.dbReader(query)[0] == "" ? source : tbMain.TbMain.dbReader(query)[0];
                return source;
            }

            tbTech.Receptures = tbMain.Receptures;
            for (int k = 0; k < receptures.Count; k++)
            {
                id = receptures[k].id;
                rec = new ReceptureStruct(id);
                string[] arr = new string[6];

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

                rec.setFields(arr);
                rec_struct.Add(rec);
            }
        }
        
        public void setListView(ref ListView lv)
        {
            int id;           
            List<Item> receptures = tbMain.Receptures;
            ListViewItem items;
     
            tbTech.Receptures = tbMain.Receptures;           
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
                lv.Items.Add(items);
            }
        }
    }
}
