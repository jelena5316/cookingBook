using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class NewReceptureController
    {
        int id_recepture = 0, category, technology;
        string name, recepture, source, author, URL, description;
        List<Item> categories, technologies;
        ReceptureStruct info;

        tbReceptureController tb;
        tbController tbCat;
        tbTechnologyController tbTech;

        public NewReceptureController(tbReceptureController tb) // openReceptureEditor() in "Categories"
        {
            this.tb = tb;
            tbCat = new tbController("Categories");
            tbTech = new tbTechnologyController("Technology");
            tbCat.setCatalog();
            categories = tbCat.getCatalog();
            tbTech.setCatalog();
            technologies = tbTech.getCatalog();
        }

        public NewReceptureController() //addNew() in "Categories"
        {
            tbCat = new tbController("Categories");
            tbTech = new tbTechnologyController("Technology");
            tbCat.setCatalog();
            categories = tbCat.getCatalog();
            tbTech.setCatalog();
            technologies = tbTech.getCatalog();
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

        public bool setIndicator(int id) => id > 0;

        public tbReceptureController TbMain
        {
            set { tb = value; }
            get => tb;
        }
        public tbController TbCat() => tbCat;
        public tbTechnologyController TbTech() => tbTech;

        public string[] getNames(string column)
        {
            return tb.dbReader($"select {column} from Recepture;").ToArray();
        }
    }
}
