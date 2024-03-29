﻿using PageOfBob.Advent2022;
using PageOfBob.Advent2022.Days;
using System.Diagnostics;
using static PageOfBob.Advent2022.Helpers;


var stopwatch = new Stopwatch();
stopwatch.Start();

// Run(Day01.PartOne, "01-example-1", "01-input");
// Run(Day01.PartTwo, "01-example-1", "01-input");

// Run(Day02.PartOne, "02-example", "02-input");
// Run(Day02.PartTwo, "02-example", "02-input");

// Run(Day03.PartOne, "03-example", "03-input");
// Run(Day03.PartTwo, "03-example", "03-input");

// Run(Day04.PartOne, "04-example", "04-input");
// Run(Day04.PartTwo, "04-example", "04-input");

// Run(Day05.PartOne, "05-example", "05-input");
// Run(Day05.PartTwo, "05-example", "05-input");

/*
RunInline(Day06.PartOne, 
    "mjqjpqmgbljsphdztnvjfqwrcgsmlb", 
    "bvwbjplbgvbhsrlpgdmjqwftvncz", 
    "nppdvjthqldpwncqszvftbrmjlhg", 
    "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",
    "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw",
    "06-input".ReadFile()
);
*/
/*
RunInline(Day06.PartTwo,
    "mjqjpqmgbljsphdztnvjfqwrcgsmlb",
    "bvwbjplbgvbhsrlpgdmjqwftvncz",
    "nppdvjthqldpwncqszvftbrmjlhg",
    "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",
    "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw",
    "06-input".ReadFile()
);
*/

// Run(Day07.PartOne, "07-example", "07-input");
// Run(Day07.PartTwo, "07-example", "07-input");

// Run(Day08.PartOne, "08-example", "08-input");
// Run(Day08.PartTwo, "08-example", "08-input");

// Run(Day09.PartOne, "09-example", "09-input");
Run(Day09.PartTwo, "09-example", "09-example2", "09-input");

stopwatch.Stop();
Console.WriteLine($"Dishes are done, man: {stopwatch.Elapsed.TotalMilliseconds} seconds");
