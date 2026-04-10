/*
 * to access table Recepture, to store lists of receptures
 */

using FormEF_test;
using System;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MajPAbGr_project
{
    public class tbReceptureController : tbController
    {
        private int id_recepture;
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

        public string[] getNames (string column)
        {
            return dbReader($"select {column} from {table};").ToArray();
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

        public bool IfRecordIs(int id)
        {
            query = $"select count(*) from Recepture where id ='{id}';";
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

        public void InsertNewRecord(ReceptureStruct rec)
        {
            string columns, values, insert_stm, rowid_stm, rowid;

            int cat, tech, ingr;
            int[] num;
            string name, source, author, url, description;
            string[] strings;

            num = rec.getIds();
            cat = num[0];
            tech = num[1];
            ingr = num[2];

            strings = rec.EditorData;
            name = strings[0];
            source = strings[1];
            author = strings[2];
            url = strings[3];
            description = strings[4];

            //syntaxis, scaves
            columns = "(";
            values = "(";

            //name, string
            columns += columns = $"{REC_COL_NAME}";
            values += $"'{name}'";
            
            
            //technology, int
            if (tech > 0)
            {
                columns += $", {REC_COL_TECHN}";
                values += $", {tech}";
            }

            //main_ingredient, int
            if (ingr > 0)
            {
                columns += $", {REC_COL_INGR}";
                values += $", {ingr}";
            }

            //category, int
            if (cat > 0)
            {
                columns += $", {REC_COL_CAT}";
                values += $", {cat}";
            }

            //description, string
            if (description != null && description != "")
            {
                columns += $",  {REC_COL_DESCRIPTION}";
                values += $", '{description}'";
            }

            //source, string
            if (source != null && description != "")
            {
                columns += $", {REC_COL_SOURCE}";
                values += $", '{source}'";
            }

            //author, string
            if (author != null && description != "")
            {
                columns += $", {REC_COL_AUTOR}";
                values += $", '{author}'";
            }

            //URL, string
            //if (url != null && description != "")
            //{
                columns += $", {REC_COL_URL}";
                values += $", '{url}'";
            //}

            //syntaxis 1, scaves
            columns += ")";
            values += ")";

            insert_stm = $"insert into {TABLE_RECEPTURE} " +
                $"{columns} values {values};";

            rowid_stm = "select last_insert_rowid();";


            query = $"{insert_stm} {rowid_stm}";
            rowid = Count(query);
            string message = info.err_message;

            if (int.TryParse(rowid, out id_recepture))
            {
                id_recepture = int.Parse(rowid);
            }
            query = "";
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

        public int UpdateReceptureOrCards(Recepture rec)
        {
            int ind = 0;
            int cat, tech, ingr;           
            string name, source, author, url, description;  

            cat = rec.CategoriesId;
            tech = (int)rec.TechnologyId;
            ingr = (int)rec.IngredientId;

            name = rec.Name;
            source = rec.Source;
            author = rec.Author;
            url = rec.Path;
            description = rec.Description;

            id_recepture = rec.Id;

            ind += base.UpdateReceptureOrCards (REC_COL_NAME, name, id_recepture);
            ind += base.UpdateReceptureOrCards (REC_COL_TECHN, tech.ToString(), id_recepture);
            ind += base.UpdateReceptureOrCards(REC_COL_INGR, ingr.ToString(), id_recepture);
            ind += base.UpdateReceptureOrCards(REC_COL_CAT, cat.ToString(), id_recepture);
            ind += base.UpdateReceptureOrCards(REC_COL_NAME, description, id_recepture);
            ind += base.UpdateReceptureOrCards(REC_COL_NAME, source, id_recepture);
            ind += base.UpdateReceptureOrCards(REC_COL_NAME, author, id_recepture);
            ind += base.UpdateReceptureOrCards(REC_COL_NAME, url, id_recepture);

            return ind;
        }
    }
}
