using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class ChainController
    {
        int id_technology = 0, id_card = 0, id_recepture;     
        tbTechnologyCardsController tbCards;
        tbTechnologyController tbTech;
        tbChainController tbChain;

        public ChainController()
        {
            tbCards = new tbTechnologyCardsController("Technology_card");
            tbCards.setCatalog();
            tbTech = new tbTechnologyController("Technology");
            tbTech.setCatalog();
            tbChain = new tbChainController("Technology_chain");
        }        

        public ChainController(int id_tech, int id_cards)
        {
            this.id_card = id_cards;
            this.id_technology = id_tech;
            tbCards = new tbTechnologyCardsController("Technology_card");
            tbCards.setCatalog();
            tbTech = new tbTechnologyController("Technology");
            tbTech.setCatalog();
            tbChain = new tbChainController("Technology_chain");
        }

        public int Technology
        {
            set
            {
                id_technology = value;
                int count = tbChain.CardsInTechnologyCount(id_technology);
                if (count != 0)
                {
                    id_card = int.Parse(tbChain.CardsInTechnology(id_technology)[0]);
                }
            }
            get { return id_technology; }
        }

        public int Card
        {
            get
            {
                if (id_card != 0)
                {
                    int count = tbChain.CardsInTechnologyCount(id_technology);
                    if (count != 0)
                    {
                        id_card = int.Parse(tbChain.CardsInTechnology(id_technology)[0]);
                        return id_card;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
        }

        public int Recepture
        {
            set { id_recepture = value; }
            get { return id_recepture; }
        }
        
        public tbTechnologyCardsController tbCardsController
        {
            get { return tbCards; }
        }

        public tbTechnologyController tbTechController
        {
            get { return tbTech; }
        }

        public tbChainController tbChainController
        {
            get { return tbChain; }
        }    

        public int ApplyToChain()
        {
            id_card = tbCards.Selected;
            id_technology = tbTech.Selected;
            string query = $"insert into Technology_chain" +
                $" (id_technology, id_card) values ({id_technology}, {id_card});";
            return tbCards.Edit(query);
        }

        public int RemoveFromChain()
        {
            int ind;
            id_card = tbCards.Selected;
            id_technology = tbTech.Selected;
            ind = tbChain.RemoveCardFromChain(id_technology.ToString(), id_card.ToString());
            return ind;
        }        

    }
}
