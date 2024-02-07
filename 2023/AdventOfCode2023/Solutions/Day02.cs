using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions
{
    public class Day02 : Solver
    {
        string _pairRegex = @"\d+\s(red|green|blue)";

        public override Task Solve()
        {
            List<string> puzzleInput = GetPuzzleInput("02.txt");

            int a = SolvePartOne(puzzleInput);

            int b = SolvePartTwo(puzzleInput);

            return Task.CompletedTask;
        }

        private int SolvePartOne(List<string> puzzleInput)
        {
            Dictionary<string, int> maxValues = new Dictionary<string, int>
                { { "red", 12 }, { "green", 13 }, { "blue", 14 } };

            int sum = puzzleInput.Select(input =>
            {
                if (Regex.Matches(input, _pairRegex).Any(pair =>
                {
                    var tuple = pair.Value.Trim().Split(" ");
                    return maxValues[tuple[1]] < int.Parse(tuple[0]);
                }))
                    return 0;

                else
                    return int.Parse(Regex.Match(input, @"\d+").Value);
            }).Sum();

            PrintResult(2, 1, sum.ToString());
            return sum;
        }

        private int SolvePartTwo(List<string> puzzleInput)
        {
            int sum = puzzleInput.Select(input =>
            {
                Dictionary<string, int> maxValues = new Dictionary<string, int>();

                foreach (Match match in Regex.Matches(input, _pairRegex))
                {
                    string[] pair = match.Value.Trim().Split(" ");
                    if (!maxValues.ContainsKey(pair[1]) || maxValues[pair[1]] < int.Parse(pair[0]))
                        maxValues[pair[1]] = int.Parse(pair[0]);
                }
                return maxValues.Values.Aggregate(1, (a, b) => a * b);

            }).Sum();

            PrintResult(2, 2, sum.ToString());
            return sum;
        }

    }
}
