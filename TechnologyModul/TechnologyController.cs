/*
 * to manage inputing technologies head using form "Technology"
 * and methods of classes tbTechnologyController and tbController
 */

using System;
using System.Collections.Generic;

namespace MajPAbGr_project
{
    public class TechnologyController
    {
		int id_technology, //tooken id, will be changed updating and creating
			id;			
		List<Item> receptures;
        tbTechnologyController tb;
        tbController tbRec;
		tbCardsController tbCard;
		tbChainController tbChain;


		public TechnologyController (int technology)
		{
			tb = new tbTechnologyController("Technology");
			tbRec = new tbController("Recepture");			

			if (technology < 0)
				technology = 0;
			id_technology = technology;
			id = technology;
			//tb.setCatalogs(tb.readTechnologies());
			//tb.Selected = technology;			  
			
		}
		

		/*
		 *  Properties
		 */
		public tbTechnologyController getTbController()
        {
			return tb;
        }

		public void setTbController(int index)
		{
            tb.setCatalogs(tb.readTechnologies());
			tb.setSelected(index);
			tb.setCurrent(index);
			if (tb.Selected != tb.Current.Id || tb.Selected != id)
			{
				tb.Selected = tb.Current.Id;
			}
			tb.Subcatalog = tb.CardsInTechnologyAsSubcatalog();
        }

		public tbController getTbRecController()
		{
			return tbRec;
		}

		

        public List<Item> Receptures
        {
            set { receptures = value; }
            get { return receptures; }
        }

		


		/*
		 * Methods
		 */
        public List<Item> Cards(int selected, out int count)
		{
			count = tb.CardsInTechnologyCount();

			if (count > 0)
			{
				return tb.CardsInTechnologyAsSubcatalog();
			}
				
			else
				return null;
        }

		public List<Item> setReceptures()
        {
			receptures = tb.RecepturesOfTechnology;				
			return receptures;
        }

		public bool NotUnique(string name)
		{
			return tb.Technologies.Exists(p => p.Name == name);
		}


		public string Submit(string name, string description, int techn_id)
        {
            int ind = 0, num, id = techn_id;
            string count = "", query, report = "";

            if (techn_id == 0) // insert
            {
				query = tb.insertTechnology(name, description);
				count = tb.Count(query);
				num = int.Parse(count);
				if (num > 0)
					tb.Selected = num;
				techn_id = num;
			}
            else
            {
				ind = tb.UpdateReceptureOrCards("name", name, techn_id);
				ind += tb.UpdateReceptureOrCards("description", description, techn_id);
			}

            tb.setCatalog();
            report = techn_id == id ? "not inserted or updated" : "inserted";
            report = ind > 0 ? "updated" : report;
			return $"Technology {name} (id {tb.Selected}) is {report}";
        }

		public bool Remove(int index, out int new_index)
        {
			int ind = 0, count = tb.getCatalog().Count;
			ind = tb.RemoveItem();
			if (ind > 0)
			{
				if (index == count - 1) index--;
				if (index == 0) index++; // ?!				
				new_index = index;
				return true;
			}
            else
            {
				new_index = index;
				return false;
            }
				
        }

		public string[] OutTechnology()
		{
			return tb.AboutCurrent;
		}
	}
}
