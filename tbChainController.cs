using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    
    class tbChainController: tbClass1
    {
        //int id_cards, id_technology;

        public tbChainController(string table) : base(table) { }        
        
        public int TechnologiesWithSelectedCardCount(int id)
        {
            int count = 0;
            query = $"select count (*) from Technology_chain where id_card = {id};";
            count = int.Parse(Count(query));
            return count;
        }

        public List<string> TechnologiesWithSelectedCard(int id)
        {
            query = $"select id_technology from {table} where id_card = {id};";
            return dbReader(query);
        }


    }
}
