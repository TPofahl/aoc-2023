using static aoc_2023.Helpers;

namespace aoc_2023.Days
{
    internal static class Day8
    {
        private class Step
        {
            public string Value { get; set; }
            public string Left { get; set; }
            public string Right { get; set; }
        }
        public static void SolvePart1()
        {
            StreamReader streamReader = GetInputData("day8-haunted-wasteland");
            List<Step> steps = new List<Step>();
            string instructions = string.Empty;
            long result = 0;
            int readCount = 0;
            while (!streamReader.EndOfStream)
            {
                string read = streamReader.ReadLine();
                if (!String.IsNullOrWhiteSpace(read))
                {
                    if (readCount == 0)
                    {
                        instructions = read;
                        readCount++;
                    }
                    else
                    {
                        string[] values = read.Split(" = ");
                        string path = values[1].Replace("(","").Replace(")", "");
                        string[] paths = path.Split(", ");
                        Step step = new Step()
                        {
                            Value = values[0],
                            Left = paths[0],
                            Right = paths[1]
                        };
                        steps.Add(step);
                    }
                }
            }
            streamReader.Close();

            string currentPath = steps.FirstOrDefault(x => x.Value == "AAA").Value;
            bool found = false;

            while (currentPath != "ZZZ")
            {
                foreach (char direction in instructions)
                {
                    if (found)
                    {
                        break;
                    }
                    else
                    {
                        Step step = steps.FirstOrDefault(x => x.Value == currentPath);
                        if (direction.ToString() == "L")
                            currentPath = step.Left;
                        else
                            currentPath = step.Right;
                        if (currentPath == "ZZZ")
                            found = true;
                        result++;
                    }
                }
                if (found) break;
            }
            OutputSolve(8, 1, result);
        }

        public static void SolvePart2()
        {
            StreamReader streamReader = GetInputData("day8-haunted-wasteland");
            List<Step> steps = new List<Step>();
            string instructions = string.Empty;
            List<long> results = new List<long>();
            long result = 0;
            int readCount = 0;

            while (!streamReader.EndOfStream)
            {
                string read = streamReader.ReadLine();
                if (!String.IsNullOrWhiteSpace(read))
                {
                    if (readCount == 0)
                    {
                        instructions = read;
                        readCount++;
                    }
                    else
                    {
                        string[] values = read.Split(" = ");
                        string path = values[1].Replace("(", "").Replace(")", "");
                        string[] paths = path.Split(", ");
                        Step step = new Step()
                        {
                            Value = values[0],
                            Left = paths[0],
                            Right = paths[1]
                        };
                        steps.Add(step);
                    }
                }
            }
            streamReader.Close();

            List<string> currentPaths = steps
                .Where(x => x.Value.EndsWith("A"))
                .Select(x => x.Value)
                .ToList();
            bool found = false;

            foreach (var currentPath in currentPaths)
            {
                string selectedPath = currentPath;
                while (!selectedPath.EndsWith("Z"))
                {
                    foreach (char direction in instructions)
                    {
                        if (found)
                        {
                            break;
                        }
                        else
                        {
                            Step step = steps.FirstOrDefault(x => x.Value == selectedPath);
                            if (direction.ToString() == "L")
                                selectedPath = step.Left;
                            else
                                selectedPath = step.Right;
                            if (selectedPath.EndsWith("Z"))
                                found = true;
                            result++;
                        }
                    }
                    if (found) break;
                }
                results.Add(result);
                found = false;
                result = 0;
            }

            long leastCommonMultiple = results[0];

            for (int i = 1; i < results.Count; i++)
            {
                long firstNumber = leastCommonMultiple;
                long secondNumber = results[i];

                if (firstNumber != 0 && secondNumber != 0) // cannot divide by 0 in numerator.
                {
                    while (secondNumber != 0)
                    {
                        long temp = secondNumber;
                        secondNumber = firstNumber % secondNumber;
                        firstNumber = temp;
                    }
                    leastCommonMultiple = leastCommonMultiple * results[i] / firstNumber;
                }
            }

            OutputSolve(8, 2, leastCommonMultiple);
        }
    }
}
