/*
 * classes Element and ReceptureStruct, structure Item
 */

using System;
using System.Collections.Generic;

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
			 

		public ReceptureStruct(int id)
		{
			this.id = id;
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
			dbController db = new dbController();

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
			 
		   void getItemData(string query, out string field, out int number)
			{
				number = 0;
				field = "unknown";
				List<Item> list = db.Catalog(query);
				if (list.Count > 0)
				{
					item = list[0];
					field = item.name;
					number = item.id;
				}
			}
 
			name = db.dbReader(SubQuery(0))[0];
			getItemData(Query(1, SubQuery(1)), out category, out data_id[0]);
			source = CheckData(db.dbReader(SubQuery(4)));
			author = CheckData(db.dbReader(SubQuery(5)));
			getItemData(Query(2, SubQuery(2)), out technology, out data_id[1]);
			getItemData(Query(3, SubQuery(3)), out ingredient, out data_id[2]);                
			description = CheckData(db.dbReader(SubQuery(6)));
			url = CheckData(db.dbReader(SubQuery(7)));
		}

		public string [] getData() => new string[] { name, category, ingredient, author, source, technology, description};

		public string [] EditorData => new string[] { name, source, author, url, description };

		public int getId() => id;

		public int [] getIds() => data_id;

		public string getName() => name;
		

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
    }

	public class DBAnswer
	{
		public AnswerInfo Info;
		public List<object[]> data;

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
