using System;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace MajPAbGr_project
{

    public class Amounts
    {
        int id_recepture, id_ingred;
        double amount;
        public Amounts(int rec, int ingred, double am)
        {
            id_recepture = rec;
            id_ingred = ingred;
            amount = am;
        }

        public int Rec { set { id_recepture = value; } get { return id_recepture; } }
        public int Ingred { set { id_ingred = value; } get { return id_ingred; } }

        public double Am { set { amount = value; } get { return amount; } }

    };

    //public class ReadWriteAmount : dbController
    //{
    //    public List<Amounts> amounts;
    //    string table, query;

    //    public ReadWriteAmount()
    //    {
    //        table = "Amounts";
    //        query = $"select * from {table};";
    //        Catalog();
    //    }

    //    public void CreateQuery(int rec, int ingred, double amount)
    //    {
    //       query = "insert into AmountsT(id_recepture, id_ingredients, amount)" +
    //                $"values ({rec}, {ingred}, {amount});";
    //    }

    //    public List<Amounts> getAmounts { get { return amounts; } }
    //    public string getQuery { get { return query; } }

    //    public void Catalog() //int, string
    //    {
    //        Amounts item;
    //        amounts =  new List<Amounts>();
    //        using (connection)
    //        {
    //            command = new SqliteCommand(query, connection);
    //            connection.Open();
    //            using (reader = command.ExecuteReader())
    //            {
    //                if (reader.HasRows)
    //                {
    //                    while (reader.Read())
    //                    {
    //                        int rec = int.Parse(reader.GetValue(0).ToString());
    //                        int ingred =  int.Parse(reader.GetValue(1).ToString());
    //                        double am = double.Parse(reader.GetValue(2).ToString());
    //                        item = new Amounts(rec, ingred, am);
    //                        amounts.Add(item);
    //                    }
    //                }
    //            }
    //            connection.Close();
    //        }
    //    }
    //}
    
    
    public partial class EditDB : Form
    {
        dbController db;
        //ReadWriteAmount db;
        string query;
        //List<Amounts> amounts;       
        
        public EditDB()
        {
            InitializeComponent();
            db = new dbController();
            // db = new ReadWriteAmount();
            //amounts = db.getAmounts;


            //ReceptureStruct rec = new ReceptureStruct(34);
            //rec.setData();
            //textBox1.Lines = rec.getData();
            // проверка метода у структуры
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            query = textBox1.Text;
            db.Edit(query);

            //int sum = 0;
            //foreach(Amounts am in amounts)
            //{
            //    db.CreateQuery(am.Rec, am.Ingred, am.Am);
            //    query = db.getQuery;
            //    sum+= db.Edit(query);
            //    textBox1.Text = sum.ToString();
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //FormMain frm = new FormMain();
            //frm.Show();
        }

    }
}
