namespace AdventOfCode2023
{
    public class Day07 : Solver
    {
        Dictionary<string, int> _labelRankings;
        Dictionary<string, int> _handTypes;

        public override Task Solve()
        {
            List<string> puzzleInput = GetPuzzleInput("07.txt");

            int a = SolvePartOne(puzzleInput);

            int b = SolvePartTwo(puzzleInput);

            return Task.CompletedTask;
        }
        private int SolvePartOne(List<string> puzzleInput)
        {
            _labelRankings = new Dictionary<string, int>()
            {//A, K, Q, J, T, 9, 8, 7, 6, 5, 4, 3, or 2.
                {"A", 0}, {"K", 1}, {"Q", 2}, {"J", 3}, {"T", 4}, {"9", 5}, {"8", 6},
                {"7", 7}, {"6", 8}, {"5", 9}, {"4", 10}, {"3", 11}, {"2", 12}  };

            int totalWinnings = RunGame(puzzleInput, false);

            PrintResult(7, 1, totalWinnings.ToString());
            return totalWinnings;
        }

        private int SolvePartTwo(List<string> puzzleInput)
        {
            _labelRankings = new Dictionary<string, int>()
            {//A, K, Q, T, 9, 8, 7, 6, 5, 4, 3, 2, J.
                {"A", 0}, {"K", 1}, {"Q", 2}, {"T", 3}, {"9", 4}, {"8", 5}, {"7", 6},
                {"6", 7}, {"5", 8}, {"4", 9}, {"3", 10 }, {"2", 11}, {"J", 12}  };

            int totalWinnings = RunGame(puzzleInput, true);

            PrintResult(7, 2, totalWinnings.ToString());
            return totalWinnings;

        }

        private int RunGame(List<string> puzzleInput, bool wildCard)
        {
            _handTypes = new Dictionary<string, int>();

            //players = (hand, bet)
            List<(string, string)> players = puzzleInput.Select(x => x.Split(" ")).Select(x => (x[0], x[1])).ToList();
            List<(string, string)> orderedHands = new List<(string, string)>();

            foreach ((string, string) player in players)
            {
                bool beatSomeoneElse = false;
                if (orderedHands.Count() == 0)
                {
                    beatSomeoneElse = true;
                    orderedHands.Add(player);
                    continue;
                }

                for (int i = 0; i < orderedHands.Count(); i++)
                {
                    string winner = CompareTwoHands(orderedHands[i].Item1, player.Item1, wildCard);
                    if (winner.Equals(player.Item1))
                    {
                        beatSomeoneElse = true;
                        orderedHands.Insert(i, player);
                        break;
                    }
                }
                if (!beatSomeoneElse)
                    orderedHands.Add(player);
            }

            int totalWinnings = 0;
            int[] orderedBets = Array.ConvertAll(orderedHands.Select(x => x.Item2).Reverse().ToArray(), Convert.ToInt32);

            orderedBets.Select((bet, index) => totalWinnings += ((index + 1) * bet)).ToArray().Count();
            return totalWinnings;

        }

        private string CompareTwoHands(string home, string away, bool wildCard = false)
        {// returns winner card

            //1st value: type of hand
            int homeType = _handTypes.ContainsKey(home) ? _handTypes[home] : GetHandType(home, wildCard);
            int awayType = _handTypes.ContainsKey(away) ? _handTypes[away] : GetHandType(away, wildCard);
            if (homeType < awayType)
                return home;
            else if (awayType < homeType)
                return away;

            //2nd & on: ranking of each card in the hand
            for (int i = 0; i < home.Count(); i++)
            {
                int homeRanking = _labelRankings[home[i].ToString()];
                int awayRanking = _labelRankings[away[i].ToString()];
                if (homeRanking < awayRanking)
                    return home;
                else if (awayRanking < homeRanking)
                    return away;
            }

            return "";
        }

        private int GetHandType(string hand, bool wildCard = false)
        { 
            bool wildCardAtPlay = (wildCard && hand.Contains("J"));
            List<Tuple<string, int>> wildCardCounts = new List<Tuple<string, int>>();


            string[] handContents = Array.ConvertAll(hand.ToCharArray(), Convert.ToString);
            List<Tuple<string, int>> labelCounts = new List<Tuple<string, int>>();
            foreach (string label in _labelRankings.Keys)
            {
                labelCounts.Add(new Tuple<string, int>(label, handContents.Count(x => x.Equals(label, StringComparison.InvariantCultureIgnoreCase))));
                if (wildCardAtPlay)
                    wildCardCounts.Add(new Tuple<string, int>(label, handContents.Count(x => x.Equals(label, StringComparison.InvariantCultureIgnoreCase)
                        || x.Equals("J", StringComparison.InvariantCultureIgnoreCase))));
            }

            if (labelCounts.Any(x => x.Item2 == 5) || (wildCardCounts.Any(x => x.Item2 == 5)))
            { //five of the same string
                _handTypes[hand] = 1;
                return 1;
            } else if (labelCounts.Any(x => x.Item2 == 4) || (wildCardCounts.Any(x => x.Item2 == 4)))
            { //four of the same string
                _handTypes[hand] = 2;
                return 2;
            } else if ((labelCounts.Any(x => x.Item2 == 3) && labelCounts.Any(x => x.Item2 == 2)) || 
                (wildCardAtPlay && wildCardCounts.Select(x => x.Item2).Max() == 3 && labelCounts.Count(x => x.Item2 == 2) == 2))
            { //three of one kind & two of another
                _handTypes[hand] = 3;
                return 3;
            } else if ((labelCounts.Any(x => x.Item2 == 3) && labelCounts.Count(x => (x.Item2 == 0 || x.Item2 == 1)) == labelCounts.Count() - 1) ||
                (wildCardAtPlay && wildCardCounts.Any(x => x.Item2 == 3) && labelCounts.Count(x => (x.Item2 == 0 || x.Item2 == 1)) == labelCounts.Count() - 1))
            { //three of one kind & two unique
                _handTypes[hand] = 4;
                return 4;
            } else if ((labelCounts.Count(x => x.Item2 == 2) == 2) ||
                (wildCardAtPlay && wildCardCounts.Count(x => x.Item2 == 2) == 2  && labelCounts.Count(x => x.Item2 == 2) == 1))
            { //two of one kind & two of another
                _handTypes[hand] = 5;
                return 5;
            } else if (labelCounts.Any(x => x.Item2 == 2) || (wildCardAtPlay && wildCardCounts.Any(x => x.Item2 == 2)))
            { //two of one kind
                _handTypes[hand] = 6;
                return 6;
            } else if (labelCounts.All(x => (x.Item2 == 0 || x.Item2 == 1)))
            { //all distinct
                _handTypes[hand] = 7;
                return 7;
            }
            return -1;
        }

    }
}
