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

        public int CardsInTechnologyCount(int id)
        {
            int count = 0;
            query = $"select count (*) from Technology_chain where id_technology = {id};";
            count = int.Parse(Count(query));
            return count;
        }

        public List <string> CardsInTechnology(int id)
        {
            query = $"select id_card from {table} where id_technology = {id};";
            return dbReader(query);
        }

        public int RemoveCardFromChain (string techn, string card)
        {
            int ind;
            string count;
            query = $"select count (*) from {table} where id_technology = {techn} and id_card = {card}";
            count = Count(query);

            if (int.TryParse(count, out ind)) ind = int.Parse(count);
            else ind = 0;
           
            if (ind > 0)
            {
                query = $"delete from {table} where id_technology = {techn} and id_card = {card}";
                ind = Edit(query);
            }
            return ind;
        }


    }
}
