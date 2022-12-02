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

        public ChainController()
        {
            tbCards = new TechnologyCardsController("Technology_card");
            tbCards.setCatalog();
            tbTech = new TechnologyController("Technology");
            tbTech.setCatalog();
        }

        public ChainController(int id_tech, int id_cards)
        {
            this.id_cards = id_cards;
            this.id_technology = id_tech;
            tbCards = new TechnologyCardsController("Technology_card");
            tbCards.setCatalog();
            tbTech = new TechnologyController("Technology");
            tbTech.setCatalog();
        }

        public TechnologyCardsController tbCardsController
        {
            get { return tbCards; }
        }

        public TechnologyController tbTechController
        {
            get { return tbTech; }
        }


    }
}
