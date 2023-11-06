using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MajPAbGr_project
{
	static class Program
	{

		//public static string connectionStringPath = "Data Source = db\\CookingBook; Mode=ReadWrite";
		//public static string connectionStringPath = "Data Source = db\\CookingBoo; Mode=ReadWrite;"; // for debugging
		public static string connectionStringPath = "";
		private static bool  connectionStringIsCorrect = true;
		private static string PATH = "C:\\Users\\user\\source\\repos\\MajPavGr_project1\\Config.txt"; // stores a coonection string
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]


		static void Main()
		{
			/*
			 * Reading connection string from config file
			 */
			using (StreamReader reader = new StreamReader(PATH))
			{
				connectionStringPath = reader.ReadLine();
				reader.Close();
			}

				/*
				 * Checking connection, links https://marketsplash.com/tutorials/c-sharp/csharp-how-to-use-sqlite/#link7
				 */
				dbController db = new dbController();
			if (!db.testConnection())
			{
				string message = $"Unable to open data base file, connection string: {db.ConnectionString}.\n" +
					$"Do you want to continue?";
				DialogResult answer = MessageBox.Show(
					message,
					"Connection test",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button1                   
					);

				if(answer == DialogResult.Yes)
				{
					message = "Want you to improve a database connecting string or to finish work?";
					answer = MessageBox.Show(
					message,
					"Connection test",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button1
					);

					if (answer == DialogResult.Yes)
						connectionStringIsCorrect = false;
					else
						return;
				}
				else
				{
					return; 
				}
			}

            /*
			 * Starting application
			 */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			if (!connectionStringIsCorrect) //connection string is wrong; 'false'
				Application.Run(new EditDB(connectionStringPath)); // connection string editing
			/* if a connection string is succesefully corrected, then a variable, what is used as condition, is equel 'true'
			 * else it is equel 'false'.
			 * Value of this variable will be changed in case the connection string is changed AND it is correct -- 'true'.
			 * In case connection string is neither not changed or not correct this variable will be not changed -- 'false'.
			 * In case connection string is changed, but stay incorrect, this variable stay unchanged too -- 'false'.
			 */
			if (connectionStringIsCorrect) //connection string is correct; 'true'
				Application.Run(new Categories());
			else //connection string is wrong; 'false'
				MessageBox.Show("Thank you for using our application!");
				Application.Exit(); //connection string is wrong
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

		public static void StoreConnectionString()
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

