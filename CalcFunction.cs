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

        public CalcFunction()
        {
            coefficient = 1.0;
        }

        public double Coefficient
        {
            set { this.coefficient = value; }
            get { return coefficient; }
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

        public double InvertCoefficient(double koeff) => (double)(Math.Pow((double)koeff, -1.0));

        public double [] ReCalc(double koeff, double[] list)
        {
            for (int k = 0; k < list.Length; k++)
                list[k] *= koeff;
            return list;
        }

        public double Summa(double[] values)
        {
            double summa = 0;
            for (int k = 0; k < values.Length; k++)
                summa += values[k];
            return summa;
        }

        public string ColonToPoint(string text)
        {
            string number;
            if (text.Contains(","))
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
