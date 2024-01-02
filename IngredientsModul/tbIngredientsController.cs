/*
 * to access table Ingredients and Categories
 */

using System;
using System.Collections.Generic;

namespace MajPAbGr_project
{
    public class tbIngredientsController : tbController
    {
        int opt;
        string used = "0";

        public tbIngredientsController() { }
        
        public tbIngredientsController(int opt) : base ()
        {
            this.opt = opt;
            base.table = opt == 1 ? "Ingredients" : "Categories";
            catalog = new List<Item>();
            subcatalog = new List<Item>();
        }

        public int getOption() { return opt; }
        
        public override void setUsed()
        {
            query = (table == "Ingredients") ? $"select count(*) from(select id_recepture from Amounts where id_ingredients = {selected});"
                    : $"select count (*) from Recepture where id_category = {selected}";
            used = Count(query);
        }

        public string Statistic
        {
            get
            {
                query = $"select count (*) from {table}";
                return Count(query);
            }
        }

        public string getStatistic(int option)
        {
            if (opt == option)
                return Statistic;
            else
            {
                table = opt == 1 ? "Categories" : "Ingredients";
                string t = Statistic;
                table = opt == 1 ? "Ingredients" : "Categories";
                return t;
            }
        }

        public override string getUsed()
        {
            return used;
        }

        public List<string> SeeMoreFunc()
        {
            query = (table == "Ingredients") ? $"select name from Recepture where id in (select id_recepture from Amounts where id_ingredients = {selected});"
                    : $"select name from Recepture where id_category = {selected}";
            List<string> list = dbReader(query);
            return list;
        }

        public int AddItem(string name)
        {
            query = $"select count(*) from {table} where name ='{name}';";
            string ind = Count(query);

            if (ind == "0")
            {
                query = $"insert into {table} (name) values ('{name}');";                
                int count = Edit(query);
                if (count != 0) resetCatalog();
                query = "";                
                return count;
            }
            else
            {
                return 0;
            }
        }

        public int UpdateItem(string name)
        {
            query = $"select count(*) from {table} where name ='{name}';";
            string ind = Count(query);

            if (ind == "0")
            {
                query = $"update {table} set name='{name}' where id = {selected};";
                int count = Edit(query);
                if (count != 0) resetCatalog();
                return count;
            }
            else
            {
                return 0;
            }
        }

        public int ReadIngredientsFromFile(string name)
        {
            string fname = "";
            List <string> names = new List<string>();
            List<string> data = new List<string>();

            //open a file dialog and get file name;

            //check file explaneing: txt and csv
            //reading file

            PrintController cntrl = new PrintController(fname);
            // will create methode for print controller: read file;
            cntrl.Strings = names;
            cntrl.PrintRecipe();

            //add code to Print.cs: accept a chois of file;

            //check file content: find list of ingredients or category list
            //storing name list: List<string>()

            for(int k=0; k < names.Count; k++)
            {
                names.Add(data[k]);
            }

            

            return 0;
        }

        public int MultyInsert(List<string> ingrs)
        {
            List<string> ingredients = ingrs;
            string string_app = "";

            int k;
            string t = "";
            for(k=0; k<ingredients.Count-1; k++)
            {
                string_app = $"('{ingredients[k]}'),";
                t += string_app;
            }
            string_app = $"('{ingredients[k]}')";
            t += string_app;

            query = $"insert into {table} (name) values {t};";

            int count;
            try
            {
                count = Edit(query);
                if (count != 0) resetCatalog();
                query = "";
                return count;
            }
            catch(Exception)
            {
                query = "";
                count = -1;
                return -1;
            } 
        }
    }
}
