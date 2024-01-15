/*
 * to access to data base
 */



using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MajPAbGr_project
{  
    public partial class EditDB : Form
    {
        dbController db;
        tbController tb;
        string query="";
        List<string> views, tables;
        int result;
        
        public EditDB()
        {
            InitializeComponent();
            db = new dbController();            
            views = db.dbReader("SELECT name FROM sqlite_schema WHERE type = 'view';");
            tables = db.dbReader("SELECT name FROM sqlite_schema WHERE type = 'table';");
            box.Items.AddRange(views.ToArray());
            box.Text = "view list";
            cmb_tables.Items.AddRange(tables.ToArray());
            if(cmb_tables.Items.Count > 0)
                cmb_tables.SelectedIndex = 0;
        }

        public EditDB(string connectionStringPath)
        {
            InitializeComponent();            
            textBox1.Text = connectionStringPath;
            button1.Enabled = false;
            button1.Visible = false;
            button2.Enabled = false;           
            box.Enabled = false;
            Button editPath = new Button();
            editPath.Location = new System.Drawing.Point(14, 219);//size 112, 35
            editPath.Text = "Edit path";
            this.Controls.Add(editPath);
            editPath.Click += new System.EventHandler(editPath_Click);
            result = 1;
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            query = textBox1.Text;
            try
            {
                result = db.Edit(query);
                textBox1.Text = result.ToString();
            }
            catch
            {
                textBox1.Text = "Oops! Something wrong!";
                result = -1;
            }
            
        }

        private void box_ChangeIndex(object sender, EventArgs e)
        {
            textBox1.Text = $"select * from '{box.Items[box.SelectedIndex].ToString()}'";
        }        
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //textBox1.Lines = db.dbReadView(textBox1.Text).ToArray();
                tb = new tbController();
                List<string> views = new List<string>();
                tb.DataViewsList(views, tb.dbReadData(textBox1.Text));
                textBox1.Lines = views.ToArray();
            }
            catch
            {
                textBox1.Text = "Oops! Something wrong!";
            }
        }

        private string Output(List<string> source)
        {
            string line;
            string[] arr;

            arr = source[0].Split('*');
            line = string.Format("{0,-12} {1,12} {2,12}", arr[0], arr[1], arr[2]);
            return line;
        }

        private void editPath_Click(object sender, EventArgs e)
        {
                /* if a connection string is succesefully corrected, then a variable, what is used as condition, is equel 'true'
				 * else it is equel 'false'.
				 * Value of this variable will be changed in case the connection string is changed AND it is correct -- 'true'.
				 * In case connection string is neither not changed or not correct this variable will be not changed -- 'false'.
				 * In case connection string is changed, but stay incorrect, this variable stay unchanged too -- 'false'.
				 */

            Program.connectionStringPath = textBox1.Text;
            result = 0;
            db = new dbController();

            if (db.testConnection())
            {
                Program.ConnectionStringIsCorrect = true;
                string message = "Want you to save new connection string for next time?";
                DialogResult answer = MessageBox.Show
                    (message,
                    "Connection test",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1
                    );
               if(answer == DialogResult.Yes)
                {
                    Program.StoreConnectionString();
                }
                this.Close();
                //the connection string is changed AND it is correct
            }
            else
            {
                string message = "Connection string not correct. Want you to continue improving?";
                DialogResult answer = MessageBox.Show(
                    message,
                    "Connection test",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1
                    );

                if (answer == DialogResult.No)
                {
                    this.Close();
                }
                // the connection string stays wrong
            }
        }

        private void exportTablescsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmb_tables.Items.Count < 1) return;
            if (cmb_tables.SelectedItem == null) return;            
            
            string fname, table = cmb_tables.SelectedItem.ToString();                
            DateTime date = System.DateTime.Now;
            List<object[]> data;
            string[] lines;

            EditDBController cntrl = new EditDBController();

            /*
             * reading data from data base
             * query: select * from table_name;
             */
            //connection with data base test
            data = cntrl.ReadAllFields(table);
            if (data == null)
                return;

            /*
             * convert read data to strings of commat separeted values
             */
            lines = cntrl.ConvertData(data);

            /*
             * writting into file with stream testing
             */
            fname = cmb_tables.Text;
            cntrl.WriteToFile(fname, lines);

            MessageBox.Show("Exporting table...");
        }     
  
        private void lbl_db_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\\Users\\user\\Documents\\SQLiteStudio\\SQLiteStudio.exe", "db\\CookingBook");
            this.Dispose();
            this.Close();
        }

        private void EditDB_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(result == 1)
            {
                editPath_Click(sender, e);
            }
        }
    }

    public class EditDBController
    {
        dbController db;
        tbController tb;
        string query = "";
        List<string> views, tables;
        int result;

        public EditDBController()
        {
            db = new dbController();
        }

        public List <object[]> ReadAllFields(string table)
        {
            if (!db.testConnection())
                return null;
            return ReadData(table);

        }

        private List<object[]> ReadData(string table)
        {
            string query = $"select * from {table}";
            return db.dbReadData(query);
        }

        public string[] ConvertData(List<object[]> data)
        {
            int arr_length = data.Count;
            string[] lines = new string[arr_length];

            //converting
            int k, q = 0;
            for (k = 0; k < arr_length; k++)
            {
                lines[k] = "";
                for (q = 0; q < data[k].Length - 1; q++)
                {
                    if (data[k][q].ToString() == "") // if fields value was null
                        data[k][q] = "null";
                    lines[k] += data[k][q].ToString() + ",";
                }
                lines[k] += data[k][q].ToString();
            }
            return lines;
        }

        public void WriteToFile(string fname, string[] lines)
        {
            const string format = "csv", fpath = @"C:\Users\user\Desktop\Tables";

            // will add a test
            if (lines == null || lines.Length < 1) //it is some data for writting there
                return;
            if (string.IsNullOrEmpty(fname)) //file name exists
                return;
            if (!Directory.Exists(fpath))
                Directory.CreateDirectory(fpath);

            WriteData(fpath, fname, format, lines);
        }

        private void WriteData(string path, string fname, string format, string[] lines)
        {
            using (System.IO.StreamWriter wr = new System.IO.StreamWriter(path + "\\" + fname + "." + format, false))
            {
                for (int k = 0; k < lines.Length; k++)
                {
                    wr.WriteLine(lines[k]);
                }
            }
        }

    }
}
