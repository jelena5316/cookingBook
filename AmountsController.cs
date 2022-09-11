using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class AmountsController : tbClass1
    {
        private List<string> amounts_id;
        private List<Element> elements;
        private int id_recepture;
        FormMainController tbRec;

        public AmountsController(string table) : base(table) { } // New Recepture; create mode

        public AmountsController(string table, FormMainController tb) : base(table) // for edit mode
        {
            id_recepture = tb.Selected;
            amounts_id = setAmountsIdList(id_recepture);
            tbRec = tb;
            elements = tb.readElement(1);
        }

        public Element getElementByID(int index)
        {
            return elements[index];
        }

        public List<Element> getElements() { return elements; }


        public void RefreshElements()
        {
            elements = tbRec.readElement(1);
        }

        public int Id_recepture
        {
            get { return id_recepture; }
        }

        public void setTable(string table)
        {
            base.table = table;
        }

        public List<string> setAmountsIdList(int recepture)
        {
            string query = $"select id  from AmountsT where id_recepture = {recepture}";
            amounts_id = dbReader(query);
            return amounts_id;
        }

        public List<string> getAmountsIdList { get {return amounts_id; } }

        public int InsertAmounts(ref System.Windows.Forms.ListView lv, int id_recepture) // mode: create
        {
            CalcFunction calc = new CalcFunction();
            int index = 0, ind = 0, sum;
            string amount, id_ingr, query;            

            //writting main ingredient id into table Recepture
            id_ingr = lv.Items[0].Tag.ToString();
            tbClass1 tb = new tbClass1("Recepture");
            ind = tb.UpdateReceptureOrCards("id_main", id_ingr, id_recepture); // Recepture
            if (ind == 0) return -1; 
 
            //writing ingredients' amounts into Amounts
            //for (index = lv.Items.Count - 1, sum = 0; index > -1; index--)                       
            for (index = 0, sum = 0; index < lv.Items.Count; index++)
            {
                id_ingr = lv.Items[index].Tag.ToString();
                amount = lv.Items[index].SubItems[2].Text;
                amount = calc.ColonToPoint(amount);

                query = "insert into AmountsT (id_recepture, id_ingredients, amount) " +
                $"values ({id_recepture}, {id_ingr}, {amount} );";                
                ind = Edit(query);
                if (ind > 0)
                    sum++; // proverka zapisi
            }
            if (sum == lv.Items.Count) return 0;// vse zapisalosj
            else return sum;
        }
        
        //случай, когда число записей не меняется
        public int UpdateAmounts
            (string [] id_ingredients, string [] amounts_of_ingredients, int id_recepture)
        {
            //перенесла код в форму
            int k, ind=0;
            int count = id_ingredients.Length;
            for (k = 0; k < count; k++)
            {
               //query = $"update {table} set id_ingredients = {id_ingredients[k]} where id = {amounts_id[k]};";
               //ind += Edit(query);             
               //query = $"update {table} set amount = {amounts_of_ingredients[k]} where id = {amounts_id[k]};";
               //ind += Edit(query);
               ind += UpdateReceptureOrCards("id_ingredients", id_ingredients[k], id_recepture);
               ind += UpdateReceptureOrCards("amount", amounts_of_ingredients[k], id_recepture);
            }
            if (ind == (count * 2)) return 0;
            else return -1;
        }

        //public List<Element> readElement() // for Form1.cs
        //{
        //    List<Element> el;

        //    query = "SELECT id_ingredients, name, amount" +
        //    " FROM Amounts AS am JOIN Ingredients AS ingr " +
        //    "ON am.id_ingredients = ingr.id WHERE am.id_recepture = "
        //    + id_recepture + ";";
        //    el = dbReadElement(query);
        //    return el;
        //}
    }
}

//public int UpdateReceptureOrCards(string column, string value, int id_recepture)
//{
//    int ind = 0;
//    query = $"update {table} set {column} = '{value}' where id = {id_recepture};";
//    ind = Edit(query);
//    return ind;
//}
