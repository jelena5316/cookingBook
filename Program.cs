using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MajPAbGr_project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        { 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormMain());

            /*
             * test mode
             */
            Application.Run(new Categories());

            //tbReceptureController tb = new tbReceptureController("Recepture");
            //tb.Selected = 5;
            //AmountsController cntrl = new AmountsController(tb);
            //Application.Run(new InsertAmounts(cntrl));

            //tb.Selected = 28;
            //tbAmountsController cntrl = new tbAmountsController("Amounts", ref tb);           
            //Application.Run(new InsertAmounts(ref cntrl));

            

            //Application.Run(new Categories());

            //TechnologyCards cards = new TechnologyCards();
            //cards.Cards = 6;
            ////cards.Technology = 1;
            //cards.activdApplyButton();
            //Application.Run(cards);
        }
    }
}
