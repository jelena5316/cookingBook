using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototip_diplom
{
    public partial class Remove : Form
    {
        public Remove()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show ("Are you sure delete the recipe?");
        }
    }
}
