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
        //Item recepture /* id, name*/, cat, techn, ingred;
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
        int category, technology; // выбранной из списка рецепторы, номера записей в базе данных
        double coefficient; // выбранного из списка рецепта либо вычисленный
        ReceptureStruct info;
        private List<Item> receptures;
        private List<Element> recipes; // рецепты
        private List<Element> elements; // сырье
        tbFormMainController tb; // контроллер таблицы Рецептуры
        CalcFunction calc; // Вычисления

        NumberFormatInfo nfi; // локализация, разделитель целых и долей("," или ".")
        string decimal_separator; // разделитель

        public FormMainController()
        {
            tb = new tbFormMainController("Recepture");
            calc = new CalcFunction();
            tb.setCatalog();
            receptures = tb.getCatalog();            
            tb.setSubCatalog("Recipe", "id_recepture"); // table Recipe, id_recepture
            recipes = tb.readElement(2);
            elements = tb.readElement(1);            

            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            nfi = CultureInfo.CurrentCulture.NumberFormat;
            decimal_separator = nfi.NumberDecimalSeparator; 
        }

        private void setNFI(string lokalizacija)
        {
            CultureInfo.CurrentCulture = new CultureInfo(lokalizacija);
            nfi = CultureInfo.CurrentCulture.NumberFormat;
            decimal_separator = nfi.NumberDecimalSeparator;
        }

    }

}
