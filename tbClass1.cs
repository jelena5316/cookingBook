using System.Collections.Generic;



namespace MajPAbGr_project
{
    public class tbClass1
    {
        int selected, count;
        string query, table, used = "0";
        List<Item> catalog, subcatalog;       
        dbController db;

        public tbClass1(string table)
        {
            selected = 0;
            this.table = table;
            catalog = new List<Item>();
            subcatalog = new List<Item>();
            db = new dbController();
        }    
        
        //setCatalog()
        public void setCatalog()
        {
            query = "select id, name from " + table + ";";
            catalog = db.Catalog(query);
            count = catalog.Count;
        }

        public List<Item> getCatalog() { return catalog; }

        public void resetCatalog()
        {
            if (catalog.Count > 0)
                catalog.Clear();
            setCatalog();
        }

        public List<Item>  setSubCatalog()
        {
            query = $"select id, name from Recipe where id_recepture = "
                + selected + ";";
            subcatalog = db.Catalog(query);
            return subcatalog;
        }

        //public List<Item> getSubCatalog() { return subcatalog; }

        public int setSelected(int temp)
        {
            this.selected = catalog[temp].id;
            return selected;
        }

        public int getSelected()
        {
            return selected;
        }

        public int Selected
        {
            set { selected = value; }
            get { return selected; }
        }

         public string getName(int index)
         {
              return catalog[index].name;
         }

        public string getTable()
        {
            return table;
        }


        /*********************
         * Для отдельных форм
         *********************/
        public void setUsed() // for Ingredients.cs
        {
            query = (table == "Ingredients") ? $"select count(*) from(select id_recepture from Amounts where id_ingredients = {selected});"
                    : $"select count (*) from Recepture where id_category = {selected}";
            used = db.Count(query);
        }

        public string getUsed() // for Ingredients
        {
            return used;
        }

         public List<string> SeeMoreFunc() // for Ingredients.cs
                {
                    query = (table == "Ingredients") ? $"select name from Recepture where id in (select id_recepture from Amounts where id_ingredients = {selected});"
                            : $"select name from Recepture where id_category = {selected}";
                    List<string> list = db.dbReader(query);
                    return list;
                }

        
        public int SelectedCount(string table, string column, int id) // for Form1.cs: before Technology to open
        {
            string query;
            query = $"select count ({column}) from {table}";
            if (id > 0)
                query += $"  where id = {id};";
            else
                query += ";";
            return int.Parse(db.Count(query));
        }
   
        public List<Element> readElement(int opt) // for Form1.cs
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
            el = db.dbReadElement(query);
            return el;
        }

        public List<string> SeeOtherCards(int id_technology) //for TechnologyCards.cs
        {
            List<string> list;            
            query = "select technology from Technology_card where id in " +
                $" (select id_card from Technology_chain where id_technology = {id_technology});";
            list = db.dbReader(query);
            return list;
        }

        public int getId (string column, int id) // for Recepture
        {
            query = $"select {column} from {table} where id = " + id + ";";
            List <string> id_list = db.dbReader(query);
            return int.Parse(id_list[0]);
        }        
      

        /********************
         * Операции с данными
         *********************/

        //remove
        public int RemoveItem()
        {
            query = $"delete from {table} where id = {selected}";
            int count = db.Edit(query);
            if (count != 0) resetCatalog();
            selected = 0;
            return count;
        }        

        //add      
        public int AddItem(string name) //Ingredients.cs
        {
            query = $"select count(*) from {table} where name ='{name}';";
            string ind = db.Count(query);

            if (ind == "0")
            {
                query = $"insert into {table} (name) values ('{name}');";
                int count = db.Edit(query);
                if (count != 0)  resetCatalog();
                return count;
            }
            else
            {
                return 0;
            }
        }

        public int insertNewRecipe(string name, string coeff) // Form1.cs, InsertAmounts.cs
        {
            string query = $"insert into Recipe (name, id_recepture, coefficient) values" +
                $" ('{name}', {selected}, {coeff});";
            return db.Edit(query);
        }

        // insert into amounts (InsertAmounts.cs)
        public int insertAmounts(int rec, string ingr, string amount)
        {
            query = "insert into Amounts (id_recepture, id_ingredients, amount) " +
                $"values ({rec}, {ingr}, {amount} );";
            return db.Edit(query);
        }

        //insert into Tecnology_chain
        public int insertTechnology (int technology, int cards)
        {
            query = $"insert into Technology_chain (id_technology, id_card) values ({technology}, {cards});";
            return db.Edit(query);
        }

        //update
        public int UpdateItem (string name) // Ingredients.cs
        {
            query = $"select count(*) from {table} where name ='{name}';";
            string ind = db.Count(query);

            if (ind == "0")
            {
                query = $"update {table} set name='{name}' where id = {selected};";
                int count = db.Edit(query);
                if (count != 0) resetCatalog();
                return count;
            }
            else
            {
                return 0;
            }
        }

        public int UpdateReceptureOrCards(string column, string value, int id_recepture)
        {
            int ind = 0;
            query = $"update {table} set {column} = '{value}' where id = {id_recepture};";
            ind = db.Edit(query);
            return ind;
        } 

    }
}
