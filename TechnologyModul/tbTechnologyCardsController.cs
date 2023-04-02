using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class tbTechnologyCardsController: tbController
    {

        private string name, description, card;
        
        public tbTechnologyCardsController(string table) : base(table) { }

        
        //set and get fields
        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        public string Description
        {
            set { description = value; }
            get { return description; }
        }
        public string Card
        {
            set { card = value; }
            get { return card; }
        }

        public void setFields() // 'cards_id' is equel selected
        {
            name = getById("name", selected);
            if (cardsCount(selected) != "0")
            {
                description = getById("description", selected);
            }
            else description = "";
            card = getById("technology", selected);
        }

        public string [] getFields()
        {
            string[] arr = new string[] { name, description, card };
            return arr;
        }

        //insert into Tecnology_chain
        public int insertCardsIntoChain(int technology, int cards)
        {
            query = $"insert into Technology_chain" +
                $" (id_technology, id_card) values ({technology}, {cards});";
            return Edit(query);
        }

        public string insertCards (string name, string technology)
        {
            return "insert into Technology_card (name, technology)" +
                    $" values ('{name}', '{technology}'); select last_insert_rowid()";
        }

        public string insertCards(string name, string description, string technology)
        {
            if(description != null)
                return $"insert into Technology_card (name, description, technology)" +
                    $" values ('{name}', '{description}', '{technology}'); select last_insert_rowid()";
            else
                return $"insert into Technology_card (name, technology)" +
                    $" values ('{name}', '{technology}'); select last_insert_rowid()";
        }

        public string cardsCount (int id) // see SelectedCount in FormMain
        {
            query = $"select count (*) from Technology_card where id = {id} and description not null;";
            return Count(query);
        }

        public string cardsCountInChain (int id)
        {
            query = $"select count(*) from Technology_chain where id = {id}";
            return Count(query);
        }
      
        //public int SelectedCount(string table, string column, int id) // for Form1.cs: before Technology to open
        //{
        //    string query;
        //    query = $"select count ({column}) from {table}";
        //    if (id > 0)
        //        query += $"  where id = {id};";
        //    else
        //        query += ";";
        //    return int.Parse(Count(query));
        //}

        public string cardsCount(string name) // see SelectedCount in FormMain
        {
            query = $"select count (*) from Technology_card where name = '{name}';";
            return Count(query);
        }

        public List<string> SeeOtherCards(int id_technology) //for TechnologyCards.cs
        {
            //id_technology здесь индефикатор технологии, а не карты
            
            List<string> list;
            query = "select name from Technology_card where id in " +
                $" (select id_card from Technology_chain where id_technology = {id_technology});";
            list = dbReader(query);
            return list;
        }

        public List <string> SeeOtherCardsFull(int id_technology)
        {
            List<string> list;
            query = "select technology from Technology_card where id in " +
                $" (select id_card from Technology_chain where id_technology = {id_technology});";
            list = dbReader(query);
            return list;
        }
    }
}
