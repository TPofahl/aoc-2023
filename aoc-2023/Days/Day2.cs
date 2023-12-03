using static aoc_2023.Helpers;

namespace aoc_2023.Days
{
    internal class Day2
    {
        public void SolvePart1()
        {
            StreamReader streamReader = GetInputData("day2-cube-conundrum");

            int currentGame = 0;
            int possibleRed = 12;
            int possibleGreen = 13;
            int possibleBlue = 14;
            int sum = 0;

            while (!streamReader.EndOfStream)
            {
                string? read = streamReader.ReadLine();
                if (!string.IsNullOrEmpty(read))
                {
                    string[] gameData = read.Split(":");
                    currentGame = Convert.ToInt32(new string(gameData[0].Where(char.IsDigit).ToArray()));
                    string games = gameData[1].Replace(";", ",");
                    string[] cubes = games.Split(",");
                    bool gameIsValid = true;

                    foreach (string cube in cubes)
                    {
                        if (gameIsValid)
                        {
                            int amount = Convert.ToInt32(new string(cube.Where(char.IsDigit).ToArray()));

                            if (cube.Contains("red") && amount > possibleRed)
                            {
                                gameIsValid = false;
                            }
                            if (cube.Contains("green") && amount > possibleGreen)
                            {
                                gameIsValid = false;
                            }
                            if (cube.Contains("blue") && amount > possibleBlue)
                            {
                                gameIsValid = false;
                            }
                        }
                    }
                    if (gameIsValid)
                    {
                        sum += currentGame;
                    }
                }
            }

            OutputSolve(2, 1, sum);
        }

        public void SolvePart2()
        {
            StreamReader streamReader = GetInputData("day2-cube-conundrum");
            int sum = 0;

            while (!streamReader.EndOfStream)
            {
                string? read = streamReader.ReadLine();
                if (!string.IsNullOrEmpty(read))
                {
                    string[] gameData = read.Split(":");
                    string[] cubes = gameData[1].Replace(";", ",").Split(",");
                    int fewestRed = 0;
                    int fewestGreen = 0;
                    int fewestBlue = 0;

                    foreach (string cube in cubes)
                    {
                            int amount = Convert.ToInt32(new string(cube.Where(char.IsDigit).ToArray()));

                            if (cube.Contains("red") && amount > fewestRed)
                            {
                                fewestRed = amount;
                            }
                            if (cube.Contains("green") && amount > fewestGreen)
                            {
                                fewestGreen = amount;
                            }
                            if (cube.Contains("blue") && amount > fewestBlue)
                            {
                                fewestBlue = amount;
                            }
                    }

                    sum += (fewestRed * fewestGreen * fewestBlue);
                }
            }

            OutputSolve(2, 2, sum);
        }
    }
}
