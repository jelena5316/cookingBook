using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MajPAbGr_project
{	
	static class Program
	{

		public static string connectionStringPath = "Data Source = db\\CookingBook; Mode=ReadWrite";
		private static bool  connectionStringIsCorrect = true;
		private static string PATH = "C:\\Users\\user\\source\\repos\\MajPavGr_project1\\Config.txt"; // stores a coonection string

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]


		static void Main()
		{
            /*
			 * Проверки
			 *	есть ли файл и строка в нём
			 *	соответсвует ли структура базы данных программной (на будущее)
			 */


            /*
			 * Чтение из App.config
			 */
            //connectionStringPath = ConfigurationManager.ConnectionStrings["SQLiteDB"].ToString();


            /*
			 * Reading connection string from config file
			 */
            //using (StreamReader reader = new StreamReader(PATH))
            //{
            //	connectionStringPath = reader.ReadLine();
            //	reader.Close();
            //}


            /*
			 * Testing connection string
			 */
            //int result = new dbController(connectionStringPath).testConnectionStringPCL();


            /*
			 * Get data base schema using patterns (debugging)
			 */
           //"SELECT name FROM sqlite_schema WHERE type = 'table';"; // LIKE '%pattern' -- b.e. '%able'
            //System.Diagnostics.Debug.WriteLine(message);
           

			/*
			 * Creating new data base if not exists and mode ir ReadWrite
			 */
			/*
            dbController dbTest = new dbController();			
			if (!dbTest.testConnection())
			{
				string message = $"Unable to open data base file, connection string: {dbTest.ConnectionString}.\n" +
					$"Do you want create a data base?";
				DialogResult answer = MessageBox.Show(
					message,
					"Connection test",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button1
					);

				if (answer == DialogResult.Yes)
				{
					Tables tbs = new Tables();
					dbTest.ConnectionString = @"Data Source = db\CookingBook; Mode=ReadWriteCreate";
                    dbTest.resetConnecting();
					dbTest.CreateDataBaseTables(dbTest);
					connectionStringIsCorrect = true;
					//creating db file and tables						
				}
				else
				{
					connectionStringIsCorrect = false;
				}
			}
			*/

			/*
			 * Starting application
			 */
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (!connectionStringIsCorrect)
				Application.Run(new EditDB(connectionStringPath));
			 //Start with EditDB to edit (correct) connection string
			/* if a connection string is succesefully corrected, then a variable, what is used as condition, is equel 'true'
			 * else it is equel 'false'.
			 * Value of this variable will be changed in case the connection string is changed AND it is correct -- 'true'.
			 * In case connection string is neither not changed or not correct this variable will be not changed -- 'false'.
			 * In case connection string is changed, but stay incorrect, this variable stay unchanged too -- 'false'.
			 */

			if (connectionStringIsCorrect)	
			//Start applications works in case when connection string is corrected									   
			{				
				//db.CreateDataBaseTables(db); //???				
				CategoriesController controller = new CategoriesController();
				controller.Catalog.SelectedRecIndex = 0;				
				Application.Run(new Categories());
			}				
			else //connection string is wrong; 'false'
				MessageBox.Show("Thank you for using our application!");
				Application.Exit();
		}



		public static void cook_error(string message)
		{
			const string PATH = "C:\\Users\\user\\Desktop\\log.txt";           
			using (StreamWriter stream = new StreamWriter(PATH, true))
			{
				if (!File.Exists(PATH))
				{
					File.CreateText(PATH);
					stream.WriteLine($"File is created: {File.GetLastWriteTime(PATH)} \n");
				}
				stream.WriteLine(message);
				stream.Close();
			}
		}

		public static bool ConnectionStringIsCorrect
		{		
			set {connectionStringIsCorrect = value;	}
			get { return connectionStringIsCorrect; }
		}

		public static void StoreConnectionString() // used in EditDB
		{
			using (StreamWriter stream = new StreamWriter(PATH))
			{
				if (!File.Exists(PATH))
				{
					File.CreateText(PATH);					
				}
				stream.WriteLine(connectionStringPath);
				stream.Close();
			}
		}

	}
}

