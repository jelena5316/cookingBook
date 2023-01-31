using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int technologyIdByName(string name)
        {
            query = $"select id from Technology where name = '{name}';";            
            return int.Parse(dbReader(query)[0]);            
        }

        public List <Item> technologiesOfSimilarName(string name)
        {
            query = $"select id, name from Technology where name = '{name}';";
            return Catalog(query);
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
        

        public string recepturesCount(int recepture, int technology)
        {
            return $"select count(*) from Recepture where id = '{recepture}' and id_technology = '{technology}';";
        }
    }
}
