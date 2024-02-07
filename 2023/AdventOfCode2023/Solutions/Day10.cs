using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Solutions
{
    public class Day10 : Solver
    {
        /*
  | is a vertical pipe connecting north and south.
  - is a horizontal pipe connecting east and west.
  L is a 90-degree bend connecting north and east.
  J is a 90-degree bend connecting north and west.
  7 is a 90-degree bend connecting south and west.
  F is a 90-degree bend connecting south and east.
  . is ground; there is no pipe in this tile.
        */

        List<List<string>> _grid;
        Dictionary<string, string[]> _pipeTypes = new Dictionary<string, string[]>()
        {
            {"S", new string[4] {"N", "E", "S", "W"} },
            {"|", new string[2] {"N", "S"} },
            {"-", new string[2] {"E", "W"} },
            {"L", new string[2] {"N", "E"} },
            {"J", new string[2] {"N", "W"} },
            {"7", new string[2] {"S", "W"} },
            {"F", new string[2] {"S", "E"} },
            {".", new string[1] {""} }
        };
        Dictionary<string, string> _directionPairs = new Dictionary<string, string>()
        {
            {"N", "S" },
            {"S", "N" },
            {"W", "E" },
            {"E", "W" }
        };

        public override Task Solve()
        {
            List<string> puzzleInput = GetPuzzleInput("10.txt");

            long a = SolvePartOne(puzzleInput);

            //long b = SolvePartTwo(puzzleInput);

            return Task.CompletedTask;
        }

        //goal, get from the S, back to the S

        private long SolvePartOne(List<string> puzzleInput)
        {
            long numberOfSteps = 0;

            _grid = new List<List<string>>();
            foreach (string input in puzzleInput)
                _grid.Add(Array.ConvertAll(input.ToCharArray(), Convert.ToString).ToList());
            Tuple<int, int> startingLocation = new Tuple<int, int>(-1, -1); ; //X,Y (indx in list), (list in list)
            for (int y = 0; y < _grid.Count; y++)
                if (_grid[y].Contains("S"))
                {
                    startingLocation = new Tuple<int, int>(_grid[y].IndexOf("S"), y);
                    break;
                }
            try
            {
                var info = EvaluateDirections(startingLocation);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return numberOfSteps;
        }

        //todo: throwing stack overflow, needs to be redone from scratch
        private List<Tuple<int, int>> EvaluateDirections(Tuple<int, int> currentLocation, List<Tuple<int, int>> previousLocations = null)
        {

            Console.WriteLine($"Current location: {currentLocation.Item1}, {currentLocation.Item2}");
            string me = _grid[currentLocation.Item2][currentLocation.Item1];

            string[] acceptablePairs = _pipeTypes[me].Select(x => _directionPairs[x]).ToArray();

            if (previousLocations == null)
                previousLocations = new List<Tuple<int, int>>();
            previousLocations.Add(currentLocation);

            List<Tuple<int, int>> possibleDirections = new List<Tuple<int, int>>();
            for (int horizIndex = currentLocation.Item1 - 1; horizIndex <= currentLocation.Item1 + 1 && horizIndex < _grid.Count; horizIndex++)
            {
                if (horizIndex < 0)
                    continue;
                for (int vertIndex = currentLocation.Item2 - 1; vertIndex <= currentLocation.Item2 + 1 && vertIndex < _grid.Count; vertIndex++)
                {
                    if (vertIndex < 0)
                        continue;
                    string potentialValue = _grid[vertIndex][horizIndex];
                    Tuple<int, int> newDirection = new Tuple<int, int>(horizIndex, vertIndex);
                    if (potentialValue == "S" && !previousLocations.Contains(newDirection))
                        return new List<Tuple<int, int>>() { newDirection };

                    if (!newDirection.Equals(currentLocation) && !previousLocations.Contains(newDirection) &&
                        _pipeTypes[potentialValue].Any(x => acceptablePairs.Contains(x)))
                        possibleDirections.Add(newDirection);
                }
            }

            if (possibleDirections.Count == 0)
                return new List<Tuple<int, int>>();

            var checkedPaths = possibleDirections.Select(x => EvaluateDirections(x, previousLocations)).ToList();
            var fine = new List<Tuple<int, int>>();
            foreach (var path in checkedPaths)
                if (path.Count > 0)
                {
                    return path;
                }

            return new List<Tuple<int, int>>();
        }

        private long SolvePartTwo(List<string> puzzleInput)
        {
            throw new NotImplementedException();
        }
    }
}
