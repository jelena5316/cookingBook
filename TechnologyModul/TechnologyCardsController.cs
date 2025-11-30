/*
 * to manage technologies cards inputing using form "Steps" and methods of class tbTechnologyController
 */

using System;
using System.Collections.Generic;

namespace MajPAbGr_project
{
    public class TechnologyCardsController
    {
		int id_card;
		List<Item> cards;
		tbCardsController tb;
		ChainsController chains;

		public TechnologyCardsController(ChainsController cntrl)
        {
			chains = cntrl;
			tb = cntrl.tbCardsController;
			id_card = tb.Selected;
			cards = tb.getCatalog();
        }

		public List<Item> Cards
        {
            get { return cards; }
        }

		public ChainsController Chains
        {
			get { return chains;  }
        }

		public tbCardsController getTbController()
		{
			return tb;
		}

		public string Submit(string name, string description, string technology, int card_id)
        {
			int ind = 0, num, id = card_id; ;
			string count = "", query, report = "";

			if (card_id == 0) // insert
			{
                if (!string.IsNullOrEmpty(description))
                    query = tb.insertCards(name, description, technology);
                else
                    query = tb.insertCards(name, technology);
                count = tb.Count(query);
				num = int.Parse(count);
				if (num > 0)
					tb.Selected = num;
				card_id = num;
			}
			else
			{
				ind = tb.UpdateReceptureOrCards("name", name, id);
				if (!string.IsNullOrEmpty(description))
					ind += tb.UpdateReceptureOrCards("description", description, id);
				else
					ind += tb.UpdateReceptureOrCards("description", null, id);
				ind += tb.UpdateReceptureOrCards("technology", technology, id);
			}

			tb.setCatalog();
			report = card_id == id ? "not inserted or updated" : "inserted";
			report = ind > 0 ? "updated" : report;
			return $"Technology card {name} (id {tb.Selected}) is {report}";
        }

		public string [] getFullInfo()
        {
			List<string> list = new List<string>();

			list.Add(tb.Selected.ToString());
			list.AddRange(tb.getFields());

			tbTechnologyController tbTech = new tbTechnologyController("Technology");
			tbChainController tbChain = new tbChainController("Technology_chain");

			List<string> tech_list = tbChain.TechnologiesWithSelectedCard(tb.Selected);
			if (tech_list != null)
            {
				list.Add("Technologies:");
				int k, q;
				for (k = 0; k < tech_list.Count; k++)
				{
					string name = tbTech.getById("name", int.Parse(tech_list[k]));
					list.Add($"{k} {name}");					
				}
				
				list.Add("Receptures:");
				for (k = 0; k < tech_list.Count; k++)				
				{
					tbTech.Selected = int.Parse(tech_list[k]);
					List<Item> receptures = tbTech.setSubCatalog("Recepture", "id_technology");
					if (receptures != null)
					{
						for(q = 0; q < receptures.Count; q++)
                        {
							list.Add($" {k}.{q} {receptures[q].name}");
                        }
					}
					else
					{
						list.Add("no recepture");
					}
				}
			}
            else
            {
				list.Add("no technology");
            }			
			return list.ToArray();			
        }
	}
}
