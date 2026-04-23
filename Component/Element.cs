/*
 * classes Element and ReceptureStruct, structure Item
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Net;
using System.Windows.Forms;

namespace MajPAbGr_project
{
	public class Element
	{
		int id;
		string name;        
		double amounts;        

		public Element()
		{
			name = "name";
			amounts = 0;            
		}

		public Element(int id, string name, double amounts)
		{
			this.id = id;
			this.name = name;
			this.amounts = amounts;
		}

		public string Name
		{
			set { name = value; }
			get { return name; }
		}
		public double Amounts
		{
			set { amounts = value; }
			get { return amounts; }
		}

		public int Id
		{
			set { id = value; }
			get { return id; }
		}

		public override string ToString()
		{
			return name;
		}
	};

	public class ReceptureStruct
	{
		int id;
		string name, category, source, author, technology, ingredient, description, url;       
		int[] data_id;
		// 0 - category, 1 - technology, 2 - ingredient
			 

		public ReceptureStruct(int id) // for reading from DB
		{
			this.id = id;
            this.name = "";
            this.source = "";
            this.author = "";
            this.description = "";
            this.url = "";
            data_id = new int[3];
            data_id[0] = 0;
            data_id[1] = 0;
            data_id[2] = 0;
        }

		public ReceptureStruct (int id, string name, int category)
			//for creating new entity without full data
		{
            this.id = id;
            this.name = name;
			this.source = "";
			this.author = "";
			this.description = "";
			this.url = "";
            data_id = new int[3];
            data_id[0] = category;
			data_id[1] = 0;
			data_id[2] = 0;
		}

		public ReceptureStruct			
            (
			int id, string name,
			string source, string author, string description, string url,
			int category, int technology, int ingredient
			)
        //for creating new entity with full data
        {
            this.id = id;
            this.name = name;
            this.source = source;
            this.author = author;
            this.description = description;
            this.url = url;
            data_id = new int[3];
            data_id[0] = category;
            data_id[1] = technology;
			data_id[2] = ingredient;
        }

		public void setDataStrings(string category, string technology, string ingredient)
		{
			this.category = category;
			this.technology = technology;
			this.ingredient = ingredient;
		}

		public void setDataStrings(string[] names)
		{
			this.category = names[0];
            this.technology = names[1];
            this.ingredient = names[2];
        }


		public void setData
			(
            string source, string author, string description, string url,
            int technology, int ingredient
            )
			// for created entity
		{
			this.source = source;
            this.author = author;
            this.description = description;
            this.url = url;            
            data_id[1] = technology;
            data_id[2] = ingredient;
        }

		public void UpdateData(ReceptureStruct rec)
		{
			string [] data = rec.EditorData;			
			int[] ids = rec.getIds();
			
			this.id = rec.getId();
			this.name = rec.getName();
			
			this.source = data[1];
			this.author = data[2];
			this.url = data[3];
			this.description = data[4];

			data_id[0] = ids[0];
			data_id[1] = ids[1];
			data_id[2] = ids[2];

			setDataStrings(rec.getNames);
        }

		public void setData()
		{
			string[]
				tables = {
				"Recepture", // 0
				"Categories", // 1
				"Technology", // 2
				"Ingredients" // 3
			},
				column_names = {
				"name", // 0
				"id_category", // 1
				"id_technology", // 2
				"id_main", // 3
				"source", // 4
				"author", // 5
				"description", // 6
				"URL" // 7
			};

			int id = this.id;
			data_id = new int[3];
			Item item;
			//dbController db = new dbController();
			tbController tb = new tbController();

			string SubQuery(int column)
				=> $"select {column_names[column]} from {tables[0]} where id = {id}";

			string Query(int table, string subquery)
				=> $"select id, {column_names[0]} from {tables[table]} where id = ({subquery});";
 
			string CheckData(List<string> data)
			{
				if (data.Count < 1)
					return "no data";
				if (data[0] == "" || data[0] == " ")
					return "unknown";
				return data[0];
			}
			 
		   void readItemData(string query, out string field, out int number)
			{
				number = 0;
				field = "unknown";
				List <Item> list = tb.Catalog(query);
				//List<object[]> data = tb.dbReadData(query);
				//List<Item> list = new List<Item>();				
				//tb.DataItemsList(list, data);
				//data.Clear();
				if (list.Count > 0)
				{
					item = list[0];
					field = item.name;
					number = item.id;
				}
			}
 
			name = tb.dbReader(SubQuery(0))[0];
			readItemData(Query(1, SubQuery(1)), out category, out data_id[0]);
			source = CheckData(tb.dbReader(SubQuery(4)));
			author = CheckData(tb.dbReader(SubQuery(5)));
			readItemData(Query(2, SubQuery(2)), out technology, out data_id[1]);
			readItemData(Query(3, SubQuery(3)), out ingredient, out data_id[2]);                
			description = CheckData(tb.dbReader(SubQuery(6)));
			url = CheckData(tb.dbReader(SubQuery(7)));
		}

		public string [] getData() => new string[] { name, category, ingredient, author, source, technology, description};

		public string [] EditorData => new string[] { name, source, author, url, description };

		

		public int getId() => id;

		public int [] getIds() => data_id;

		public string getName() => name;

		public string[] getNames => new string[] {category, technology, ingredient };

		public string getCategory() => category;
	}



	public struct Item
	{
		public int id;
		public string name;

		public void createItem(int id, string name)
		{
			this.id = id;
			this.name = name;
		}
		public override string ToString()
		{
			return name;
		}
	}


	/*
	 * For error handling
	 */
	public struct AnswerInfo
    {
		public int err_code;
		public string err_message;
		public string query;
		public string database;

		public AnswerInfo(int code,
						string message,
						string queryText,
						string address)
        {
			err_code = code;
			err_message = message;
			query = queryText;
			database = address;
		}

		public void setErrInfo(int code, string message)
		{
            err_code = code;
            err_message = message;
        }

		public void setQueryInfo(string queryText, string address)
		{
			query = queryText;
            database = address;            
        }

		public void Clear()
		{
            err_code = 0;
            err_message = "";
            query = "";
            database = "";
        }
    }

	public class DBAnswer
	{
		private AnswerInfo Info;
		private List<object[]> data;

        public DBAnswer(
						int code,
						string message,
						string queryText,
						string address,
						List <object[]> values
						)
        {
			Info = new AnswerInfo(
									code,
									message,
									queryText,
									address
									);			
			data = values;
        }

		public List<object[]> getData
		{
			get
			{
				return data;
			}
		}

		public AnswerInfo getInfo
        {
            get
            {
				return Info;
            }
        }
		
		
	}
}
