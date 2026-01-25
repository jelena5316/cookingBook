/*
 * to manage technologies cards inputing using form "Steps" and methods of class tbTechnologyController
 */

using FormEF_test;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MajPAbGr_project
{
    public class TechnologyCardsController
    {
		int id_card;
		List<Item> cards;
		tbCardsController tb;
		ChainsController chains;
		SubmitMode submitMode;


		public TechnologyCardsController(ChainsController cntrl)
        {
			chains = cntrl;
			tb = cntrl.tbCardsController;
			id_card = tb.Selected;
			cards = tb.getCatalog();
        }


        /*
		 * Properties
		 */

        public SubmitMode Mode
        {
            get { return submitMode; }
            set { submitMode = value; }
        }

        public List<Item> Cards
        {
            get { return cards; }
        }

		public ChainsController Chains
        {
			get { return chains;  }
        }


		/*
		 * Methods
		 */

		//data base controller: setting, access
		public tbCardsController getTbController()
		{
			return tb;
		}

		public void setTbController()
		{
			tb.setCatalogs(tb.readTechnologiesCards());
		}

        //others

        public bool NotUnique(string name)
        {
            return tb.TechnologyCards.Exists(p => p.Name == name);
        }

        private void ChangeModelAfterInserting(int rowid, string name, string description, string technology)
        {
			Card inserted = new Card(rowid, name, description, technology);
			tb.TechnologyCards.Add(inserted);
			int card = tb.TechnologyCards.FindIndex(p => p == inserted);
			tb.setCurrent(card);
			tb.getCatalog().Add(new Item() { id = rowid, name = name });
			tb.Selected = rowid;
		}

        public string InsertNew(string name, string description, string technology, int techn_id)
        {
            int rowid;
            string count;
            if (NotUnique(name))
            {
                return $"Data base has {name} technologies`cards with this name.";
            }
            else
            {
				count = tb.insertCards(name, description, technology);				
                rowid = int.Parse(count);
                if (rowid > 0)
                    tb.Selected = rowid;               

                ChangeModelAfterInserting(rowid, name, description, technology);
               
                return $"Technology {name} (id {tb.Selected}) is inserted";
            }
        }


        public string Submit(string name, string description, string technology, int card_id)
        {
			int ind = 0, num, id = card_id; ;
			string count = "", query, report = "";

			if (card_id == 0) // insert
			{
                if (!string.IsNullOrEmpty(description))
                    query = tb.insertCardsQuery(name, description, technology);
                else
                    query = tb.insertCardsQuery(name, technology);
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
