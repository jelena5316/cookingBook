using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace MajPAbGr_project
{
    public class RecipesController
    {
        int selected;        
        private List<Item> receptures, subcatalog;
        private List<Element> recipes; // recipes (coefficients)
        private List<Element> elements; // stuffs
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
            subcatalog = tb.setSubCatalog("Recipe", "id_recepture"); // table Recipe, id_recepture
            recipes = tb.readElement(2);
            elements = tb.readElement(1);

            setNFI("us_US");            
        }

        public tbReceptureController TbMain() => this.tb;

        public tbRecipeController TbCoeff() => this.tbCoeff;

        public List<Item> getCatalog() => this.receptures;  

        public List<Element> Recipes
        {
            get { return recipes; }
        }

        public List<Element> Amounts
        {
            get 
            {
                tb.Selected = selected;
                elements = tb.readElement(1);
                return elements;
            } 
        }

        public void setSubcatalog(int index)
        {       
            subcatalog = tbCoeff.setSubCatalog();
            recipes = tbCoeff.readElement(2);            
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
        public void setNFI(string lokalizacija)
        {
            current = new CultureInfo(lokalizacija);
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
        public List <string> button1_onClick(string text)
        {
            double amount;
            double[] amounts = calc.getAmounts();

            if (amounts.Length < 1) 
                return null;
               
            int indikator = (int)calcBase;
            string temp, t;

            //if (nfi.NumberDecimalSeparator == ".")
            //    txb_coeff.Text = calc.ColonToPoint(txb_coeff.Text);
            // или
            if (current.Name == "us-US")
                text = calc.ColonToPoint(text);
            // потому что при английской локализации (".")
            // число с запятой парсируется, но не верно;

            if (double.TryParse(text, out amount))
            {
                amount = double.Parse(text);//us_Us: from '0,x' get a 'x'
            }
            else // при латышской или русской локализации
            {
                //MessageBox.Show("String was not in correct format");
                //point to colon -- улавливает ошибку неверного формата строки и исрпавляет её
                t = text;
                temp = "";
                if (t.Contains('.'))
                {
                    int k;
                    for (k = 0; k < t.Length; k++)
                    {
                        if (t[k] != '.')
                            temp += t[k];
                        else
                            temp += ',';
                    }
                    t = temp;
                    text = temp;
                }

                if (double.TryParse(t, out amount))
                {
                    amount = double.Parse(t);
                }
                else
                {
                    amount = 1;
                    text = "not number";
                    return null;
                }
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
            tbRecipeController tbCoef = new tbRecipeController("Recipe");
            try
            {
                ind = tbCoef.UpdateReceptureOrCards("name", name, id);
            }
            catch
            {
                ind = -1;
            }                        
            return ind;
        }

        public int btn_remove_onClick(int recipe, int recepture)
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
            ind = tbCoeff.RemoveItem(); // replace 'override'!
            tbCoeff.Selected = id;
            return ind;
        }
    }

}
