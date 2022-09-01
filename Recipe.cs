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
    public partial class Recipe : Form
    {

        int old_box_x, old_box_y, recepture_id;       
            
        FormMain frm;
        tbClass1 tb;
        CalcFunction calc;
        Button btn_remove;

        public Recipe()
        {
            InitializeComponent();
        }

        public Recipe(FormMain frm, tbClass1 tb, CalcFunction calc, int index)
        {
            InitializeComponent();
            this.frm = frm;
            this.tb = tb;
            this.calc = calc;
            recepture_id = tb.Selected;

            string name = tb.getName(index);
            this.Text = $"Recipes of '{name}' (id {recepture_id.ToString()})";

            old_box_x = frm.groupBox1.Location.X;
            old_box_y = frm.groupBox1.Location.Y;                      
        }

         private void setLabels()
        {
            RecipeController rec = new RecipeController("Recipe");
            rec.Recepture = tb.Selected;
            string info = rec.ReceptureInfo();
            if (info == "" || info == "\n\n\n\n\n") info = "none description";
            label1.Text = "Description: " + info;
            label1.AutoSize = true;
            label2.Location = new Point
                (label1.Location.X, (label1.Location.Y + label1.Height + 20));
        }
        private void Recepture_Load(object sender, EventArgs e)
        {
            this.Controls.Add(frm.groupBox1);
            frm.groupBox1.Location = new Point(27, 36);

            btn_remove = new Button();
            btn_remove.Location = new Point(13, 199);
            btn_remove.Text = "btn_remove";
            frm.groupBox1.Controls.Add(btn_remove);
            btn_remove.Click += new System.EventHandler(btn_remove_Click);

            label1.Location = new Point
                ((frm.groupBox1.Location.X + frm.groupBox1.Width + 20), frm.groupBox1.Location.X);
            setLabels();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            int category = tb.getId("id_category", recepture_id);
            int selected = tb.getSelected();            
            ReceptureController cntrl = new ReceptureController("Recepture", recepture_id, category);
            NewRecepture frm = new NewRecepture(cntrl);
            frm.ShowDialog();
            setLabels();
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (frm.comboBox2.Items.Count < 1) return;
            if (frm.comboBox2.SelectedIndex < 0) return;
            
            DialogResult result = MessageBox.Show(
                    "Do delete recipe?", "",
                    MessageBoxButtons.OKCancel);
           
            if (result == DialogResult.OK)
            {
                int id = 0, indicator = 0;
                RecipeController recipe = new RecipeController
                    ("Recipe", frm.comboBox2.SelectedIndex, tb.Selected);
                id = recipe.Selected;
                indicator = recipe.RemoveItem();               
                MessageBox.Show($"Recipe {id} is deleted");
                tb.setSubCatalog();
                frm.fillSubCatalog();
            }
            else
            {
                MessageBox.Show("Ok");
            } 
        }

        private void Recipe_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm.groupBox1.Controls.Remove(btn_remove);
            frm.Controls.Add(frm.groupBox1);
            frm.groupBox1.Location = new Point(old_box_x, old_box_y);            
        }
    }
}
