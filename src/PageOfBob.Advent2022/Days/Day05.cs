namespace PageOfBob.Advent2022.Days
{
    internal static class Day05
    {
        public static void PartOne(string data)
        {
            var (unparsedCrates, unparsedCommands) = data.SplitOnDoubleNewlines().SplitAsTwoStrings();

            var totalStacks = (unparsedCrates.Lines().Reverse().First().Length + 1) / 4;

            var stacks = Enumerable.Range(0, 9).Select(x => new Stack<char>()).ToList();

            foreach (var line in unparsedCrates.Lines().Reverse().Skip(1))
            {
                for (var i = 0; i < totalStacks; i++)
                {
                    char? value = line.CharAt(1 + i * 4);
                    if (value.HasValue && value.Value != ' ')
                        stacks[i].Push(value.Value);
                }
            }

            var commands = unparsedCommands.Lines().IgnoreEmpties().Select(x =>
            {
                var split = x.Split(" ");
                return new SplitThree<string>(split[1], split[3], split[5]).Transform(int.Parse);
            }).ToList();

            foreach (var (move, from, to) in commands)
            {
                for (var x = 0; x < move; x ++)
                {
                    var popped = stacks[from - 1].Pop();
                    stacks[to - 1].Push(popped);
                }
            }

            foreach (var stack in stacks)
            {
                var value = stack.Count > 0 ? stack.Pop() : ' ';
                Console.Write(value);
            }

            Console.WriteLine();
        }

        public static void PartTwo(string data)
        {
            var (unparsedCrates, unparsedCommands) = data.SplitOnDoubleNewlines().SplitAsTwoStrings();

            var totalStacks = (unparsedCrates.Lines().Reverse().First().Length + 1) / 4;

            var stacks = Enumerable.Range(0, 9).Select(x => new Stack<char>()).ToList();

            foreach (var line in unparsedCrates.Lines().Reverse().Skip(1))
            {
                for (var i = 0; i < totalStacks; i++)
                {
                    char? value = line.CharAt(1 + i * 4);
                    if (value.HasValue && value.Value != ' ')
                        stacks[i].Push(value.Value);
                }
            }

            var commands = unparsedCommands.Lines().IgnoreEmpties().Select(x =>
            {
                var split = x.Split(" ");
                return new SplitThree<string>(split[1], split[3], split[5]).Transform(int.Parse);
            }).ToList();

            foreach (var (move, from, to) in commands)
            {
                // Lazy...
                var substack = new Stack<char>();
                for (var x = 0; x < move; x++)
                    substack.Push(stacks[from - 1].Pop());
                while (substack.Count > 0)
                    stacks[to  - 1].Push(substack.Pop());
            }

            foreach (var stack in stacks)
            {
                var value = stack.Count > 0 ? stack.Pop() : ' ';
                Console.Write(value);
            }

            Console.WriteLine();
        }
    }
}
