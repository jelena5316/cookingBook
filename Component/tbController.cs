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
		 * set and get catalog and subcatalog
		 */

		public void setCatalog()
		{
			//query = "select id, name from " + table + ";";
			//catalog = Catalog(query);
			//count = catalog.Count;

			List<object[]> data;
			//DBAnswer answer;

			if (catalog.Count > 0)
			{
				catalog.Clear();
			}

			query = "select id, name from " + table + ";";
			//answer = dbReadData(query);
			//data = answer.getData;
			data = dbReadData(query);
			DataItemsList(catalog, data); // convert data to list of Item enstance
			count = catalog.Count;
			data.Clear();
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
			List<object[]> data;
			//DBAnswer answer;
			
			query = $"select id, name from {subtable} where {column} = "
				+ selected + ";";

			if (subcatalog.Count > 0)
			{
				subcatalog.Clear();
			}	

			//answer = dbReadData(query);
			//data = answer.getData;
			data = dbReadData(query);
			DataItemsList(subcatalog, data);// convert data to list of Item enstance
			
			count = catalog.Count;
			//answer.getData.Clear();
			data.Clear();		
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

			//el = dbReadElement(query);

			List <object[]> data = dbReadData(query);
			el = new List<Element>();
			DataElementsList(el, data);			
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

		public  int Error_code { get { return Info.err_code; } }

		public string Error_message { get { return Info.err_message; } }

		public string LastQuery { get { return Info.query; } }

		public string DataBaseName { get { return Info.database; } }

		public void ResetError_info()
		{
			Info.err_code = 0;
			Info.err_message = "";
			Info.query = "";
			Info.database = "";
		}

		/*
		 * methods to convert data from list of objects` arrays getted from data base (getValues() in dbDataReader()) to wanted type;
		 */

		public void DataItemsList(List<Item> items, List<object[]> data) // to have exchange dbController.Catalog()
		{
			for (int k = 0; k < data.Count; k++)
			{
				Item item = new Item();
				object[] arr = data[k];
				int id = (int)(long)arr[0];
				string name = arr[1].ToString();
				item.createItem(id, name);
				items.Add(item);
			}
		}

		public void DataElementsList(List<Element> elements, List<object[]> data) // to have change dbController.dbReadElement()
		{
			for (int k = 0; k < data.Count; k++)
			{
				object[] arr = data[k];
				int id = (int)(long)arr[0];
				string name = arr[1].ToString();
				double num = (double)arr[2];
				Element el = new Element(id, name, num);
				elements.Add(el);
			}
		}

		public void DataNamesList(List<string> names, List<object[]> data) // to have change dbController.dbReader()
		{
			for (int k = 0; k < data.Count; k++)
			{
				object[] arr = data[k];
				string name = arr[0].ToString();
				names.Add(name);
			}
		}

		public void DataTechnologiesList(List<string> cards, List<object[]> data)
		{
			for (int k = 0; k < data.Count; k++)
			{
				object[] arr = data[k];
				string technology = arr[0].ToString();
				technology += "*" + arr[1].ToString();
				cards.Add(technology);
			}
		}

		public void DataViewsList(List<string> views, List<object[]> data)
		{
			string view = "";
			for (int k = 0; k < data.Count; k++)
			{
				object[] arr = data[k];
				view += arr[0].ToString();
				for (int q = 1; q < arr.Length; q++)
				{
					view += " " + arr[q].ToString();
				}
				views.Add(view);
				view = "";
			}
		}
	}
}
