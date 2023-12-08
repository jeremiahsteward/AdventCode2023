using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ACCoreComponents;

namespace AC23
{
    internal class Day01
    {
        public Day01() { }
        public Day01(string filename)
        {
            data = filename.ReadInFile();
        }

        List<string> data = new List<string>();

        public string RunPartOne()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("Day 1, Part 1 Result:");

            int totalValue = 0;

            foreach (var line in data)
            {
                string numVal = string.Empty;

                PullNumbers(line, ref numVal);

                totalValue += numVal.ToInt(0);
            }

            sb.AppendLine($"Final Total: {totalValue}.");
            sb.AppendLine();

            return sb.ToString();
        }

        public string RunPartTwo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Day 1, Part 2 Result:");

            int totalValue = 0;

            foreach (var line in data)
            {
                var regex = new Regex(@"(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)", RegexOptions.IgnoreCase);
                var regexRev = new Regex(@"(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)", RegexOptions.RightToLeft | RegexOptions.IgnoreCase);
                string numVal = string.Empty;
                string convertLine = regex.Replace(line, new MatchEvaluator(ReplaceTextDigit));
                string revConvertLine = regexRev.Replace(line, new MatchEvaluator(ReplaceTextDigit));

                RevLine(convertLine, revConvertLine, ref numVal);

                totalValue += numVal.ToInt(0);
            }

            sb.AppendLine($"Final Total: {totalValue}.");
            sb.AppendLine();

            return sb.ToString();
        }

        private static void PullNumbers(string lineValue, ref string numVal)
        {
            for (int i = 0; i < lineValue.Length; i++)
            {
                if (lineValue[i].IsDigit())
                {
                    numVal += lineValue[i];
                    break;
                }
            }

            for (int i = 1; i <= lineValue.Length; i++)
            {
                if (lineValue[lineValue.Length - i].IsDigit())
                {
                    numVal += lineValue[lineValue.Length - i];
                    break;
                }
            }
        }

        public static void RevLine(string lineValue, string backLineValue, ref string numVal)
        {
            for (int i = 0; i < lineValue.Length; i++)
            {
                if (lineValue[i].IsDigit())
                {
                    numVal += lineValue[i];
                    break;
                }
            }

            for (int i = 1; i <= backLineValue.Length; i++)
            {
                if (backLineValue[backLineValue.Length - i].IsDigit())
                {
                    numVal += backLineValue[backLineValue.Length - i];
                    break;
                }
            }
        }

        private string ReplaceTextDigit(Match m)
        {
            switch (m.Value.ToLower())
            {
                case "one":
                    return "1";
                case "two":
                    return "2";
                case "three":
                    return "3";
                case "four":
                    return "4";
                case "five":
                    return "5";
                case "six":
                    return "6";
                case "seven":
                    return "7";
                case "eight":
                    return "8";
                case "nine":
                    return "9";
                default:
                    return "0";
            }
        }
    }
}
