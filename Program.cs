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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        
        static void Main()
        {
            /*
             * Checking connection, links https://marketsplash.com/tutorials/c-sharp/csharp-how-to-use-sqlite/#link7
             */
            dbController db = new dbController();
            if (db.testConnection())
            {
                string message = $"Unable to open data base file, connection string: {db.ConnectionString}.";
                MessageBox.Show(message);                
                return;
            }

            /*
             * Starting application
             */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Categories());
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

    }
}

