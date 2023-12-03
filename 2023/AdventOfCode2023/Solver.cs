using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public abstract class Solver
    {
        public abstract Task Solve();

        public List<string> GetPuzzleInput(string fileName)
        {
            return File.ReadLines(Path.Combine(Environment.CurrentDirectory, "Input", fileName)).ToList();
        }

        public void PrintResult(int day, int puzzleNum, string result)
        {
            Console.WriteLine($"Result for Day {day}, puzzle number {puzzleNum}: {result}");
        }
    }
}
