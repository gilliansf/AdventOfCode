namespace AdventOfCode2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Solver> completedSolvers = new List<Solver>()
            {
                new Day01(),
                new Day02(),
                new Day03(),
                new Day04(),
                new Day05(),
                new Day06()
            };

            while (true)
            {
                Console.WriteLine("\nWhich puzzle would you like to solve?");
                string? day = Console.ReadLine(); //assuming knowledgeable user

                if (int.TryParse(day, out int dayInt) && dayInt <= completedSolvers.Count)
                    completedSolvers[dayInt - 1].Solve();
            }

        }
    }
}