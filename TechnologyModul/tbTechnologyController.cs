/*
 * to access table "Technology"
 */

using FormEF_test;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace MajPAbGr_project
{
    public class tbTechnologyController: tbController
    {
        string used = "0";
        Technology current;
        List<Item> receptures;
        List<Technology> technologies;
        List<Card> cards;


        public tbTechnologyController(string table) : base(table)
        {
            technologies = new List<Technology>();
            cards = new List<Card>();
            receptures = new List<Item>();
            current = null;
        }

        /*
         * Properties
         */

        public Technology Current
        {
            get { return current; }
        }

        public string[] AboutCurrent //into textbox
        {
            get { return new string[] { current.Name, current.Note }; }
        }

        public List<Technology> Technologies
        {
            get { return technologies; }
        }

        public List<Item> RecepturesOfTechnology
        {
            get { return receptures; }
        }


        /*
         * Methods (seed data)
         */

        public List<object[]> readTechnologies()
        {
            List<object[]> list;

            if (technologies.Count > 0)
                technologies.Clear();

            query = $"Select id, name, description from {table}";
            list = dbReadData(query);
            return list;
        }

        public void setCatalogs(List <object[]> data)
        {
            foreach (object[] techn in data)
            {
                long id = (long)techn[0];  
                string name = techn[1].ToString();
                string note = techn[2].ToString();

                Technology t = new Technology((int)id, name, note);  
                technologies.Add(t);

                Item item = new Item();
                item.createItem((int)id, name);
                catalog.Add(item);
            }
        }


        public void setCurrent(int index)
        {
            current = technologies[index];
            selected = setSelected(index);

            receptures.Clear();
            receptures = setSubCatalog("Recepture", "id_technology");
            used = receptures.Count().ToString();
            subcatalog.Clear();
            subcatalog = CardsInTechnologyAsSubcatalog();
        }

        public void getCurrentReadingFromDB()
        {
            object[] technology = null;
            query = $"select name, description from {table} where id = {selected};"; //id			
            technology = dbReadData(query)[0];
            current = new Technology
                (
                selected,
                technology[0].ToString(),
                technology[1].ToString() 
                );    
            
            receptures = setSubCatalog("Recepture", "id_technology");
            used = receptures.Count().ToString();
        }


        /*
         * Statistics data
         */

        public string Statistic
        {
            get
            {
                query = $"Select count (*) from {table};";
                return Count(query);
            }
        }

        public override void setUsed()
        {
            query = $"select count (*) from Recepture where id_technology = {selected};";
            used = Count(query);
        }


        public override string getUsed() { return used; }


        /*
         *  Technologies
         */

        public List <Item> getTechnologiesIdsByName(string name)
        {            
            query = $"select id, name from Technology where name = '{name}';";
            return Catalog(query); // if name not unique -- will do nothing! No dialog!
        }

        
        public string insertTechnology(string name, string description)
        {
            query = "insert into Technology (name, description)" +
                $" values ('{name}', '{description}'); select last_insert_rowid()";                       
            return Count(query);
        }

        /*
         * Cards
         */

        public string Statisic_cards
        {
            get
            {
                query = $"select count (*) from {TABLE_CARDS}";
                return Count(query);
            }
        }

        public int CardsInTechnologyCount()
        {
            int count = 0;
            query = $"select count (*) from {TABLE_CHAINS} where id_technology = {selected};";
            count = int.Parse(Count(query));
            return count;
        }

        public List<Item> CardsInTechnologyAsSubcatalog()
        {
            query = $"select id, name from {TABLE_CARDS} where id in (select id_card from {TABLE_CHAINS} where {CHAIN_COL_TECHN} = {selected});";
            return Catalog(query);
        }


        /*
         * Chains: add and remove technologies card
         */
        
        public /* FormEF_test.Chains */ string AddCardsToTechnology(Item card)
        {
            query = $"insert into {TABLE_CHAINS}" +
                    $" ({CHAIN_COL_CARD}, {CHAIN_COL_TECHN})" +
                    $" values ({card.id}, {selected});";
            return Count(query);
        }

        public string RemoveCardsFromTechnology(Item card)
        {
            query = $"remove from {TABLE_CHAINS}" +
                $" where {CHAIN_COL_CARD} = {card.id}" +
                $" and {CHAIN_COL_TECHN} = {selected};";
            return Count(query);
        }
    }
} 
