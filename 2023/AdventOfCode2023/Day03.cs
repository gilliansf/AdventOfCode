using System.Text.RegularExpressions;

namespace AdventOfCode2023
{
    public class Day03 : Solver
    {

        public override Task Solve()
        {
            List<string> puzzleInput = GetPuzzleInput("03.txt");

            int a = SolvePartOne(puzzleInput);

            int b = SolvePartTwo(puzzleInput);

            return Task.CompletedTask;
        }

        private int SolvePartOne(List<string> puzzleInput)
        {
            string symbolRegex = @"(%|\/|-|\*|\$|&|\+|#|=|@)";

            int sum = 0;
            for (int i = 0; i < puzzleInput.Count; i++)
            {
                (List<Match> numberMatches, List<Match> possibleKeys) = GetRowInfo(puzzleInput, i, @"\d+", symbolRegex);

                foreach (Match number in numberMatches)
                {
                    //index range = start -1 to end +1
                    int min = number.Index - 1;
                    int max = number.Index + number.Length;

                    if (possibleKeys.Any(x => x.Index >= min && x.Index <= max))
                        sum += int.Parse(number.Value);
                }
            }
            PrintResult(3, 1, sum.ToString());
            return sum;
        }

        private int SolvePartTwo(List<string> puzzleInput)
        {
            int sum = 0;
            for (int i = 0; i < puzzleInput.Count; i++)
            {
                (List<Match> starMatches, List<Match> possibleNumbers) = GetRowInfo(puzzleInput, i, @"\*", @"\d+");

                foreach (Match star in starMatches)
                {
                    List<int> gearNumbers = new List<int>();

                    foreach (Match number in possibleNumbers)
                    {
                        int min = number.Index - 1;
                        int max = number.Index + number.Length;
                        if (star.Index >= min && star.Index <= max)
                            gearNumbers.Add(int.Parse(number.Value));
                    }

                    if (gearNumbers.Count == 2)
                        sum += (gearNumbers[0] * gearNumbers[1]);
                }
            }

            PrintResult(3, 2, sum.ToString());
            return sum;
        }

        private (List<Match>, List<Match>) GetRowInfo(List<string> puzzleInput, int i, string mainRegex, string keyRegex)
        {
            List<Match> mainMatches = Regex.Matches(puzzleInput[i], mainRegex).ToList();

            List<Match> possibleKeys = Regex.Matches(puzzleInput[i], keyRegex).Where(x => x.Value != string.Empty).ToList();
            if (i - 1 >= 0)
                possibleKeys.AddRange(Regex.Matches(puzzleInput[i - 1], keyRegex).Where(x => x.Value != string.Empty).ToList());
            if (i + 1 < puzzleInput.Count)
                possibleKeys.AddRange(Regex.Matches(puzzleInput[i + 1], keyRegex).Where(x => x.Value != string.Empty).ToList());

            return (mainMatches, possibleKeys);
        }
    }
}
