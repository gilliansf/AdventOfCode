namespace AdventOfCode2023.Solutions
{
    public class Day04 : Solver
    {

        public override Task Solve()
        {
            List<string> puzzleInput = GetPuzzleInput("04.txt");

            int a = SolvePartOne(puzzleInput);

            int b = SolvePartTwo(puzzleInput);

            return Task.CompletedTask;
        }

        private int SolvePartOne(List<string> puzzleInput)
        {
            int sum = 0;

            foreach (string input in puzzleInput)
            {
                int winCount = DetermineWinCount(input);

                sum += (int)(1 * Math.Pow(2, winCount - 1));
            }

            PrintResult(4, 1, sum.ToString());
            return sum;
        }


        private int SolvePartTwo(List<string> puzzleInput)
        {
            int sum = 0;
            Dictionary<int, int> gameWins = new Dictionary<int, int>(); //wins per game
            Queue<int> scoreQueue = new Queue<int>(); //cards to be scored (copies)
            for (int i = 1; i <= puzzleInput.Count(); i++)
                scoreQueue.Enqueue(i); //receiving original copies of all cards

            while (scoreQueue.TryPeek(out int gameNumber))
            {
                scoreQueue.Dequeue();
                sum += 1;
                string input = puzzleInput[gameNumber - 1];
                if (!gameWins.ContainsKey(gameNumber))
                    gameWins[gameNumber] = DetermineWinCount(input);

                for (int i = 1; i <= gameWins[gameNumber]; i++)
                    scoreQueue.Enqueue(gameNumber + i); //recieving newly won copies of proceeding cards
            }

            PrintResult(4, 2, sum.ToString());
            return sum;
        }


        private int DetermineWinCount(string input)
        {
            string trInput = input.Replace("  ", " ");
            int findAllCrossover = 0;
            List<string> winners = trInput.Split("|")[0].Split(":")[1].Trim().Split(" ").ToList();
            List<string> potential = trInput.Split("|")[1].Trim().Split(" ").ToList();

            winners.Sort();
            potential.Sort();

            foreach (string winner in winners)
            {
                findAllCrossover += potential.FindAll(x => x.Equals(winner)).Count();
            }

            return findAllCrossover;
        }
    }
}
