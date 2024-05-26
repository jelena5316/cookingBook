using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project.Component
{
	public class FormCreator
	{
		RecCatalog rec_catalog;
		
		public FormCreator() {}

		public FormCreator(RecCatalog catalog)
		{
			this.rec_catalog = catalog;
		}
		bool CheckSelected() => true;

		
		//methods to open different forms
		Recipes recipes(int id) => new Recipes(id);

		public Recipes recipes (RecCatalog rec_catalog)
        {
			int id = rec_catalog.ReceptureStruct[rec_catalog.SelectedRecIndex].getId();			
			Recipes frm = new Recipes(id);
			frm.Controller.RecCatalog = rec_catalog;
			return frm;
        }

		public Recipes recipes(CategoriesController controller)
		{
			int id = controller.Catalog.ReceptureStruct[controller.Catalog.SelectedRecIndex].getId();
			Recipes frm = new Recipes(id);
			frm.Controller.CategoriesController = controller;
			frm.Controller.RecCatalog = controller.Catalog;
			return frm;
		}

		Recipes recipes() => new Recipes();// will be colled from Program

		public Categories categories() => new Categories();

		Categories categories(CategoriesController cntrl) => new Categories(cntrl); // for any case

		Technology technology(int technology) => new Technology(technology);

		Ingredients simpletable(int opt) => new Ingredients(new tbIngredientsController(opt));

		InsertAmounts amounts(tbReceptureController tb, ReceptureStruct rec)
		{
			AmountsController cntrl = new AmountsController(tb);
			cntrl.Info = rec;
			return new InsertAmounts(cntrl);
		}
	   
		NewRecepture recepture(tbReceptureController tb)
		{
			NewReceptureController rec = new NewReceptureController();			
			rec.ReceptureInfo = new ReceptureStruct(0);
			return new NewRecepture(tb, rec);
		}

		NewRecepture recepture(tbReceptureController tb, ReceptureStruct rec)
		{
			NewReceptureController cntrl = new NewReceptureController(tb);
			cntrl.ReceptureInfo = rec;
			return new NewRecepture(cntrl);
		}	
	}

	public enum Forms
	{
		RECIPES,
		TECHNOLOGY,
		INGREDIENTS,
		AMOUNTS,
		ADDNEW,
		EDITREC
	}
}
