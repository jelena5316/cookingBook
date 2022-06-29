using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MajPAbGr_project
{
    public partial class EditDB : Form
    {
        dbController db;
        string query;
        public EditDB()
        {
            InitializeComponent();
            db = new dbController();
            query = "";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            query = textBox1.Text;             
            db.Edit(query);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }
    }
}
