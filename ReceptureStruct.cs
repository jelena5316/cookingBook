using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MajPAbGr_project
{

    public struct ReceptureStruct
    {
        int id;
        string name, category, source, author, technology, ingredient;
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
}
