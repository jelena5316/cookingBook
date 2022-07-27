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
    public partial class NewRecepture : Form
    {
        int id_recepture, category;
        bool indicator; // choose mode
        string name, query, recepture, source, author, URL, description;

        tbClass1 tbCat;
        
        public NewRecepture()
        {
            InitializeComponent();
            id_recepture = 0;
            SetForm();
        }

        public NewRecepture(int id, int category)
        {
            InitializeComponent();
            id_recepture = id;
            this.category = category;
            SetForm();
        }

        private void SetForm()
        {
            tbCat = new tbClass1("Categories");
            tbCat.setCatalog();
            setIndicator(); // set mode
            FillCatalog();

            if (indicator)
            {
                dbController db = new dbController();
                List <string> data;

                query = $"select name from Recepture where id = {id_recepture};";
                data = db.dbReader(query);
                txbRecepture.Text = data[0];

                query = $"select source from Recepture where id = {id_recepture};";
                data = db.dbReader(query);
                txbSource.Text = data[0];

                query = $"select author from Recepture where id = {id_recepture};";
                data = db.dbReader(query);
                txbAuthor.Text = data[0];

                query = $"select URL from Recepture where id = {id_recepture};";
                data = db.dbReader(query);
                txbURL.Text = data[0];

                query = $"select description from Recepture where id = {id_recepture};";
                data = db.dbReader(query);
                txbDescription.Text = data[0];
            }
        }

        private void setIndicator()
        {
            if (id_recepture > 0) indicator = true;
            else indicator = false;
        }

        private void FillCatalog()
        {
            List<Item> cat = tbCat.getCatalog();

            //fill catalog
            if (cat.Count != 0)
            {
                if (cmbCat.Items.Count > 0)
                {
                    cmbCat.Items.Clear();
                }
                for (int index = 0; index < cat.Count; index++)
                {
                    cmbCat.Items.Add(cat[index].name);
                }
            }
            if (cmbCat.Items.Count > 0)
            {
                cmbCat.SelectedIndex = 0;
                cmbCat.Text = cmbCat.SelectedItem.ToString();
                //cmbCat.Text = cmbCat.Items[0].ToString();
            }

            //index, name (edit mode)
            if (indicator)
            {
                int index;
                Item item = new Item();

                item = cat.Find(it => it.id == category);
                index = cat.FindIndex(it => it.id == category);

                cmbCat.Text = item.name;
                cmbCat.SelectedIndex = index; // setSelected(int) внутри

                //test
                this.Text += " " + index + " " + tbCat.getSelected();
            } 
        }

        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbCat.setSelected(cmbCat.SelectedIndex);
        }

        private void button2_Click(object sender, EventArgs e) // set / write into db (Recepture)
        {
            int num = 0;
            dbController db = new dbController();
            tbClass1 tb = new tbClass1("Recepture");
            if (!indicator)
            {
                if (string.IsNullOrEmpty(txbRecepture.Text)) return;
                if (string.IsNullOrEmpty(cmbCat.SelectedItem.ToString())) return;

                name = txbRecepture.Text;
                category = tbCat.getSelected();
                if (category == 0) category = 1;

                query = $"select count(*) from Recepture where name ='{name}';";
                recepture = db.Count(query);
                if (recepture != "0") return;

                query = $"insert into Recepture (name, id_category) " +
                $"values ('{name}', {category}); select last_insert_rowid() ";
                recepture = db.Count(query);

                if (int.TryParse(recepture, out id_recepture))
                {
                    id_recepture = int.Parse(recepture);
                    label1.Enabled = true;
                }
                // add new recepture and get it's id
            }
            else
            {
                if (string.IsNullOrEmpty(txbRecepture.Text)) return;

                name = txbRecepture.Text;               
                category = tbCat.getSelected();

                // Do initialize a new tbClass1 object! Table must be Recepture!
                // Is done!
                num = tb.UpdateReceptureOrCards("name", name, id_recepture);
                num = tb.UpdateReceptureOrCards("id_category", category.ToString(), id_recepture);
            }

            if (string.IsNullOrEmpty(txbAuthor.Text)) return;
            if (string.IsNullOrEmpty(txbSource.Text)) return;
            if (string.IsNullOrEmpty(txbURL.Text)) return;
            if (string.IsNullOrEmpty(txbDescription.Text)) return;

            source = txbSource.Text;
            author = txbAuthor.Text;
            URL = txbURL.Text;
            description = txbDescription.Text; // note

            // Do initialize a new tbClass1 object! Table must be Recepture!
            //Is done!            
            num = tb.UpdateReceptureOrCards("source", source, id_recepture);
            num = tb.UpdateReceptureOrCards("author", author, id_recepture);
            Report(num, "author");
            num = tb.UpdateReceptureOrCards("URL", URL, id_recepture);
            Report(num, "URL");
            num = tb.UpdateReceptureOrCards("description", description, id_recepture);
            Report(num, "description");
        }

        private void Report (int num, string variable)
        {
            if (num == 0) this.Text += $" {variable} not writted";
            else this.Text += $" {variable} is writted";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (id_recepture == 0) return;
            InsertAmounts frm = new InsertAmounts(id_recepture);
            frm.ShowDialog();
        }
    }
}
