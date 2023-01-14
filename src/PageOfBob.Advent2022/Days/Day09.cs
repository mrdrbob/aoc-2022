namespace PageOfBob.Advent2022.Days
{
    internal static class Day09
    {
        private static Dictionary<string, Vector2<int>> Movements = new Dictionary<string, Vector2<int>>
        {
            { "R", Vectors.East },
            { "L", Vectors.West },
            { "D", Vectors.South },
            { "U", Vectors.North }
        };

        private static void DebugPrint(Vector2<int> head, Vector2<int> tail)
        {
            foreach (var y in Enumerable.Range(-10, 20))
            {
                foreach (var x in Enumerable.Range(-10, 20))
                {
                    var pt = new Vector2<int>(x, y);
                    var c = head == pt ? "H" :
                            tail == pt ? "T" :
                            " ";
                    Console.Write(c);
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------");
        }


        private static Vector2<int> Follow(Vector2<int> head, Vector2<int> tail)
        {
            var tailDiff = head.Difference(tail);
            var absTailDiff = tailDiff.Abs();

            bool isTouching = absTailDiff.BothMatch(x => x <= 1);
            if (isTouching)
                return tail;

            bool isBelowAndNeedsToMove = absTailDiff.X == 0 && absTailDiff.Y > 1;
            if (isBelowAndNeedsToMove)
                return tail.Translate(new Vector2<int>(0, tailDiff.Y < 0 ? 1 : -1));

            bool isBesideAndNeedsToMove = absTailDiff.Y == 0 && absTailDiff.X > 1;
            if (isBesideAndNeedsToMove)
                return tail.Translate(new Vector2<int>(tailDiff.X < 0 ? 1 : -1, 0));

            // Otherwise we're at an angle and need to move both ways
            var angleTranslation = tailDiff
                .Apply(x => Math.Min(1, x))
                .Apply(x => Math.Max(-1, x))
                .Apply(x => -x);

            return tail.Translate(angleTranslation);
        }

        public static void PartOne(IEnumerable<string> actions)
        {
            var head = new Vector2<int>(0, 0);
            var tail = new Vector2<int>(0, 0);

            var visited = new HashSet<Vector2<int>>
            {
                tail
            };

            foreach (var action in actions)
            {
                var (dir, times) = action.SplitAsTwoStrings(" ");
                var movement = Movements[dir];
                for (int t = int.Parse(times); t > 0; t--)
                {
                    visited.Add(tail);
                    // DebugPrint(head, tail);

                    head = head.Translate(movement);
                    tail = Follow(head, tail);
                }
            }
            visited.Add(tail);
            // DebugPrint(head, tail);


            Console.WriteLine($"Tail visited: {visited.Count}");
        }

        public static void PartTwo(IEnumerable<string> actions)
        {
            var knots = Enumerable.Range(0, 10).Select(x => new Vector2<int>(0, 0)).ToList();
            var visited = new HashSet<Vector2<int>>();

            foreach (var action in actions)
            {
                var (dir, times) = action.SplitAsTwoStrings(" ");
                var movement = Movements[dir];
                for (int t = int.Parse(times); t > 0; t--)
                {
                    var head = knots.First();
                    head = head.Translate(movement);
                    knots[0] = head;

                    var follower = head;
                    for (var i = 1; i < knots.Count; i++)
                    {
                        var tail = knots[i];
                        tail = Follow(follower, tail);

                        knots[i] = tail;
                        follower = tail;
                        if (i == knots.Count - 1)
                            visited.Add(tail);
                    }
                }
            }

            Console.WriteLine($"Tail visited: {visited.Count}");
        }
    }
}
