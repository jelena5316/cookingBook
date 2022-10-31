﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MajPAbGr_project
{
    public class Class1
    {
        static public List <Item> FillCombo(List <Item> items, ref ComboBox combo)
        {
            // в Форме 1:
            //List<Item> catalog = tb.getCatalog();
            //Class1.FillCombo(catalog, ref combo);

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

        static public List<string> FormatAmounts(List<Element> ingr, double summa)
        {
           List<string> texts = new List<string>();
            //форматировать числа при переводе в строку
            int k;
            string t = "";
            for (k = 0; k < ingr.Count; k++)
            {
                t = string.Format("{0:f1}", ingr[k].Amounts);
                texts.Add(t);
                t = "";
            }
            t = string.Format("{0:f1}", summa);
            texts.Add(t);
            return texts;
        }

       

    static public void FillListView(List <Element> ingr, List<string> texts, ref ListView list)
        {
            /*запись в списочное представления*/
            int k = 0;           
            ListViewItem items;
            list.Items.Clear();
            for(k = 0; k <ingr.Count; k++)
            {
                items = new ListViewItem(ingr[k].Name);
                items.Tag = ingr[k].Id;
                items.SubItems.Add(texts[k]);
                list.Items.Add(items);
            }
            /*сумма: счет и вывод */
            items = new ListViewItem("Total");
            items.Tag = -1;
            items.SubItems.Add(texts[k]);
            list.Items.Add(items);

            //return texts;
        }

        

        static public double[] AmountsFromListToArray(ref ListView lv, byte num)
        {
            double[] arr = new double[lv.Items.Count];
            string t;
            double arr_item;
            for (int index = 0; index < arr.Length; index++)
            {
                t = lv.Items[index].SubItems[num].Text;
                if (double.TryParse(t, out arr_item))
                    arr[index] = arr_item;
                else
                    arr[index] = 0.0;
            }
            return arr;
        }
    }
}
