using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public class Element
    {
        int id;
        string name;        
        double amounts;        

        public Element()
        {
            name = "name";
            amounts = 0;            
        }

        public Element(int id, string name, double amounts)
        {
            this.id = id;
            this.name = name;
            this.amounts = amounts;
        }

        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        public double Amounts
        {
            set { amounts = value; }
            get { return amounts; }
        }

        public int Id
        {
            set { id = value; }
            get { return id; }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
