using System.Reflection.Metadata;

namespace PageOfBob.Advent2022.Days
{
    internal static class Day02
    {
        #region Part 1
        private static RPS[] PossibleHands = new[]
        {
            new RPS('A', new[] { 'X' }, 'C', 1),
            new RPS('B', new[] { 'Y' }, 'A', 2),
            new RPS('C', new[] { 'Z' }, 'B', 3)
        };

        private static RPS Find(char letter)
        {
            var byId = PossibleHands.SingleOrDefault(x => x.Id == letter);
            if (byId is not null)
                return byId;

            return PossibleHands.Single(x => x.Aliases.Contains(letter));
        }

        public static void PartOne(IEnumerable<string> input)
        {
            var score = input.Sum(line =>
            {
                var (left, right) = line.SplitAsTwoChars();

                var them = Find(left);
                var me = Find(right);

                var winScore = them.Beats == me.Id ? 0
                             : me.Beats == them.Id ? 6
                             : 3;

                return me.PlayScore + winScore;
            });

            Console.WriteLine($"Total Score: {score}");
        }
        #endregion

        #region Part 2
        private static RPS2[] PossibleHands2 = new[]
        {
            new RPS2('A', 'C', 1),
            new RPS2('B', 'A', 2),
            new RPS2('C', 'B', 3)
        };

        private static RPS2 Find2(char letter)
            => PossibleHands2.Single(x => x.Id == letter);

        private static RPS2 FindWinsAgainst(RPS2 hand)
            => PossibleHands2.Single(x => x.Beats == hand.Id);

        private static RPS2 FindLosesTo(RPS2 hand)
            => PossibleHands2.Single(x => x.Id == hand.Beats);

        private static RPS2 FindDraw(RPS2 hand)
            => PossibleHands2.Single(x => x.Id != hand.Beats && x.Beats != hand.Id);

        public static void PartTwo(IEnumerable<string> input)
        {
            var score = input.Sum(line =>
            {
                var (left, right) = line.SplitAsTwoChars();

                var them = Find2(left);
                var action = right;
                return action switch
                {
                    'Y' => FindDraw(them).PlayScore + 3,
                    'X' => FindLosesTo(them).PlayScore + 0,
                    'Z' => FindWinsAgainst(them).PlayScore + 6,
                    _ => throw new NotImplementedException($"Unknown action: {action}")
                };
            });

            Console.WriteLine($"Total Score: {score}");
        }

        public record RPS(char Id, char[] Aliases, char Beats, int PlayScore);

        public record RPS2(char Id, char Beats, int PlayScore);
        #endregion
    }
}
