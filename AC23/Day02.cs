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
    internal class Day02
    {
        List<string> data = new List<string>();

        public Day02(string filename)
        {
            data = filename.ReadInFile();
        }


        public string RunPartOne()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Day 2, Part 1 Result:");

            List<Tuple<int, List<CubeRound>>> games = new List<Tuple<int, List<CubeRound>>>();
            List<Tuple<int, List<CubeRound>>> validGames = new List<Tuple<int, List<CubeRound>>>();

            ProcessData(games);

            CubeRound limit = new CubeRound()
            {
                Red = 12,
                Green = 13,
                Blue = 14
            };

            foreach (var game in games)
            {
                bool validRounds = true;

                foreach (var round in game.Item2)
                {
                    validRounds &= round.isValid(limit);
                }

                if (validRounds)
                {
                    validGames.Add(game);
                }
            }

            int idTotal = validGames.Select(s => s.Item1).Sum();

            sb.AppendLine($"Final Total: {idTotal}.");
            sb.AppendLine();

            return sb.ToString();
        }

        public string RunPartTwo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Day 2, Part 2 Result:");

            List<Tuple<int, List<CubeRound>>> games = new List<Tuple<int, List<CubeRound>>>();
            List<int> powerSets = new List<int>();

            ProcessData(games);

            foreach (var game in games)
            {
                int redMin = game.Item2.Max(s => s.Red);
                int blueMin = game.Item2.Max(s => s.Blue);
                int greenMin = game.Item2.Max(s => s.Green);

                powerSets.Add(redMin * blueMin * greenMin);
            }

            sb.AppendLine($"Final Total: {powerSets.Sum()}.");

            return sb.ToString();
        }

        private void ProcessData(List<Tuple<int, List<CubeRound>>> games)
        {
            foreach (string line in data)
            {
                List<CubeRound> roundData = new List<CubeRound>();
                string workingLine = line.Replace(" ", "").ToLower();

                var gameRound = workingLine.Split(':');

                var rounds = gameRound[1].Split(";");


                foreach (var round in rounds)
                {
                    List<string> roundVals = round.Split(",").ToList();
                    roundData.Add(new CubeRound()
                    {
                        Blue = roundVals.Where(r => r.Contains("blue")).FirstOrDefault()?.Replace("blue", "").ToInt() ?? 0,
                        Red = roundVals.Where(r => r.Contains("red")).FirstOrDefault()?.Replace("red", "").ToInt() ?? 0,
                        Green = roundVals.Where(r => r.Contains("green")).FirstOrDefault()?.Replace("green", "").ToInt() ?? 0,
                    });
                }

                games.Add(new Tuple<int, List<CubeRound>>(gameRound[0].Replace("game", "").ToInt(), roundData));
            }
        }
    }

    class CubeRound
    {
        public int Red { get; set; } = 0;
        public int Blue { get; set; } = 0;
        public int Green { get; set; } = 0;

        public bool isValid (CubeRound limits)
        {
            return 
                this.Red <= limits.Red &&
                this.Green <= limits.Green &&
                this.Blue <= limits.Blue;
        }
    }
}
