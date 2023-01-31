using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MajPAbGr_project
{
    public partial class Ingredients : Form
    {
        int option, l_used=0;
        string used;

        List<string> output;
        Print frm;

        tbIngredientsController tb;

        public Ingredients(int opt)
        {
            InitializeComponent();            
            this.option = opt;
            string table = opt == 1 ? "Ingredients" : "Categories";
            //tb = new tbClass1(table);            
            // this `tb` is a object of class tbClass1 (from previous version of code)
            
            //tb.setCatalog();
            //Elements();
            //fillCatalog(tb.getCatalog());            
        }

        public Ingredients (tbIngredientsController controller)
        {
            InitializeComponent();
            tb = controller;
            this.option = tb.getOption();
            string table = tb.getTable();
            tb.setCatalog();            
        }

        private void Ingredients_Load(object sender, EventArgs e)
        {
            string table;            
            frm = new Print();
            
            // Elements()
            table = tb.getTable();
            groupBox1.Text = table;
            this.Text = table;

            btn_add.Enabled = false;
            btn_remove.Enabled = false;
            btn_edit.Enabled = false;
            lblTest.Text = "0";

            label1.Text = "";
            label1.Visible = false;
            // к списку записей

            frm.Show();
            Output();       
        }

        private void Output()
        {
            int index;
            List<int> id;
            output = Class1.FillComboString(tb.getCatalog(), ref cmbData, out id);
            frm.richTextBox1.Text = "";            
            
            for (index = 0; index < output.Count; index++)
            {
                tb.Selected = id[index];                
                tb.setUsed();
                used = tb.getUsed();

                // for csv files, to open in Exel
                // переделать на "человеческий" формат!!!
                frm.richTextBox1.Text += index + 1 + "," + output[index];
                string t = "";
                if (used == "0")
                {
                    t =  ",not used\n";                    
                }
                else
                {
                    t = "," + used + "_records";
                    List<string> recipes = tb.SeeMoreFunc();
                    foreach (string li in recipes)
                    { 
                        t += "," + li;
                    }
                    t += "\n";
                }
                frm.richTextBox1.Text += t;
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (btn_edit.Text == "edit")
            {
                if (cmbData.SelectedIndex != -1)
                {
                    //txbAdd.Text = tb.getName(cmbData.SelectedIndex);
                    string name = cmbData.SelectedItem.ToString();
                    groupBox2.Text = name;
                    txbAdd.Text = name;
                    txbAdd.Select();                    
                    btn_add.Text = "update";                    
                    btn_edit.Text = "cancel";
                }
            }
            else if (btn_edit.Text == "cancel")
            {

                btn_add.Enabled = false;
                txbAdd.Clear();
                groupBox2.Text = "Insert new";

                btn_add.Text = "add";
                btn_edit.Text = "edit";
                btn_edit.Enabled = false;                
            }           
        }

        private void AddButton_Click (object sender, EventArgs e)
        {
            int count;
            if (btn_add.Text == "add")
            {
                count = AddNewItem();
            }
            else if (btn_add.Text == "update")
            {
                count = EditItem();
                btn_add.Text = "add";
                btn_edit.Text = "edit";
                btn_edit.Enabled = true;
            }
            else count = 0;

            if (count != 0)
            {
                //fillCatalog(tb.getCatalog());
                Output();

                btn_add.Enabled = false;
                txbAdd.Clear();
                groupBox2.Text = "Insert new";                
            }
        }

        private int AddNewItem()
        {
            if (string.IsNullOrEmpty(txbAdd.Text)) return 0;
            int count = tb.AddItem(txbAdd.Text);
            return count;
        }

        private int EditItem()
        {
            if (string.IsNullOrEmpty(txbAdd.Text)) return 0;
            int count = tb.UpdateItem(txbAdd.Text);
            return count;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbAdd.Text))
            {
                btn_add.Enabled = true;
            }
        }        

        private void RemoveButton_Click (object sender, EventArgs e)
        {
            TextBox t = txbAdd;
            ComboBox c = cmbData;            

            int count = RemoveSelected();
            if (count > 0)
            {
                //fillCatalog(tb.getCatalog());
                Output();
                btn_remove.Enabled = false;
            }
            else
            {
                if (count == -1)
                    MessageBox.Show("This one is used. You can change it name");
                else
                    MessageBox.Show("Nothing is deleted!");
            }
        }

        private int RemoveSelected()
        {
            if (used == "0")
            {
                int count = tb.RemoveItem();
                return count;
            }
            return -1;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Label l = label1;
            l.Visible = (l.Visible == false) ? true : false;
        }

        private void SeeMore ()
        {
            Label l = label1;
            if (l_used > 0)
            {
                List<string> list = tb.SeeMoreFunc();
                foreach (string li in list)
                {
                    l.Text += li + "\n";
                    l_used = 0;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox t = txbAdd;
            ComboBox c = cmbData;
            Label l = lblInfo;

            used = "0"; // в скольких записях используется
            l.Text = "Is used in";

            if (c.SelectedIndex < tb.getCatalog().Count)
            {
                int temp = c.SelectedIndex;    

                tb.setSelected(temp);
                tb.setUsed();
                used = tb.getUsed();

                btn_remove.Enabled = (used == "0") ? true : false;
                btn_edit.Enabled = true;
                l.Text += $"{used} record(s) >>";
                lblTest.Text = tb.getSelected().ToString();

                //для  управлением списка рецептов
                l_used = 1; 
                label1.Text = "";
                SeeMore();
            }           
        }
    }
}
