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

        public static SplitTwo<char> SplitAsTwoChars(this string line, string separator = " ")
        {
            var split = line.Split(separator);
            return new SplitTwo<char>(split[0][0], split[1][0]);
        }

        public static SplitTwo<string> SplitAsTwoStrings(this string line, string separator = " ")
        {
            var split = line.Split(separator);
            return new SplitTwo<string>(split[0], split[1]);
        }

        public static SplitTwo<string> SplitAsTwoStrings(this IEnumerable<string> lines, string separator = " ")
        {
            return new SplitTwo<string>(lines.First(), lines.Skip(1).First());
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

        public static IEnumerable<IEnumerable<T>> GroupByCount<T>(this IEnumerable<T> enumerable, int count)
        {
            while (enumerable.Any())
            {
                var next = enumerable.Take(count).ToList();
                yield return next;
                enumerable = enumerable.Skip(count);
            }
        }

        public static IEnumerable<string> IgnoreEmpties(this IEnumerable<string> enumerable)
            => enumerable.Where(x => !string.IsNullOrWhiteSpace(x));

        public static char? CharAt(this string line, int index)
        {
            if (index < line.Length)
                return line[index];
            return null;
        }
    }

    public record SplitTwo<T>(T Left, T Right)
    {
        public SplitTwo<K> Transform<K>(Func<T, K> transform)
            => new SplitTwo<K>(transform(Left), transform(Right));
    }

    public record SplitThree<T>(T One, T Two, T Three)
    {
        public SplitThree<K> Transform<K>(Func<T, K> transform)
            => new SplitThree<K>(transform(One), transform(Two), transform(Three));
    }
}
