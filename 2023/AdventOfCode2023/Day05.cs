

namespace AdventOfCode2023
{
    public class Day05 : Solver
    {
        public override Task Solve()
        {
            List<string> puzzleInput = GetPuzzleInput("05.txt");

            long a = SolvePartOne(puzzleInput);

            long b = SolvePartTwo(puzzleInput);

            return Task.CompletedTask;
        }

        private long SolvePartOne(List<string> puzzleInput)
        {
            long lowestLocation = 0;

            long[] seeds = puzzleInput[0].Split(": ")[1].Split(" ").Select(x => long.Parse(x)).ToArray();

            lowestLocation = seeds.Select(x => FindSeedLocation(puzzleInput, x)).Min();


            PrintResult(5, 1, lowestLocation.ToString());
            return lowestLocation;
        }

        private long SolvePartTwo(List<string> puzzleInput)
        {
            IEnumerable<long[]> seedRanges = puzzleInput[0].Split(": ")[1].Split(" ").Select(x => long.Parse(x)).ToArray().Chunk(2);
            List<long[]> ranges = new List<long[]>();

            //explanation: so for each seed range, it's going to recursively break down each following range
            // so instead of searching seed-by-seed, it's going to search interval-by-interval, then return
            // the lowest starting interval range
            foreach (long[] seedRange in seedRanges)
            {
                var result = FindSeedLocation(puzzleInput, seedRange[0], seedRange[0] + seedRange[1]);
                ranges = ranges.Concat(result).ToList();
            }
            long lowestDestination = ranges.Select(y => y[0]).Min(); 

            PrintResult(5, 2, lowestDestination.ToString());
            return lowestDestination; 

        }
        
        private long FindSeedLocation(List<string> puzzleInput, long seed)
        {
            List<int> mapIndexes = puzzleInput.Select((v, i) => new { v, i })
                    .Where(x => x.v.Contains("-to-"))
                    .Select(x => x.i).ToList();

            long currentEntryPoint = seed;

            for (int i = 0; i < mapIndexes.Count(); i++)
            {
                int sectionLength = i + 1 == mapIndexes.Count() ? puzzleInput.Count() - mapIndexes[i] - 1
                    : mapIndexes[i + 1] - mapIndexes[i] - 2;

                foreach (string map in puzzleInput.GetRange(mapIndexes[i]+ 1, sectionLength))
                {
                    long[] vals = map.Split(" ").Select(x => long.Parse(x)).ToArray();

                    if (currentEntryPoint >= vals[1] && currentEntryPoint <= vals[1] + vals[2])
                    {
                        currentEntryPoint = (currentEntryPoint - vals[1]) + vals[0];
                        break;
                    }
                }
            }
            return currentEntryPoint;
        }

        private List<long[]> FindSeedLocation(List<string> puzzleInput, long seedRangeStart, long seedRangeEnd, int mapIndex = 0)
        {
            List<int> mapIndexes = puzzleInput.Select((v, i) => new { v, i })
                    .Where(x => x.v.Contains("-to-"))
                    .Select(x => x.i).ToList(); //get map dividers

            List<long[]> newRanges = new List<long[]>();

            long workingRangeStart = seedRangeStart;
            long workingRangeEnd = seedRangeEnd;

            for (int i = mapIndex; i < mapIndexes.Count(); i++)
            {
                int sectionLength = i + 1 == mapIndexes.Count() ? puzzleInput.Count() - mapIndexes[i] - 1
                    : mapIndexes[i + 1] - mapIndexes[i] - 2;

                foreach (string map in puzzleInput.GetRange(mapIndexes[i] + 1, sectionLength))
                {
                    long[] vals = map.Split(" ").Select(x => long.Parse(x)).ToArray();
                    var mapSourceRange = new Tuple<long, long>(vals[1], vals[1] + vals[2]);
                    var mapDestinationRange = new Tuple<long, long>(vals[0], vals[0] + vals[2]);
                    //vals[0] = dest range start
                    //vals[1] = source range start
                    //vals[2] = range length

                    if ((mapSourceRange.Item1 <= workingRangeStart && workingRangeStart < mapSourceRange.Item2) && //range opens in the map
                            (mapSourceRange.Item1 <= workingRangeEnd && workingRangeEnd < mapSourceRange.Item2)) //and range closes in the map
                    {
                        workingRangeStart = (workingRangeStart - mapSourceRange.Item1 + mapDestinationRange.Item1);
                        workingRangeEnd = (workingRangeEnd - mapSourceRange.Item1 + mapDestinationRange.Item1);
                        break;
                    }

                    else if ((mapSourceRange.Item1 <= workingRangeStart && workingRangeStart < mapSourceRange.Item2) && //range opens in the map
                            (mapSourceRange.Item1 <= workingRangeEnd && workingRangeEnd > mapSourceRange.Item2)) //and range closes outside the map
                    {
                        //being broken up

                        //pass untranslated to new loop (end of mapSource, then end of workingRange)
                        //then move on with translated (translated start of workingRange, then translated end of mapDestination

                        newRanges = newRanges.Concat(FindSeedLocation(puzzleInput, mapSourceRange.Item2, workingRangeEnd, i)).ToList();

                        workingRangeStart = (workingRangeStart - mapSourceRange.Item1 + mapDestinationRange.Item1);
                        workingRangeEnd = mapDestinationRange.Item2;
                        break;

                    }
                    else if ((mapSourceRange.Item1 > workingRangeStart && workingRangeStart <= mapSourceRange.Item2) && //range opens outside the map
                            (mapSourceRange.Item1 <= workingRangeEnd && workingRangeEnd <= mapSourceRange.Item2)) //and range closes in the map
                    {
                        //being broken up

                        //pass untranslated to new loop
                        //then move on with translated
                        newRanges = newRanges.Concat(FindSeedLocation(puzzleInput, workingRangeStart, mapSourceRange.Item1 - 1, i)).ToList();

                        workingRangeStart = mapDestinationRange.Item1;
                        workingRangeEnd = (workingRangeEnd - mapSourceRange.Item1 + mapDestinationRange.Item1);
                        break;
                    }

                    //if no change, just keep going
                }
            }

            newRanges.Add(new long[] { workingRangeStart, workingRangeEnd });
            return newRanges;
        }



    }
}
