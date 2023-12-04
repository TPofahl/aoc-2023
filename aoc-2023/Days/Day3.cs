using System.Text.RegularExpressions;
using static aoc_2023.Helpers;

namespace aoc_2023.Days
{
    internal class Day3
    {
        public void SolvePart1()
        {
            StreamReader streamReader = GetInputData("day3-engine-schematic");
            List<(int Row, int Number, int Index)> rows = new List<(int, int, int)>();
            List<(int Row, string Character, int Index)> characters = new List<(int, string, int)>();
            int rowNumber = 0;
            int sum = 0;

            while (!streamReader.EndOfStream)
            {
                string? read = streamReader.ReadLine();
                string numberRegexPattern = @"\d+";
                string characterRegexPattern = @"[*+$@%#_!&=/-]+";
                rowNumber++;
                if (!string.IsNullOrEmpty(read))
                {
                    foreach (Match number in Regex.Matches(read, numberRegexPattern))
                    {
                        rows.Add((rowNumber, Convert.ToInt32(number.Value), number.Index));
                    }
                    foreach (Match character in Regex.Matches(read, characterRegexPattern))
                    {
                        characters.Add((rowNumber, character.Value, character.Index));
                    }
                }

                if (rowNumber == 2)
                {
                    var firstRowCharacters = characters.Where(x => x.Row == 1).ToList();
                    var secondRowCharacters = characters.Where(x => x.Row == 2).ToList();
                    var firstRowNumbers = rows.Where(x => x.Row == 1).ToList();
                    var tempRowNumbers = rows.Where(x => x.Row == 1).ToList();
                    foreach (var number in firstRowNumbers)
                    {
                        // check for special characters next to numbers in the first row.
                        foreach (var character in firstRowCharacters)
                        {
                            if (character.Index == number.Index - 1 || character.Index == number.Index + number.Number.ToString().Length + 1)
                            {
                                if (tempRowNumbers.Contains(number))
                                {
                                    sum += number.Number;
                                    tempRowNumbers.Remove(number);
                                    break;
                                }
                            }
                        }
                        if (tempRowNumbers.Count <= 0) break;
                        // check for special characters in second row.
                        foreach (var character in secondRowCharacters)
                        {
                            if (number.Index - 1 <= character.Index && number.Index + number.Number.ToString().Length + 2 >= character.Index)
                            {
                                if (tempRowNumbers.Contains(number))
                                {
                                    sum += number.Number;
                                    tempRowNumbers.Remove(number);
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (rowNumber > 2)
                {
                    // check for valid numbers, based on special characters above/between/below the previous read row.
                    var currentRowNumbers = rows.Where(x => x.Row == rowNumber - 1).ToList();
                    var tempRowNumbers = rows.Where(x => x.Row == rowNumber - 1).ToList();

                    var upperRowCharacters = characters.Where(x => x.Row == rowNumber - 2).ToList();
                    var currentRowCharacters = characters.Where(x => x.Row == rowNumber - 1).ToList();
                    var lowerRowCharacters = characters.Where(x => x.Row == rowNumber).ToList();
                   
                    foreach (var number in currentRowNumbers)
                    {
                        // upper.
                        foreach (var character in upperRowCharacters)
                        {
                            if (number.Index - 1 <= character.Index && number.Index + number.Number.ToString().Length >= character.Index)
                            {
                                if (tempRowNumbers.Contains(number))
                                {
                                    sum += number.Number;
                                    tempRowNumbers.Remove(number);
                                    break;
                                }
                            }
                        }
                        if (tempRowNumbers.Count <= 0) break;
                        // middle.
                        foreach (var character in currentRowCharacters)
                        {
                            if (character.Index == number.Index - 1 || character.Index == number.Index + number.Number.ToString().Length)
                            {
                                if (tempRowNumbers.Contains(number))
                                {
                                    sum += number.Number;
                                    tempRowNumbers.Remove(number);
                                    break;
                                }
                            }
                        }
                        if (tempRowNumbers.Count <= 0) break;
                        // lower.
                        foreach (var character in lowerRowCharacters)
                        {
                            if (number.Index - 1 <= character.Index && number.Index + number.Number.ToString().Length >= character.Index)
                            {
                                if (tempRowNumbers.Contains(number))
                                {
                                    sum += number.Number;
                                    tempRowNumbers.Remove(number);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            // check for valid numbers in the last row.
            var secondToLastRowCharacters = characters.Where(x => x.Row == rowNumber - 1).ToList();
            var lastRowCharacters = characters.Where(x => x.Row == rowNumber).ToList();
            var lastRowNumbers = rows.Where(x => x.Row == rowNumber).ToList();
            var lastTempRowNumbers = rows.Where(x => x.Row == rowNumber).ToList();
            foreach (var number in lastRowNumbers)
            {
                // check for special characters next to numbers in the second to last row.
                foreach (var character in secondToLastRowCharacters)
                {
                    if (number.Index - 1 <= character.Index && number.Index + number.Number.ToString().Length + 2 >= character.Index)
                    {
                        if (lastTempRowNumbers.Contains(number))
                        {
                            sum += number.Number;
                            lastTempRowNumbers.Remove(number);
                            break;
                        }
                    }
                }
                if (lastTempRowNumbers.Count <= 0) break;
                // check for special characters in last row.
                foreach (var character in lastRowCharacters)
                {
                    if (character.Index == number.Index - 1 || character.Index == number.Index + number.Number.ToString().Length)
                    {
                        if (lastTempRowNumbers.Contains(number))
                        {
                            sum += number.Number;
                            lastTempRowNumbers.Remove(number);
                            break;
                        }
                    }
                }
            }

            streamReader.Close();
            OutputSolve(3, 1, sum);
        }
    }
}
