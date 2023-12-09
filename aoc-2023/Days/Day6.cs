using System.ComponentModel;
using System.Text.RegularExpressions;
using static aoc_2023.Helpers;

namespace aoc_2023.Days
{
    internal static class Day6
    {
        public static void SolvePart1()
        {
            StreamReader streamReader = GetInputData("day6-wait-for-it");
            List<int> times = new List<int>();
            List<int> distances = new List<int>();
            int result = 1;
            int count = 0;
            while (!streamReader.EndOfStream)
            {
                foreach (Match number in Regex.Matches(streamReader.ReadLine(), @"\d+"))
                {
                    if (count == 0)
                        times.Add(Convert.ToInt32(number.Value));
                    else
                        distances.Add(Convert.ToInt32(number.Value));
                }
                count++;
            }
            streamReader.Close();
            for (int i = 0; i < times.Count; i++) // process ways to win.
            {
                count = 0;
                for (int j = 0; j < times[i] + 1; j++)
                    if ((times[i] - j) * j > distances[i]) count++;
                result *= count;
            }
            OutputSolve(6, 1, result);
        }

        public static void SolvePart2()
        {
            StreamReader streamReader = GetInputData("day6-wait-for-it");
            List<long> times = new List<long>();
            List<long> distances = new List<long>();
            long result = 1;
            long count = 0;
            while (!streamReader.EndOfStream)
            {
                string num = "";
                foreach (Match number in Regex.Matches(streamReader.ReadLine(), @"\d+"))
                    num += number.Value;
                if (count == 0)
                    times.Add(Convert.ToInt64(num));
                else
                    distances.Add(Convert.ToInt64(num));
                count++;
            }
            streamReader.Close();
            for (int i = 0; i < times.Count; i++) // process ways to win.
            {
                count = 0;
                for (int j = 0; j < times[i] + 1; j++)
                    if ((times[i] - j) * j > distances[i]) count++;
                result *= count;
            }
            OutputSolve(6, 2, result);
        }
    }
}
