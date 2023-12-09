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
            double[] results = new double[seedList.Count];
            foreach (var map in mapList)
            {
                int count = 0;
                if (map.Name == mapList[0].Name) // process first map
                {
                    ProcessMap(seedList, map, results, count);
                }
                else
                {
                    ProcessOtherMaps(map, results, count);
                }
            }
            foreach (var result in results) // find lowest
            {
                if (result < lowest) lowest = result;
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
                        for (int i = 0; i <= seeds.Length - 1; i+=2) // read seed ranges in pairs, and create seeds.
                        {
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
            double[] results = new double[seedList.Count];
            foreach (var map in mapList)
            {
                int count = 0;
                if (map.Name == mapList[0].Name) // process first map
                {
                    ProcessMap(seedList, map, results, count);
                }
                else
                {
                    ProcessOtherMaps(map, results, count);
                }
            }
            foreach (var result in results) // find lowest
            {
                if (result < lowest) lowest = result;
            }
            OutputSolve(5, 2, lowest);
        }

        private static void ProcessMap(List<double> seedList, Map map, double[] results, int count)
        {
            Console.WriteLine($"{map.Name} Processing");
            foreach (var seed in seedList)
            {
                foreach (var mapRow in map.MapRows)
                {
                    if (mapRow.SourceRangeStart <= seed && mapRow.SourceRangeStart + mapRow.RangeLength - 1 >= seed) // find if in range.
                        results[count] = seed - mapRow.SourceRangeStart + mapRow.DesinationRangeStart;
                }
                if (results[count] == 0) // if no ranges match, assign to desination.
                    results[count] = seed;
                count++;
            }
        }

        private static void ProcessOtherMaps(Map map, double[] results, int count)
        {
            Console.WriteLine($"{map.Name} Processing");
            foreach (var result in results)
            {
                foreach (var mapRow in map.MapRows)
                {
                    if (mapRow.SourceRangeStart <= result && mapRow.SourceRangeStart + mapRow.RangeLength - 1 >= result) // find if in range.
                        results[count] = result - mapRow.SourceRangeStart + mapRow.DesinationRangeStart;
                }
                count++;
            }
        }
    }
}
