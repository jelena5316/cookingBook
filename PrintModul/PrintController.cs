using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    class PrintController
    {
        const string PATH = "C:\\Users\\user\\Desktop\\";       
        string file;
        List<string> strings,
            info,
            ingredients,
            technology,
            cards;          
           
  
        public PrintController(string fname)
        {
            file = fname;
        }

        public List<string> Strings { set { strings = value; } }
        public List<string> Info { set { info = value; } }
        public List<string> Ingredients { set { ingredients = value; } }
        public List<string> Technology { set { technology = value; } }
        public List<string> Cards { set { cards = value; } }

        public void PrepareRecipeToOutput()
        {
            strings = new List<string>();
            strings.Add("ABOUT\n");
            strings.AddRange(info);
            strings.Add("\nINGREDIENTS (%)\n");
            strings.AddRange(ingredients);
            strings.Add("\nCOOKING\n");
            strings.AddRange(technology);
            strings.Add("\nBY STEPS\n");
            strings.AddRange(cards);
        }

        public void PrintRecipe()
        {
            Print frm = new Print(strings, file);
            frm.Show();           
        }

        public void PrepareRecipeIngredientsOutput()
        {
            strings = new List<string>();
            strings.Add("RECIPE");
            strings.AddRange(info);
            strings.Add("\nINGREDIENTS");
            strings.AddRange(ingredients);
        }
    }

    
}
