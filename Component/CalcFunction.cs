using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project   
{
       public class CalcFunction
        {
        /* Суммировать общую массу для расчётов по ней
         * Посчитать коэффициент:
         * а) поделить старое количество на новое (новое вводиться)
         * б) поделить новое на 100 (новое -- введёное, здесь за старое, а за новое - 100);
         * Рассчитать рецептуру:
         * 1) считаем коэффициент (делим граммы главного продукта на сто)
         * 2) остальное делим на полученный коэфициент (умножаем на перевернутый)
         * Расчитать назад в рецепт (для вывода):
         * умножить проценты на коэффицинт из базы.
         * Расчитать на другое количество главного продукта:
         * 1) получить коэффициент (другое / 100)
         * 2) перемножить на него остальное (в процентах)
         * Расчитать на другое количество неглавного или сумму продукта:
         * 1) расчитать коэффициент
         * 2) посчитать новое количество главного продукта (умножить на полученный коэффициент)
         * 3) рассчитать на другое количество главного продукта
         */

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

       // public int getCount() { return count; }

        public double Summa()
        {
            double summa = 0;
            for (int k = 0; k < amounts.Length; k++)
            {
                summa += amounts[k];
            }
            return summa;
        }

        public double getTotal() { return total; }

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

        public double Summa(double[] values)
        {
            double summa = 0;
            for (int k = 0; k < values.Length; k++)
                summa += values[k];
            return summa;
        }

        public List<string> FormatAmounts(double[] arr, double summa)
        {
            List<string> texts = new List<string>();
            //форматировать числа при переводе в строку
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

            //форматировать числа при переводе в строку           
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
    }
}


