/*
 * to access to data base
 */



using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MajPAbGr_project
{  
    public partial class EditDB : Form
    {
        dbController db;
        tbController tb;
        string query="";
        List<string> views;
        int result;
        
        public EditDB()
        {
            InitializeComponent();
            db = new dbController();            
            views = db.dbReader("SELECT name FROM sqlite_schema WHERE type = 'view';");            
            box.Items.AddRange(views.ToArray());
            box.Text = "view list";
        }

        public EditDB(string connectionStringPath)
        {
            InitializeComponent();            
            textBox1.Text = connectionStringPath;
            button1.Enabled = false;
            button2.Enabled = false;
            box.Enabled = false;
            Button editPath = new Button();
            editPath.Location = new System.Drawing.Point(469, 178);
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

        private void EditDB_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(result == 1)
            {
                editPath_Click(sender, e);
            }
        }
    }
}
