/*
 * to output data into file and read user manual
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace MajPAbGr_project
{
    public partial class Print : Form
    {
        string file;
        List<string> strings; 
        
        public Print()
        {
            InitializeComponent();            
        }

        public Print(List <string> strings, string file)
        {
            InitializeComponent();
            this.strings = strings;
            this.file = file;
            textBox1.Text = file;    
        }

        private void PrintToFile()
        {
            const string PATH ="C:\\Users\\user\\Desktop\\";              
            string path;
            file = $"{file}.txt";

            path = PATH + file;
            using (StreamWriter stream = new StreamWriter(path, true))
            {
                if (!File.Exists(path))
                {
                    File.CreateText(path);
                    stream.WriteLine($"File is created: {File.GetLastWriteTime(path)} \n");
                }
                for (int k = 0; k < strings.Count; k++)
                {
                    stream.WriteLine(strings[k]);
                }
                stream.WriteLine($"[Record of: {System.DateTime.Now}]\n");
                stream.Close();
            }
            string message = $"File {path} is created";
            MessageBox.Show(message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                file = textBox1.Text;
            }
            else
            {
                if (file == null)
                {
                    file = "recipe";
                }
                textBox1.Text = file;
            }

            if (strings != null)
            {
                PrintToFile();
            }
            else
            {
                if (richTextBox1.Lines != null)
                {
                    strings = new List<string>();
                    string[] lines = richTextBox1.Lines;
                    foreach (string l in lines)
                    {
                        strings.Add(l);
                    }
                    PrintToFile();
                }
                else
                {
                    MessageBox.Show("Nothing to write into file");
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            label1.Text = "File name";            
            button1.Text = "Cancel";
            button2.Text = "Clear";
            this.StartPosition = (FormStartPosition)1;
            
            richTextBox1.ReadOnly = true;
            richTextBox1.Text = "";

            PrintToBox();
        }

        private bool PrintToBox()
        {
            if (strings != null)
            {
                foreach (string str in strings)
                {
                    richTextBox1.Text += str + "\n";
                }
                return true;
            }
            else
                return false;
        }

        private void lbl_chooseFile_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void openFile()
        {
            string path;           

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            // get file
            path = openFileDialog1.FileName;
            // read file as one string
            string fileText = System.IO.File.ReadAllText(path);
            MessageBox.Show("Файл открыт");

            strings.Clear();           
            string[] arr = fileText.Split('\n');
            strings.AddRange(arr);            
            PrintToBox();      
        }

        private void OpenFile (string path)
        {
            //  read file as one string
            string fileText = System.IO.File.ReadAllText(path);           

            strings.Clear();            
            string[] arr = fileText.Split('\n');
            strings.AddRange(arr);
            PrintToBox();
        }

        public void OpenFile1 (string path, string fname)
        {
            this.file = fname;
            textBox1.Text = file;
            strings = new List<string>();
            OpenFile(path);            
        }

        public void Button3_Enabled_status(bool enabled)
        {
            button3.Enabled = enabled;
        }

        private void cntxt_item_ingr_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool frmopen = false;
            string formName = "";
            Ingredients ingr; ;
            tbIngredientsController tb;
            string fulltext = "";
            List<String> ingr_list;

           
            if (richTextBox1.SelectedText.Length > 0)
            {
                fulltext = richTextBox1.SelectedText;
            }
            else
            {
                fulltext = richTextBox1.Text;
                if (fulltext == "")
                    return;
            }

            string[] arr = fulltext.Split('\n');
            ingr_list = new List<string>();
            ingr_list.AddRange(arr);
            ingr_list.RemoveAt(ingr_list.Count - 1);          



            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "Ingredients")
                {
                   frmopen  = true;
                   formName = frm.Name;
                   //int option = frm.Option;
                   ingr = (Ingredients)frm;
                    //ingr.Focus();
                    tb = ingr.TbIngr;
                    tb.MultyInsert(ingr_list);
                    break;
                } 
            }
            if(!frmopen)
            {
                tb = new tbIngredientsController(1);
                tb.MultyInsert(ingr_list);
            }
        }
    }
}
