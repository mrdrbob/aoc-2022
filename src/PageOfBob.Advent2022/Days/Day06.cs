namespace PageOfBob.Advent2022.Days
{
    internal static class Day06
    {

        public static void Execute(string line, int tagLength)
        {
            int index = tagLength;
            while (index <= line.Length)
            {
                var substring = line.Substring(index - tagLength, tagLength);
                if (substring.Distinct().Count() == tagLength)
                {
                    Console.WriteLine($"{index} - {substring}");
                    return;
                }
                index++;
            }

            Console.WriteLine("Not found");
        }

        public static void PartOne(string line)
            => Execute(line, 4);

        public static void PartTwo(string line)
            => Execute(line, 14);
    }
}
