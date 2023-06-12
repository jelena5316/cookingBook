/*
 *  enum for work with amounts of recipe ingredients
 */


namespace MajPAbGr_project
{
    public enum Mode //amounts editor
    {
       Create,
       Edit,
       EditNewMain
    }

    enum Columns // amounts editor
    {
        id_ingredients,
        amount,
        id_recepture
    }

    public enum CalcBase // recipes' calculator
    {
        Main,
        Total,
        Coefficient
    }
}
