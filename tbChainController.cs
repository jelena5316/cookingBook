using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    
    public class tbChainController: tbClass1
    {
        int technology, card;
        string name;
        List<Item> cards, technologies;

        public tbChainController(string table) : base(table)
        {
            cards = new List<Item>();
        }        
        

        /***********************************************************************
         Getters and setters
         ***********************************************************************/
        // public int Card
        //{
        //    set { card = value; }
        //    get { return card; }
        //}

        //public int Technology
        //{
        //    set { technology = value; }
        //    get { return technology; }
        //}

        //public string Name
        //{
        //    set { name = value; }
        //    get { return name; }
        //}

        //public void setCards(TechnologyCardsController tbCards)
        //{
        //    query = $"select id_card from {table} where id_technology = {technology};";
        //    List <string> id = dbReader(query);            
        //    for (int k = 0; k < id.Count; k++)
        //    {
        //        Item item = new Item();
        //        item.createItem(int.Parse(id[k]), tbCards.dbReader($"select name from {tbCards.getTable()} where id = {id[k]};")[0]);
        //        cards.Add(item);
        //    }  
        //}

        //public List<Item> getCards()
        //{
        //    return cards;
        //}

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

        public List <string> CardsInTechnology(int id)
        {
            query = $"select id_card from {table} where id_technology = {id};";
            return dbReader(query);
        }


        /*******************************************************************************
         Write and remove data in table
         *******************************************************************************/
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
