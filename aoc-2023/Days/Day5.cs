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
            public float DesinationRangeStart { get; set; }
            public float SourceRangeStart { get; set; }
            public float RangeLength { get; set; }
        }

        public static void SolvePart1()
        {
            StreamReader streamReader = GetInputData("day5-almanac");
            List<Map> mapList = new List<Map>();
            List<float> seedList = new List<float>();
            Map newMap = new Map();
            float lowest = 1000000000000000;

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
            string firstMap = mapList[0].Name;
            float[] results = new float[seedList.Count];
            foreach (var map in mapList)
            {
                int count = 0;
                // process first map
                if (map.Name == firstMap)
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
                    OutputLine(map, results);
                }
                // process all other maps.
                else
                {
                    float[] currentResults = new float[seedList.Count];
                    for (int i = 0; i < results.Length; i++)
                    {
                        currentResults[i] = results[i];
                    }
                    foreach (var currentResult in currentResults)
                    {
                        foreach (var mapRow in map.MapRows)
                        {
                            // find if in range.
                            if (mapRow.SourceRangeStart <= currentResult && mapRow.SourceRangeStart + mapRow.RangeLength - 1 >= currentResult)
                                currentResults[count] = currentResult - mapRow.SourceRangeStart + mapRow.DesinationRangeStart;
                        }
                        // if no ranges match, assign to desination.
                        if (results[count] != currentResults[count])
                            results[count] = currentResults[count];
                        count++;
                    }
                    OutputLine(map, results);
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

        private static void OutputLine(Map map, float[] results)
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
