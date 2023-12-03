using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day01 : Solver
    {
        public override Task Solve()
        {
            List<string> puzzleInput = GetPuzzleInput("01.txt");

            int a = SolvePartOne(puzzleInput);

            int b = SolvePartTwo(puzzleInput);

            return Task.CompletedTask;
        }

        private int SolvePartOne(List<string> puzzleInput)
        {
            string regEx = @"\d";
            int calibrationSum = 0;
            foreach (string input in puzzleInput)
            {
                string first = Regex.Match(input, regEx).Value;
                string last = Regex.Match(input, regEx, RegexOptions.RightToLeft).Value;

                calibrationSum += int.Parse($"{first}{last}");
            }

            PrintResult(1, 1, calibrationSum.ToString());
            return calibrationSum;
        }

        private int SolvePartTwo(List<string> puzzleInput)
        {
            string regEx = @"\d|zero|one|two|three|four|five|six|seven|eight|nine";
            int calibrationSum = 0;
            foreach (string input in puzzleInput)
            {
                string first = Regex.Match(input, regEx).Value;
                string last = Regex.Match(input, regEx, RegexOptions.RightToLeft).Value;

                if (!Regex.Match(first, @"\d").Success)
                    first = GetNumber(first);
                if (!Regex.Match(last, @"\d").Success)
                    last = GetNumber(last);

                calibrationSum += int.Parse($"{first}{last}");
            }

            PrintResult(1, 1, calibrationSum.ToString());
            return calibrationSum;
        }

        private string GetNumber(string number)
        {
            string[] representations = new string[10] {"zero", "one", "two", "three", "four", "five", 
                "six", "seven", "eight", "nine"};
            return Array.IndexOf(representations, number).ToString();
        }
    }
}
