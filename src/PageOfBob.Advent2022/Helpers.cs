using System.Collections.Generic;

namespace PageOfBob.Advent2022
{
    internal static class Helpers
    {
        public static void Run(Action<string> action, params string[] files)
        {
            foreach (var file in files)
            {
                using (var stream = typeof(Helpers).Assembly.GetManifestResourceStream($"PageOfBob.Advent2022.Data.{file}.txt"))
                using (var reader = new StreamReader(stream!))
                {
                    action(reader.ReadToEnd());
                }
            }
        }

        public static void Run(Action<IEnumerable<string>> action, params string[] files)
        {
            foreach (var file in files)
            {
                using (var stream = typeof(Helpers).Assembly.GetManifestResourceStream($"PageOfBob.Advent2022.Data.{file}.txt"))
                using (var reader = new StreamReader(stream!))
                {
                    action(reader.ReadToEnd().Lines());
                }
            }
        }

        public static SplitTwoChars SplitAsTwoChars(this string line)
        {
            var split = line.Split(" ");
            return new SplitTwoChars(split[0][0], split[1][0]);
        }

        public static IEnumerable<string> Lines(this string line)
        {
            using var reader = new StringReader(line);
            string? l;
            while ((l = reader.ReadLine()) != null)
            {
                yield return l;
            }
        }

        public static string[] SplitOnNewlines(this string text)
        {
            var splitOnWindows = text.Split("\r\n");
            if (splitOnWindows.Length != 1)
                return splitOnWindows;

            return text.Split("\n");
        }

        public static string[] SplitOnDoubleNewlines(this string text)
        {
            var splitOnWindows = text.Split("\r\n\r\n");
            if (splitOnWindows.Length != 1)
                return splitOnWindows;

            return text.Split("\n\n");
        }
    }

    public record SplitTwoChars(char Left, char Right);
}
