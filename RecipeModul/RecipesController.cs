/*
 * to manage work with recipes using form "Recepture" and classes` CalcFunction and tbRecipeController methods
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using MajPAbGr_project.Component;

namespace MajPAbGr_project
{
	public class RecipesController
	{
		int selected;        
		private List<Item> receptures, subcatalog;
		private List<Element> recipes; // recipes (coefficients)
		private List<Element> elements; // ingredients
		tbReceptureController tb; // formulation
		tbRecipeController tbCoeff; // recipes (coefficients)

		RecCatalog rec_catalog; // list of RecepturesStruct items, contains data about receptures
		CategoriesController categories; //list of categories (id, name) contained in 'tbCat.catalog', for selecting by category; 

		// calculations
		CalcBase calcBase = 0;
		CalcFunction calc; 

		CultureInfo current;
		NumberFormatInfo nfi; // number format
		string decimal_separator /* ("," or ".") */;
		

		public RecipesController(int id)
		{
			tb = new tbReceptureController("Recepture");
			tbCoeff = new tbRecipeController("Recipe");
			calc = new CalcFunction();
			tb.setCatalog();
			receptures = tb.getCatalog();
			tb.Selected = id;
			tbCoeff.Recepture = id;
			selected = id;
			setNFI("us-US");            
		}

		public tbReceptureController TbMain() => this.tb;

		public tbRecipeController TbCoeff() => this.tbCoeff;

		public List<Item> getCatalog() => this.receptures;

		public List<Element> Recipes => recipes;       

		public List<Element> Amounts
		{
			get { return elements; }
		}

		public RecCatalog RecCatalog
        {
			set { rec_catalog = value; }
        }

		public CategoriesController CategoriesController
        {
			get { return categories; }
			set { categories = value; }
        }

		public string AboutRecepture(int index)
		{
			int category, cat_index; // id, index
			string info;
			
			//get info about category of selected recepture to ouput into combobox
			category = int.Parse(tb.getById("id_category", selected)); 

			//cat_index = FormFunction.ChangeIndex(categories.TbCat.getCatalog(), category);
			//categories.TbCat.Selected = category;

			//build string with other info
			info = $"{tb.getName(index)}: id {tb.getSelected()}, category ({category})\n";

			//if (recipes.Count > 0)
			//{
			//	info += $"recipes \n";
			//	for (int k = 0; k < recipes.Count; k++)
			//	{
			//		info += $"  {recipes[k].Name} ({recipes[k].Amounts}) \n";
			//	}
			//}
			return info;
		}

		public int cmbCatIndex(int index)
        {
			int id, cat_index;

			id = rec_catalog.ReceptureStruct[index].getIds()[0];
			categories.TbCat.Selected = id;
			cat_index = FormFunction.ChangeIndex(categories.TbCat.getCatalog(), id);
			return cat_index;
		}

		public List<string> changeSubcatalog(int index)
		{
			//Coefficients
			selected = getCatalog()[index].id;
			tbCoeff.Recepture = getCatalog()[index].id;            
			subcatalog = tbCoeff.setSubCatalog();
			recipes = tbCoeff.readElement(2);
			
			//Amounts
			tb.Selected = selected;
			elements = tb.readElement(1);			
			calc.setAmounts(elements);

			return calc.FormatAmounts
				(calc.getAmounts(), calc.getTotal());
		}

		public int Selected
		{
			set { selected = value; }
			get { return selected; }
		}

		public void setRecepture(int index)
		{
			tbCoeff.Recepture = receptures[index].id;
		}

		public CalcFunction Calc
		{
			get { return calc; }
		}

		public CalcBase CalcBase
		{
			set
			{
				calcBase = value;
				calc.calcBase = value;
			}
		}

		/*
		 * Lokalization
		 */
		public void setNFI(string lokation)
		{
			current = new CultureInfo(lokation);
			CultureInfo.CurrentCulture = current;
			nfi = current.NumberFormat;
			decimal_separator = nfi.NumberDecimalSeparator;
		}

		public CultureInfo Current() => this.current;      

		public string InfoLocal()   => current.Name + " (\'" + decimal_separator + "\')";

		public string CurrentName()
		{
			string name;
			name = current.TwoLetterISOLanguageName;
			name = name.ToUpper();
			return name;
		}

		/*
		 * buttons click handlers
		 */
		public List <string> button2_onClick(string text)
		{
			double amount;
			double[] amounts = calc.getAmounts();

			if (amounts.Length < 1) 
				return null;
			   
			int indikator = (int)calcBase;           

			if (nfi.NumberDecimalSeparator == ".")               
				text = calc.ColonToPoint(text);

			if (nfi.NumberDecimalSeparator == ",")
				 text = calc.PointToColon(text);

			if (double.TryParse(text, out amount))
			{
				amount = double.Parse(text);//us_Us: from '0,x' get a 'x'
			}
			else
			{
				amount = 1;
				return null;
			}

			calc.setNewRecipesCoefficient(amount);
			amounts = calc.ReCalc();
			List<string> list = calc.FormatAmounts(amounts, calc.Summa(amounts));
			return list;
		}
	
		public int btn_edit_onClick(string name, int index)
		{
			int ind, id = 1;
			id = subcatalog[index].id;            
			try
			{
				ind = tbCoeff.UpdateReceptureOrCards("name", name, id);
			}
			catch
			{
				ind = -1;
			}                        
			return ind;
  
		}

		public int btn_remove_onClick(int recipe, int recepture) // remove recipe from DB
		{
			// comboboxes index changed handlers
			int id = 0, ind = 0, index;
			index = recipe;
			tbCoeff.setSubCatalog();
			tbCoeff.setSelected(index);
			id = tbCoeff.Selected;
			index = recepture;
			setRecepture(index);

			// deleting
			ind = tbCoeff.RemoveItem();
			tbCoeff.Selected = id;
			return ind;
		}

		public int cmbCat_onSelectedIndexChanged(int category) // index
        {
			rec_catalog.SelectedCatIndex = category;
			return 0;
        }

		public List <Item> SelectByCategory(int index)
        {
			List<ReceptureStruct> rec = rec_catalog.selectByCategory(index);
			List<Item> selected = new List<Item>();
			int k = 0;
			for ( k = 0; k < rec.Count; k++)
            {
				Item item = new Item();
				item.createItem(rec[k].getId(), rec[k].getName());
				selected.Add(item);
            }
			categories.TbCat.Subcatalog = selected;
			return selected;
        }

		public int ImpoveIndex (int index)
        {
			int new_index;
			if (rec_catalog.Full == null)
				return index;

			//rec_catalog.Full = rec_catalog.ReceptureStruct; // is already done
			rec_catalog.SelectRecepture(categories.TbMain, index, "");
			new_index = rec_catalog.SelectedRecIndex;
			//rec_catalog.Full = null; // will be done in other place
            return new_index;
        }

		public int UnDoSelectingByCategory()
        {
			rec_catalog.Full = null;
			rec_catalog.SelectedRec.Clear();
			rec_catalog.SelectedRec = null;
			categories.TbMain.Subcatalog.Clear();			
			return 0;
        }

		public bool AboutRecepture()
        {
			int index;
			ReceptureStruct rec;
			FormCreator creator;
			NewRecepture frm;

			if (!rec_catalog.ExistsSelected)
				return false;

            index = rec_catalog.SelectedRecIndex;
            rec = rec_catalog.ReceptureStruct[index];
			tb.Selected = rec.getId();
			tb.Id = rec.getId(); // зачем мне "Id"?

            creator = new FormCreator();
            frm = creator.recepture(tb, rec);
            frm.ShowDialog();
            return true;


            /* FormCreator.recepture() */
            //	NewReceptureController cntrl = new NewReceptureController(tb);
            //	cntrl.ReceptureInfo = rec;
            //	return new NewRecepture(cntrl);

            /*CategoriesController.editRec()*/
            //if (!ExistsSelected)
            //	return false;
            //int id = CheckTbSelected(getMinIdOfReceptures());
            //if (id == 0)
            //	return false;

            //id = ReceptureStruct[SelectedRecepture].getId();

            //if (tb.Selected != id)
            //{
            //	tb.Selected = id;
            //}

            ////проверить, есть ли запись с таким номером
            //tb.Id = id;
            //NewReceptureController rec = new NewReceptureController(tb);
            //rec.ReceptureInfo = ReceptureStruct[SelectedRecepture];
            //NewRecepture frm = new NewRecepture(rec);
            //frm.ShowDialog();
            //return true;
        }
    }

}
