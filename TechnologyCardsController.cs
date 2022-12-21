using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class TechnologyCardsController
    {
		int id_card;
		List<Item> cards;
		tbTechnologyCardsController tb;		

		public TechnologyCardsController(int card)
        {
			tb = new tbTechnologyCardsController("Technology_card");	
			if (card < 0) card = 0;
			id_card = card;
			tb.setCatalog();
			cards = tb.getCatalog();			
			tb.Selected = card;
		}

		public List<Item> Cards
        {
            get { return cards; }
        }
		public tbTechnologyCardsController getTbController()
		{
			return tb;
		}

		public int Submit()
        {
			return 0;
        }
	}
}
