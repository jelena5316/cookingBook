/*
 * to access table Amounts and Ingredients, to store list of recipe ingredients: id, name and amount
 */


using System;
using System.Collections.Generic;

namespace MajPAbGr_project
{
    public class tbAmountsController : tbController
    {
        private int amount_id_count, elements_count, selected_element, id_recepture;   
        private List<string> amounts_id;
        private List<Element> elements;   // list of recipe ingredients
        private tbReceptureController tbRec;             

        public tbAmountsController(string table, tbReceptureController tb) : base(table)
        {
            id_recepture = tb.Selected;
            amounts_id = setAmountsIdList(id_recepture);
            tbRec = tb;
            elements = tb.readElement(1);

            // for data updating
            amount_id_count = amounts_id.Count;
            elements_count = elements.Count;
        }

        public new int Selected
        {
            set { selected_element = value; }
            get { return selected_element; }
        }

        public int Amount_id_count { get { return amount_id_count; } }

        public List<Element> getElements() { return elements; }

        public void RefreshElements()
        {
            elements = tbRec.readElement(1);
            elements_count = elements.Count;
        }

        public List<string> setAmountsIdList(int recepture)
        {
            string query = $"select id from Amounts" +
                $" where id_recepture = {recepture}";
            amounts_id = dbReader(query);
            return amounts_id;
        }
                
        public int updateRecords()
        {
            int k, ind = 0;
            string query, amount;           
            CalcFunction calc = new CalcFunction();            
            elements_count = elements.Count;

            string UpdateAmountQuery(Columns column, string value, string id_recepture)=>
             $"update {table} set {column.ToString()} = '{value}' where id = {id_recepture};";

             
            if (amount_id_count >= elements_count) // new list of ingredientsis <= old list of ingredients                                                   
            {        
                for (k = 0; k < elements_count; k++)
                {
                    query = UpdateAmountQuery(Columns.id_ingredients, elements[k].Id.ToString(), amounts_id[k]);
                    try
                    {
                        ind += Edit(query);                        
                    }
                    catch
                    {
                        continue;
                    }

                    amount = calc.ColonToPoint(elements[k].Amounts.ToString());                       
                    query = UpdateAmountQuery(Columns.amount, amount, amounts_id[k]);
                    try
                    {      
                        Edit(query);
                    }
                    catch
                    {
                        ind--;
                        continue;
                    }                    
                }
                if (amount_id_count - elements_count > 0) // new list of ingredientsis < old list of ingredients 
                {
                    // rest deleting                   
                    int rezult = 0, q = 0;
                    for (q = k; q < amount_id_count; q++)
                    {
                        query = $"delete from Amounts where id = {amounts_id[q]};";                        
                        try
                        {
                            rezult += Edit(query);                                                      
                        }
                        catch
                        {
                            continue;
                        }
                    }                    
                    ind += rezult;
                }
            }
            else // new list of ingredientsis >= old list of ingredients 
            {
                // new list of ingredientsis > old list of ingredients, updating common part              
                for (k = 0; k < amount_id_count; k++)
                {
                    query = UpdateAmountQuery(Columns.id_ingredients, elements[k].Id.ToString(), amounts_id[k]);
                    ind = Edit(query);
                    amount = calc.ColonToPoint(elements[k].Amounts.ToString());
                    query = UpdateAmountQuery(Columns.amount, amount, amounts_id[k]);
                    ind += Edit(query);                    
                }

                //new list of ingredientsis = old list of ingredients, inserting extra item
                for (int q = k; q < elements_count; q++)
                {
                    // inserting new and getting id               
                    amount = calc.ColonToPoint(elements[q].Amounts.ToString());
                    query = $"insert into Amounts ({Columns.id_recepture.ToString()},"+
                        $"{Columns.id_ingredients.ToString()}, {Columns.amount.ToString()})" +
                        $"values ({id_recepture}, {elements[q].Id.ToString()}, {amount});"+
                        "select last_insert_rowid();";
                    string id="0";
                    try
                    {
                        id = Count(query);
                        ind++;
                        // adding id of extra Item in list of ID
                        amounts_id.Add(id);                        
                    }
                    catch
                    {
                        continue;
                    }    
                }
                amount_id_count = amounts_id.Count;                
            }
            return ind;
        }
    }
}
