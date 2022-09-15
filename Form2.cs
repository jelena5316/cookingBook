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

            if (!string.IsNullOrEmpty(textBox1.Text))
                file = textBox1.Text;
            
            file = $"{file}.txt";
            path = PATH + file;
            using (StreamWriter stream = new StreamWriter(path, true))
            {
                if (!File.Exists(path))
                {
                    File.CreateText(path);
                }
                for (int k = 0; k < strings.Count; k++)
                {
                    stream.WriteLine(strings[k]);
                }
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
            PrintToFile();
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
