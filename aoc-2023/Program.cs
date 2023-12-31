﻿using static aoc_2023.Helpers;
using aoc_2023.Days;
using System.Diagnostics;

OutputChristmasTree();
var stopWatch = new Stopwatch();
stopWatch.Start();

/*
Day1 day1 = new Day1();
day1.SolvePart1();
day1.SolvePart2();

Day2 day2 = new Day2();
day2.SolvePart1();
day2.SolvePart2();

Day3 day3 = new Day3();
day3.SolvePart1();
day3.SolvePart2();

Day4.SolvePart1();
Day4.SolvePart2();

Day5.SolvePart1();
Day5.SolvePart2();

Day6.SolvePart1();
Day6.SolvePart2();

Day7.SolvePart1();
Day7.SolvePart2();
*/

Day8.SolvePart1();
Day8.SolvePart2();

stopWatch.Stop();
Console.WriteLine($"total ms: {stopWatch.ElapsedMilliseconds}");