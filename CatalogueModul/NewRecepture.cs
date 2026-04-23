/*
 * to input formulation's data and insert one into DB
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace MajPAbGr_project
{
    public partial class NewRecepture : Form
    {
        int id_recepture, category, technology;
        bool indicator; // choose mode
        string name, recepture, source, author, URL, description;

        tbReceptureController tb;
        tbController tbCat;
        tbTechnologyController tbTech;
        CatalogueController controller;


        public NewRecepture(tbReceptureController tbMain, CatalogueController controller) // addNew() in "Categories"
        {
            InitializeComponent();           
            this.controller = controller;
            controller.TbMain = tbMain;
            this.tb = controller.TbMain;           
            id_recepture = controller.ReceptureInfo.getId();
            this.category = 0;
            this.technology = 0;

            tbCat = new tbController("Categories");
            tbCat.setCatalog();
            tbTech = new tbTechnologyController("Technology");
            tbTech.setCatalog();          
            indicator = controller.Indicator;
        }

        public NewRecepture(CatalogueController controller) // openReceptureEditor() in "Categories"
        {
            InitializeComponent();
            this.controller = controller;
            tb = controller.TbMain;
            tbCat = controller.TbCat;
            tbTech = controller.TbTech;

            ReceptureStruct rec = controller.ReceptureInfo;
            int[] ids = rec.getIds();
            id_recepture = rec.getId();
            category = ids[0];
            technology = ids[1];
            indicator = controller.Indicator; // choose mode

            if (indicator)
            {                
                name = controller.Data[0];
                source = controller.Data[1];
                author = controller.Data[2];
                URL = controller.Data[3];
                description = controller.Data[4];
            }
        }


        private void NewRecepture_Load(object sender, EventArgs e)
        {
            int ind;
            ind = TextBoxAutocomplet("name", txbRecepture);
            ind = TextBoxAutocomplet("source", txbSource);
            ind = TextBoxAutocomplet("author", txbAuthor);
            ind = TextBoxAutocomplet("URL", txbURL);
            SetForm();

            if(!indicator || controller.Mode == SubmitMode.UPDATE)
            {
                button2.Text = "Update";
            }

            if(controller.Mode == SubmitMode.INSERT)
            {
                new_rec_Click(sender, e);
            }
        }

        private int TextBoxAutocomplet(string column, TextBox box)
        {
            int length = 0;
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();

            if (tb.IfRecordIs())
            {
                source.AddRange(controller.TbMain.getNames(column));
                box.AutoCompleteCustomSource = source;
                box.AutoCompleteMode = AutoCompleteMode.Suggest;
                box.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            else
            {
                source.Add("empty");
            }
            return length;
        }

        private void SetForm()
        {
            int temp;
            List<Item> items;          

            temp = category;
            items = tbCat.getCatalog();            
            FormFunction.FillCombo(items, cmbCat);
            category = temp;
            if (category > 0)
                cmbCat.SelectedIndex = FormFunction.ChangeIndex(items, category);
            else
            {
                if (cmbCat.Items.Count > 0)
                    cmbCat.SelectedIndex = 0;
            }
                

            temp = technology;            
            items = tbTech.getCatalog();
            FormFunction.FillCombo(items, cmbTech);     
            technology = temp;            
            if (items.Count > 0)
            {
                if (technology > 0)
                {
                    cmbTech.SelectedIndex = FormFunction.ChangeIndex(items, technology);
                }
                else
                {
                    cmbTech.Text = "choose any technology";
                }
            }
            else
            {              
                cmbTech.Text = "empty";
                chBox_technology.Checked = true;
                chBox_technology.Enabled = false;

            }  

            if (indicator)
            {
                txbRecepture.Text = name;
                txbSource.Text = source;
                txbAuthor.Text = author;
                txbURL.Text = URL;
                txbDescription.Text = description;                
            }
        }


        private void cmbTech_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbTech.SelectedIndex;
            technology = tbTech.setSelected(index);
            controller.CurrentRecepture.Technology.Id = technology;
            controller.CurrentRecepture.Technology.Name = tbTech.getCatalog()[index].name;

            //tbTech.setCurrent(index); //technologies count is 0
        }

        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbCat.SelectedIndex;
            category = tbCat.setSelected(index);
            controller.CurrentRecepture.CategoriesId = category;            
            controller.CurrentRecepture.Categories.Name = tbCat.getCatalog()[index].name;
        }


        private void lbl_open_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbURL.Text))
                return;
            string PATH = txbURL.Text;            
            try            {
                
                Process.Start(PATH);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}");                
            }          
        }

        public void cmbCat_IndexChange(int id)
        {
            int temp = FormFunction.ChangeIndex(tbCat.getCatalog(), id);
            if (temp > -1)
                cmbCat.SelectedIndex = temp;
            else
                return;                
        }

        private void chBox_technology_CheckedChanged(object sender, EventArgs e)
        {
            if (chBox_technology.Checked)
            {
                cmbTech.Enabled = false;
                if (cmbTech.Items.Count > 0)
                {
                    cmbTech.SelectedIndex = 0;
                    controller.CurrentRecepture.TechnologyId = 0;
                    tbTech.Current.Id = 0;
                }
                    
                technology = 0;
            }
            else
            {
                //???
                tbTechnologyController tbTech = new tbTechnologyController("Technology");
                tbTech.setCatalog();
                cmbTech.Enabled = true;
                technology = tbTech.setSelected(0);
            }
        }


        //CRUD
        
        private void new_rec_Click(object sender, EventArgs e)
        {
            id_recepture = 0;
            txbRecepture.Clear();
            txbRecepture.Focus();
            button1.Enabled = false; // 'delete'
            new_rec.Enabled = false;
            button2.Text = "Insert";
            controller.Mode = SubmitMode.INSERT;
        }

        private void button2_Click(object sender, EventArgs e) // set / write into db (Recepture)
        {
            int result = 0;
            string message = "Recepture is updated (inserted)";

            //conditions
            if (string.IsNullOrEmpty(txbRecepture.Text))
                return;
            if (cmbCat.Items.Count == 0 || cmbCat.SelectedIndex == -1)
                return;

            //data storing in class variables
            name = txbRecepture.Text;
            category = tbCat.getSelected();
            if (category == 0)
                category = 1;


            //operation checking
            if (controller.Indicator == false)
            {
                controller.Mode = SubmitMode.INSERT;
            }
                

            switch ((int)controller.Mode)
            {
                case 0: //insert
                    id_recepture = 0;
                    controller.CompletData
                        (
                        txbRecepture.Text,
                        txbAuthor.Text,
                        txbSource.Text,
                        txbURL.Text,
                        txbDescription.Text
                        );
                    result = controller.InsertNew(); // -1: no data, -2: name not unique, 0: nothing is inserted
                    ResultReporting();
                    setDialogResult();                    
                    this.Dispose();
                    this.Close();
                    break;
                case 1: //update
                    controller.CurrentRecepture.Id = id_recepture;
                    controller.CompletData
                        (
                        txbRecepture.Text,
                        txbAuthor.Text,
                        txbSource.Text,
                        txbURL.Text,
                        txbDescription.Text
                        );
                    result = controller.UpdateExisited(); // -1: no data, -2: name not unique,  0: nothing is inserted
                    ResultReporting();
                    setDialogResult();
                    break;
                default:
                    break;
            }

            void ResultReporting()
            {
                //success
                if (result > 0)
                {
                    message = "Recepture is updated (inserted)";
                    MessageBox.Show(message);
                    return;
                }

                //no success
                if (result == 0)
                {
                    message = "Nothing is updated (inserted)";
                    MessageBox.Show(message);
                    return;
                }

                // errors
                if (result == -1)
                {
                    message = "Please, check name and category be inputed and then try again!";
                    MessageBox.Show(message);
                    return;
                }
                if (result == -2)
                {
                    message = "Recepture with such name already exists. Please, type new name and try again";
                    MessageBox.Show(message);
                    return;
                }
            }

            void setDialogResult()
            {
                if (result > 0)
                    this.DialogResult = DialogResult.OK;
                else
                    this.DialogResult = DialogResult.Cancel;
            }

            
        }


        private void button1_Click(object sender, EventArgs e) //remove
        {
            int selected = tb.Selected;
            if (selected == 0)
            {
                MessageBox.Show($"Recipe do not recorded into data base!");
                this.Dispose();
                this.Close();
                return;
            }
            int count = tb.RemoveItem();
            MessageBox.Show($"Is removed {count} records");
            this.Dispose();
            this.Close();
        }

        
        private int WriteIntoDataBase()
        {
            int num = 0;
            if (!indicator) // вводим новый рецепт
            {
                if (string.IsNullOrEmpty(txbRecepture.Text)) return -1;
                if (string.IsNullOrEmpty(cmbCat.SelectedItem.ToString())) return -1;

                name = txbRecepture.Text;
                category = tbCat.getSelected();
                if (category == 0) category = 1;

                if (tb.IfRecordIs(name)) return -2;
                //if name is not unique

                tb.InsertNewRecord(name, category);
                // insert name and category into DB, take from DB id of last record's id
                id_recepture = tb.getId();
                //get the id   
            }
            else // updating existing record
            {
                if (string.IsNullOrEmpty(txbRecepture.Text)) return -1;

                name = txbRecepture.Text;
                category = tbCat.getSelected();

                num = tb.UpdateReceptureOrCards("name", name, id_recepture);
                num = tb.UpdateReceptureOrCards("id_category", category.ToString(), id_recepture);
                
                if (technology != 0 || !chBox_technology.Checked)
                {
                    if (technology > 0 )
                        num = tb.UpdateReceptureOrCards("id_technology", technology.ToString(), id_recepture);                    
                }
                else
                {
                    if (chBox_technology.Checked)
                    {
                        num = tb.UpdateReceptureOrCards("id_technology", null, id_recepture);
                    }                    
                }
            }

            if (!string.IsNullOrEmpty(txbAuthor.Text))
            {
                author = txbAuthor.Text;
                num = tb.UpdateReceptureOrCards("author", author, id_recepture);                
            }

            if (!string.IsNullOrEmpty(txbSource.Text))
            { 
                source = txbSource.Text;
                num = tb.UpdateReceptureOrCards("source", source, id_recepture);            
            }
                
            if (!string.IsNullOrEmpty(txbDescription.Text))
            {
                description = txbDescription.Text;
                num = tb.UpdateReceptureOrCards("description", description, id_recepture);               
            }             

            if (!string.IsNullOrEmpty(txbURL.Text))
            {
                URL = txbURL.Text;
                num = tb.UpdateReceptureOrCards("URL", URL, id_recepture);               
            }

             return id_recepture;           
        }

        // end of CRUD

    }
}
