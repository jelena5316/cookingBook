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
        List<FormEF_test.Technology> technologies = new List<FormEF_test.Technology>();

        
        public tbTechnologyController(string table) : base(table)
        {
            //TECHN_COL_ID = "id";
            //TECHN_COL_NAME = "name";
            //TECHN_COL_DESCRIPTION = "description";
        }

        public void readAllTechsByQuery()
        {
            List<object[]> list;

            if (technologies.Count > 0)
                technologies.Clear();            

            query = $"Select id, name, description from {table}";
            list = dbReadData(query);
            
            foreach (object[] item in list)
            {
                technologies.Add(new FormEF_test.Technology
                    (
                    (int)item[0],
                    item[1].ToString(),
                    item[2].ToString()
                    ));
            }      
        }

        public void setCurrent()
        {
            object[] technology = null;            

            query = $"select name, description from Technology where id = {selected};"; //id			
            technology = dbReadData(query)[0];
            current = new Technology
                (
                selected,
                technology[0].ToString(),
                technology[1].ToString() 
                );
        }

        public string[] OutTechnology() //into textbox
        {           
            return new string[] { current.Name, current.Note };
        }

        public override void setUsed()
        {
            query = $"select count (*) from Recepture where id_technology = {selected};";
            used = Count(query);
        }

        public override string getUsed() { return used; }

        public string Statistic
        {
            get
            {
                query = $"Select count (*) from {table};";
                return Count(query);
            }
        }

        public string Statisic_cards
        {
            get
            {
                query = $"select count (*) from Technology_card";
                return Count(query);
            }
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

        public List<Item> CardsInTechnologyAsSubcatalog()
        {
            query = $"select id, name from {TABLE_CARDS} where id in (select id_card from {TABLE_CHAINS} where {CHAIN_COL_TECHN} = {selected});";
            return Catalog(query);
        }
    }
}
