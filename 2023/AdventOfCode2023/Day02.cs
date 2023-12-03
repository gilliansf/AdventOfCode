using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day02 : Solver
    {

        public override Task Solve()
        {
            List<string> puzzleInput = GetPuzzleInput("02.txt");

            int a = SolvePartOne(puzzleInput);

            int b = SolvePartTwo(puzzleInput);

            return Task.CompletedTask;
        }

        private int SolvePartTwo(List<string> puzzleInput)
        {
            throw new NotImplementedException();
        }

        private int SolvePartOne(List<string> puzzleInput)
        {
            throw new NotImplementedException();
        }
    }
}
