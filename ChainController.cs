using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    class ChainController
    {
        int id_technology, id_cards;
        TechnologyCardsController tbCards;
        TechnologyController tbTech;
        tbChainController tbChain;

        public ChainController()
        {
            tbCards = new TechnologyCardsController("Technology_card");
            tbCards.setCatalog();
            tbTech = new TechnologyController("Technology");
            tbTech.setCatalog();
            tbChain = new tbChainController("Technology_chain");
        }

        public ChainController(int id_tech, int id_cards)
        {
            this.id_cards = id_cards;
            this.id_technology = id_tech;
            tbCards = new TechnologyCardsController("Technology_card");
            tbCards.setCatalog();
            tbTech = new TechnologyController("Technology");
            tbTech.setCatalog();
            tbChain = new tbChainController("Technology_chain");
        }

        public TechnologyCardsController tbCardsController
        {
            get { return tbCards; }
        }

        public TechnologyController tbTechController
        {
            get { return tbTech; }
        }

        public tbChainController tbChainController
        {
            get { return tbChain; }
        }    

        public int CreateChain()
        {
            id_cards = tbCards.Selected;
            id_technology = tbTech.Selected;
            string query = $"insert into Technology_chain" +
                $" (id_technology, id_card) values ({id_technology}, {id_cards});";
            return tbCards.Edit(query);
        }

        public int RemoveFromChain()
        {
            int ind;
            id_cards = tbCards.Selected;
            id_technology = tbTech.Selected;
            ind = tbChain.RemoveCardFromChain(id_technology.ToString(), id_cards.ToString());
            return ind;
        }


    }
}
