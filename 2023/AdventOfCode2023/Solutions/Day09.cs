using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Solutions
{
    public class Day09 : Solver
    {

        public override Task Solve()
        {
            List<string> puzzleInput = GetPuzzleInput("09.txt");

            long a = SolvePartOne(puzzleInput);

            long b = SolvePartTwo(puzzleInput);

            return Task.CompletedTask;
        }

        private long SolvePartOne(List<string> puzzleInput)
        {
            long total = 0;

            foreach(string input in puzzleInput)
            {
                List<long> currentValues = Array.ConvertAll(input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    , Convert.ToInt64).ToList();
                long nextValue = GetDerivative(currentValues);
                total += nextValue;
            }

            PrintResult(9, 1, total.ToString());
            return total;
        }

        private long GetDerivative(List<long> values, bool forward = true)
        {
            if (values.All(x => x == 0))
                return 0;
            List<long> derivativeValues = new List<long>();
            for (int i = 1; i < values.Count; i++)
            {
                long deriv = values[i] - values[i - 1];
                derivativeValues.Add(deriv);
            }
            if (forward)
                return (values.Last() + GetDerivative(derivativeValues, forward));
            else
                return (values.First() - GetDerivative(derivativeValues, forward));
        }

        private long SolvePartTwo(List<string> puzzleInput)
        {
            long total = 0;

            foreach (string input in puzzleInput)
            {
                List<long> currentValues = Array.ConvertAll(input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    , Convert.ToInt64).ToList();
                long previousValue = GetDerivative(currentValues, false);
                total += previousValue;
            }

            PrintResult(9, 2, total.ToString());
            return total;
        }
    }
}
