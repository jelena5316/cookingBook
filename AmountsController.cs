using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class AmountsController : tbClass1
    {
        private int amount_id_count, elements_count, selected_element;
        private List<string> amounts_id;
        private List<Element> elements;
        private int id_recepture;
        FormMainController tbRec;

        enum Columns
        {
            id_ingredients,
            amount,
            id_recepture
        }

        public AmountsController(string table) : base(table) { } // New Recepture; create mode

        public AmountsController(string table, ref FormMainController tb) : base(table) // for edit mode
        {
            id_recepture = tb.Selected;
            amounts_id = setAmountsIdList(id_recepture);
            tbRec = tb;
            elements = tb.readElement(1);

            // for data updating
            amount_id_count = amounts_id.Count;
            elements_count = elements.Count;
        }

        public FormMainController TbRec
        {
            set { tbRec = value; }
        }

        public void tbRecSelected(int id)
        {
            tbRec.Selected = id;
        }

        public new int setSelected(int temp)
        {
            this.selected_element = elements[temp].Id;
            return selected_element;
        }

        public int Amount_id_count { get { return amount_id_count; } }

        public int Elements_count { get { elements_count = elements.Count; return elements_count; } }

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

        public void setTable(string table)
        {
            base.table = table;
        }

        public List<string> setAmountsIdList(int recepture)
        {
            string query = $"select id  from Amounts where id_recepture = {recepture}";
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
            for (index = 0, sum = 0; index < lv.Items.Count-1; index++)
            {
                id_ingr = lv.Items[index].Tag.ToString();
                amount = lv.Items[index].SubItems[2].Text;
                amount = calc.ColonToPoint(amount);

                query = "insert into Amounts (id_recepture, id_ingredients, amount) " +
                $"values ({id_recepture}, {id_ingr}, {amount} );";                
                ind = Edit(query);
                if (ind > 0)
                    sum++; // proverka zapisi
            }
            if (sum == lv.Items.Count) return 0;// vse zapisalosj
            else return sum;
        }

        public int UpdateAmountsNoChangeInRowCount
            (string [] id_ingredients, string [] amounts_of_ingredients, int id_recepture) // mode: edit
        //случай, когда число записей не меняется
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

        public int updateRecords(ref Form2 frm)
        {
            int k, ind = 0;
            string query, amount;           
            CalcFunction calc = new CalcFunction();
            
            string UpdateAmountQuery(Columns column, string value, string id_recepture)=>
             $"update {table} set {column.ToString()} = '{value}' where id = {id_recepture};";

            if (amount_id_count >= elements_count)//amount_count >= element_count
            {
                frm.richTextBox1.Text += "\n***\nUpdating db\n";
                for (k = 0; k < elements_count; k++)
                {
                    query = UpdateAmountQuery(Columns.id_ingredients, elements[k].Id.ToString(), amounts_id[k]);
                    ind = Edit(query);
                    amount = calc.ColonToPoint(elements[k].Amounts.ToString());
                    query = UpdateAmountQuery(Columns.amount, amount, amounts_id[k]);
                    ind+= Edit(query);
                    frm.richTextBox1.Text += ind / 2 + "th records, where id " + amounts_id[k] + "\n";
                }
                if (amount_id_count - elements_count > 0)//amount_count - element_count > 0
                {
                    //удаляем лишнее
                    frm.richTextBox1.Text += "\n***\nDeleting from db\n";
                    for (int q = k; q < amount_id_count; q++)
                    {
                        query = $"delete from Amounts where id = {amounts_id[q]};";
                        int rezult = Edit(query);
                        frm.richTextBox1.Text += rezult + " records, where id " + amounts_id[q] + "\n";
                    }
                }
            }
            else
            {
                frm.richTextBox1.Text += "\n***\nUpdating db\n";
                for (k = 0; k < amount_id_count; k++)
                {
                    query = UpdateAmountQuery(Columns.id_ingredients, elements[k].Id.ToString(), amounts_id[k]);
                    ind = Edit(query);
                    amount = calc.ColonToPoint(elements[k].Amounts.ToString());
                    query = UpdateAmountQuery(Columns.amount, amount, amounts_id[k]);
                    ind += Edit(query);
                    frm.richTextBox1.Text += ind / 2 + "th records, where id " + amounts_id[k] + "\n";
                }

                frm.richTextBox1.Text += "\n***\nInserting into db\n";
                for (int q = k; q < elements_count; q++)
                {
                    // дописывает недостающее и получаем номера                    
                    amount = calc.ColonToPoint(elements[q].Amounts.ToString());
                    query = $"insert into Amounts ({Columns.id_recepture.ToString()},"+
                        $"{Columns.id_ingredients.ToString()}, {Columns.amount.ToString()})" +
                        $"values ({id_recepture}, {elements[q].Id.ToString()}, {amount});"+
                        "select last_insert_rowid();";
                    string id = Count(query);

                    // вносим номера в список номеров в контроллере
                    amounts_id.Add(id);

                    frm.richTextBox1.Text += "last insert records id " + id + ", where ingredients id " + elements[q].Id.ToString() + "\n";
                }
                frm.richTextBox1.Text += "records are " + amounts_id.Count + "\n";
            }
            return ind;
        }
    }
}
