using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class RecipeController : tbClass1
    {
        int recepture;

        public RecipeController(string table) : base(table) { }        

        public RecipeController (string table, int index, int recepture) : base (table)
        {
            this.recepture = recepture;
            selected = recepture;

            if(index >-1)
            {
                catalog = setSubCatalog();
                selected = setSelected(index);  
            }       
        }

        public new List<Item> setSubCatalog()
        {
            query = $"select id, name from Recipe where id_recepture = "
                + recepture + ";";
            subcatalog = Catalog(query);
            return subcatalog;
        }

        public int Recepture
        {
            set { recepture = value; }
        }

        public string ReceptureInfo()
        {
            string info = "\n";
            info += dbReader($"select source from Recepture where id = {recepture};")[0] + "\n";
            info += dbReader($"select author from Recepture where id = {recepture};")[0] + "\n";
            info += dbReader($"select URL from Recepture where id = {recepture};")[0] + "\n";
            info += dbReader($"select description from Recepture where id = {recepture};")[0]+ "\n";
            // queries will be usefull for the form InsertAmounts!
            return info;
        }

        public override int RemoveItem()
        {
            if (selected < 1) return 0;
            return base.RemoveItem();
        }
    }
}
