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

        ReceptureStruct info;

        public tbReceptureController(string table) : base(table)
        {
            this.table = table;
        }


        public string Statistic_common{
            get {
                query = $"select count (*) from {table}";
                return Count(query);                 
            }
        }

        public string Statistic_formula
        {
            get {
                query = $"select count (*) from {table} where description is null";                               
                return Count(query); ;            
            }
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
    }
}
