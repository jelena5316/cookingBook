﻿/*
 * to manage work with recipes using form "Recepture" and classes` CalcFunction and tbRecipeController methods
 */

using System;
using System.Collections.Generic;
using System.Globalization;

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

		public string AboutRecepture(int index)
		{
			int category;
			string info;
			
			category = int.Parse(tb.getById("id_category", selected));
			info = $"{tb.getName(index)}: id {tb.getSelected()}, category ({category})\n";

			if (recipes.Count > 0)
			{
				info += $"recipes \n";
				for (int k = 0; k < recipes.Count; k++)
				{
					info += $"  {recipes[k].Name} ({recipes[k].Amounts}) \n";
				}
			}
			return info;
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
	}

}
