using PageOfBob.Advent2022.Days;
using System.Diagnostics;
using static PageOfBob.Advent2022.Helpers;


var stopwatch = new Stopwatch();
stopwatch.Start();

// Run(Day01.PartOne, "01-example-1", "01-input");
Run(Day01.PartTwo, "01-example-1", "01-input");

stopwatch.Stop();
Console.WriteLine($"Dishes are done, man: {stopwatch.Elapsed.TotalMilliseconds} seconds");
