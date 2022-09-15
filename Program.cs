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

            FormMainController tb = new FormMainController("Recepture");
            tb.Selected = 8;
            AmountsController cntrl = new AmountsController("Amounts", ref tb);            
            //Application.Run(new FormMain());
            Application.Run(new InsertAmounts(Mode.Edit, ref cntrl));
        }
    }
}
