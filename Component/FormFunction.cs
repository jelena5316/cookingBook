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
    }
}
