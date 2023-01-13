using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace MajPAbGr_project
{

    public struct ReceptureStruct
    {
        int id;
        string name, category, source, author, technology, ingredient;
        //Item recepture, cat, techn, ingred;
        //Item?!

        public ReceptureStruct(int id)
        {
            this.id = id;
            name = "unknown";
            category = "unknown";
            source = "unknown";
            author = "unknown";
            technology = "unknown";
            ingredient = "unknown";
        }

        public void setFields()
        {
            string[] //it need enum!
                tables = {
                "Recepture", // 0
                "Categories", // 1
                "Technology", // 2
                "Ingredients" // 3
            },
                column_names = {
                "name", // 0
                "id_category", // 1
                "id_technology", // 2
                "id_main", // 3
                "source", // 4
                "author" // 5
            };

            int id = this.id;
            dbController db = new dbController();

            string SubQuery(int column)
                => $"select {column_names[column]} from {tables[0]} where id = {id}";          

            string Query(int table, string subquery)
                => $"select {column_names[0]} from {tables[table]} where id = ({subquery});";           

            name = db.dbReader(SubQuery(0))[0];          
            category = db.dbReader(Query(1, SubQuery(1)))[0];               
            source = db.dbReader(SubQuery(4))[0];                  
            author = db.dbReader(SubQuery(5))[0];      
            technology = db.dbReader(Query(2, SubQuery(2)))[0];           
            ingredient = db.dbReader(Query(3, SubQuery(3)))[0];            
        }

        public void setFields
            (string name, string category, string source,
            string author, string technology, string ingredient )
        {
            this.name = name;
            this.category = category;
            this.source = source;
            this.author = author;
            this.technology = technology;
            this.ingredient = ingredient;
        }

        public void setFields (string [] fields)
        {
            this.name = fields[0];
            this.category = fields[1];
            this.source = fields[2];
            this.author = fields[3];
            this.technology = fields[4];
            this.ingredient = fields[5];
        }

        public string [] getFields()
        {
            string[] arr = new string[] { name, category, source, author, technology, ingredient };
            return arr;
        }

        public int getId()
        {
            return id;
        }

        public string getName()
        {
            return name;
        }

        public string getCategory()
        {
            return category;
        }
    }

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
