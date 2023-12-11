using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day06 : Solver
    {
        public override Task Solve()
        {
            List<string> puzzleInput = GetPuzzleInput("06.txt");

            int a = SolvePartOne(puzzleInput);

            int b = SolvePartTwo(puzzleInput);

            return Task.CompletedTask;
        }

        private int SolvePartOne(List<string> puzzleInput)
        {
            int[] pathsToWinning = new int[4];
            int[] times = Array.ConvertAll(Regex.Matches(puzzleInput[0], @"\d+").Select(x => x.Value).ToArray(), Convert.ToInt32);
            int[] dists = Array.ConvertAll(Regex.Matches(puzzleInput[1], @"\d+").Select(x => x.Value).ToArray(), Convert.ToInt32);

            for (int i = 0; i < times.Count(); i++)
            {
                int winnablePaths = 0;
                //for each race
                int loadingTime = 0;
                while (loadingTime <= times[i])
                {
                    if (((times[i] - loadingTime) * loadingTime) > dists[i])
                        winnablePaths++;

                    loadingTime++;
                }
                pathsToWinning[i] = winnablePaths;
            }
            int result = pathsToWinning.Aggregate(1, (a, b) => a * b);
            PrintResult(6, 1, result.ToString());

            return result;
        }

        private int SolvePartTwo(List<string> puzzleInput)
        {
            int winnablePaths = 0;
            int time = int.Parse(string.Join("", Regex.Matches(puzzleInput[0], @"\d+").Select(x => x.Value)));
            long dist = long.Parse(string.Join("", Regex.Matches(puzzleInput[1], @"\d+").Select(x => x.Value)));


            for (int i = 1; i <= time; i++)
            {
                if ((time - i) > (dist / i))
                    winnablePaths++;
            }

            PrintResult(6, 2, winnablePaths.ToString());
            return winnablePaths;
        }
    }
}
