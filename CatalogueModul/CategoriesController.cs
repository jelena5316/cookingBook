﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MajPAbGr_project
{
    class CategoriesController
    {
        int category, recepture;
        List<int> receptures_id;
        List<Item> categories, receptures;
        List<ReceptureStruct> rec_struct;  
        
        
        tbReceptureController tb;
        tbIngredientsController tbCat;   
        TechnologyController tbTech;

        public CategoriesController()        
        {
            tbCat = new tbIngredientsController(2);
            tbCat.setCatalog();            
            categories = tbCat.getCatalog();
            category = 0;

            tb = new tbReceptureController("Recepture");
            tb.setCatalog();
            receptures = tb.getCatalog();
            
            receptures_id = new List<int>();
            for (int k = 0; k < receptures.Count; k++)
            {
                receptures_id.Add(receptures[k].id);
            }
            recepture = 0;
            
            tbTech = new TechnologyController(1);
            tbTech.Receptures = this.receptures;
            rec_struct = new List<ReceptureStruct>();
            setFields();
        }

        public List<ReceptureStruct> ReceptureStruct
        {
            get { return rec_struct; }
        }

        public List<Item> Categories
        {
            set { categories = value; }
            get { return categories; }
        }

        public List<Item> Receptures
        {
            get { return receptures; }
        }

        public tbReceptureController TbMain
        {
            get { return tb; }
        }

        public tbIngredientsController TbCat
        {
            get { return tbCat; }
        }

        public int getMinIdOfReceptures()
            => int.Parse(tb.dbReader("select min(id) from Recepture;")[0]);
        

        //public void SelectedByCategoryRecepture(int id)
        //{
        //    string query;
        //    query = $"select id, name from Recepture where id_category = {id};";            
        //    receptures = tb.Catalog(query);
        //}

        public void setReceptures()
        {           
            tb.setCatalog();
            receptures = tb.getCatalog();
        }

        public void setFields()
        {
            int id;            
            ReceptureStruct rec;
            for (int k = 0; k < receptures.Count; k++)
            {
                id = receptures[k].id;
                rec = new ReceptureStruct(id);
                rec.setData();               
                rec_struct.Add(rec);
            }
        }

        public void setListView(ListView lv)
        {
            ListViewItem items;
            for (int k = 0; k < receptures.Count; k++)
            {                
                items = new ListViewItem(receptures[k].name);
                items.Tag = receptures[k].id;                
                string[] arr = rec_struct[k].getData();

                for (int q = 1; q < 4; q++)
                {
                    items.SubItems.Add(arr[q]);
                }
                lv.Items.Add(items);               
            }
            lv.Items[0].Selected = true;
        }       
    
         public List<ReceptureStruct> selectByCategory(int index)
         {
            List<ReceptureStruct> selected;
            //string param, cat;
            //param = Categories[index].name;
            //bool signalka = false;
            //int count = 0, nocount = 0;

            //for (int k = 0; k < ReceptureStruct.Count; k++)
            //{

            //    cat = ReceptureStruct[k].getCategory();               
            //    //cat = "pirogi";
            //    if (param == " ") count++;
            //    if (param != " ") nocount--;
            //    if (cat == " ") count++;
            //    if (cat != " ") nocount--;
            //    if (param == cat) count++;
            //    if (param != cat) nocount--;
            //    if (cat == param) count++;
            //    if (cat != param) nocount--;
            //}

            selected = rec_struct.FindAll(p => p.getCategory() == categories[index].name);
            return selected;
         }


        /*
         * Print to file
         */


        public void Print(int index, int option)
        {
            string file = "file";
            string[] arr;
            List<string> output = new List<string>();           

            if (index < 0) option = 1;

            switch (option)
            {
                case 0:
                    file = rec_struct[index].EditorData[0];
                    PrintController print = new PrintController(file);
                    print.Info = PrintInfo(index).ToList();
                    print.Ingredients = new List<string>() {PrintRecepture()};
                    print.Technology = PrintTechnology(index).ToList();
                    print.Cards = PrintCards(index).ToList();
                    print.PrepareRecipeToOutput();
                    print.PrintRecipe();
                    break;
                case 1:
                    arr = new string[] { "Recipes count ", "Technologies count", "Technologies cards count " };
                    output.AddRange(arr);
                    file = "report";
                    break;
                default:
                    arr = new string[] { "Home e-cooking book is apps to store and manage recipes` and technologies` collections" };
                    output.AddRange(arr);
                    file = "about";
                    break;
            }
            //Print frm = new Print(output, file);
            //frm.Show();
        }


        public string[] PrintInfo(int index)
        {
           string[] data = rec_struct[index].getData();
           return new string[]
            {
                $"Name: {data[0]}\n",
                $"Category: {data[1]}\n",
                $"Source: {data[4]}\n",
                $"Author: {data[3]}\n",
                $"Technology (name): {data[5]}\n",
                $"Main_ingredient: {data[2]}\n",
                $"Description: {data[6]}"               
                // name, category, ingredient, author, source, technology, description
            };           
        }

        public string[] PrintTechnology(int index)
        {
            int id_technology;
            TechnologyController tehn;
            string[] arr;

            id_technology = ReceptureStruct[index].getIds()[1];


            if (id_technology < 0)
                return new string[] { "has no tehnology" };
            else
            {
                tehn = new TechnologyController(id_technology);
                tehn.setReceptures();
            }
            //arr = tehn.getFullInfo();
            arr = tehn.OutTechnology(id_technology);
            return arr;
        }

        public string[] PrintCards(int index)
        {
            int id_technology, k=0;       
            string[] arr;
            List<string> steps;
            tbTechnologyCardsController cards;

            id_technology = ReceptureStruct[index].getIds()[1];

            if (id_technology < 0)
                return new string[] { "has no cards" };
            else
            {
                cards = new tbTechnologyCardsController("Technology_card");
            }

            steps = cards.SeeOtherCardsFull(id_technology);
            arr = new string[steps.Count];
            for (k = 0; k < steps.Count(); k++)
            {
                arr[k] = $"{k + 1} {steps[k]}\n";
            }
            return arr;
        }

        public string PrintRecepture()
        {
            int k = 0;
            string rec, amount;
            List<Element> el;

            el = tb.readElement(1);
            rec = "";

            if (el.Count == 0) return "no formulation";
            if (el.Count == 1)
            {
                amount = string.Format("{0:f2}", el[k].Amounts);
                rec += $"{el[k].Name} {amount}.";
            }
            else
            {
                for (k = 0; k < el.Count - 1; k++)
                {
                    amount = string.Format("{0:f2}", el[k].Amounts);
                    rec += $"{el[k].Name} {amount}, ";
                }
                amount = string.Format("{0:f2}", el[k].Amounts);
                rec += $"{el[k].Name} {amount}.";
            }          
            return rec;
        }
    }   
}
