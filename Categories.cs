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
    public partial class Categories : Form
    {
        CategoriesController categoriesController;
        int pragma;
        
        public Categories()
        {
            InitializeComponent();
            categoriesController = new CategoriesController();
            pragma = 0;
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            Class1.setBox(categoriesController.Categories, ref cmb_categories);            
            Class1.FillListView(categoriesController.Receptures, ref lv_recepture);
            cmb_categories.Text = "all";
            pragma = 1;
        }

        private void cmb_categories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pragma == 0) return;
            int index = cmb_categories.SelectedIndex;            
            int id = categoriesController.Categories[index].id;            
            categoriesController.SelectedRecepture(id);
            Class1.FillListView(categoriesController.Receptures, ref lv_recepture);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            cmb_categories.SelectedIndex = 0;
            categoriesController.setReceptures();
            Class1.FillListView(categoriesController.Receptures, ref lv_recepture);            
            cmb_categories.Text = "all";
        }
    }
}
