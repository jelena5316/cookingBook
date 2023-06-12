/*
 * to manage creating  of technological chains using form "Chains"
 * and methods of classes tbChainController, tbTechnologyController and tbTechnologyCardsController
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class ChainsController
    {
        int id_technology = 0,
            id_card = 0; // id of the first cards of a selected technology             
        tbTechnologyCardsController tbCards;
        tbTechnologyController tbTech;
        tbChainController tbChain;

        public ChainsController()
        {
            tbChain = new tbChainController("Technology_chain");
            tbCards = new tbTechnologyCardsController("Technology_card");
            tbCards.setCatalog();
            tbTech = new tbTechnologyController("Technology");
            tbTech.setCatalog();
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
            if (tbTech.getCatalog().Count() == 0 || tbCards.getCatalog().Count() == 0)
            {
                return -2;
            }

            int ind = tbChain.ApplyCardToChain(
                tbTech.Selected.ToString(),
                tbCards.Selected.ToString()
                );
            return ind;
        }

        public int RemoveFromChain()
        {
            if (tbTech.getCatalog().Count() == 0 || tbCards.getCatalog().Count() == 0)
            {
                return -2;
            }
           
            int ind = tbChain.RemoveCardFromChain(
                tbTech.Selected.ToString(),
                tbCards.Selected.ToString()
                );
            return ind;
        }
    }
}
