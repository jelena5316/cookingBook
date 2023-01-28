using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class tbAmountsController : tbClass1
    {
        private int amount_id_count, elements_count, selected_element, id_recepture;        
        private List<string> amounts_id;
        private List<Element> elements; // id and name, for amounts      
        private tbReceptureController tbRec;

        enum Columns
        {
            id_ingredients,
            amount,
            id_recepture
        }

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
            string query = $"select id from Amounts where id_recepture = {recepture}";
            amounts_id = dbReader(query);
            return amounts_id;
        }

        public int updateRecords(ref Form2 frm)
        {
            int k, ind = 0;
            string query, amount;           
            CalcFunction calc = new CalcFunction();

            //writting main ingredient id into table Recepture
            if (elements.Count > 0 && id_recepture > 0)
            {
                tbClass1 tb = new tbClass1("Recepture");
                ind = tb.UpdateReceptureOrCards("id_main", elements[0].Id.ToString(), id_recepture); // Recepture
                if (ind == 0) return -1;
                else ind = 0;
            }
            else ind = 0;

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
