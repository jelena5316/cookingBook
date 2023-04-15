using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class AmountsController
    {
        bool set_main, reset_main;
        int selected_rec, main_ingredient_id;
        Mode mode;
        double summa, recipe;
        string recipe_name;
        tbAmountsController tbAmount;
        tbIngredientsController tbIngred;
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

            tbIngred = new tbIngredientsController(1);
            tbIngred.setCatalog();
            ingredients = tbIngred.getCatalog();

            this.mode = (elements.Count < 1) ? Mode.Create : Mode.Edit;
            // mode autodetector
        }

        public ReceptureStruct Info
        {
            set { RecStruct = value; }
        }

        public tbAmountsController TbAmount
        {
            get { return tbAmount; }
        }
        public tbIngredientsController TbIngred
        {
            get { return tbIngred; }
        }
        public CalcFunction Calc
        {
            get { return calc; }
        }
        public double Summa { get { return summa; } }

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
            for (k = 1; k < elements.Count + 1; k++)
            {
                arr[k] = $"({elements[k - 1].Id.ToString()}) {elements[k - 1].Name},\t{elements[k - 1].Amounts.ToString()}";
            }
            arr[k] = "\n";
            arr[k + 1] = "From listview: name: value, (old value)";

            for (int q = k + 2; q < items.Count + k + 2; q++)
            {
                string[] source = items[q - k - 2];
                arr[q] = $"{source[0]}:\t{source[1]},\t({source[2]});";
            }
            return arr;
        }


        /***************************************************
            New logic (according PPS, using old code too)
        ****************************************************/
       
        public Mode getMode { get { return mode; } }

        public int SetMain(double amount, int index)
        {
            if (elements.Count < 1)
            {
                main_ingredient_id = tbIngred.Selected;
                if (mode == Mode.Create)
                    recipe = CalcFunction.calculateCoefficient(100.0, amount);
                calc.Coefficient = CalcFunction.calculateCoefficient(amount, 100.0);
                reset_main = false;
                set_main = true;                
            }
            return index+1;
           
            //else
            {
                if (set_main || reset_main) return -2;
                else
                {
                    //if (index == 0)
                    //{
                    //    main_ingredient_id = tbIngred.Selected;
                    //    if (mode == Mode.Create)
                    //        recipe = CalcFunction.calculateCoefficient(100.0, amount);
                    //    calc.Coefficient = CalcFunction.calculateCoefficient(amount, 100.0);
                    //    reset_main = false;
                    //    set_main = true;
                    //    return index;
                    //}
                    //else
                    //    return index;
                }
            }           
        }


        public void RemoveMain()
        {
            set_main = false;
            reset_main = false;
            main_ingredient_id = 0;
            recipe = 0;
            calc.Coefficient = 1;
            if (mode == Mode.Edit)
                mode = Mode.EditNewMain;
        }

        public bool ResetMain() //after RemoveMain()
        {
            double amount;
            if (elements.Count < 1) return false;
            if (mode != Mode.Edit)
            {
                amount = elements[0].Amounts;
                main_ingredient_id = elements[0].Id;
                if (mode == Mode.Create)
                    recipe = CalcFunction.calculateCoefficient(amount, 100);
                calc.Coefficient = CalcFunction.calculateCoefficient(100, amount);
                reset_main = true;                
            }
            return true;
        }

        public int RemoveElement(int index)
        {
            if (index < 0) return -1;
            if (elements.Count > index)// 1 > 0
            { 
                elements.RemoveAt(index);
                if (index > 0) index--;
                if (index < 0) index = 0;                
                return index;
            }
            else
                return -2;
        }

        public Element AddElement(int index, string name, double amount)
        {
            Element el = new Element();
            el.Id = tbIngred.getSelected(); // id of ingredient 
            el.Name = name;
            el.Amounts = amount;

            if (elements.Count > 0)
                elements.Insert(index + 1, el);
            else
                elements.Add(el);
            return el;
        }

        public Element AddElement(int index_ingr, int index)
        {
            Element el = new Element();
            el.Id = tbIngred.Selected;
            el.Name = tbIngred.getName(index_ingr);            
            
            if (elements.Count > 0)            
                elements.Insert(index + 1, el);
            else
                elements.Add(el);
            return elements[elements.Count-1];
        }

        public double SetAmounts(double amount, Element el)
        {             
            if (elements.Count > 1)
            {
                if (mode == Mode.Edit)
                    el.Amounts = amount; 
                else
                    el.Amounts = amount * calc.Coefficient;
            }
            else
                el.Amounts = 100.0;
            return el.Amounts;

            //if (mode != Mode.Edit)
            //    if (elements.Count == 1 || set_main)
            //    {
            //        el.Amounts = 100.0;
            //        return 100.0;
            //    }
            //    else
            //    {
            //        el.Amounts = amount * calc.Coefficient;
            //        return el.Amounts;
            //    }                            
            //return -1;
        }

        public List<Element> ResetAmounts() // after ResetMain()
        {
            const string PATH = "C:\\Users\\user\\Desktop\\ResetElements.txt";
            int k = 0;
            //old_elements[0].Amounts = elements[0].Amounts;
            elements[0].Amounts = 100;

            using (StreamWriter stream = new StreamWriter(PATH, true))
            {
                if (!File.Exists(PATH))
                {
                    File.CreateText(PATH);
                    stream.WriteLine($"File is created: {File.GetLastWriteTime(PATH)} \n");
                }
                stream.WriteLine("Coefficient " + " " + calc.Coefficient.ToString());
                stream.WriteLine(k + " " + elements[k].Name + " " + elements[k].Amounts);// + "\t" + old_elements[k].Amounts);
                if (elements.Count > 1)
                {
                    for (k = 1; k < elements.Count; k++)
                    {
                        //old_elements[k].Amounts = elements[k].Amounts;
                        elements[k].Amounts = elements[k].Amounts * calc.Coefficient;
                        stream.WriteLine("\t" + (k - 1) + " " + elements[k - 1].Amounts);
                        stream.WriteLine(k + " " + elements[k].Name + " " + elements[k].Amounts);// + "\t" + old_elements[k].Amounts);
                    }

                    stream.WriteLine("\nTest");
                    for (k = 1; k < elements.Count; k++)
                    {
                        stream.WriteLine(k + " " + elements[k].Name + " " + elements[k].Amounts);
                    }
                    stream.WriteLine($"[Record of: {System.DateTime.Now}]\n");
                stream.Close();
                return elements;
                }                     
            }      
            return elements;
        }
    } 
}

