using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class tbRecipeController : tbController
    {
        int recepture;

        public tbRecipeController(string table) : base(table) { }        

        public tbRecipeController (string table, int index, int recepture) : base (table) // for inserting new recipe (coefficient)
        {
            this.recepture = recepture;
            catalog = setSubCatalog();
            selected = setSelected(0);

            if (index > -1)
            {
                selected = setSelected(index);
            }  
        }

        public List<Item> setSubCatalog()
        {
            query = $"select id, name from Recipe where id_recepture = "
                + recepture + ";";
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

        public int Recepture
        {
            set { recepture = value; }
            get { return recepture; }
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
