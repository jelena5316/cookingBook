using System;
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
