using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class ChainsController
    {
        int id_technology = 0, id_card = 0, id_recepture;     
        tbTechnologyCardsController tbCards;
        tbTechnologyController tbTech;
        tbChainController tbChain;

        public ChainsController()
        {
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

        public List <string> Names(List<string> ids)
        {
            List<string> list = new List<string>();            
            for (int k = 0; k < ids.Count; k++)
            {
                list.Add
                    (tbCards.dbReader($"select name from {tbCards.getTable()} where id = {ids[k]};")[0]);
            }
            return list;
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
            int ind = tbChain.ApplyCardToChain(
                tbTech.Selected.ToString(),
                tbCards.Selected.ToString()
                );
            return ind;
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
