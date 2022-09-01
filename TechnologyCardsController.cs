using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    class TechnologyCardsController: tbClass1
    {
        public TechnologyCardsController(string table) : base(table) { }

        //insert into Tecnology_chain
        public int insertCardsIntoChain(int technology, int cards)
        {
            query = $"insert into Technology_chain" +
                $" (id_technology, id_card) values ({technology}, {cards});";
            return Edit(query);
        }

        public string insertTechnology (string name, string technology)
        {
            return "insert into Technology_card (name, technology)" +
                    $" values ('{name}', '{technology}'); select last_insert_rowid()";
        }

        public string insertTechnology(string name, string description, string technology)
        {
            return $"insert into Technology_card (name, description, technology)" +
                    $" values ('{name}', '{description}', '{technology}'); select last_insert_rowid()";
        }

        public string cardsCount (int id) // see SelectedCount in FormMain
        {
            query = $"select count (*) from Technology_card where id = {id} and description not null;";
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
            query = $"select count (*) from Technology_card where name = {name};";
            return Count(query);
        }

    }
}
