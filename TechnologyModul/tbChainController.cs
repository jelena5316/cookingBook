/*
 * to access table "Technology_chain"
 */

using System;
using System.Collections.Generic;


namespace MajPAbGr_project
{
    
    public class tbChainController: tbController
    {
       
        public tbChainController(string table) : base(table) {}

        /************************************************************************
         Read data from table
         ************************************************************************/
        public int TechnologiesWithSelectedCardCount(int id)
        {
            int count = 0;
            query = $"select count (*) from {table} where id_card = {id};";
            count = int.Parse(Count(query));
            return count;
        }

        public int CardsInTechnologyCount(int id)
        {
            int count = 0;
            query = $"select count (*) from {table} where id_technology = {id};";
            count = int.Parse(Count(query));
            return count;
        }

        public List<string> TechnologiesWithSelectedCard(int id)
        {
            query = $"select id_technology from {table} where id_card = {id};";
            return dbReader(query);
        }

        public List<string> getNames(List<string> ids)
        {
            if (ids.Count > 0)
            {
                int k;
                string range = "";
                    for (k = 0; k < ids.Count - 1; k++)
                    range += $"{ids[k]}, ";
                    range += ids[k];
                    return dbReader($"select name from Technology where id in ({range})");           
            }
            else
            {
                return null;
            }        
        }

        public List <string> CardsInTechnology(int id)
        {
            query = $"select id_card from {table} where id_technology = {id};";
            return dbReader(query);
        }       

        public List<Item> CardsInTechnologyAsSubcatalog(string subtable, string column, int id)
        {
            query = $"select id, name from {subtable} where id in (select id_card from {table} where {column} = {id});";            
            return Catalog(query);
        }


        /*******************************************************************************
         Write and remove data in table
         *******************************************************************************/
       public int ApplyCardToChain(string techn, string card)
        {
            query = $"insert into {table}" +
                $" (id_technology, id_card) values ({techn}, {card});";
            return Edit(query);
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
