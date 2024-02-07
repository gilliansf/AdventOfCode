using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions
{
    public class Day08 : Solver
    {

        public override Task Solve()
        {
            List<string> puzzleInput = GetPuzzleInput("08.txt");

            int a = SolvePartOne(puzzleInput);

            long b = SolvePartTwo(puzzleInput);

            return Task.CompletedTask;
        }

        private int SolvePartOne(List<string> puzzleInput)
        {
            int steps = 0;

            //get data into a format I like
            Dictionary<string, (string, string)> paths = new Dictionary<string, (string, string)>();
            string[] directions = Array.ConvertAll(puzzleInput[0].ToCharArray(), Convert.ToString);
            foreach (string inputLine in puzzleInput.GetRange(2, puzzleInput.Count() - 2))
            {
                var matches = Regex.Matches(inputLine, @"[a-zA-Z]*")
                    .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                    .Select(x => x.Value)
                    .ToList();
                paths[matches[0]] = (matches[1], matches[2]);
            }

            //now run through the steps
            int stepIndex = 0;
            string currentSquare = "AAA";
            while (currentSquare != "ZZZ")
            {
                if (directions[stepIndex] == "L")
                    currentSquare = paths[currentSquare].Item1;
                else
                    currentSquare = paths[currentSquare].Item2;
                
                stepIndex++;
                if (stepIndex == directions.Count())
                    stepIndex = 0;
                steps++;
            }

            PrintResult(8, 1, steps.ToString());
            return steps;
        }

        private long SolvePartTwo(List<string> puzzleInput)
        {
            //get data into a format I like
            Dictionary<string, (string, string)> paths = new Dictionary<string, (string, string)>();
            string[] directions = Array.ConvertAll(puzzleInput[0].ToCharArray(), Convert.ToString);
            foreach (string inputLine in puzzleInput.GetRange(2, puzzleInput.Count() - 2))
            {
                var matches = Regex.Matches(inputLine, @"[a-zA-Z]*")
                    .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                    .Select(x => x.Value)
                    .ToList();
                paths[matches[0]] = (matches[1], matches[2]);
            }

            //least common multiple
            List<string> currentSquares = paths.Keys.Where(x => x.EndsWith("A")).ToList();
            List<long> stepsNeeded = new List<long>(); 
            foreach (string square in currentSquares)
            {
                long steps = 0;
                int stepIndex = 0;
                string activeSquare = square;
                while (!activeSquare.EndsWith("Z"))
                {
                    if (directions[stepIndex] == "L")
                        activeSquare = paths[activeSquare].Item1;
                    else
                        activeSquare = paths[activeSquare].Item2;

                    stepIndex++;
                    if (stepIndex == directions.Count())
                        stepIndex = 0;
                    steps++;
                }
                stepsNeeded.Add(steps);
            }

            long x = LeastCommonMultiple(stepsNeeded);

            PrintResult(8, 2, x.ToString());
            return x;
        }

        private long LeastCommonMultiple(IEnumerable<long> numbers) =>
            numbers.Aggregate((long) 1, (current, number) => current / GreatestCommonDivisor(current, number) * number);

        private long GreatestCommonDivisor(long a, long b)
        {
            while (b != 0)
            {
                a %= b;
                (a, b) = (b, a);
            }
            return a;
        }
    }
}
