using static aoc_2023.Helpers;
using aoc_2023.Days;
using System.Diagnostics;

OutputChristmasTree();


/*
Day1 day1 = new Day1();
day1.SolvePart1();
day1.SolvePart2();

Day2 day2 = new Day2();
day2.SolvePart1();
day2.SolvePart2();


Day3 day3 = new Day3();
day3.SolvePart2();
day3.SolvePart1();

*/
var sw = new Stopwatch();
sw.Start();
// day4.SolvePart1();
Day4.SolvePart2();
sw.Stop();
Console.WriteLine($"total ms: {sw.ElapsedMilliseconds}");