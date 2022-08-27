using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class IngredientsController : tbClass1
    {
        int opt;
        string used = "0";

        public IngredientsController() { }
        public IngredientsController(int opt) : base ()
        {
            this.opt = opt;
            base.table = opt == 1 ? "Ingredients" : "Categories";
            catalog = new List<Item>();
            subcatalog = new List<Item>();
        }

        public int getOption() { return opt; }
        
        public void setUsed()
        {
            query = (table == "Ingredients") ? $"select count(*) from(select id_recepture from Amounts where id_ingredients = {selected});"
                    : $"select count (*) from Recepture where id_category = {selected}";
            used = Count(query);
        }

        public string getUsed()
        {
            return used;
        }

        public List<string> SeeMoreFunc() // for Ingredients.cs
        {
            query = (table == "Ingredients") ? $"select name from Recepture where id in (select id_recepture from Amounts where id_ingredients = {selected});"
                    : $"select name from Recepture where id_category = {selected}";
            List<string> list = dbReader(query);
            return list;
        }

        public int AddItem(string name) //Ingredients.cs
        {
            query = $"select count(*) from {table} where name ='{name}';";
            string ind = Count(query);

            if (ind == "0")
            {
                query = $"insert into {table} (name) values ('{name}');";
                int count = Edit(query);
                if (count != 0) resetCatalog();
                return count;
            }
            else
            {
                return 0;
            }
        }

        public int UpdateItem(string name) // Ingredients.cs
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

    }
}
