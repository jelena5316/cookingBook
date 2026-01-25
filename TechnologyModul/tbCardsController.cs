/*
 *  to access table "Technology_card"
*/

using System;
using System.Collections.Generic;
using FormEF_test;
using System.Reflection;

namespace MajPAbGr_project
{
    public class tbCardsController: tbController
    {

        private string name, description, card;
        private Card current;
        private List<Card> cards;
        
        public tbCardsController(string table) : base(table)
        {
            cards = new List<Card>();
            current = null;        
        } // table == TABLE_CARDS

        
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

        public List <Card> TechnologyCards
        {
            get { return cards; }
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

        public void setCurrent(int index)
        {
            current = cards[index];
            selected = setSelected(index);
        }

        public string[] AboutCurrent()
        {
            string[] arr = new string[] { current.Name, current.Description, current.Technology };
            return arr;
        }

        /*
        * Seed data
        */

        public List<object[]> readTechnologiesCards()
        {
            List<object[]> list;

            if (cards.Count > 0)
                cards.Clear();

            query = $"Select id, name, description, technology from {table}";
            list = dbReadData(query);
            return list;
        }

        public void setCatalogs(List <object[]> data)
        {
            //foreach (object[] card in data)
            //{
            //    long id = (long)card[0];
            //    string name = card[1].ToString();
            //    string note = card[2].ToString();
            //    string step = card[3].ToString();

            //    Card c = new Card((int)id, name, note, step);
            //    cards.Add(c);

            //    Item item = new Item();
            //    item.createItem((int)id, name);
            //    catalog.Add(item);
            //}

            if (cards == null || cards.Count == 0)
            {
                foreach (object[] card in data)
                {
                    long id = (long)card[0];
                    string name = card[1].ToString();
                    string note = card[2].ToString();
                    string step = card[3].ToString();

                    Card c = new Card((int)id, name, note, step);
                    cards.Add(c);
                }
            }

            if (catalog == null || catalog.Count == 0)
            {
                foreach (object[] card in data)
                {
                    long id = (long)card[0];
                    string name = card[1].ToString();
                    Item item = new Item();
                    item.createItem((int)id, name);
                    catalog.Add(item);
                }
            }
        }


        //insert into Tecnology_chain
        public int insertCardsIntoChain(int technology, int cards)
        {
            query = $"insert into {TABLE_CHAINS}" +
                $" (id_technology, id_card) values ({technology}, {cards});";
            return Edit(query);
        }

        //CRUD
        public string insertCardsQuery (string name, string technology)
        {
            return $"insert into {table} (name, technology)" +
                    $" values ('{name}', '{technology}'); select last_insert_rowid()";
        }

        public string insertCardsQuery(string name, string description, string technology)
        {
            if(description != null)
                return $"insert into {table} (name, description, technology)" +
                    $" values ('{name}', '{description}', '{technology}'); select last_insert_rowid()";
            else
                return $"insert into {table} (name, technology)" +
                    $" values ('{name}', '{technology}'); select last_insert_rowid()";
        }

        public string insertCards(string name, string description, string technology)
        {
            if (description != null)
                query = $"insert into {table} (name, description, technology)" +
                    $" values ('{name}', '{description}', '{technology}'); select last_insert_rowid()";
            else
                query = $"insert into {table} (name, technology)" +
                    $" values ('{name}', '{technology}'); select last_insert_rowid()";
            return Count(query);
        }

        public string cardsCount (int id) // see SelectedCount in FormMain
        {
            query = $"select count (*) from {table} where id = {id} and description not null;";
            return Count(query);
        }

        public string cardsCountInChain (int id)
        {
            query = $"select count(*) from {TABLE_CHAINS} where id = {id}";
            return Count(query);
        }
      
        public string cardsCount(string name) // see SelectedCount in FormMain
        {
            query = $"select count (*) from {table} where name = '{name}';";
            return Count(query);
        }

        public List <string> SeeOtherCardsFull(int id_technology)
        {
            List<string> list;
            query = $"select technology from {table} where id in " +
                $" (select id_card from {TABLE_CHAINS} where id_technology = {id_technology});";
            list = dbReader(query);
            return list;
        }
    }
}
