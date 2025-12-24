/*
 * to manage inputing technologies head using form "Technology"
 * and methods of classes tbTechnologyController and tbController
 */

using FormEF_test;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MajPAbGr_project
{
    public enum SubmitMode
	{
		INSERT = 0,
		UPDATE = 1,
		DELETE = 2
	}
	
	public class TechnologyController
    {	
		int id_technology, //tooken id, will be changed updating and creating
			id;
		SubmitMode submitMode;
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

		public SubmitMode Mode
		{
			get { return  submitMode; }
			set { submitMode = value; }
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

		// CRUD
		public bool NotUnique(string name)
		{
			return tb.Technologies.Exists(p => p.Name == name);
		}


		private void ChangeModelAfterInserting(int rowid, string name, string description)
		{
            Technology inserted = new Technology(rowid, name, description);
            tb.Technologies.Add(inserted);
            int tech = tb.Technologies.FindIndex(p => p == inserted);
            tb.setCurrent(tech);
            tb.getCatalog().Add(new Item() { id = rowid, name = name });
            tb.Selected = rowid;
        }
		

        public string InsertNew(string name, string description, int techn_id)
		{
			int rowid;
			string count;
			if (NotUnique(name))
            {
                return $"Data base has {name} technologies with this name.";
            }
            else
            {
                count = tb.insertTechnology(name, description);	
                rowid = int.Parse(count);
                if (rowid > 0)
                    tb.Selected = rowid;
				//techn_id = rowid;

				ChangeModelAfterInserting(rowid, name, description);

                //tb.setCatalog();
                return $"Technology {name} (id {tb.Selected}) is inserted";
            }
        }

		private void ChangeModelAfterUpdating(string name, string description, int index)
		{
			Item selected = tb.getCatalog()[index];
			Technology current = tb.Technologies[index];
			current.Name = name;
			current.Note = description;
			selected.name = name;

			tb.getCatalog()[index] = selected;
			tb.Technologies[index] = current;
		}

		public string UpdateExisted(string name, string description, int index)
		{
			int ind;
			int techn_id = tb.setSelected(index);
			

			if (techn_id > 0)
            {
                ind = tb.UpdateReceptureOrCards("name", name, techn_id);
                ind += tb.UpdateReceptureOrCards("description", description, techn_id);

				if (ind == 2)
				{
					ChangeModelAfterUpdating(name, description, index);
                }
				
                //tb.setCatalog();
                return $"Technology {name} (id {tb.Selected}) is updated";
            }
            else
            {
				return $"Data base has no technologies with this id: id = {techn_id}";
            }
        }

		public string Submit(string name, string description, int techn_id)
        {
            string report = "";

			switch ((int)submitMode)
			{
				case 0:
					report = InsertNew(name, description, techn_id);
					break;
				case 1:
					report = UpdateExisted(name, description, techn_id);
                    break;				
				default:
					report = "Something goes wrong";
					break;
            }
			return report;

			//         tb.setCatalog();
			//         report = techn_id == id ? "not inserted or updated" : "inserted";
			//         report = ind > 0 ? "updated" : report;
			//return $"Technology {name} (id {tb.Selected}) is {report}";
        }


		private int ChangeModelAfterDeleting(int index) // removes Technology and Item, return new index
		{
            int count = tb.getCatalog().Count;

			if (count > index)
			{
				Technology deleted = tb.Technologies.First(p => p.Id == tb.Selected);
				tb.Technologies.Remove(deleted);
				Item item = tb.getCatalog().First(p => p.id == tb.Selected);
				tb.getCatalog().Remove(item);
				if (index > 0)
					index--;
				if (index < 0)
					index = 0;
				return index;
			}
			else
				return -2;
			

			/* From AmountModel
            if (index < 0) return -1;
            if (elements.Count > index)// 1 > 0
            {
                elements.RemoveAt(index);
                if (index > 0) index--;
                if (index < 0) index = 0;
                return index;
            }
            else
                return -2;
			*/
        }

		public bool Remove(int index, out int new_index)
        {
			int ind = 0;

			if (tb.getUsed() != "0")
			{
				new_index = index;
				return false;
			}

			ind = tb.RemoveItem();
			if (ind > 0)
			{		
				new_index = ChangeModelAfterDeleting(index);
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
