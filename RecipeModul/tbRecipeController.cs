/*
 * to access table Recipe, to store list of recipes of recepture: id, name and coefficients
 */

using System;
using System.Collections.Generic;

namespace MajPAbGr_project
{
    public class tbRecipeController : tbController
    {
        int recepture;

        public tbRecipeController(string table) : base(table) { }

        public int Recepture
        {
            set { recepture = value; }
            get { return recepture; }
        }

        public List<Item> setSubCatalog() // for table's "Coefficients" controller
        {
            query = $"select id, name from Recipe where id_recepture = "
                + recepture + ";";

            if (subcatalog.Count > 0)
            {
                subcatalog.Clear();
            }

            if (catalog.Count > 0)
            {
                catalog.Clear();
            }
            subcatalog = Catalog(query);
            catalog = subcatalog;
            return subcatalog;
        }

        public override List<Element> readElement(int opt)
        {
            int temp = selected;
            selected = recepture;
            List<Element> el = base.readElement(2);
            selected = temp;
            return el;
        }

        public override int RemoveItem()
        {
            if (selected < 1) return 0;
            return base.RemoveItem();
        }

        public int insertNewRecipe(string name, string coeff)
        {
            string query = $"insert into Recipe" +
                $" (name, id_recepture, coefficient) values" +
                $" ('{name}', {recepture}, {coeff});";
            return Edit(query);
        }
    }
}
