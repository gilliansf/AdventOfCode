using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day06 : Solver
    {
        public override Task Solve()
        {
            List<string> puzzleInput = GetPuzzleInput("06.txt");

            int a = SolvePartOne(puzzleInput);

            //int b = SolvePartTwo(puzzleInput);

            return Task.CompletedTask;
        }

        private int SolvePartOne(List<string> puzzleInput)
        {
            //brute force minimum number of ways to win, then assume gravy from there



            return 0;
        }

        private int SolvePartTwo(List<string> puzzleInput)
        {
            throw new NotImplementedException();
        }
    }
}
