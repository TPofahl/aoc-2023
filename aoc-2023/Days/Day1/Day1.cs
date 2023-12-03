using static aoc_2023.Helpers;

namespace aoc_2023.Days.Day1
{
    internal class Day1
    {
        public void SolvePart1()
        {
            StreamReader streamReader = GetInputDataPath("day1-calibration");

            int sum = 0;

            while (!streamReader.EndOfStream)
            {
                string? read = streamReader.ReadLine();
                if (!string.IsNullOrEmpty(read))
                {
                    string numbers = new string(read.Where(Char.IsDigit).ToArray());
                    sum += Convert.ToInt32(numbers.First().ToString() + numbers.Last().ToString());
                }
            }

            OutputSolve(1, 1, sum);
        }

        public void SolvePart2()
        {
            StreamReader streamReader = GetInputDataPath("day1-calibration");

            Dictionary<string, int> validNumbers = new Dictionary<string, int>()
            {
                {"one", 1}, {"two", 2}, {"three", 3}, {"four", 4}, {"five", 5}, {"six", 6}, {"seven", 7}, {"eight", 8}, {"nine", 9}
            };
            int sum = 0;

            while (!streamReader.EndOfStream)
            {
                string? read = streamReader.ReadLine();

                if (!string.IsNullOrEmpty(read))
                {
                    string firstNum = string.Empty;
                    string lastNum = string.Empty;
                    int firstNumPos = 1000000;
                    int lastNumPos = -2;
                    
                    foreach (string number in validNumbers.Keys)
                    {
                        // First check word values.
                        if (read.Contains(number))
                        {
                            int posFirst = read.IndexOf(number);
                            int posLast = read.LastIndexOf(number);
                            if (posFirst < firstNumPos || posLast < firstNumPos)
                            {
                                firstNum = validNumbers[number].ToString();
                                firstNumPos = posFirst;
                            }
                            if (posFirst > lastNumPos || posLast > lastNumPos)
                            {
                                lastNum = validNumbers[number].ToString();
                                lastNumPos = posLast;
                            }
                        }
                    }
                    // Check number values.
                    foreach (int number in validNumbers.Values)
                    {
                        string num = number.ToString();
                        if (read.Contains(num))
                        {
                            int posFirst = read.IndexOf(num);
                            int posLast = read.LastIndexOf(num);
                            if (posFirst < firstNumPos || posLast < firstNumPos)
                            {
                                firstNum = num;
                                firstNumPos = posFirst;
                            }
                            if (posFirst > lastNumPos || posLast > lastNumPos)
                            {
                                lastNum = num;
                                lastNumPos = posLast;
                            }
                        }
                    }

                    sum += Convert.ToInt32(firstNum + lastNum);
                }
            }

            OutputSolve(1, 2, sum);
        }
    }
}
