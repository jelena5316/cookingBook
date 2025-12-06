/*
 * read data from tables of data base and manage it
 */

using System.Collections.Generic;
using FormEF_test;

namespace MajPAbGr_project
{
	public class tbController : dbController
	{
		protected string table;
		protected string query;
		protected List<Item> catalog, subcatalog;
		protected int selected;
		protected int count;


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
		 * Properties
		 */
		public int Selected
		{
			set { selected = value; }
			get { return selected; }
		}

		public List<Item> Subcatalog
		{
			set 
			{
                if (subcatalog.Count > 0)
                {
                    subcatalog.Clear();
                }
                subcatalog = value;
			}	
			get { return subcatalog; }
		}

		/*
		 * set and get catalog and subcatalog
		 */

		public void setCatalog()
		{
			if (catalog.Count > 0)
			{
				catalog.Clear();
			}

			query = "select id, name from " + table + ";";
			catalog = Catalog(query); // connection open
			count = catalog.Count;
		}

		public List<Item> getCatalog() { return catalog; }

		public void resetCatalog()
		{
			if (catalog.Count > 0)
				catalog.Clear();
			setCatalog(); // connection open
		}

		public List<Item> setSubCatalog(string subtable, string column) //Recipe, id_recepture
		{
			query = $"select id, name from {subtable} where {column} = {selected};";
			return Catalog(query); // connection open
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


        /*
		 * get fields values
		 */

        public string getTable()
        {
            return table;
        }

        public string getById(string column, int id) // for Recepture and others
        {
            query = $"select {column} from {table} where id = " + id + ";";
            List<string> id_list = dbReader(query); // connectio open
            if (id_list.Count > 0)
                return id_list[0];
            else
                return "0";
        }

        public string getName(int index)
		{
			return catalog[index].name;
		}

		public List<string> getNamesFromDB() // for Amounts form, autocomplete
		{
			return dbReader($"select name from {table}");
		}

		public List<string> getNamesFromDB(string subtable)
		{
            return dbReader($"select name from {subtable}"); // connection opne
        }
				
		/*
		 * checking selected item use count
		 */

		public virtual void setUsed() { }

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
			el = dbReadElement(query); // connection open
			return el;
		}



		/*
		 * Operation with data
		 */

		//remove
		public virtual int RemoveItem()
		{
			int count;

			selected = 0;
			query = $"delete from {table} where id = {selected}";
			
			count = Edit(query); // connection open
			if (count != 0)
				resetCatalog(); // connection open
			
			return count;
		}

		public int UpdateReceptureOrCards(string column, string value, int id_recepture)
		{
			int ind = 0;
			if (value != null)
				query = $"update {table} set {column} = '{value}' where id = {id_recepture};";
			else
				query = $"update {table} set {column} = null where id = {id_recepture};";
			ind = Edit(query); // connection open
			return ind;
		}

		/*
		 * Error code and message, getters
		 */

		public int Err_code { get { return error_code; } }
		public string Err_message { get { return error_message; } }

		public void ResetErr_info() // see dbController too!!!
		{
			error_code = 0;
			error_message = "";
		}

		public  int Error_code { get { return info.err_code; } }

		public string Error_message { get { return info.err_message; } }

		public string LastQuery { get { return info.query; } }

		public string DataBaseName { get { return info.database; } }

		public void ResetError_info()
		{
			info.err_code = 0;
			info.err_message = "";
			info.query = "";
			info.database = "";
		}
	}
}
