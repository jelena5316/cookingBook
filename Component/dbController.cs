﻿/*
 * to provide accessig to data base
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.Data.Sqlite;

namespace MajPAbGr_project
{ 
        public class dbController /*access data base*/
    {
        private string connectionString;
        private SqliteConnection connection;
        private SqliteDataReader reader;
        private SqliteCommand command;
        

        public dbController ()
        {
            connectionString = "Data Source = db\\CookingBook; Mode=ReadWrite";            
            connection = new SqliteConnection(connectionString);
        }

        /*
         * 'select'
         */

        public List<Item> Catalog (string query) //int, string
        {
            Item item;
            List<Item> list = new List<Item>();
            using (connection)
            {
                command = new SqliteCommand(query, connection);
                connection.Open();                
                using (reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = int.Parse(reader.GetValue(0).ToString());
                            string name = reader.GetValue(1).ToString();
                            item = new Item();
                            item.createItem(id, name);
                            list.Add(item);
                        }
                    }
                }
                connection.Close();
            }            
            return list;
        }        

        public List<string> dbReader(string query)  // only strings   
        {
            List<string> list = new List<string>();
            connection.Open();
            using (connection)
            {
                command = new SqliteCommand(query, connection);
                using (reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var name = reader.GetValue(0);                           
                            list.Add(name.ToString());
                        }
                    }   
                }
            }
            connection.Close();
            return list;
        }

        public List<Element> dbReadElement(string query) // int, string, double
        {
            List<Element> list = new List<Element>();
            Element element;
            using (connection)
            {
                command = new SqliteCommand(query, connection);
                connection.Open();
                using (reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            element = new Element();
                            var id_ingr = reader.GetValue(0);
                            string name = reader.GetValue(1).ToString();
                            var amounts = reader.GetValue(2);
                            element.Name = name;
                            element.Id = int.Parse(id_ingr.ToString());
                            element.Amounts = double.Parse(amounts.ToString());
                            list.Add(element);
                        }
                    }                 
                }
             connection.Close();  
                return list;
            }
        }

        public List<String> dbReadTechnology(string query) // only strings
        {
            List <String> cards = new List<String>();
            using (connection)
            {
                command = new SqliteCommand(query, connection);
                connection.Open();
                using (reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string technology = reader.GetValue(0).ToString();
                            technology += "*" + reader.GetValue(1).ToString();
                            cards.Add(technology);
                        }
                    }
                }
                connection.Close();
            }
            return cards;
        }

        public List<String> dbReadView(string query)
        {
            List<String> view_data = new List<String>();
            using (connection)
            {
                command = new SqliteCommand(query, connection);
                connection.Open();
                using (reader = command.ExecuteReader())
                {
                    string data = "";                    
                    if (reader.HasRows)
                    {                        
                        while (reader.Read())
                        {
                            data += reader.GetValue(0).ToString();
                            for (int k = 1; k < reader.FieldCount; k++)
                            {
                                data += " " + reader.GetValue(k).ToString();
                            }                           
                            view_data.Add(data);
                            data = "";
                        }
                    }
                }
                connection.Close();
            }
            return view_data;
        }

        /*
         * 'update', 'delete', 'insert'
         */

        public int Edit(string query)
        {
            int ind;            
            using (connection)
            {
                command = new SqliteCommand(query, connection);
                connection.Open();                
                ind = command.ExecuteNonQuery();
                connection.Close();
            }
            return ind;
        }


        /*
         * insert and get id of last inserted row; 'select' with function return a number
         */

        public string Count(string query)
        {
            string count="";
            using (connection)
            {
                command = new SqliteCommand(query, connection);
                connection.Open();                
                var num = command.ExecuteScalar();
                count = num.ToString();
                connection.Close();                              
            }
            return count;
        }
    }
}