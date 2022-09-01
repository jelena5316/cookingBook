using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class TechnologyController: tbClass1
    {

        public TechnologyController(string table) : base(table) { }

        public int technologiesCountByName(string name)
        {
            query = $"select id from Technology where name = '{name}';";
            return int.Parse(dbReader(query)[0]);            
        }

        public string insertTechnology(string name, string description)
        {
            return "insert into Technology (name, description)" +
                $" values ('{name}', '{description}'); select last_insert_rowid()";
        }

        //SelectedCount (used in FormMain), cardsCount (used in TechnologyCards)
        public string technologiesCount(string name)
        {
            query = $"select count(*) from Technology where name = '{name}';";
            return Count(query);
        }
        public int recepturesCount(int id)
        {
            query = $"select count (*) from Recepture count where id_technology = {id};";
            return int.Parse(Count(query));
        }

        public string recepturesCount(int recepture, int technology)
        {
            return $"select count(*) from Recepture where id = '{recepture}' and id_technology = '{technology}';";
        }

        public string getRecepturesName()
        {
            int id;
            string tech_of_rec = null, name = "", data = null;            
            query = $"select id_technology from Recepture where id={selected};";
            data = dbReader(query)[0];

            if (data!= null)
                tech_of_rec = data;

            if (!string.IsNullOrEmpty(tech_of_rec))
            {
                //query = $"select name from Technology where id = {tech_of_rec}";
                //data = dbReader(query)[0];

                id = int.Parse(tech_of_rec);
                data = catalog.Find(d => d.id == id).name;
                if (data != null)
                {
                    name = data;
                }
                name = "no";
            }
            return name;
        }
    }
}
