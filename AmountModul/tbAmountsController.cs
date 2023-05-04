using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class tbAmountsController : tbController
    {
        private int amount_id_count, elements_count,
            selected_element, id_recepture;        
        private List<string> amounts_id;
        private List<Element> elements; // id and name, for amounts      
        private tbReceptureController tbRec;
                

        public tbAmountsController(string table) : base(table) { } // New Recepture; create mode

        public tbAmountsController(string table, tbReceptureController tb) : base(table) // for edit mode
        {
            id_recepture = tb.Selected;
            amounts_id = setAmountsIdList(id_recepture);
            tbRec = tb;
            elements = tb.readElement(1);

            // for data updating
            amount_id_count = amounts_id.Count;
            elements_count = elements.Count;
        }

        public tbReceptureController TbRec
        {
            set { tbRec = value; }
            get { return tbRec; }
        }

        public void tbRecSelected(int id)
        {
            tbRec.Selected = id;
        }

        public new int setSelected(int temp) // а использую ли я это где-нибудь?
        {
            this.selected_element = elements[temp].Id;
            return selected_element;
        }

        public new int Selected
        {
            set { selected_element = value; }
            get { return selected_element; }
        }

        public int Amount_id_count { get { return amount_id_count; } }

        public int Elements_count
        {           
            get { elements_count = elements.Count; return elements_count; }
        }

        public Element getElementByIndex(int index)
        {
            return elements[index];
        }

        public List<Element> getElements() { return elements; }

        public void RefreshElements()
        {
            elements = tbRec.readElement(1);
            elements_count = elements.Count;
        }

        public int Id_recepture
        {
            set { id_recepture = value; }
            get { return id_recepture; }
        }
       

        public List<string> setAmountsIdList(int recepture)
        {
            string query = $"select id from Amounts" +
                $" where id_recepture = {recepture}";
            amounts_id = dbReader(query);
            return amounts_id;
        }

        public int UpdateMain()
        {
            //writting main ingredient id into table Recepture
            int ind = 0;
            tbController tb = new tbController("Recepture");
            
            if (elements.Count > 0 && id_recepture > 0)
            {
                try
                {
                    ind = tb.UpdateReceptureOrCards("id_main", elements[0].Id.ToString(), id_recepture); // Recepture
                }
                catch
                {
                    return -2;
                }              
            }
            else
            {
                if (id_recepture > 0)
                {
                    string main = null;
                    try
                    {
                        ind = tb.UpdateReceptureOrCards("id_main", main, id_recepture); // Recepture                        
                    }
                    catch
                    {
                        return -2;
                    }
                    ind = 0;
                }
                else
                {
                    ind = -1;                    
                } 
            }
            return ind;
        }
        
        public int updateRecords()
        {
            int k, ind = 0;
            string query, amount;           
            CalcFunction calc = new CalcFunction();            
            elements_count = elements.Count;

            string UpdateAmountQuery(Columns column, string value, string id_recepture)=>
             $"update {table} set {column.ToString()} = '{value}' where id = {id_recepture};";

             
            if (amount_id_count >= elements_count) // новый список сырья меньше или равен изначальному
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
                if (amount_id_count - elements_count > 0) // список сырья меньше изначального
                {
                    //удаляем лишнее                   
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
            else // список сырья больше изначального или таковым и является
            {
                // больше изначального, обнавляем изначальное               
                for (k = 0; k < amount_id_count; k++)
                {
                    query = UpdateAmountQuery(Columns.id_ingredients, elements[k].Id.ToString(), amounts_id[k]);
                    ind = Edit(query);
                    amount = calc.ColonToPoint(elements[k].Amounts.ToString());
                    query = UpdateAmountQuery(Columns.amount, amount, amounts_id[k]);
                    ind += Edit(query);                    
                }

                // являлся изначальным, вводим
                for (int q = k; q < elements_count; q++)
                {
                    // дописывает недостающее и получаем номера                    
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
                        // вносим номера в список номеров в контроллере
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
