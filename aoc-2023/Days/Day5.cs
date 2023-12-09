using System.Text.RegularExpressions;
using static aoc_2023.Helpers;

namespace aoc_2023.Days
{
    internal static class Day5
    {
        private class Map
        {
            public string Name { get; set; } = string.Empty;
            public List<MapRow> MapRows { get; set; } = new List<MapRow>();
        }

        private class MapRow
        {
            public double DesinationRangeStart { get; set; }
            public double SourceRangeStart { get; set; }
            public double RangeLength { get; set; }
        }

        public static void SolvePart1()
        {
            StreamReader streamReader = GetInputData("day5-almanac");
            List<Map> mapList = new List<Map>();
            List<double> seedList = new List<double>();
            Map newMap = new Map();
            double lowest = 1000000000000000;

            while (!streamReader.EndOfStream)
            {
                string read = streamReader.ReadLine();
                if (!String.IsNullOrWhiteSpace(read))
                {
                    if (read.Contains("seeds:"))
                    {
                        var splitRead = read.Split(": ");
                        var seeds = splitRead[1].Split(" ");
                        foreach (var seed in seeds)
                        {
                            seedList.Add(Convert.ToInt64(seed));
                        }
                    }
                    else if (read.Contains("map:"))
                    {
                        if (read != newMap.Name)
                        {
                            if (newMap.Name != string.Empty)
                                mapList.Add(newMap);
                            newMap = new Map();
                            newMap.Name = read;
                        }
                    }
                    else
                    {
                        MapRow mapRow = new MapRow();
                        var row = read.Split(" ");
                        mapRow.DesinationRangeStart = Convert.ToInt64(row[0]);
                        mapRow.SourceRangeStart = Convert.ToInt64(row[1]);
                        mapRow.RangeLength = Convert.ToInt64(row[2]);
                        newMap.MapRows.Add(mapRow);
                    }
                }
            }
            streamReader.Close();
            mapList.Add(newMap);
            // process maps.
            double[] results = new double[seedList.Count];
            foreach (var map in mapList)
            {
                int count = 0;
                // process first map
                if (map.Name == mapList[0].Name)
                {
                    foreach (var seed in seedList)
                    {
                        foreach (var mapRow in map.MapRows)
                        {
                            // find if in range.
                            if (mapRow.SourceRangeStart <= seed && mapRow.SourceRangeStart + mapRow.RangeLength - 1 >= seed)
                                results[count] = seed - mapRow.SourceRangeStart + mapRow.DesinationRangeStart;
                        }
                        // if no ranges match, assign to desination.
                        if (results[count] == 0)
                            results[count] = seed;
                        count++;
                    }
                    // OutputLine(map, results);
                }
                // process all other maps.
                else
                {
                    foreach (var result in results)
                    {
                        foreach (var mapRow in map.MapRows)
                        {
                            // find if in range.
                            if (mapRow.SourceRangeStart <= result && mapRow.SourceRangeStart + mapRow.RangeLength - 1 >= result)
                                results[count] = result - mapRow.SourceRangeStart + mapRow.DesinationRangeStart;
                        }
                        count++;
                    }
                    // OutputLine(map, results);
                }
            }
            // find lowest
            foreach (var result in results)
            {
                if (result < lowest)
                    lowest = result;
            }
            OutputSolve(5, 1, lowest);
        }

        public static void SolvePart2()
        {
            StreamReader streamReader = GetInputData("day5-almanac");
            List<Map> mapList = new List<Map>();
            List<double> seedList = new List<double>();
            Map newMap = new Map();
            double lowest = 1000000000000000;

            while (!streamReader.EndOfStream)
            {
                string read = streamReader.ReadLine();
                if (!String.IsNullOrWhiteSpace(read))
                {
                    if (read.Contains("seeds:"))
                    {
                        var splitRead = read.Split(": ");
                        var seeds = splitRead[1].Split(" ");

                        // read seed ranges in pairs, and create seeds.
                        for (int i = 0; i <= seeds.Length - 1; i+=2)
                        {
                            //Console.WriteLine($"amount of seeds to create: {Math.Abs(Convert.ToInt64(seeds[i]) - Convert.ToInt64(seeds[i + 1]))}");
                            //Console.WriteLine($"first seed to create: {seeds[i]} , last seed to create: {Convert.ToInt64(seeds[i]) + Convert.ToInt64(seeds[i + 1]) - 1}");
                            for (int j = 0; j <= Convert.ToInt64(seeds[i + 1]) - 1; j++)
                            {
                                seedList.Add(Convert.ToInt64(seeds[i]) + j);
                            }
                        }
                        Console.WriteLine($"Total seeds created: {seedList.Count}");

                    }
                    else if (read.Contains("map:"))
                    {
                        if (read != newMap.Name)
                        {
                            if (newMap.Name != string.Empty)
                                mapList.Add(newMap);
                            newMap = new Map();
                            newMap.Name = read;
                        }
                    }
                    else
                    {
                        MapRow mapRow = new MapRow();
                        var row = read.Split(" ");
                        mapRow.DesinationRangeStart = Convert.ToInt64(row[0]);
                        mapRow.SourceRangeStart = Convert.ToInt64(row[1]);
                        mapRow.RangeLength = Convert.ToInt64(row[2]);
                        newMap.MapRows.Add(mapRow);
                    }
                }
            }
            streamReader.Close();





            mapList.Add(newMap);
            // process maps.
            double[] results = new double[seedList.Count];
            foreach (var map in mapList)
            {
                int count = 0;
                // process first map
                if (map.Name == mapList[0].Name)
                {
                    Console.WriteLine($"{map.Name} Processing");
                    foreach (var seed in seedList)
                    {
                        foreach (var mapRow in map.MapRows)
                        {
                            // find if in range.
                            if (mapRow.SourceRangeStart <= seed && mapRow.SourceRangeStart + mapRow.RangeLength - 1 >= seed)
                                results[count] = seed - mapRow.SourceRangeStart + mapRow.DesinationRangeStart;
                        }
                        // if no ranges match, assign to desination.
                        if (results[count] == 0)
                            results[count] = seed;
                        count++;
                    }
                    // OutputLine(map, results);
                }
                // process all other maps.
                else
                {
                    Console.WriteLine($"{map.Name} Processing");
                    foreach (var result in results)
                    {
                        foreach (var mapRow in map.MapRows)
                        {
                            // find if in range.
                            if (mapRow.SourceRangeStart <= result && mapRow.SourceRangeStart + mapRow.RangeLength - 1 >= result)
                                results[count] = result - mapRow.SourceRangeStart + mapRow.DesinationRangeStart;
                        }
                        count++;
                    }
                    // OutputLine(map, results);
                }
            }
            // find lowest
            foreach (var result in results)
            {
                if (result < lowest)
                    lowest = result;
            }
            OutputSolve(5, 2, lowest);
        }

        private static void OutputLine(Map map, double[] results)
        {
            Console.WriteLine(map.Name + " ");
            var lastResult = results.Last();
            foreach (var result in results)
            {
                Console.Write(result);
                if (result != lastResult)
                    Console.Write(" , ");
            }
            Console.WriteLine();
        }
    }
}
