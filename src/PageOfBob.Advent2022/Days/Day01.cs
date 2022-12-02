using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageOfBob.Advent2022.Days
{
    internal static class Day01
    {
        public static void PartOne(string input)
        {
            var maxCalories = input.SplitOnDoubleNewlines()
                .Select(x =>
                    x.SplitOnNewlines()
                        .Where(x => !string.IsNullOrEmpty(x))
                        .Select(int.Parse)
                        .ToList()
                ).Select(x => x.Sum()).Max();

            Console.WriteLine(maxCalories);
        }

        public static void PartTwo(string input)
        {
            var maxCalories = input.SplitOnDoubleNewlines()
                .Select(x =>
                    x.SplitOnNewlines()
                        .Where(x => !string.IsNullOrEmpty(x))
                        .Select(int.Parse)
                        .ToList()
                ).Select(x => x.Sum())
                .OrderByDescending(x => x)
                .Take(3)
                .Sum();

            Console.WriteLine(maxCalories);
        }
    }
}
