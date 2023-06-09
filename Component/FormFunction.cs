﻿/*
 * to control lists view, combobox and 
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MajPAbGr_project
{
    public class FormFunction
    {
        static public int ChangeIndex(List<Item> items, int test)
        {
            int index = -1, temp_id = test;
            if (items.Count != 0)
            {
                
                if (temp_id > 0)
                {
                    for (index = 0; index < items.Count; index++)
                    {
                        if (items[index].id == temp_id)   
                        {                    
                            break;
                        }
                    }
                }
                return index;
            }
            else return index;          
        }
        
        static public List <string> setBox (List <Item> items, ComboBox combo)
        {
            List<string> list = new List<string>();
            if (items.Count != 0)
            {
                if (combo.Items.Count > 0)
                    combo.Items.Clear();
                for (int index = 0; index < items.Count; index++)
                {
                    string t = items[index].name;
                    combo.Items.Add(t);
                    list.Add(t);
                }
                combo.Text = combo.Items[0].ToString();
            }
            else
            {
                combo.Text = "empty";
                list.Add("empty");
            }
            combo.Focus();            
            return list;
        }

        static public List<Item> FillCombo(List<Item> items, ComboBox combo)
        {
            if (items.Count != 0)
            {
                if (combo.Items.Count > 0)
                    combo.Items.Clear();
                for (int index = 0; index < items.Count; index++)
                {
                    combo.Items.Add(items[index].name);
                }
                combo.Text = combo.Items[0].ToString();
            }
            else combo.Text = "empty";
            combo.Focus();
            return items;
        }

        static public void FillCombo(List<Element> elements, ComboBox combo)
        {
            if (combo.Items.Count > 0) combo.Items.Clear();
            if (elements.Count > 0)
            {
                for (int k = 0; k < elements.Count; k++)
                {
                    combo.Items.Add(elements[k].Name);
                }
            }
        }
 
        static public void FillListView(List <Element> ingr, List<string> texts, ListView list)
        {
            /*output in list view recipe: ingredients name with amounts*/
            int k = 0;           
            ListViewItem items;
            list.Items.Clear();
            for(k = 0; k <ingr.Count; k++)
            {
                items = new ListViewItem(ingr[k].Name);
                items.Tag = ingr[k].Id;
                items.SubItems.Add(texts[k]);
                items.SubItems.Add(""); // for recipes
                list.Items.Add(items);
            }
            /*sum: calculate and output */
            items = new ListViewItem("Total");
            items.Tag = -1;
            items.SubItems.Add(texts[k]);
            items.SubItems.Add(""); // for recipes
            list.Items.Add(items);
        }
    }
}
