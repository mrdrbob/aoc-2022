namespace PageOfBob.Advent2022.Days
{
    internal static class Day08
    {
        public static void PartOne(string input)
        {
            var grid = Grid<int>.Create(input, t => int.Parse(t.ToString()));
            var tallTallTrees = grid.AllPoints().Count(pt =>
            {
                var value = grid.GetAt(pt);
                var visibleFromAnyEdge = Vectors.CardinalDirections.Any(
                    dir => grid.ListPointsInDirection(pt, dir).All(y => y < value)
                );
                return visibleFromAnyEdge;
            });

            Console.WriteLine($"Visible tree: {tallTallTrees}");
        }

        public static void PartTwo(string input)
        {
            var grid = Grid<int>.Create(input, t => int.Parse(t.ToString()));

            var highestScore = grid.AllPoints().Select(pt =>
            {
                var value = grid.GetAt(pt);
                return Vectors.CardinalDirections.Select(dir =>
                    grid.ListPointsInDirection(pt, dir)
                        .TakeWhileInclusive(x => x < value)
                        .Count()
                )
                    .Aggregate(1, (acc, value) => acc * value);
            }).Max();

            Console.WriteLine($"Best score: {highestScore}");
        }

    }
}
