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

        public AmountsController(ref tbReceptureController tb)
        {
            summa = 0;
            tbAmount = new tbAmountsController("Amounts", ref tb);
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
    }
}
