namespace _03.SoftuniNumerals
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class SoftuniNumerals
    {
        private const int NumericSystem = 5;
        private static readonly Dictionary<string, string> QuinaryNums = 
            new Dictionary<string, string>
        {
            { "aa", "0" },
            { "aba", "1" },
            { "bcc", "2" },
            { "cc", "3" },
            { "cdc", "4" }
        };

        public static void Main()
        {
            var quinaryStr = Console.ReadLine();
            var newString = new StringBuilder();

            var matchCollection = Regex.Matches(quinaryStr, @"aa|aba|bcc|cc|cdc");
            foreach (Match match in matchCollection)
            {
                newString.Append(QuinaryNums[match.Value]);
            }

            var res = QuinaryToDecimal(newString.ToString());

            Console.WriteLine(res);
        }

        private static BigInteger QuinaryToDecimal(string quinaryString)
        {
            BigInteger quinaryNum = 0;
            for (int i = quinaryString.Length - 1, pow = 0; i >= 0; i--, pow++)
            {
                var num = BigInteger.Parse(quinaryString[i].ToString()) * Power(NumericSystem, pow);
                quinaryNum += num;
            }

            return quinaryNum;
        }

        private static BigInteger Power(long num, int power)
        {
            BigInteger res = 1;
            for (int i = 0; i < power; i++)
            {
                res *= num;
            }

            return res;
        }
    }
}
