using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MajPAbGr_project
{
    public partial class Form2 : Form
    {
        List<string> strings;
        string file;
        
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(List <string> strings, string file)
        {
            InitializeComponent();
            this.strings = strings;
            this.file = file;
            textBox1.Text = file;
        }

        private void PrintToFile()
        {
            const string PATH = "C:\\Users\\user\\Documents\\2_diplom\\Receptures\\";
            string path;
           
            //file = $"{file}.txt";
            file = $"{file}.csv"; // for lists to be able to be opened with Exel
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

        private void button2_Click(object sender, EventArgs e)
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

        private void Form2_Load(object sender, EventArgs e)
        {
            
            label1.Text = "File name";            
            button1.Text = "Cancel";
            button2.Text = "Print";
            this.StartPosition = (FormStartPosition)1;
            
            richTextBox1.ReadOnly = true;
            richTextBox1.Text = "";

            if (strings != null)
            {
                foreach (string str in strings)
                {
                    richTextBox1.Text += str + "\n";
                }
            }
            
        }
    }
}
