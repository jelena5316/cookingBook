/*
 * to access table "Technology"
 */

using FormEF_test;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace MajPAbGr_project
{
    public class tbTechnologyController: tbController
    {
        string used = "0";
        FormEF_test.Technology current;
        List<Item> receptures;
        List<Card> cards;


        public tbTechnologyController(string table) : base(table)
        {
           cards = new List<Card>();
        }

        public FormEF_test.Technology Current
        {
            get { return current; }
        }


        public List<Item> RecepturesOfTechnology
        {
            get { return receptures; }
        }


        public void setCurrent()
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
            setUsed();
            receptures = setSubCatalog("Recepture", "id_technology");            
            //subcatalog = CardsInTechnologyAsSubcatalog();
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

        public string[] OutTechnology() //into textbox
        {
            return new string[] { current.Name, current.Note };
        }

        public List <Item> getTechnologiesIdsByName(string name)
        {            
            query = $"select id, name from Technology where name = '{name}';";
            return Catalog(query);
        }

        public string technologiesCount(string name)
        {
            query = $"select count(*) from Technology where name = '{name}';";            
            return Count(query);
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
                query = $"select count (*) from Technology_card";
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
