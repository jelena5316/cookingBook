using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajPAbGr_project
{
    public enum Mode
    {
       Create,
       Edit,
       EditNewMain
    }

    enum Columns
    {
        id_ingredients,
        amount,
        id_recepture
    }

    public enum CalcBase
    {
        Main,
        Total,
        Coefficient
    }

    public enum Operation
    {
        Add,
        Remove
    }
}
