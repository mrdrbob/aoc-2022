namespace PageOfBob.Advent2022.Days
{
    internal static class Day04
    {
        public static void PartOne(IEnumerable<string> lines)
        {
            var count = lines
                .IgnoreEmpties()
                .Select(x => {
                    var (left, right) = x
                        .SplitAsTwoStrings(",")
                        .Transform(r => 
                            new Range<int>(
                                r.SplitAsTwoStrings("-")
                                .Transform(int.Parse)
                            )
                        );

                    bool oneContainsTheOther = left.Contains(right) || right.Contains(left);
                    return oneContainsTheOther ? 1 : 0;
                })
                .Sum();

            Console.WriteLine($"Total count: {count}");
        }

        public static void PartTwo(IEnumerable<string> lines)
        {
            var count = lines
                .IgnoreEmpties()
                .Select(x => {
                    var (left, right) = x
                        .SplitAsTwoStrings(",")
                        .Transform(r =>
                            new Range<int>(
                                r.SplitAsTwoStrings("-")
                                .Transform(int.Parse)
                            )
                        );

                    bool overlaps = left.Overlaps(right);
                    return overlaps ? 1 : 0;
                })
                .Sum();

            Console.WriteLine($"Total count: {count}");
        }
    }
}
