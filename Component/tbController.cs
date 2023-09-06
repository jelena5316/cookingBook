/*
 * read data from tables od data base and manage it
 */

using System.Collections.Generic;

namespace MajPAbGr_project
{
    public class tbController : dbController
    {
        protected string table;
        protected string query;
        protected List<Item> catalog, subcatalog;
        protected int selected;
        int count;


        public tbController() : base()
        {
            selected = 0;
        }
        public tbController(string table)
        {
            selected = 0;
            this.table = table;
            catalog = new List<Item>();
            subcatalog = new List<Item>();
        }


        /*
         * set and get catalog and subcatalog
         */

        public void setCatalog()
        {
            query = "select id, name from " + table + ";";
            catalog = Catalog(query);
            count = catalog.Count;
        }

        public List<Item> getCatalog() { return catalog; }

        public void resetCatalog()
        {
            if (catalog.Count > 0)
                catalog.Clear();
            setCatalog();
        }

        public List<Item> setSubCatalog(string subtable, string column) //Recipe, id_recepture
        {
            query = $"select id, name from {subtable} where {column} = "
                + selected + ";";
            subcatalog = Catalog(query);
            return subcatalog;
        }       

        /*
         * selected item
         */

        public int setSelected(int temp)
        {
            this.selected = catalog[temp].id;
            return selected;
        }

        public int getSelected()
        {
            return selected;
        }

        public int Selected
        {
            set { selected = value; }
            get { return selected; }
        }


        /*
         * get fields values
         */

        public string getName(int index)
        {
            return catalog[index].name;
        }

        public string getTable()
        {
            return table;
        }

        public string getById(string column, int id) // for Recepture and others
        {
            query = $"select {column} from {table} where id = " + id + ";";
            List<string> id_list = dbReader(query);
            if (id_list.Count > 0)
                return id_list[0];
            else
                return "0";
        }

        /*
         * checking selected item use count
         */

        public virtual void setUsed() {}

        public virtual string getUsed() { return ""; }


        public virtual List<Element> readElement(int opt)
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

        /*
         * Operation with data
         */

        //remove
        public virtual int RemoveItem()
        {
            query = $"delete from {table} where id = {selected}";
            int count = Edit(query);
            if (count != 0) resetCatalog();
            selected = 0;
            return count;
        }
   
        public int UpdateReceptureOrCards(string column, string value, int id_recepture)
        {
            int ind = 0;
            if (value != null)
                query = $"update {table} set {column} = '{value}' where id = {id_recepture};";
            else
                query = $"update {table} set {column} = null where id = {id_recepture};";
            ind = Edit(query);
            return ind;
        }

        /*
         * Error code and message, getters
         */

        public int Err_code { get { return error_code; } }
        public string Err_message { get { return error_message; } }

        public void ResetErr_info()
        {
            error_code = 0;
            error_message = "";
        }
    }
}
