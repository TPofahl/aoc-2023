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
            List<string> readList = new List<string>();
            List<Map> mapList = new List<Map>();
            List<float> seedList = new List<float>();
            Map map = new Map();

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
                        if (read != map.Name)
                        {
                            if (map.Name != string.Empty)
                                mapList.Add(map);
                            map = new Map();
                            map.Name = read;
                        }
                    }
                    else
                    {
                        MapRow mapRow = new MapRow();
                        var row = read.Split(" ");
                        mapRow.DesinationRangeStart = Convert.ToInt64(row[0]);
                        mapRow.SourceRangeStart = Convert.ToInt64(row[1]);
                        mapRow.RangeLength = Convert.ToInt64(row[2]);
                        map.MapRows.Add(mapRow);
                    }
                }
            }
            streamReader.Close();
            mapList.Add(map);

            foreach (var row in mapList)
            {
                foreach (var entry in row.MapRows)
                {
                    Console.WriteLine($"Desination Range Start: {entry.DesinationRangeStart}, Source Range Start: {entry.SourceRangeStart}, Range Length: {entry.RangeLength}, test: {entry.DesinationRangeStart > entry.SourceRangeStart}");
                }
            }
        }
    }
}
