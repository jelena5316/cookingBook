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
        tbClass1 tbRec;

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
			tbRec = new tbClass1("Recepture");
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

		public tbClass1 getRecTbController()
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

	}
}
