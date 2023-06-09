﻿/*
 * to calculate sum of ingredients amounts and coefficients,
 * to recalculate the ingredients amounts using the coefficients,
 * to format results,
 * to check and change a decimal separator
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace MajPAbGr_project   
{
       public class CalcFunction
        {
        int count;
        double coefficient, total;
        double[] amounts;
        CalcBase calcbase;

        public CalcFunction()
        {
            coefficient = 1.0;
        }

        public double Coefficient
        {
            set { this.coefficient = value; }
            get { return coefficient; }
        }

        public CalcBase calcBase
        {
            set { calcbase = value; }
            get { return calcbase; }
        }


        public void setAmounts(List<Element> source)
        {
            amounts = new double[source.Count];
            for (int index = 0; index < amounts.Length; index++)
            {
                amounts[index] = source[index].Amounts;
            }
            total = Summa();
            this.count = source.Count();
        }       

        public double[] getAmounts()
        {
            return amounts;
        }
        public double getTotal() { return total; }

        public double Summa()
        {
            double summa = 0;
            for (int k = 0; k < amounts.Length; k++)
            {
                summa += amounts[k];
            }
            return summa;
        }

        public double Summa(double[] values)
        {
            double summa = 0;
            for (int k = 0; k < values.Length; k++)
                summa += values[k];
            return summa;
        }


        public double[] ReCalc()
        {
            if (coefficient == 0) return amounts;
            else
            {
                double[] arr = new double[amounts.Length];
                for (int k = 0; k < arr.Length; k++)
                {
                    arr[k] = amounts[k] * coefficient;
                }
                return arr;
            }
        }
     
        static public double calculateCoefficient(double old_amount, double new_amount)
            => (old_amount / new_amount);      

        public void setNewRecipesCoefficient(double amount)
        {
            if (amounts.Length < 1)
                return;
            switch ((int)calcbase)
            {
                case 1: // total                   
                    coefficient = amount / Summa();                    
                    break;
                case 0: //main
                    coefficient = amount / amounts[0];
                    break;
                case 2: // coefficient
                    coefficient = amount;                    
                    break;
                default: coefficient = amount / amounts[0];
                    break;
            }
        }

        public List<string> FormatAmounts(double[] arr, double summa)
        {
            List<string> texts = new List<string>();
            //to convert and format numbers
            int k;
            string t = "";
            for (k = 0; k < arr.Length; k++)
            {
                t = string.Format("{0:f2}", arr[k]);
                texts.Add(t);
                t = "";
            }
            t = string.Format("{0:f2}", summa);
            texts.Add(t);
            return texts;
        }

        public List<string> FormatAmounts()
        {
            double[] arr = ReCalc();
            int k;
            string t = "";
            List<string> texts = new List<string>();

            //to convert and format numbers        
            for (k = 0; k < arr.Length; k++)
            {
                t = string.Format("{0:f2}", arr[k]);
                texts.Add(t);
                t = "";
            }
            t = string.Format("{0:f2}", Summa(arr));
            texts.Add(t);
            return texts;
        }

        public string ColonToPoint(string text)
        {
            string number;
            if (text.Contains(",")) // if (text.Contains(",") || text.Contains((char)44)))
            {
                int k;
                number = "";
                for (k = 0; k < text.Length; k++)
                {
                    if (text[k] != ',')
                        number += text[k];
                    else
                        number += '.';
                }
            }
            else
            {
                number = text;
            }
            return number;
        }

        public string PointToColon(string text)
        {
            string number;          

            number = "";
            if (text.Contains('.'))
            {
                int k;
                for (k = 0; k < text.Length; k++)
                {
                    if (text[k] != '.')
                        number += text[k];
                    else
                        number += ',';
                }
            }
            else
            {
                number = text;
            }
            return number;
        }
    }
}


