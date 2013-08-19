using System;

namespace BusinessLogic.Helpers
{
    public static class Numbers
    {
        public static int RoundTo(this double i, int roundTo)
        {
            return (int)((Math.Round(i / roundTo)) * roundTo);
        }
    }
}
