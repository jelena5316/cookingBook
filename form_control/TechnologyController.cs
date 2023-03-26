using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class TechnologyController
    {
        int id_technology, id_recepture, id, selected_tech/*, selected_rec*/;
        List<Item> technologies, receptures;
        tbTechnologyController tb;
        tbController tbRec;

		/*		 
		 * id_technology -- переданный номер, выставляет с поле имя из списка в поле технологий;  номер исправленной технологии (теперь и новой);
		 * selected_tech -- номер выбранной из списка в поле, равен c tb.Selected; обнуляется при их очистке; по нему удаляем  и правим;
		 * id - возможно, больше не нужен, (уже не) используется в  OutTechnology(), с прежних конструкторов на случай,
		 * если не технология не передавалась;
		 * submit new, setStatusLabel3, comboBox2_SelectedIndexChanged;
		 * id_recepture - используется в fillCatalogRec();
		 * selected_rec (убрала) - только для вывода в статусную полоску, определяется только при очистке полей редактора.
		 */

		public TechnologyController (int technology)
		{
			tb = new tbTechnologyController("Technology");
			tbRec = new tbController("Recepture");
			id_recepture = 0;

			if (technology < 0) technology = 0;
			id_technology = technology;
			tb.setCatalog();
			technologies = tb.getCatalog();

			selected_tech = technology;
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
			string query = $"select name, description from Technology where id ={selected};"; //id
			string technology = "";
			technology = tb.dbReadTechnology(query)[0];
			string[] arr = null;
			arr = technology.Split('*');
			return arr;
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

		public string SeeRecepturesCategory(int index)
        {
			int selected;
			string category = "category: ", query;
			if (receptures.Count > 0)
			{
				tbRec.Selected = receptures[index].id;	
				selected = receptures[index].id;							
				query = $"select name from Categories where id = " +
					$"(select id_category from Recepture where id = {selected});";
				category += tbRec.dbReader(query)[0];
				
			}
			return category;
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

		public string IsUsed()
		{			
			string used = tb.getUsed();
			if (used == "0")
				return "";
			else
				return $"The technology is used in {used} Receptures.\nPlease, remove it before deleting";
        }

		public bool Remove(int index, out int new_index)
        {
			int ind = 0, count = tb.getCatalog().Count;
			ind = tb.RemoveItem();
			if (ind > 0)
			{
				if (index == count - 1) index--;
				if (index == 0) index++;				
				new_index = index;
				return true;
			}
            else
            {
				new_index = index;
				return false;
            }
				
        }

		public string[] getFullInfo()
		{
			List<string> list = new List<string>();			

			list.Add(tb.Selected.ToString());
			list.AddRange(OutTechnology(tb.Selected));

			List<string> chain;
			tbTechnologyCardsController tbCards;

			//выведет название рецепта
			if (tbRec.Selected > 0 || receptures.Count > 0)
            {
                list.Add("Receptures:");
                int id_rec = tbRec.Selected;
                //list.Add( $"Technology is used in {receptures.Count.ToString()} receptures:");               
                for (int k = 0; k < receptures.Count; k++)
                {
                    string category = SeeRecepturesCategory(k);
                    string rec = receptures[k].name;
                    list.Add($"{k + 1}. name: {rec}, id {tbRec.Selected}, {category}");
                }
                tbRec.Selected = id_rec;
            }
            else
            {
               list.Add("no recepture");
            }
            list.Add("Cards:");

			//выведет на печать технологию с цепочкой карт
			tbCards = new tbTechnologyCardsController("Technology_card");
			chain = tbCards.SeeOtherCards(tb.Selected);
			list.AddRange(chain);

			//if (tb.Selected > 0)
   //         {
   //             tbCards = new tbTechnologyCardsController("Technology_card");
   //             chain = tbCards.SeeOtherCards(tb.Selected);
   //             list.AddRange(chain);               
   //         }
            return list.ToArray();			
		}
	}
}
