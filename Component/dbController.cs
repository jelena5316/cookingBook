/*
 * to provide accessig to data base
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.Data.Sqlite;

namespace MajPAbGr_project
{ 
	public abstract class ADataBaseCreator
    {
		//tables
		protected string TABLE_INGREDIENTS;
		protected string TABLE_CATEGORIES;
		protected string TABLE_CARDS;
		protected string TABLE_TEHNOLOGY;
		protected string TABLE_CHAINS;
		protected string TABLE_RECEPTURE;
		protected string TABLE_AMOUNTS;
		protected string TABLE_RECIPE;
		// table "Recipe" store coefficients
		// and futher will be used short name "COEF"
		// as part of name for variables storing it's columns` names
		//further three tables storing congiguration data
		protected string TABLE_VERSION;
		protected string TABLE_DB;		
		protected string TABLE_TB;
		protected string TABLE_COL;		

		//columns in table "Ingredients"
		protected string INGR_COL_ID;
		protected string INGR_COL_NAME;

		//columns in table "Categories"
		protected string CAT_COL_ID;
		protected string CAT_COL_NAME;

		//columns in table "Technology_card"
		protected string CARD_COL_ID;
		protected string CARD_COL_NAME;
		protected string CARD_COL_DESCRIPTION;
		protected string CARD_COL_TECHNOLOGY;

		//columns in table "Technology"
		protected string TECHN_COL_ID;
		protected string TECHN_COL_NAME;
		protected string TECHN_COL_DESCRIPTION;

		//columns in table "Technology_chain"
		protected string CHAIN_COL_ID;
		protected string CHAIN_COL_TECHN;
		protected string CHAIN_COL_CARD;

		//columns in table "Recepture"
		protected string REC_COL_ID;
		protected string REC_COL_NAME;
		protected string REC_COL_CAT;
		protected string REC_COL_TECHN;
		protected string REC_COL_INGR;
		protected string REC_COL_DESCRIPTION;
		protected string REC_COL_SOURCE;
		protected string REC_COL_AUTOR;
		protected string REC_COL_URL;

		//columns in table "Amounts"
		protected string AM_COL_ID;		
		protected string AM_COL_REC;		
		protected string AM_COL_INGR;
		protected string AM_COL_AMOUNTS;

		//columns in table "Recipe"
		protected string COEF_COL_ID;
		protected string COEF_COL_NAME;
		protected string COEF_COL_REC;		
		protected string COEF_COL_COEFFICIENT;

		//columns int table "db_version"
		protected string V_COL_ID;
		protected string V_COL_NUM;
		protected string V_COL_NAME;
		protected string V_COL_DATE;
		protected string V_COL_TABLES;
		protected string V_COL_COLS;
		protected string V_COL_CONTROL;

		//columns in table "db_config" 
		protected string DB_COL_ID;
		protected string DB_COL_NAME;
		protected string DB_COL_V;
		protected string DB_COL_CON;

		//columns in table "db_tables"
		protected string TB_COL_ID;
		protected string TB_COL_V;
		protected string TB_COL_DEF_NAME; // default name
		protected string TB_COL_CUR_NAME; // current name

		//columns in table "db_columns"
		protected string COl_COL_ID;
		protected string COL_COL_V;
		protected string COl_COL_TABLE;  // table id
		protected string COL_COL_DEF_NAME; // default name
		protected string COL_COL_CUR_NAME; //current name

		protected abstract void setTablesColumnsNames(Tables tbs);		
		protected abstract string[] createQueries();

		public abstract TablesCreator CreateDataBaseTables(dbController db);
	}

	public class DataBaseCreator : ADataBaseCreator
	{
		private static int version = 0;
		protected string[] pragmaQuery = new string[]
		{
			"pragma user_version;",
			 $"pragma user_version = {version};",
		};

		public string PragmaQuery(int new_version, int action)
		{
			// 0 - get version reading it in data base
			// 1 - set new data base
			if (action == 0)
			{
				return pragmaQuery[0];
			}
			else
			{
				version = new_version;
				string query = pragmaQuery[1];
				version = 0;
				return query;
			}
		}

		protected override void setTablesColumnsNames(Tables tbs)
		{
			//define the fields						
			Table[] tables = tbs.getTables;
			for (int k = 0; k < tables.Length; k++)
				setNames(tables[k]);
		}

		private void setNames(Table table)
		{
			int title = table.Title;
			switch (title)
			{
				case 0: // ingredients
					{
						TABLE_INGREDIENTS = table.Name;
						INGR_COL_ID = table.Columns[0];
						INGR_COL_NAME = table.Columns[1];
						break;
					}
				case 1: // categories
					{
						TABLE_CATEGORIES = table.Name;
						CAT_COL_ID = table.Columns[0];
						CAT_COL_NAME = table.Columns[1];
						break;
					}
				case 2: // cards
					{
						TABLE_CARDS = table.Name;
						CARD_COL_ID = table.Columns[0];
						CARD_COL_NAME = table.Columns[1];
						CARD_COL_DESCRIPTION = table.Columns[2];
						CARD_COL_TECHNOLOGY = table.Columns[3];
						break;
					}
				case 3: // technology
					{
						TABLE_TEHNOLOGY = table.Name;
						TECHN_COL_ID = table.Columns[0];
						TECHN_COL_NAME = table.Columns[1];
						TECHN_COL_DESCRIPTION = table.Columns[2];
						break;
					}
				case 4: // chains
					{
						TABLE_CHAINS = table.Name;
						CHAIN_COL_ID = table.Columns[0];
						CHAIN_COL_TECHN = table.Columns[1];
						CHAIN_COL_CARD = table.Columns[2];
						break;
					}
				case 5: // recepture
					{
						TABLE_RECEPTURE = table.Name;
						REC_COL_ID = table.Columns[0];
						REC_COL_NAME = table.Columns[1];
						REC_COL_TECHN = table.Columns[2];
						REC_COL_INGR = table.Columns[3];
						REC_COL_CAT = table.Columns[4];
						REC_COL_DESCRIPTION = table.Columns[5];
						REC_COL_SOURCE = table.Columns[6];
						REC_COL_AUTOR = table.Columns[7];
						REC_COL_URL = table.Columns[8];
						break;
					}
				case 6: // amounts
					{
						TABLE_AMOUNTS = table.Name;
						AM_COL_ID = table.Columns[0];
						AM_COL_REC = table.Columns[1];
						AM_COL_INGR = table.Columns[2];
						AM_COL_AMOUNTS = table.Columns[3];
						break;
					}
				case 7: // recipe
					{
						TABLE_RECIPE = table.Name;
						COEF_COL_ID = table.Columns[0];
						COEF_COL_NAME = table.Columns[1];
						COEF_COL_REC = table.Columns[2];
						COEF_COL_COEFFICIENT = table.Columns[3];
						break;
					}
				case 8: // data base version
                    {
                        TABLE_VERSION = table.Name;
                        V_COL_ID = table.Columns[0];
                        V_COL_NUM = table.Columns[1];
                        V_COL_NAME = table.Columns[2];
                        V_COL_DATE = table.Columns[3];
                        V_COL_TABLES = table.Columns[4];
                        V_COL_COLS = table.Columns[5];
                        V_COL_CONTROL = table.Columns[6];
                        break;
                    }
				case 9: // data base configs
                    {
						TABLE_DB = table.Name;
						DB_COL_ID = table.Columns[0];
						DB_COL_NAME = table.Columns[1];
						DB_COL_V = table.Columns[2];
						DB_COL_CON = table.Columns[3];
						break;
                    }
				case 10: // data base tables
                    {
						TABLE_TB = table.Name;
						TB_COL_ID = table.Columns[0];
						TB_COL_V = table.Columns[1];
						TB_COL_DEF_NAME = table.Columns[2];
						TB_COL_CUR_NAME = table.Columns[3];
						break;
                    }
				case 11: // data base columns
                    {
						TABLE_COL = table.Name;
						COl_COL_ID = table.Columns[0];
						COL_COL_V = table.Columns[1];
						COl_COL_TABLE = table.Columns[2];
						COL_COL_DEF_NAME = table.Columns[3];
						COL_COL_CUR_NAME = table.Columns[4];
						break;
                    }
				default: break;
			}
		}

		public string TechnologiesQuery => createQueries()[3];

		protected override string[] createQueries()
		{
			//set queries for database creating
			return new string[]
			{
				IngredientsQ(), CategoriesQ(), CardsQ(),
				TechnologyQ(), ChainsQ(), ReceptureQ(),
				AmountsQ(), RecipeQ(),
				DbVersionQ(), DbConfigQ(), DbTablesQ(), DbColumnsQ()
				};
		}

		public virtual void onRun() { }

		private string IngredientsQ() => $"CREATE TABLE IF NOT EXISTS {TABLE_INGREDIENTS}(" +
										$" {INGR_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
										$"{INGR_COL_NAME} VARCHAR UNIQUE NOT NULL " +
										$"CHECK ({INGR_COL_NAME} != \"\" AND length({INGR_COL_NAME}) <= 20));";

		private string CategoriesQ() => $"CREATE TABLE IF NOT EXISTS {TABLE_CATEGORIES}(" +
										$"{INGR_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
										$"{INGR_COL_NAME} VARCHAR UNIQUE NOT NULL " +
										$"CHECK({CAT_COL_NAME} != \"\" AND length({CAT_COL_NAME}) <= 20));";

		private string CardsQ() => $"CREATE TABLE IF NOT EXISTS {TABLE_CARDS}(" +
									$"{CARD_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
									$"{CARD_COL_NAME} VARCHAR NOT NULL, " +
									$"{CARD_COL_DESCRIPTION} VARCHAR, " +
									$"{CARD_COL_TECHNOLOGY} VARCHAR NOT NULL " +
									$"CHECK(({CARD_COL_NAME} != \"\") " +
									$"AND ({CARD_COL_DESCRIPTION} != \"\") " +
									$"AND ({CARD_COL_TECHNOLOGY} != \"\") " +
									$"AND length({CARD_COL_NAME}) <= 30 " +
									$"AND length({CARD_COL_DESCRIPTION}) <= 250 " +
									$"AND length({CARD_COL_TECHNOLOGY}) <= 500));";

		private string TechnologyQ() => $"CREATE TABLE IF NOT EXISTS {TABLE_TEHNOLOGY}(" +
										$"{TECHN_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
										$"{TECHN_COL_NAME} VARCHAR NOT NULL, " +
										$"{TECHN_COL_DESCRIPTION} VARCHAR, " +
										$"CHECK(({TECHN_COL_NAME} != \"\") " +
										$"AND	({TECHN_COL_DESCRIPTION} != \"\")" +
										$"AND length({TECHN_COL_NAME}) <= 30 " +
										$"AND length({TECHN_COL_DESCRIPTION}) <= 150));";

		private string ChainsQ() => $"CREATE TABLE IF NOT EXISTS {TABLE_CHAINS}(" +
									$"{CHAIN_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
									$"{CHAIN_COL_TECHN} NOT NULL, " +
									$"{CHAIN_COL_CARD} NOT NULL REFERENCES {TABLE_CARDS}({CARD_COL_ID}) ON DELETE CASCADE, " +
									$"FOREIGN KEY({CHAIN_COL_TECHN}) " +
									$"REFERENCES {TABLE_TEHNOLOGY}({TECHN_COL_ID}) ON DELETE CASCADE," +
									$"FOREIGN KEY({CHAIN_COL_CARD}) REFERENCES {TABLE_CARDS}({CARD_COL_ID}) ON DELETE CASCADE);";

		private string ReceptureQ() => $"CREATE TABLE {TABLE_RECEPTURE}(" +
										$"{REC_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
										$"{REC_COL_NAME} VARCHAR UNIQUE NOT NULL " +
										$"CHECK({REC_COL_NAME} != \"\" AND length({REC_COL_NAME}) <= 25), " +
										$"{REC_COL_TECHN} INTEGER, " +
										$"{REC_COL_INGR} INTEGER REFERENCES {TABLE_INGREDIENTS}({INGR_COL_ID}) ON DELETE NO ACTION, " +
										$"{REC_COL_CAT} INTEGER NOT NULL DEFAULT 1, " +
										$"{REC_COL_DESCRIPTION} VARCHAR CHECK({REC_COL_DESCRIPTION} != \"\" AND length({REC_COL_DESCRIPTION}) <= 300), " +
										$"{REC_COL_SOURCE} VARCHAR CHECK({REC_COL_SOURCE} != \"\" AND length({REC_COL_SOURCE}) <= 20), " + 
										$"{REC_COL_AUTOR} VARCHAR CHECK({REC_COL_AUTOR} != \"\" AND length({REC_COL_AUTOR}) <= 25), " +
										$"{REC_COL_URL} VARCHAR CHECK({REC_COL_URL} != \"\" AND length({REC_COL_URL}) <= 200), " +
										$"FOREIGN KEY({REC_COL_TECHN}) " + 
										$"REFERENCES {TABLE_TEHNOLOGY}({TECHN_COL_ID}) ON DELETE SET NULL, " + 
										$"FOREIGN KEY({REC_COL_CAT}) " +
										$"REFERENCES {TABLE_CATEGORIES}({INGR_COL_ID}) ON DELETE SET DEFAULT);";


		private string AmountsQ() => $"CREATE TABLE IF NOT EXISTS {TABLE_AMOUNTS} ( " +
									$"{AM_COL_ID} INTEGER  PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, " +
									$"{AM_COL_REC}   INTEGER NOT NULL " +
									$"CONSTRAINT[привязка к рецептуре] REFERENCES Recepture({REC_COL_ID}) ON DELETE CASCADE, " +
									$"{AM_COL_INGR} INTEGER  NOT NULL " +
									$"CONSTRAINT[имя сырья] REFERENCES Ingredients({INGR_COL_ID}) ON DELETE CASCADE, " +
									$"{AM_COL_AMOUNTS} REAL(6) NOT NULL DEFAULT 0 " +
									$"CHECK({ AM_COL_AMOUNTS} > 0));";

		private string RecipeQ() => $"CREATE TABLE IF NOT EXISTS  NOT EXISTS {TABLE_RECIPE} ( " +
									$"{COEF_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
									$"{COEF_COL_NAME} VARCHAR NOT NULL " +
									$"CHECK({COEF_COL_NAME} != \"\" AND length({COEF_COL_NAME}) <= 20), " +
									$"{COEF_COL_REC} INTEGER NOT NULL, " +
									$"{COEF_COL_COEFFICIENT}  REAL NOT NULL DEFAULT 1, " +
									$"FOREIGN KEY({COEF_COL_REC}) REFERENCES Recepture({REC_COL_ID}) ON DELETE CASCADE);";

		private string DbVersionQ() =>  $"CREATE TABLE IF NOT EXISTS {TABLE_VERSION} ( " +
										$"{V_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
										$"{V_COL_NUM} INTEGER," +
										$"{V_COL_NAME} VARCHAR," +
										$"{V_COL_DATE} TEXT," +
										$"{V_COL_TABLES} INT," +
										$"{V_COL_COLS} INT," +
										$"{V_COL_CONTROL} INT);";

		private string DbConfigQ() =>	$"CREATE TABLE IF NOT EXISTS {TABLE_DB} ( " +
										$"{DB_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
										$"{DB_COL_NAME} STRING, " +
										$"{DB_COL_V} INT REFERENCES {TABLE_VERSION}({V_COL_ID}), " +
										$"{DB_COL_CON} STRING);";

		private string DbTablesQ() =>	$"CREATE TABLE IF NOT EXISTS {TABLE_TB} ( " +
										$"{TB_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
										$"{TB_COL_V} INT REFERENCES {TABLE_DB}({DB_COL_V}), " +
										$"{TB_COL_DEF_NAME} STRING, " +
										$"{TB_COL_CUR_NAME} STRING);";

		private string DbColumnsQ() =>  $"CREATE TABLE IF NOT EXISTS {TABLE_COL} ( " +
										$"{COl_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
										$"{COl_COL_TABLE} INT REFERENCES {TABLE_TB}({TB_COL_ID}), " +
										$"{COL_COL_DEF_NAME} STRING, " +
										$"{COL_COL_CUR_NAME} STRING);";

		public override TablesCreator CreateDataBaseTables(dbController db)
		{
			return new TablesCreator(createQueries(), db);
		}
	}



	public class dbController :DataBaseCreator /*access data base*/
	{
		private string connectionString;
		private SqliteConnection connection;
		private SqliteDataReader reader;
		private SqliteCommand command;



		protected int error_code = 0;
		protected string error_message="";
		protected AnswerInfo Info;

		public dbController ()
		{
			Tables tbs = new Tables();
			setTablesColumnsNames(tbs);
			connectionString = Program.connectionStringPath;
			connection = new SqliteConnection(connectionString);
		}


        /*
         * From Formtest_EF
         */
       
        public SqliteConnection Connection => connection;
        public SqliteDataReader Reader
        {
            get { return reader; }
            set { reader = value; }
        }
        public SqliteCommand Command
        {
            get { return command; }
            set { command = value; }
        }
		/*
		 * End
		 */

        public AnswerInfo getInfo() { return Info; }

		/*
		 * Testing conection with data base file for class Program
		 */

		public string ConnectionString
		{
			set
			{
				connectionString = value;
			}
			get
			{ 
				return connectionString.ToString();
			} 
		}

		public void resetConnecting()
        {
			connection = new SqliteConnection(connectionString);
		}

		public bool testConnection()
		{
			string message = "";
			using (connection)
			{
				try
				{
					connection.Open();
					connection.Close();
					return true;
				}
				catch(SqliteException ex) // https://marketsplash.com/tutorials/c-sharp/csharp-how-to-use-sqlite/#link7
				{
					if (ex.SqliteErrorCode == 14)
						message = $"{System.DateTime.Now} {ex.Message} {connectionString}";
						Program.cook_error(message);
					return false;
				}
			}
		}

		public override TablesCreator CreateDataBaseTables(dbController db)
		{
			TablesCreator tc = base.CreateDataBaseTables(this);
			int result = tc.createTable();
			Console.WriteLine($"Created tables number is {result}");
			return tc;
		}


        /*
		 * 'update', 'delete', 'insert'
		 */
        public int Edit(string query)
        {
            int ind; //to store the number of rows inserted, updated, or deleted. 
            using (connection)
            {
                command = new SqliteCommand(query, connection);
                connection.Open();
                try
                {
                    ind = command.ExecuteNonQuery();
                }
                catch
                {
                    ind = 0; // -1 for 'select' statements
                }
                finally
                {
                    connection.Close();
                }
            }
            return ind;
        }


        /*
		 * insert and get id of last inserted row; 'select' with function return a number
		 */

        public string Count(string query)
        {
            string count = "";
            using (connection)
            {
                command = new SqliteCommand(query, connection);
                connection.Open();
                var num = command.ExecuteScalar();
                count = num.ToString();
                connection.Close();
            }
            return count;
        }

        /*
		 * 'select'
		 */

        public List<Item> Catalog (string query) //int, string
		{
			Item item;
			List<Item> list = new List<Item>();
			using (connection)
			{
				command = new SqliteCommand(query, connection);
				try
				{
					connection.Open();
					using (reader = command.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								int id = int.Parse(reader.GetValue(0).ToString());
								string name = reader.GetValue(1).ToString();
								item = new Item();
								item.createItem(id, name);
								list.Add(item);
							}
						}
					}
					connection.Close();
					Info = new AnswerInfo(0, "", query, connectionString);
					return list;
				}
				catch (SqliteException ex)
				{
					error_code = ex.SqliteErrorCode; // получаем код ошибки
					error_message = ex.Message; // получаем сообщение об ошибке                   
					Program.cook_error($"{System.DateTime.Now} {ex.Message}");
					return list;
				}                                
			}            
		}
		//was used in public List <Item> getTechnologiesIdsByName(string name) (tbTechnologyController.cs)
		//was used in public void setData() (Element.cs)

		public List<string> dbReader(string query)  // only strings   
		{
			List<string> list = new List<string>();
			command = new SqliteCommand(query, connection);
			try
			{
				connection.Open();
				using (connection)
				{
				
					using (reader = command.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								var name = reader.GetValue(0);                           
								list.Add(name.ToString());
							}
						}   
					}
				}
			Info = new AnswerInfo(0, "", query, connectionString);
			connection.Close();
			return list;
			}
			catch (SqliteException ex)
			{
				Info = new AnswerInfo(ex.ErrorCode, ex.Message, query, connectionString);
				connection.Close();
				Program.cook_error($"{System.DateTime.Now} {ex.Message}");
				return list;
			}
		}
		//was used in public void setData() (Element.cs) too

		public List<Element> dbReadElement(string query) // int, string, double
		{
			List<Element> list = new List<Element>();
			Element element;
			using (connection)
			{
				command = new SqliteCommand(query, connection);
				try
				{
					connection.Open();
					using (reader = command.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								element = new Element();
								var id_ingr = reader.GetValue(0);
								string name = reader.GetValue(1).ToString();
								var amounts = reader.GetValue(2);
								element.Name = name;
								element.Id = int.Parse(id_ingr.ToString());
								element.Amounts = double.Parse(amounts.ToString());
								list.Add(element);
							}
						}
					}
					connection.Close();
					Info = new AnswerInfo(0, "", query, connectionString);
					return list;
				}
				catch (SqliteException ex)
				{
					error_code = ex.SqliteErrorCode; // получаем код ошибки
					error_message = ex.Message; // получаем сообщение об ошибке                   
					Program.cook_error($"{System.DateTime.Now} {ex.Message}");
					return list;
				}
			}
		}

		public List<String> dbReadTechnology(string query) // only strings
		{
			List <String> cards = new List<String>();
			using (connection)
			{
				command = new SqliteCommand(query, connection);
				connection.Open();
				using (reader = command.ExecuteReader())
				{
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							string technology = reader.GetValue(0).ToString();
							technology += "*" + reader.GetValue(1).ToString();
							cards.Add(technology);
						}
					}
				}
				connection.Close();
			}
			return cards;
		}

		public List<String> dbReadView(string query)
		{
			List<String> view_data = new List<String>();
			using (connection)
			{
				command = new SqliteCommand(query, connection);
				connection.Open();
				using (reader = command.ExecuteReader())
				{
					string data = "";                    
					if (reader.HasRows)
					{                        
						while (reader.Read())
						{
							data += reader.GetValue(0).ToString();
							for (int k = 1; k < reader.FieldCount; k++)
							{
								data += " " + reader.GetValue(k).ToString();
							}                           
							view_data.Add(data);
							data = "";
						}
					}
				}
				connection.Close();
			}
			return view_data;
		}

		public List<object[]> dbReadData(string query)
		{
			int length = 0;
			//DBAnswer answer;
			List<object[]> data = new List<object[]>();

			try
			{
				connection.Open();
				using (command = new SqliteCommand(query, connection))
				{
					using (reader = command.ExecuteReader())
					{
						length = reader.FieldCount;
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								object[] arr = new object[length];
								reader.GetValues(arr);
								data.Add(arr);
							}
						}
					}				
				}
			
					//answer = new DBAnswer(0, "", query, connectionString, data);
					//Info = answer.getInfo;					
					//return answer.getData;
					
					Info = new AnswerInfo(0, "", query, connectionString);
					return data;
				}
				catch (SqliteException ex)
				{
					//error_code = ex.SqliteErrorCode; // получаем код ошибки
					//error_message = ex.Message; // получаем сообщение об ошибке                   
					//Program.cook_error($"{System.DateTime.Now} {ex.Message}");
					//answer = new DBAnswer(error_code, error_message, query, connectionString, data);
					//Info = answer.getInfo;					
					//return answer.getData;

					error_code = ex.SqliteErrorCode; // получаем код ошибки
					error_message = ex.Message; // получаем сообщение об ошибке                   
					Program.cook_error($"{System.DateTime.Now} {ex.Message}");
					Info = new AnswerInfo(error_code, error_message, query, connectionString);
					return data;
				}
				finally
				{
                    connection.Close();
                }
		}


		/*
		 * Methods will be replaced in subclasses
		 */
		public List<FormEF_test.Technology> DbReadTech(string query)
		{
			List<FormEF_test.Technology> cards = new List<FormEF_test.Technology>();
            using (connection)
            {
                command = new SqliteCommand(query, connection);
                connection.Open();
                using (reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
							FormEF_test.Technology tech = new FormEF_test.Technology();
							tech.Id = int.Parse(reader.GetValue(0).ToString());
                            tech.Name = reader.GetValue(1).ToString();
                            tech.Note =  reader.GetValue(2).ToString();
                            cards.Add(tech);
                        }
                    }
                }
                connection.Close();
            }
            return cards;
        }

	}


	

	

	/*
	 * Querie`s creators for table`s creating
	 */
	/*
	public class IngredientsQ: TableQueries
    {
		string query;
		public IngredientsQ(): base () {}
		public override void setNames(Table_v1 table)
        {
			TABLE_INGREDIENTS = table.setName;
			INGR_COL_ID = table.getColumns[0];
			INGR_COL_NAME = table.getColumns[1];
        }

		public override string createQuery()
        {
			query = $"CREATE TABLE {TABLE_INGREDIENTS}"+
					$" ({ INGR_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
					$"{INGR_COL_NAME} VARCHAR UNIQUE NOT NULL" +
					$"CHECK ({INGR_COL_NAME} != \"\" AND length(name) <= 20);";
			return query;
		}
	}

	public class CategoriesQ : TableQueries
	{
		string query;
		public CategoriesQ() : base() { }
		public override void setNames(Table_v1 table)
		{
			TABLE_CATEGORIES = table.setName;
			CAT_COL_ID = table.getColumns[0];
			CAT_COL_NAME = table.getColumns[1];			
		}

		public override string createQuery()
		{
			query = $"CREATE TABLE {TABLE_CATEGORIES}"+
				    $"({INGR_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT " +
				    $"NOT NULL {INGR_COL_NAME} VARCHAR UNIQUE NOT NULL " +
				    $"CHECK({CAT_COL_NAME} != \"\" AND length({CAT_COL_NAME}) <= 20);";
			return query;
		}
	}

	public class CardsQ : TableQueries
	{
		string query;
		public CardsQ() : base() { }
		public override void setNames(Table_v1 table)
		{
			TABLE_CARDS = table.setName;
			CARD_COL_ID = table.getColumns[0];
			CARD_COL_NAME = table.getColumns[1];
			CARD_COL_DESCRIPTION = table.getColumns[2];
			CARD_COL_DESCRIPTION = table.getColumns[3];
		}

		public override string createQuery()
		{
			query = $"CREATE TABLE {TABLE_CARDS} (" +
				$"{CARD_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
				$"{CARD_COL_NAME}  VARCHAR NOT NULL, " +
				$"{CARD_COL_DESCRIPTION} VARCHAR, " +
				$"{CARD_COL_TECHNOLOGY}  VARCHAR NOT NULL " +
				$"CHECK(length({CARD_COL_TECHNOLOGY}) <= 500), " +
				$"CHECK(({CARD_COL_NAME} != \"\") " +
				$"AND ({CARD_COL_DESCRIPTION} != \"\") " +
				$"AND ({CARD_COL_DESCRIPTION} != \"\") " +
				$"AND 	length({CARD_COL_TECHNOLOGY}) <= 30 " +
				$"AND length({CARD_COL_DESCRIPTION}) <= 250 " +
				$"AND length({CARD_COL_TECHNOLOGY}) <= 500));";
			return query;
		}
	}

	public class TechnologyQ: TableQueries
	{
		string query;
		public TechnologyQ() : base() { }

		public override void setNames(Table_v1 table)
		{
			TECHN_COL_ID = table.setName;
			TECHN_COL_NAME = table.getColumns[0];
			TECHN_COL_DESCRIPTION = table.getColumns[1];
		}
		public override string createQuery()
        {
			query = $"CREATE TABLE {TABLE_TEHNOLOGY}("+
					$"{TECHN_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, "+
					$"{TECHN_COL_NAME} VARCHAR NOT NULL, "+
					$"{TECHN_COL_DESCRIPTION} VARCHAR, "+
					$"CHECK(({TECHN_COL_NAME} != \"\") "+
					$"AND	({TECHN_COL_DESCRIPTION} != \"\")"+
					$"AND length({TECHN_COL_NAME}) <= 30 "+
					$"AND length({TECHN_COL_DESCRIPTION}) <= 150));";

			return query;
        }
	}

	public class ChainsQ : TableQueries
    {
		string query;
		public ChainsQ() : base() { }
		
		public override void setNames(Table_v1 table)
		{
			CHAIN_COL_ID = table.setName;
			CHAIN_COL_TECHN = table.getColumns[0];
			CHAIN_COL_CARD = table.getColumns[1];
		}

		public override string createQuery() 
		{
			query = $"CREATE TABLE {TABLE_CHAINS}(" +
					$"{CHAIN_COL_ID} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
					$"{CHAIN_COL_TECHN} NOT NULL, " +
					$"{CHAIN_COL_CARD} NOT NULL REFERENCES {TABLE_CARDS}({CARD_COL_ID}) ON DELETE CASCADE, " +
					$"FOREIGN KEY({CHAIN_COL_TECHN})	" +
					$"REFERENCES {TABLE_TEHNOLOGY}({TECHN_COL_ID}) ON DELETE CASCADE," +
					$"FOREIGN KEY(id_card{CHAIN_COL_CARD}) REFERENCES {TABLE_CARDS}({CARD_COL_ID}) ON DELETE CASCADE);";
			return query;
		}
	}

	public class ReceptureQ : TableQueries
	{
		string query;
		public ReceptureQ() : base() { }

		public override void setNames(Table_v1 table)
		{
			REC_COL_ID = table.setName;
			REC_COL_NAME = table.getColumns[0];
			REC_COL_CAT = table.getColumns[1];
			REC_COL_TECHN = table.getColumns[2];
			REC_COL_INGR = table.getColumns[3];
			REC_COL_DESCRIPTION = table.getColumns[4];
			REC_COL_SOURCE = table.getColumns[5];
			REC_COL_AUTOR = table.getColumns[6];
			REC_COL_URL = table.getColumns[7];
		}
		public override string createQuery()
		{
			query = "";
			return query;
		}
	}
	*/

	

	
	public enum Titles
	{
		INGR,
		CAT,
		CARD,
		TECHN,
		CHAIN,
		REC,
		AM,
		COEF,
		DB_VERS,
		DB_CONFIGS,
		DB_TBLS,
		DB_COL
	}


	public class Table
	{
		int title;
		string name;
		string[] columns;
		public Table() { }

		public int Title { set; get; }
		public string Name { set; get; }
		public string[] Columns { set; get; }
	}

	public class Tables
	{
		Table[] tables;

		public Tables()
		{
			tables = new Table[]
			{
				createIngr(), createCat(),
				createCard(), createTech(), createChain(),
				createRec(), createAm(), createCoef(),
				createVers(), createConf(), createTables(),
				createCol()
			};
		}

		public Table[] getTables
		{
			get { return this.tables; }
		}

		private Table createIngr()
		{
			Table tb = new Table();
			tb.Title = (int)Titles.INGR;
			tb.Name = "Ingredients";
			tb.Columns = new string[] { "id", "name" };
			return tb;
		}

		private Table createCat()
		{
			Table tb = new Table();
			tb.Title = (int)Titles.CAT;
			tb.Name = "Categories";
			tb.Columns = new string[] { "id", "name" };
			return tb;
		}

		private Table createCard()
		{
			Table tb = new Table();
			tb.Title = (int)Titles.CARD;
			tb.Name = "Technology_card";
			tb.Columns = new string[] { "id", "name", "description", "technology" };
			return tb;
		}

		private Table createTech()
		{
			Table tb = new Table();
			tb.Title = (int)Titles.TECHN;
			tb.Name = "Technology";
			tb.Columns = new string[] { "id", "name", "technology" };
			return tb;
		}

		private Table createChain()
		{
			Table tb = new Table();
			tb.Title = (int)Titles.CHAIN;
			tb.Name = "Technology_chain";
			tb.Columns = new string[] { "id", "id_technology", "id_card" };
			return tb;
		}

		private Table createRec()
		{
			Table tb = new Table();
			tb.Title = (int)Titles.REC;
			tb.Name = "Recepture";
			tb.Columns = new string[] {"id", "name",
										  "id_technology", "id_main",
										  "id_category", "description",
										   "source", "author", "URL"};
			return tb;
		}

		private Table createAm()
		{
			Table tb = new Table();
			tb.Title = (int)Titles.AM;
			tb.Name = "Amounts";
			tb.Columns = new string[] {"id", "id_recepture",
										  "id_ingredients", "amounts",};
			return tb;
		}

		private Table createCoef()
		{
			Table tb = new Table();
			tb.Title = (int)Titles.COEF;
			tb.Name = "Recipe";
			tb.Columns = new string[] {"id", "name",
										   "id_recepture","Coefficient",};
			return tb;
		}

		private Table createVers()
        {
			Table tb = new Table();
			tb.Title = (int)Titles.DB_VERS;
			tb.Name = "db_version";
			tb.Columns = new string[] {"id", "number", "name",
											"date", "tables",
											"columns", "control" };

			return tb;
		}

		private Table createConf()
        {
			Table tb = new Table();
			tb.Title = (int)Titles.DB_CONFIGS;
			tb.Name = "db_config";
			tb.Columns = new string[] {"id", "name", "version",
											"connection" };
			return tb;
		}

		private Table createTables()
		{
			Table tb = new Table();
			tb.Title = (int)Titles.DB_TBLS;
			tb.Name = "db_tables";
			tb.Columns = new string[] {"id", "version",
									   "default_name", "current_name" };
			return tb;
		}

		private Table createCol()
        {
			Table tb = new Table();
			tb.Title = (int)Titles.DB_COL;
			tb.Name = "db_columns";
			tb.Columns = new string[] {"id", "version", "id_table",
									   "default_name", "current_name" };
			return tb;
		}
	}

	public class TablesCreator
	{
		dbController db;
		string[] queries;

		public TablesCreator(string[] queries, dbController db)
		{
			this.db = db;
			this.queries = queries;
		}

		public int createTable()
		{
			int count, k;
			count = queries.Length;
			//count = 0;


			for (k = 0; k < queries.Length; k++)
			{
				try
				{
					//count += db.Edit(queries[k]);
					db.Edit(queries[k]);
					Console.WriteLine(queries[k]);
					Console.WriteLine("\n***\n");
				}
				catch
				{
					count--;
				}

			}
			return count;
		}
	}
}

