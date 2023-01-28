using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class AmountsController
    {
        int selected_rec;
        double summa;
        tbAmountsController tbAmount;
        IngredientsController tbIngred;
        List<Item> ingredients;
        List<Element> elements, old_elements, formated;
        CalcFunction calc;
        ReceptureStruct RecStruct;

        public AmountsController(tbReceptureController tb)
        {
            summa = 0;
            tbAmount = new tbAmountsController("Amounts", tb);
            selected_rec = tb.Selected;
            elements = tbAmount.getElements();
            old_elements = new List<Element>();
            old_elements.AddRange(elements);
            calc = new CalcFunction();
            calc.setAmounts(elements);
            summa = calc.getTotal();

            tbIngred = new IngredientsController(1);
            tbIngred.setCatalog();
            ingredients = tbIngred.getCatalog();            
        }

        public ReceptureStruct Info
        {
            set { RecStruct = value; }
        }

        public tbAmountsController TbAmount
        {
            get { return tbAmount; }
        }
        public IngredientsController TbIngred
        {
            get { return tbIngred; }
        }
        public CalcFunction Calc
        {
            get { return calc; }
        }
        public double Summa { get { return summa; }  }

        public List<Element> Elements { get { return elements; } }

        public List<Element> Old { get { return old_elements; } }

        public List<string> Formated
        {
            get
            {
                int length = elements.Count;
                double[] arr = new double[length];
                for (int k = 0; k < length; k++)
                {
                    arr[k] = elements[k].Amounts;
                }
                return calc.FormatAmounts(arr, summa);
            }
        }

        public List<Item> Ingredients
        {
            get { return ingredients; }
        }

        public string[] PrintAmount(List<string[]> items)
        {
            int length, id, k;
            string name, category, info;
            
            length = tbAmount.Elements_count + 1;
            length += items.Count + 2;
            id = RecStruct.getIds()[0];
            name = RecStruct.getName();
            category = RecStruct.getCategory();
            string[] arr = new string[length];

            info = $"Recepture from DB:\n name {name} (id {tbAmount.Id_recepture}), category {category} ({id})";            
            arr[0] = info + "\n";
            for (k = 1; k < elements.Count+1; k++)
            {
                arr[k] = $"({elements[k-1].Id.ToString()}) {elements[k-1].Name},\t{elements[k-1].Amounts.ToString()}";
            }
            arr[k] = "\n";
            arr[k + 1] = "From listview: name: value, (old value)";

            for (int q = k+2;  q < items.Count+k+2; q++)
            {              
                string[] source = items[q-k-2];
                arr[q] =  $"{source[0]}:\t{source[1]},\t({source[2]});";
            }
            return arr;
        }
    }
}
