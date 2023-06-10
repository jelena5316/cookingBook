using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class tbReceptureController : tbController
    {
        private int id_recepture, category;        
        string[,] Info = new string[2, 6];      

        public tbReceptureController(string table) : base(table)
        {
            this.table = table;
        }

        public string Statistic_common
        {
            get
            {
                query = $"select count (*) from {table}";
                return Count(query);                 
            }
        }

        public string Statistic_formula
        {
            get
            {
                query = $"select count (*) from {table} where description is null";                               
                return Count(query); ;            
            }
        }

        public int Id
        {
            set { id_recepture = value; }

            get { return id_recepture; }
        }
        
        /*
         * for form `About Recepture'
         */
        public int getId() { return id_recepture; }

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
    }
}
