/*
 * to manage work with recipe ingredients using form "Insert Amounts and classes` CalcFunction and tbAmountsController methods
 */

using System;
using System.Collections.Generic;

namespace MajPAbGr_project
{
    public class AmountsController
    {
        int selected_rec, main_ingredient_id = 0;
        Mode mode;
        double summa, recipe = 1;       
        tbAmountsController tbAmount;
        tbIngredientsController tbIngred;
        List<Item> ingredients;
        List<Element> elements, old_elements;
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

            main_ingredient_id = (elements.Count < 1) ? 0 : elements[0].Id;
        }

        public ReceptureStruct Info
        {
            set { RecStruct = value; }
            get { return RecStruct; }
        }

        public tbAmountsController TbAmount
        {
            get { return tbAmount; }
        }

        public List <string> getRecipesNames()
        {
            try
            {            
                List <string> names = tbAmount.dbReader($"select name from Recipe;");
                if (names.Count > 1)
                    return names;
                else
                    return null;
            }
            catch
            {
                return null;
            } 
        }
        
        public tbIngredientsController TbIngred
        {
            get { return tbIngred; }
        }

        public CalcFunction Calc
        {
            get { return calc; }
        }
     
        public List<Element> Elements { get { return elements; } }

        public Element changeSelectedElement (int index)
        {
            tbAmount.Selected = elements[index].Id;
            return elements[index];
        }

        public List<Item> Ingredients
        {
            get { return ingredients; }
        }

        public void RefreshElements()
        {
            if (mode == Mode.Create) return;
            if (elements != null)
                elements.Clear();
            tbAmount.RefreshElements();
            elements = tbAmount.getElements();
            mode = Mode.Edit;
        }


        /*
         *   Main ingredients and calculation
        */

        public Mode getMode { get { return mode; } }

        public double Recipe { get { return recipe; } }

        public string Main
        { get
            {
                if (main_ingredient_id > 0)
                {
                    string t = tbIngred.dbReader($"select name from {tbIngred.getTable()} where id = {main_ingredient_id}")[0];
                    if (t != "")
                        return $"{t} (id {main_ingredient_id})";
                    else
                        return "none";
                }
                else
                    return "none";

            }
        }

        public int SetMain(double amount, int index)
        {
            double new_amount = 100.0;
            if (elements.Count < 1)
            {
                main_ingredient_id = tbIngred.Selected;
                if (mode == Mode.Create)
                    recipe = CalcFunction.calculateCoefficient (amount, new_amount); // amount, 100
                calc.Coefficient = CalcFunction.calculateCoefficient(new_amount, amount); //100, amount                            
            }
            return index+1;
        }

        public void RemoveMain()
        {
            main_ingredient_id = 0;
            recipe = 0;
            calc.Coefficient = 1;
            if (mode == Mode.Edit)
                mode = Mode.EditNewMain;
        }

        public bool ResetMain(double amount) //after RemoveMain()
        {
            double new_amount = 100.0;
            if (elements.Count < 1) return false;
            if (mode != Mode.Edit)
            {
                main_ingredient_id = elements[0].Id;
                if (mode == Mode.Create)
                    recipe = CalcFunction.calculateCoefficient(amount, new_amount); // amount, 100
                calc.Coefficient = CalcFunction.calculateCoefficient(new_amount, amount); // 100, amount                      
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
            {
                elements.Insert(index + 1, el);
                return elements[index + 1];
            }
            else
            {
                elements.Add(el);
                return elements[elements.Count-1];
            }
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
        }

        public List<Element> ResetAmounts() // after ResetMain()
        {
            int k = 0;            
            elements[0].Amounts = 100;

           if (elements.Count > 1)
                {
                 for (k = 1; k < elements.Count; k++)
                    {
                        elements[k].Amounts = elements[k].Amounts * calc.Coefficient;                        
                    }                
                return elements;
                }                     
                
            return elements;                
        }

        public int UpdateMain()
        {
            //writting main ingredient id into table Recepture
            int ind = 0;
            tbController tb = new tbController("Recepture");           

            if (selected_rec > 0 && main_ingredient_id > 0)
            {
                try
                {
                    ind = tb.UpdateReceptureOrCards("id_main", main_ingredient_id.ToString(), selected_rec); // table Recepture
                }
                catch
                {
                    return -2;
                }
            }
            else
            {
                if (selected_rec > 0)
                {
                    string main = null;
                    try
                    {
                        ind = tb.UpdateReceptureOrCards("id_main", main, selected_rec); //table  Recepture
                    }
                    catch
                    {
                        return -2;
                    }
                }
                else
                    ind--;
            }
            return ind;           
        }

        public int saveRecipe(string name)
        {
            int ind = 0;
            double coefficient = Recipe;
            tbRecipeController tb = new tbRecipeController("Recipe");
            tb.Selected = selected_rec;
            tb.Recepture = selected_rec;

            if (coefficient != 0)
            {
                string coeff = calc.ColonToPoint(coefficient.ToString());
                try
                {
                    ind = tb.insertNewRecipe(name, coeff);
                }
                catch
                {
                    ind = -1;
                }
            }
            ind--;
            return ind;
        }
    } 
}

