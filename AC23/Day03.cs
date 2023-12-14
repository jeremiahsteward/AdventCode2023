using ACCoreComponents;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AC23
{
    internal class Day03
    {
        List<string> data = new List<string>();
        string singleLineData;
        List<char> matchSymbols;

        public Day03(string filename)
        {
            data = filename.ReadInDataAsListAndString(out singleLineData);

            Regex regex = new Regex(@"[^\d.]");

            matchSymbols = regex.Matches(singleLineData).Cast<Match>().Select(s => s.Value.Single()).Distinct().ToList();
        }

        public string RunPartOne()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Day 3, Part 1 Result:");

            Regex regex = new Regex(@"(\d+)");

            List<int> possiblePartNumbers = regex.Matches(singleLineData).Cast<Match>().Select(m => m.Value.ToInt()).ToList();
            
            List<int> confirmedPartNumbers = new List<int>();
            List<int> nonPartNumbers = new List<int>();

            string activePartNumber;
            bool activePartValid;

            for (int i = 0; i < data.Count; i++)
            {
                activePartNumber = null;
                activePartValid = false; 

                for (int j = 0; j < data[i].Length; j++)
                {
                    char currentChar = (char)data[i][j];

                    if (currentChar.IsDigit() && activePartNumber != null)
                    {
                        activePartNumber += currentChar;
                    }
                    else if (currentChar.IsDigit() && activePartNumber == null)
                    {
                        activePartNumber = string.Empty + currentChar;
                    }
                    else if (!currentChar.IsDigit() && activePartNumber != null)
                    {
                        if (activePartValid) { confirmedPartNumbers.Add(activePartNumber.ToInt()); }
                        else { nonPartNumbers.Add(activePartNumber.ToInt()); }

                        activePartNumber = null;
                        activePartValid = false;
                        continue;
                    }
                    else if (!currentChar.IsDigit() && activePartNumber == null)
                    {
                        continue;
                    }

                    if (!activePartValid)
                    {
                        // Check if it should be
                        activePartValid |= CheckAboveAndBelow(i, j);

                        //Check to Left
                        if (j > 0)
                        {
                            activePartValid |= CheckAboveAndBelow(i, j - 1);
                        }

                        //Check to right
                        if (j < data[i].Length - 1)
                        {
                            activePartValid |= CheckAboveAndBelow(i, j + 1);
                        }
                    }
                }

                if (activePartNumber != null && activePartValid)
                {
                    confirmedPartNumbers.Add(activePartNumber.ToInt());
                }
                else if (activePartNumber != null)
                {
                    nonPartNumbers.Add(activePartNumber.ToInt());
                }
            }

            sb.AppendLine($"Final Total: {confirmedPartNumbers.Sum()}.");
            sb.AppendLine();

            return sb.ToString();
        }

        public string RunPartTwo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Day 3, Part 2 Result:");


            return sb.ToString();
        }

        private bool CheckAboveAndBelow(int currentRow, int currentCol)
        {
            // Check Above
            if (currentRow > 0)
            {
                char aboveChar = data[currentRow - 1][currentCol];

                if (matchSymbols.Contains(aboveChar)) { return true; }
            }

            // Check Below
            if (currentRow < data.Count - 1)
            {
                char belowChar = data[currentRow + 1][currentCol];

                if (matchSymbols.Contains(belowChar)) { return true; }
            }

            // Check Self

            char currentChar = data[currentRow][currentCol];

            if (matchSymbols.Contains(currentChar)) { return true; }

            return false;
        }
    }
}
