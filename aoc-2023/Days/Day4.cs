using System.Text.RegularExpressions;
using static aoc_2023.Helpers;

namespace aoc_2023.Days
{

    internal static class Day4
    {
        internal class Card()
        {
            public int CardNumber {  get; set; }
            public List<string> Numbers { get; set; } = new List<string>();
            public List<string> WinningNumbers { get; set; } = new List<string>();
        }
        public static void SolvePart1()
        {
            StreamReader streamReader = GetInputData("day4-scratchcards");
            string pattern = @"\d+";
            int sum = 0;

            while (!streamReader.EndOfStream)
            {
                string[]? read = streamReader.ReadLine().Split(":");
                string[] numbers = read[1].Split(" | ");
                string numbersYouHave = numbers[0];
                string winningNumbers = numbers[1];
                int cardTotal = 0;

                foreach (Match number in Regex.Matches(numbersYouHave, pattern))
                {
                    foreach (Match winningNumber in Regex.Matches(winningNumbers, pattern))
                    {
                        if (number.Value == winningNumber.Value)
                        {
                            if (cardTotal == 0) 
                                cardTotal = 1;
                            else if (cardTotal > 0)
                                cardTotal *= 2;
                        }
                    }
                }
                sum += cardTotal;
            }

            streamReader.Close();
            OutputSolve(4, 1, sum);
        }

        public static void SolvePart2()
        {
            StreamReader streamReader = GetInputData("day4-scratchcards");
            List<Card> cards = new List<Card>();
            string pattern = @"\d+";

            while (!streamReader.EndOfStream)
            {
                Card card = new Card();
                string[] read = streamReader.ReadLine().Split(":");
                string[] numbers = read[1].Split(" | ");
                Match cardNumber = Regex.Match(read[0], pattern);

                if (cardNumber.Success)
                    card.CardNumber = Convert.ToInt32(cardNumber.Value);

                foreach (Match number in Regex.Matches(numbers[0], pattern))
                {
                    card.Numbers.Add(number.Value);
                }
                foreach (Match number in Regex.Matches(numbers[1], pattern))
                {
                    card.WinningNumbers.Add(number.Value);
                }
                cards.Add(card);
            }
            streamReader.Close();

            int startingCardCount = cards.Count;
            for (int i = 1; i < startingCardCount + 1; i++)
            {

                List<Card> currentCards = cards.Where(x => x.CardNumber == i).ToList();
                int matchingNumbers = 0;

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"num of {i} cards: {currentCards.Count}");
                Console.ResetColor();

                foreach (Card card in currentCards)
                {
                    foreach (string number in card.Numbers)
                    {
                        foreach (string winningNumber in card.WinningNumbers)
                        {
                            if (number == winningNumber)
                                matchingNumbers++;
                        }
                    }

                    if (matchingNumbers > 0)
                    {
                        if (i + 1 + matchingNumbers > startingCardCount)
                            Console.WriteLine($"Card {i} has {matchingNumbers} matches, duplicating cards {i + 1} - {startingCardCount}");
                        else
                            Console.WriteLine($"Card {i} has {matchingNumbers} matches, duplicating cards {i + 1} - {i + 1 + matchingNumbers}");
                    }

                    // After getting total number of matches for 1 card of a particular number, make the duplicates.
                    for (int j = 1; j <= matchingNumbers; j++)
                    {
                        if (i == startingCardCount - 1)
                        {
                            Card lastCard = cards.Where(x => x.CardNumber == i + j).First();
                            cards.Add(lastCard);
                        }
                        if (i + j >= startingCardCount)
                            break;
                        Card newCard = cards.Where(x => x.CardNumber == i + j).First();
                        cards.Add(newCard);
                    }
                    matchingNumbers = 0;
                }
            }

            OutputSolve(4, 2, cards.Count);
        }
    }
}
