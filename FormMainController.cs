﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class FormMainController : tbClass1
    {

        int id_technology;

        public FormMainController(string table): base(table)
        {
            this.table = table;
        }

        public string getCategory()
        {
           return dbReader($"select id_category from {table} where id = {selected};")[0];
        }

        public string getTechnology()
        {
            return dbReader($"select id_technology from Recepture where id = {selected};")[0];
        }

        public new List<Element> readElement(int opt) // for Form1.cs
        {
            List<Element> el;
            switch (opt)
            {
                case 1: // amounts
                    query = "SELECT id_ingredients, name, amount" +
                    " FROM Amounts AS am JOIN Ingredients AS ingr " +
                    "ON am.id_ingredients = ingr.id WHERE am.id_recepture = "
                    + selected + ";";

                    break;
                case 2: // recipe
                    query = "SELECT id, name, coefficient" +
                    " FROM Recipe WHERE id_recepture = "
                    + selected + ";";
                    break;
                default:
                    query = "SELECT id, name, coefficient" +
                    " FROM Recipe WHERE id_recepture = "
                    + selected + ";";
                    break;
            }
            el = dbReadElement(query);
            return el;
        }

        public int SelectedCount(string table, string column, int id) // for Form1.cs: before Technology to open
        {
            string query;
            query = $"select count ({column}) from {table}";
            if (id > 0)
                query += $"  where id = {id};";
            else
                query += ";";
            return int.Parse(Count(query));
        }


    }
}
