using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class ReceptureController: tbClass1
    {
        private int id_recepture, category;      
        private bool indicator; // choose mode
        string[,] Info = new string[2, 5];       

        public ReceptureController(string table) // add new recepture
        {
            base.table = table;
            id_recepture = 0;
            category = 0;
            indicator = false;
           
            Info[0, 0] = "name";
            Info[0, 1] = "source";
            Info[0, 2] = "author";
            Info[0, 3] = "URL";
            Info[0, 4] = "description";

            setData();
        }

        public ReceptureController (string table, int id, int category) // edit recepture
            : base()
        {
            base.table = table;
            id_recepture = id;
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

        private void setData ()// in place SetForm
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

        public int getId () { return id_recepture; }
        public int getCategory () { return category; }

        public void WriteIntoDataBase(string name, int category)
        {
            bool recordIs = IfRecordIs(name);
            InsertNewRecord (name, category);           
        }

        public bool IfRecordIs (string name)
        {
            query = $"select count(*) from Recepture where name ='{name}';";
            string recepture = Count(query);
            if (recepture != "0")
                return true;
            else
                return false;
        }

        public void InsertNewRecord (string name, int category)
        {
            string query = $"insert into Recepture (name, id_category) " +
               $"values ('{name}', {category}); select last_insert_rowid() ";
            string recepture =  Count(query);            
            if (int.TryParse(recepture, out id_recepture))
            {
                id_recepture = int.Parse(recepture);                
            }
        }

        public new int UpdateReceptureOrCards(string column, string value, int id_recepture)
        {
            int ind = base.UpdateReceptureOrCards(column, value, id_recepture);
            int k = 0;
            while ( k < 5)
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

