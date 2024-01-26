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
		EditDBController cntrl;		
		dbController db;
		tbController tb;
		string query="";
		List<string> views, tables;
		int result;

		//FlatStyle flatStyle;
		
		public EditDB(EditDBController controller)
		{
			InitializeComponent();
			//db = new dbController();
			cntrl = controller;
			views = cntrl.Views;
			tables = cntrl.Tables;
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
			CreateTable();
		}

		private void CreateTable()
        {
			cntrl.Query = textBox1.Text;
			textBox1.Text = cntrl.Execute();
		}

		private void box_ChangeIndex(object sender, EventArgs e)
		{
			SelectView();
		}

		private void SelectView()
        {
			if (box.SelectedIndex < 0)
				return;
			cntrl.setViewStatement(box.SelectedIndex);
			textBox1.Text = cntrl.Query;
		}
	
		private void button2_Click(object sender, EventArgs e)
		{
			DisplayView();			
		}

		private void DisplayView()
        {
			//cntrl.Query = textBox1.Text;
			textBox1.Lines = cntrl.ReadView();			
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
			//string fname = cmb_tables.Text,
			//	table = cmb_tables.SelectedItem.ToString();
			int table = cmb_tables.SelectedIndex;
			cntrl.ExportTableDataToFile(table);
			MessageBox.Show("Exporting table...");
		}     
  
		private void lbl_db_Click(object sender, EventArgs e)
		{
			Process.Start(@"C:\\Users\\user\\Documents\\SQLiteStudio\\SQLiteStudio.exe", "db\\CookingBook");
			this.Dispose();
			this.Close();
		}

        private void backupDbToolStripMenuItem_Click(object sender, EventArgs e)
        {
			//will save data base file in files system
			cntrl.CopyPasteDB();
        }

        private void EditDB_FormClosed(object sender, FormClosedEventArgs e)
		{
			if(result == 1)
			{
				editPath_Click(sender, e);
			}
		}
	
	}


	public enum Status
    {
		OK, WRONG
    }

	public class EditDBController
	{
		dbController db;
		string query, err_message;
		string[] data;
		List<string> views, tables;
		int result;

		public EditDBController()
		{
			db = new dbController();
			query = "";
			err_message = "Oops! Something wrong!";
			views = db.dbReader("SELECT name FROM sqlite_schema WHERE type = 'view';");
			tables = db.dbReader("SELECT name FROM sqlite_schema WHERE type = 'table';");			
		}

		public string Query
        {
			set { query = value; }
			get { return query; }
        }
		public List<string> Views
		{
			get { return views; }
		}

		public List<string> Tables
		{
			get { return tables; }
		}

		public int Result
        {
			get { return result; }
            set { result = value; }
        }


		/*
		 * set and get statement for view reading from data base
		 */

		public void setViewStatement(int num)
        {
			if (views.Count < num+1)
				return;
			query = getViewStatement(num);
        }
		
		private string getViewStatement(int num)
		{
			return $"select * from '{views[num]}'";
		}

		/*
		 * back up data base file
		 */

		public void CopyPasteDB()
        {
			string fname = "CookingBook",
				path = @"db\CookingBook",
				des_path = @"C:\Users\user\Desktop";
			
			des_path = des_path+@"\"+fname;			
			FileInfo fileInf = new FileInfo(path);
			if (fileInf.Exists)
			{
				fileInf.CopyTo(des_path, true);
				// альтернатива с помощью класса File
				// File.Copy(path, newPath, true);
			}
		}

		/*
		* exsporting data table in text file (csv)
		*/

		public void ExportTableDataToFile(int index)
        {
			string table, fname;			
			DateTime date = System.DateTime.Now;
			List<object[]> data;
			string[] lines;

			table = tables[index];
			fname = table + date.ToString();

			/*
			 * reading data from data base
			 * query: select * from table_name;
			 */
			//connection with data base test
			data = ReadAllFields(table);
			if (data == null)
				return;

			/*
			 * convert read data to strings of commat separeted values
			 */
			lines = ConvertData(data);

			/*
			 * writting into file with stream testing
			 */			
			WriteToFile(fname, lines);			
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

		/*
		* editing data base using textbox to input SQL statements
		*/

		public string Execute()
        {
			result = db.Edit(query);
			Status status = ExecutedNonQueryResult(result);
			query = "";

			switch (status)
            {
				case Status.OK:
					return result.ToString();
				case Status.WRONG:
					return WRONG_message();
				default:
					return result.ToString();
			}		
        }

		private Status ExecutedNonQueryResult(int result)
        {
			if (result == 0) 
				return Status.WRONG;
			return Status.OK;
		}

        /*
		 * dislpay selected views data (executing views SQL statements)
		 */

        public string [] ReadView()
        {
			Status status;						
			List<object[]> objects;

			if (string.IsNullOrEmpty(query))
				return new string[] { "Empty statement!" };

			objects = db.dbReadData(query);
			status = ExecutedReaderResult(db.getInfo().err_code);
			data = OutputView (objects, status);
			err_message = "";
			query = "";
			return data;
		}

		private Status ExecutedReaderResult(int err_code)
        {
			
			if (err_code > 0)
				return  Status.WRONG;
			else
				return Status.OK;
		}

		private string [] OutputView(List<object[]> objects, Status status)
        {
			List<string> data;
			switch (status)
			{
				case Status.OK:
					{
						data = new List<string>();
						DataViewsList(data, objects);
						return data.ToArray();
					}

				case Status.WRONG:
					return new string[] { WRONG_message() };
				default:
					try
					{
						data = new List<string>();
						DataViewsList(data, objects);
						return data.ToArray();
					}
					catch
					{
						return new string[] { WRONG_message() };
					}
			}
		}

		public void DataViewsList(List <string> views, List<object[]> data)
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


		private string WRONG_message()
		{
			string message = "Oops! Something wrong!";
			if (db != null && !string.IsNullOrEmpty(db.getInfo().err_message))
				message =  db.getInfo().err_message;
			if (!string.IsNullOrEmpty(this.err_message))
				message = this.err_message;
			return message;
		}
	}
}
