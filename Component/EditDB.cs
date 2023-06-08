using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace MajPAbGr_project
{  
    public partial class EditDB : Form
    {
        dbController db;        
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
            textBox1.Text = $"select * from {box.Items[box.SelectedIndex].ToString()}";
        }        
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Lines = db.dbReadView(textBox1.Text).ToArray();
                //textBox1.Text = Output(db.dbReadView(textBox1.Text));
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

        private void EditDB_Load(object sender, EventArgs e)
        {

        }
    }
}
