using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class tbReceptureController : tbController
    {
        private int id_recepture, category, technology = 0;
        private bool indicator; // choose mode
        string[,] Info = new string[2, 6];
        //from tbReceptureController

        ReceptureStruct info;

        public tbReceptureController(string table): base(table)
        {
            this.table = table;
        }

        //Конструкторы для формы ввода новой рецептуры (3)
        // пока передаётся существующий, а не создаётся новый
        // FormMain.cs: new NewRecepture(..)
        // Categories.cs: lv_recepture_DoubleClick(..)
        // Recipes.cs: new Recepture(..) (now is a comment)
        public tbReceptureController(string table, bool indicator) // add new recepture
        {
            base.table = table;
            id_recepture = 0;
            category = 0;
            this.indicator = indicator;

            Info[0, 0] = "name";
            Info[0, 1] = "source";
            Info[0, 2] = "author";
            Info[0, 3] = "URL";
            Info[0, 4] = "description";

            setData();
        }

        public tbReceptureController(string table, int id, int category) // edit recepture
           : base()
        {
            base.table = table;
            id_recepture = id;
            selected = id;
            this.category = category;

            Info[0, 0] = "name";
            Info[0, 1] = "source";
            Info[0, 2] = "author";
            Info[0, 3] = "URL";
            Info[0, 4] = "description";

            //from method setIndicator (): void
            if (id_recepture > 0) indicator = true;
            else indicator = false;

            setData();
        }

        public tbReceptureController(string table, int id, int category, int technology) // edit recepture
            : base()
        {
            base.table = table;
            id_recepture = id;
            selected = id;
            this.category = category;
            this.technology = technology;

            Info[0, 0] = "name";
            Info[0, 1] = "source";
            Info[0, 2] = "author";
            Info[0, 3] = "URL";
            Info[0, 4] = "description";

            //from method setIndicator (): void
            if (id_recepture > 0) indicator = true;
            else indicator = false;

            setData();
        }

        private void setData()// in place SetForm
        {
            setCatalog();
            if (indicator)
            {

                for (int k = 0; k < 5; k++)
                {
                    query = $"select {Info[0, k]} from Recepture where id = {id_recepture};";
                    Info[1, k] = dbReader(query)[0];
                }
            }
        }

        public List<string> getData()
        {
            if (indicator)
            {
                List<string> data = new List<string>();
                for (int k = 0; k < 5; k++)
                {
                    data.Add(Info[1, k]);
                }
                return data;
            }
            else return null;
        }

        public ReceptureStruct ReceptureInfo
        {
            set {
                info = value;
                string[] arr = info.EditorData;
                int[] ids = info.getIds();
                for (int k = 0; k < 5; k++)
                    Info[1, k] = arr[k];
                category = ids[0];
                technology = ids[1];  
            }
            get { return info; }
        }

        public int Id
        {
            set
            {
                id_recepture = value;
                if (id_recepture > 0) indicator = true;
                else indicator = false;
            }
            get => id_recepture;
        }
        public int getId() { return id_recepture; }
        public int Category
        {
            set { category = value;}
            get { return category; }
        }

        public int Technology
        {
            set { technology = value; }
            get { return technology; }
        }

        public int getTechnology() { return technology; }

        public bool IfRecordIs()
        {
            return Count("select count (name) from Recepture;") == "0" ? false : true;
        }

        public bool IfRecordIs(string name)
        {
            query = $"select count(*) from Recepture where name ='{name}';";
            string recepture = Count(query);
            if (recepture != "0")
                return true;
            else
                return false;
        }

        public void InsertNewRecord(string name, int category)
        {
            string query = $"insert into Recepture (name, id_category) " +
               $"values ('{name}', {category}); select last_insert_rowid() ";
            string recepture = Count(query);
            if (int.TryParse(recepture, out id_recepture))
            {
                id_recepture = int.Parse(recepture);
            }
        }

        //public void WriteIntoDataBase(string name, int category)
        //{
        //    bool recordIs = IfRecordIs(name);
        //    InsertNewRecord(name, category);
        //}

        public new int UpdateReceptureOrCards(string column, string value, int id_recepture)
        {
            int ind = base.UpdateReceptureOrCards(column, value, id_recepture);
            int k = 0;
            while (k < 5)
            {
                if (Info[0, k] == column)
                {
                    Info[1, k] = value;
                }
                break;
            }
            return ind;
        }

        //tbFormainController
        public string readCategory()
        {
           return dbReader($"select id_category from {table} where id = {selected};")[0];
        }

        public string readTechnology()
        {
            return dbReader($"select id_technology from Recepture where id = {selected};")[0];
        }

        public new List<Element> readElement(int opt) // for Form1.cs
        {
            List<Element> el;
            switch (opt)
            {
                case 1: // amounts
                    query = "SELECT id_ingredients, name, amount" +
                    " FROM Amounts AS am JOIN Ingredients AS ingr " +
                    "ON am.id_ingredients = ingr.id WHERE am.id_recepture = "
                    + selected + ";";

                    break;
                case 2: // recipe
                    query = "SELECT id, name, coefficient" +
                    " FROM Recipe WHERE id_recepture = "
                    + selected + ";";
                    break;
                default:
                    query = "SELECT id, name, coefficient" +
                    " FROM Recipe WHERE id_recepture = "
                    + selected + ";";
                    break;
            }
            el = dbReadElement(query);
            return el;
        }

        public int SelectedCount(string table, string column, int id) // for Form1.cs: before Technology to open
        {
            string query;
            query = $"select count ({column}) from {table}";
            if (id > 0)
                query += $"  where id = {id};";
            else
                query += ";";
            return int.Parse(Count(query));
        }
    }
}
