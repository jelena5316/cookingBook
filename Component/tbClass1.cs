using System.Collections.Generic;



namespace MajPAbGr_project
{
    public class tbClass1 : dbController
    {
        protected string table;
        protected string query;
        //protected dbController db;
        protected List<Item> catalog, subcatalog;
        protected int selected;
        int count;


        public tbClass1() : base()
        {
            selected = 0;
            //db = new dbController();
        }
        public tbClass1(string table)
        {
            selected = 0;
            this.table = table;
            catalog = new List<Item>();
            subcatalog = new List<Item>();
            //db = new dbController();
        }

        //setCatalog()
        public void setCatalog()
        {
            query = "select id, name from " + table + ";";
            catalog = Catalog(query);
            count = catalog.Count;
        }

        public List<Item> getCatalog() { return catalog; }

        public void resetCatalog()
        {
            if (catalog.Count > 0)
                catalog.Clear();
            setCatalog();
        }

        public List<Item> setSubCatalog(string subtable, string column) //Recipe, id_recepture
        {
            query = $"select id, name from {subtable} where {column} = "
                + selected + ";";
            subcatalog = Catalog(query);
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

        public string getById(string column, int id) // for Recepture and others
        {
            query = $"select {column} from {table} where id = " + id + ";";
            List<string> id_list = dbReader(query);
            if (id_list.Count > 0)
                return id_list[0];
            else
                return "0";
        }

        public virtual void setUsed() {}

        public virtual string getUsed() { return ""; }


        /*********************
         * Для отдельных форм
         *********************/
        //public void setUsed() // for Ingredients.cs
        //{
        //    query = (table == "Ingredients") ? $"select count(*) from(select id_recepture from Amounts where id_ingredients = {selected});"
        //            : $"select count (*) from Recepture where id_category = {selected}";
        //    used = Count(query);
        //}

        //public string getUsed() // for Ingredients
        //{
        //    return used;
        //}

        //public List<string> SeeMoreFunc() // for Ingredients.cs
        //{
        //    query = (table == "Ingredients") ? $"select name from Recepture where id in (select id_recepture from Amounts where id_ingredients = {selected});"
        //            : $"select name from Recepture where id_category = {selected}";
        //    List<string> list = dbReader(query);
        //    return list;
        //}


        //public int SelectedCount(string table, string column, int id) // for Form1.cs: before Technology to open
        //{
        //    string query;
        //    query = $"select count ({column}) from {table}";
        //    if (id > 0)
        //        query += $"  where id = {id};";
        //    else
        //        query += ";";
        //    return int.Parse(Count(query));
        //}

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
            el = dbReadElement(query);
            return el;
        }

        //public List<string> SeeOtherCards(int id_technology) //for TechnologyCards.cs
        //{
        //    List<string> list;
        //    query = "select technology from Technology_card where id in " +
        //        $" (select id_card from Technology_chain where id_technology = {id_technology});";
        //    list = dbReader(query);
        //    return list;
        //}

        


        /********************
         * Операции с данными
         *********************/

        //remove
        public virtual int RemoveItem()
        {
            query = $"delete from {table} where id = {selected}";
            int count = Edit(query);
            if (count != 0) resetCatalog();
            selected = 0;
            return count;
        }

        //add      
        //public int AddItem(string name) //Ingredients.cs
        //{
        //    query = $"select count(*) from {table} where name ='{name}';";
        //    string ind = Count(query);

        //    if (ind == "0")
        //    {
        //        query = $"insert into {table} (name) values ('{name}');";
        //        int count = Edit(query);
        //        if (count != 0)  resetCatalog();
        //        return count;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        // Form1.cs, InsertAmounts.cs, RecipeController
        //public int insertNewRecipe(string name, string coeff)
        //{
        //    string query = $"insert into Recipe" +
        //        $" (name, id_recepture, coefficient) values" +
        //        $" ('{name}', {selected}, {coeff});";
        //    return Edit(query);
        //}

        //insert into Tecnology_chain
        //public int insertTechnology(int technology, int cards)
        //{
        //    query =$"insert into Technology_chain" +
        //        $" (id_technology, id_card) values ({technology}, {cards});";
        //    return Edit(query);
        //}
        // this two: from int to string, add param from table -> static or virtual

        // insert into amounts (InsertAmounts.cs)
        //public int insertAmounts(int rec, string ingr, string amount)
        //{
        //    query = "insert into Amounts (id_recepture, id_ingredients, amount) " +
        //        $"values ({rec}, {ingr}, {amount} );";
        //    return Edit(query);
        //}
        // if give params as array, then is possible join with two upper methods


        //новые методы взамен трех выше
        public int InsertItem (string table, string column, string value)
        {
            query = $"insert into {table} ({column}) " +
                $"values ({value});";
            return Edit(query);
        }

        public int InsertItem (string column, string value)
        {
            query = $"insert into {table} ({column}) " +
                $"values ({value});";
            return Edit(query);
        }

        public int insertItems (string table, string [] columns, string [] values)
        {
            int k = 0;
            string column = "", value = "";
            for (k = 0; k < values.Length - 1; k++)
            {
                column += columns[k] + ", ";
                value += values[k] + ", ";
            }
            column += columns[k];
            value += values[k];
            query = $"insert into {table} ({column}) values({values});";        
            return Edit(query);
        }

        public int insertItems (string [] columns, string[] values)
        {
            //переписать в метода выше!
            query = $"insert into {table} (id_technology, id_card) values (";
            int k = 0;
            for (k = 0; k < values.Length - 1; k++)
            {
                query += values[k] + ",";
            }
            query += values[k] + ");";
            return Edit(query);
        }

        //update

        //public int UpdateItem(string name) // Ingredients.cs
        //{
        //    query = $"select count(*) from {table} where name ='{name}';";
        //    string ind = Count(query);

        //    if (ind == "0")
        //    {
        //        query = $"update {table} set name='{name}' where id = {selected};";
        //        int count = Edit(query);
        //        if (count != 0) resetCatalog();
        //        return count;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        public int UpdateReceptureOrCards(string column, string value, int id_recepture)
        {
            int ind = 0;
            query = $"update {table} set {column} = '{value}' where id = {id_recepture};";
            ind = Edit(query);
            return ind;
        }

    }
}
