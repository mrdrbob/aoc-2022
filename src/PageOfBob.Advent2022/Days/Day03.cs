namespace PageOfBob.Advent2022.Days
{
    internal static class Day03
    {
        private static int LetterPriority(char c)
            => char.IsLower(c) ? (c - 'a') + 1 : (c - 'A') + 27;

        public static void PartOne(IEnumerable<string> lines)
        {
            var total = lines
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(line =>
                {
                    if (line.Length % 2 != 0)
                        throw new FormatException("ONLY EVEN LINES ALLOWED JERKFACE");
                    
                    var first = new HashSet<char>(line.Take(line.Length / 2));
                    var second = new HashSet<char>(line.Skip(line.Length / 2));
                    var intersection = first.Intersect(second).Single();

                    return LetterPriority(intersection);
                })
                .Sum();

            Console.WriteLine($"Sum: {total}");
        }

        public static void PartTwo(IEnumerable<string> lines)
        {
            var total = lines
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .GroupByCount(3)
                .Select(line =>
                {
                    var one = new HashSet<char>(line.ElementAt(0));
                    var two = new HashSet<char>(line.ElementAt(1));
                    var three = new HashSet<char>(line.ElementAt(2));

                    var intersection = one.Intersect(two).Intersect(three).Single();
                    return LetterPriority(intersection);
                })
                .Sum();

            Console.WriteLine($"Sum: {total}");
        }
    }
}
