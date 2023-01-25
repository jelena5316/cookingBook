using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace MajPAbGr_project
{
    public class FormMainController
    {
        int category, technology, selected; // выбранной из списка рецепторe, номера записей в базе данных
        double coefficient; // выбранного из списка рецепта либо вычисленный
        ReceptureStruct info;
        private List<Item> receptures, subcatalog;
        private List<Element> recipes; // рецепты
        private List<Element> elements; // сырье
        tbReceptureController tb; // контроллер таблицы Рецептуры
        CalcFunction calc; // Вычисления

        CultureInfo current;
        NumberFormatInfo nfi; // локализация, разделитель целых и долей("," или ".")
        string decimal_separator /*разделитель */;
        

        public FormMainController(int id)
        {
            tb = new tbReceptureController("Recepture");
            calc = new CalcFunction();
            tb.setCatalog();
            receptures = tb.getCatalog();
            tb.Selected = id;
            selected = id;
            subcatalog = tb.setSubCatalog("Recipe", "id_recepture"); // table Recipe, id_recepture
            recipes = tb.readElement(2); //читает полностью, все три поля
            elements = tb.readElement(1); //читает полностью, все три поля     

            setNFI("ru_RU");            
        }

        public tbReceptureController TbMain() => this.tb;

        public List<Item> getCatalog() => this.receptures;  

        public List<Element> Recipes
        {
            get { return recipes; }
        }

        public List<Element> Amounts
        {
            get 
            { 
                elements = tb.readElement(1);
                return elements;
            } 
        }

        public void setSubcatalog(int index)
        {
            selected = tb.setSelected(index);
            subcatalog = tb.setSubCatalog("Recipe", "id_recepture"); // table Recipe, id_recepture
            recipes = tb.readElement(2);
        }

        public List<Item> SubCatalog
        {
            get { return subcatalog; }
        }

        public int Selected
        {
            get { return selected; }
        }

        public CalcFunction Calc
        {
            get { return calc; }
        }


        /*
         * Локализация
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


        public double[] button1_onClick(string text, string indicator)
        {
            double summa, amount;
            double[] amounts = calc.getAmounts();
            string indikator = indicator;
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
                    return calc.getAmounts();
                }
            }

            // вынести в CalcFunction.cs (?) в FormMainController
            switch (indikator)
            {
                case "total":
                    summa = calc.Summa();
                    coefficient = amount / summa;
                    //calc.Coefficient =  calc.calculateCoefficient(amount, calc.Summa());
                    break;

                case "main":
                    coefficient = amount / amounts[0];
                    //calc.Coefficient = calc.calculateCoefficient(amount, amounts[0]);

                    break;

                case "coefficient":
                    coefficient = amount;
                    //calc.Coefficient = amount;
                    break;
                default: coefficient = amount / amounts[0]; ; break;
            }
            calc.Coefficient = coefficient;           
            return calc.ReCalc();
        }
    }

}
