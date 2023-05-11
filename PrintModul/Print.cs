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
    public partial class Print : Form
    {
        string file;
        List<string> strings,
            info,
            ingredients,
            technology,
            cards;
        
        
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

        public List<string> Info { set { info = value; } }
        public List<string> Ingredients { set { ingredients = value; } }
        public List<string> Technology { set { technology = value; } }
        public List<string> Cards { set { cards = value; } }



        private void PrintToFile()
        {
            const string PATH ="C:\\Users\\user\\Desktop\\"; 
              //  "C:\\Users\\user\\Documents\\2_diplom\\Receptures\\";
            string path;
            file = $"{file}.txt";

            //if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            //{
            //    //file = $"{file}.csv"; // for lists to be able to be opened with Exel
            //    path = PATH + file;
            //}
            //else
            //{
            //    path = saveFileDialog1.FileName;
            //}


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
            string input, path;           

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            // получаем выбранный файл
            path = openFileDialog1.FileName;
            // читаем файл в строку
            string fileText = System.IO.File.ReadAllText(path);
            MessageBox.Show("Файл открыт");

            strings.Clear();
            //strings.Add(path);
            string[] arr = fileText.Split('\n');
            strings.AddRange(arr);            
            PrintToBox();        

            //читаем файл построчно
            //using (StreamReader reader = new StreamReader(path))
            //{
            //    input = reader.ReadLine();
            //    while (input != null)
            //    {
            //        strings.Add(input);
            //        input = reader.ReadLine();
            //    }
            //    reader.Close();
            //}            
            //PrintToBox();
        }

        private void OpenFile (string path)
        {
            // читаем файл в строку
            string fileText = System.IO.File.ReadAllText(path);
            //MessageBox.Show("Файл открыт");

            strings.Clear();            
            string[] arr = fileText.Split('\n');
            strings.AddRange(arr);
            PrintToBox();
        }

        public void OpenFile1 (string path)
        {
            strings = new List<string>();
            OpenFile(path);
        }
    }
}
