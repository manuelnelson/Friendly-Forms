using System;

namespace BusinessLogic.Helpers
{
    public static class Numbers
    {
        static readonly string[] Ones = new[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        static readonly string[] Teens = new[] { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        static readonly string[] Tens = new[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        static readonly string[] ThousandsGroups = { "", " Thousand", " Million", " Billion" };

        public static int RoundTo(this double i, int roundTo)
        {
            return (int)((Math.Round(i / roundTo)) * roundTo);
        }

        private static string FriendlyInteger(int n, string leftDigits, int thousands)
        {
            if (n == 0)
            {
                return leftDigits;
            }
            var friendlyInt = leftDigits;
            if (friendlyInt.Length > 0)
            {
                friendlyInt += " ";
            }

            if (n < 10)
            {
                friendlyInt += Ones[n];
            }
            else if (n < 20)
            {
                friendlyInt += Teens[n - 10];
            }
            else if (n < 100)
            {
                friendlyInt += FriendlyInteger(n % 10, Tens[n / 10 - 2], 0);
            }
            else if (n < 1000)
            {
                friendlyInt += FriendlyInteger(n % 100, (Ones[n / 100] + " Hundred"), 0);
            }
            else
            {
                friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);
            }

            return friendlyInt + ThousandsGroups[thousands];
        }

        /// <summary>
        /// Converts integer to written integer.  12 becomes twelve, etc.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string IntegerToWritten(int n)
        {
            if (n == 0)
            {
                return "Zero";
            }
            if (n < 0)
            {
                return "Negative " + IntegerToWritten(-n);
            }

            return FriendlyInteger(n, "", 0);
        }

    }
}
