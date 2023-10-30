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

        public EditDB(dbController db, string connectionStringPath)
        {
            InitializeComponent();
            this.db = db;
            textBox1.Text = connectionStringPath;
            button1.Enabled = false;
            button2.Enabled = false;
            box.Enabled = false;
            Button editPath = new Button();
            editPath.Location = new System.Drawing.Point(469, 178);
            editPath.Text = "Edit path";
            this.Controls.Add(editPath);
            editPath.Click += new System.EventHandler(editPath_Click);
        }

        private void editPath_Click(object sender, EventArgs e)
        {
            //db.improveConnection(textBox1.Text);
            Program.connectionStringPath = textBox1.Text;
            db = new dbController();
            if (db.testConnection())
            {
                MessageBox.Show("false");
                //this.Close();
            }
            else
            {
                Categories frm = new Categories();
                frm.Show();
                //this.Close();
            }
        }

        //public string EditDBconnectionString
        //{
        //    get { return db.ConnectionString; }
        //}


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
    }
}
