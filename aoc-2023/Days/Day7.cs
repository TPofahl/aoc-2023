using System.Diagnostics;
using System.Net;
using System.Runtime.ExceptionServices;
using static aoc_2023.Helpers;

namespace aoc_2023.Days
{
    internal static class Day7
    {
        private class Hand
        {
            public string Cards { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;    
            public int Rank { get; set; }
            public int Bid { get; set; }
        }

        public static void SolvePart1()
        {
            StreamReader streamReader = GetInputData("day7-camel-cards");
            List<Hand> hands = new List<Hand>();

            while (!streamReader.EndOfStream)
            {
                string[] read = streamReader.ReadLine().Split(" ");
                Hand hand = new Hand()
                { 
                    Cards = read[0], 
                    Bid = Convert.ToInt32(read[1]),
                    Type = GetType(read[0]),
                    Rank = 0
                };
                hands.Add(hand);
            }
            streamReader.Close();
            // get ranks for each hand
            List<Hand> sortedHand = new List<Hand>();
            string strengths = "AKQJT98765432";
            var cardTypes = Enum.GetValues(typeof(CardTypes));
            Array.Reverse(cardTypes);
            foreach (CardTypes cardType in cardTypes)
            {
                var handsToSort = hands.Where(hands => hands.Type == cardType.ToString()).ToList();
                // bubble sort.

                for (int i = 0; i < handsToSort.Count - 1; i++)
                {
                    for (int j = 0; j < handsToSort.Count - i - 1; j++)
                    {
                        if (hands[j].Cards[0] > hands[j + 1].Cards[0])
                        {
                            Hand tempHand = hands[j];
                            hands[j] = hands[j + 1];
                            hands[j + 1] = tempHand;
                        }
                    }
                }


                foreach (Hand hand in handsToSort)
                    sortedHand.Add(hand);
            }

            // Each hand wins an amount equal to its bid multiplied by its rank
            OutputSolve(7, 1, -1);
        }

        private static string GetType(string cards)
        {
            List<string> characters = new List<string>();
            int highestMatch = 1;

            foreach (char item in cards)
                characters.Add(item.ToString());

            for (int i = 1; i < characters.Count; i++)
            {
                int matches = characters.Count(x => x.Equals(characters[i]));
                if (matches == 5)
                    return CardTypes.FiveKind.ToString();
                else if (matches == 4)
                    return CardTypes.FourKind.ToString();
                else if (matches == 3) // check for full house
                {
                    highestMatch = matches;
                    List<string> chars = new List<string>(characters);
                    foreach (string c in characters)
                    {
                        if (chars.Count(x => x.Equals(c)) == 3)
                        {
                            chars.RemoveAll(x => x.Equals(c));
                            if (chars.Count == 2 && chars[0] == chars[1])
                                return CardTypes.FullHouse.ToString();
                            else break;
                        }
                    }
                }
                else if (matches == 2 && matches > highestMatch) // check for two pair.
                {
                    List<string> chars = new List<string>(characters);
                    int pairs = 0;
                    highestMatch = matches;
                    foreach (string c in characters)
                    {
                        if (chars.Count(x => x.Equals(c)) == 2)
                        {
                            chars.RemoveAll(x => x.Equals(c));
                            pairs++;
                        }
                        if (pairs == 2)
                            return CardTypes.TwoPair.ToString();
                    }
                }
            }
            return highestMatch switch
            {
                4 => CardTypes.FourKind.ToString(),
                3 => CardTypes.ThreeKind.ToString(),
                2 => CardTypes.OnePair.ToString(),
                _ => CardTypes.HighCard.ToString()
            };
        }

        private enum CardTypes
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeKind,
            FullHouse,
            FourKind,
            FiveKind
        }
    }
}
