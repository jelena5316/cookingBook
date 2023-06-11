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

		public TechnologyController (int technology)
		{
			tb = new tbTechnologyController("Technology");
			tbRec = new tbController("Recepture");			

			if (technology < 0)
				technology = 0;
			id_technology = technology;
			tb.setCatalog();
			tb.Selected = technology;
			id = technology;
		}
		
		public tbTechnologyController getTbController()
        {
			return tb;
        }

		public tbController getRecTbController()
		{
			return tbRec;
		}
		
		public string [] OutTechnology(int selected) //into textbox
		{
			string query, technology = "";
			string[] arr = null;

			query = $"select name, description from Technology where id = {selected};"; //id			
			technology = tb.dbReadTechnology(query)[0];			
			arr = technology.Split('*');
			return arr;
		}

		public List<string> Names(List <string> cards_id)
        {
			int k;
			string range = "";
			for (k = 0; k < cards_id.Count - 1; k++)
				range += $"{cards_id[k]}, ";
			range += cards_id[k];		
			return tb.dbReader($"select name from Technology_card where id in ({range})");
		}

		public List <string> Cards (int selected, out int count)
        {
			tbChainController chains;
			chains = new tbChainController("Technology_chain");
			count = chains.CardsInTechnologyCount(selected);
			if (count > 0)
				return chains.CardsInTechnology(selected);
			else
				return null;
		}

		public List<Item> setReceptures()
        {
			receptures = tb.setSubCatalog("Recepture", "id_technology");
			return receptures;
        }

		public List <Item> Receptures
        {
			set { receptures = value; }
			get { return receptures; }
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
	}
}
