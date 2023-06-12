/*
 * to access table "Technology"
 */

using System;
using System.Collections.Generic;

namespace MajPAbGr_project
{
    public class tbTechnologyController: tbController
    {
        string used="0";
        
        public tbTechnologyController(string table) : base(table) { }

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

        public string insertTechnology(string name, string description)
        {
            return "insert into Technology (name, description)" +
                $" values ('{name}', '{description}'); select last_insert_rowid()";
        }

        public string technologiesCount(string name)
        {
            query = $"select count(*) from Technology where name = '{name}';";
            return Count(query);
        }
    }
}
