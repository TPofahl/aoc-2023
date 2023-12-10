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
            string strengths = "AKQJT98765432";
            int rankNumber = 0;
            int result = 0;
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
            int handCount = hands.Count;
            rankNumber = handCount;
            // get ranks for each hand
            List<Hand> sortedHand = new List<Hand>();
            var cardTypes = Enum.GetValues(typeof(CardTypes));
            Array.Reverse(cardTypes);
            foreach (CardTypes cardType in cardTypes)
            {
                var handsToSort = hands.Where(hands => hands.Type == cardType.ToString()).ToList();

                int numOfHands = handsToSort.Count;
                // bubble sort
                for (int i = 0; i < numOfHands - 1; i++)
                {
                    for (int j = 0; j < numOfHands - i - 1; j++)
                    {
                        if (CompareHands(handsToSort[j], handsToSort[j + 1], strengths) > 0)
                        {
                            Hand temp = handsToSort[j];
                            handsToSort[j] = handsToSort[j + 1];
                            handsToSort[j + 1] = temp;
                        }
                    }
                }

                foreach (Hand hand in handsToSort)
                {
                    hand.Rank = rankNumber;
                    sortedHand.Add(hand);
                    rankNumber--;
                }
            }

            // Each hand wins an amount equal to its bid multiplied by its rank
            foreach (var hand in sortedHand)
            {
                result += hand.Bid * hand.Rank;
            }
            OutputSolve(7, 1, result);
        }
        public static void SolvePart2()
        {
            StreamReader streamReader = GetInputData("day7-camel-cards");
            List<Hand> hands = new List<Hand>();
            string strengths = "AKQT98765432J";
            int rankNumber = 0;
            double result = 0;
            while (!streamReader.EndOfStream)
            {
                string[] read = streamReader.ReadLine().Split(" ");
                Hand hand = new Hand()
                {
                    Cards = read[0],
                    Bid = Convert.ToInt32(read[1]),
                    Type = GetType(read[0], true),
                    Rank = 0
                };
                hands.Add(hand);
            }
            streamReader.Close();
            int handCount = hands.Count;
            rankNumber = handCount;
            // get ranks for each hand
            List<Hand> sortedHand = new List<Hand>();
            var cardTypes = Enum.GetValues(typeof(CardTypes));
            Array.Reverse(cardTypes);
            foreach (CardTypes cardType in cardTypes)
            {
                var handsToSort = hands.Where(hands => hands.Type == cardType.ToString()).ToList();

                int numOfHands = handsToSort.Count;
                // bubble sort
                for (int i = 0; i < numOfHands - 1; i++)
                {
                    for (int j = 0; j < numOfHands - i - 1; j++)
                    {
                        if (CompareHands(handsToSort[j], handsToSort[j + 1], strengths) > 0)
                        {
                            Hand temp = handsToSort[j];
                            handsToSort[j] = handsToSort[j + 1];
                            handsToSort[j + 1] = temp;
                        }
                    }
                }

                foreach (Hand hand in handsToSort)
                {
                    hand.Rank = rankNumber;
                    sortedHand.Add(hand);
                    rankNumber--;
                }
            }

            // Each hand wins an amount equal to its bid multiplied by its rank
            foreach (var hand in sortedHand)
            {
                result += hand.Bid * hand.Rank;
            }
            OutputSolve(7, 2, result);
        }
        private static int CompareHands(Hand hand1, Hand hand2, string strengths)
        {
            for (int i = 0; i < hand1.Cards.Length; i++)
            {
                int result = strengths.IndexOf(hand1.Cards[i]) - strengths.IndexOf(hand2.Cards[i]);
                if (result != 0)
                    return result;
            }
            return 0;
        }
        private static string GetType(string cards, bool cardsHaveJokers = false)
        {
            List<string> characters = new List<string>();
            int highestMatch = 1;

            foreach (char item in cards)
                characters.Add(item.ToString());

            for (int i = 1; i < characters.Count; i++)
            {
                int matches = characters.Count(x => x.Equals(characters[i]));

                if (cardsHaveJokers && characters[i] != "J")
                    matches += characters.Count(x => x.Equals("J"));

                if (matches == 5 || characters.Count(x => x.Equals("J")) == 4 && cardsHaveJokers)
                    return CardTypes.FiveKind.ToString();
                else if (matches == 4)
                    return CardTypes.FourKind.ToString();
                else if (matches == 3) // check for full house
                {
                    highestMatch = matches;
                    List<string> chars = new List<string>(characters);
                    foreach (string c in characters)
                    {
                        if (cardsHaveJokers)
                        {
                            int count = 0;
                            if (c != "J")
                            {
                                count = chars.Count(x => x.Equals(c)) + chars.Count(x => x.Equals("J"));
                            }
                            else
                            {
                                count = chars.Count(x => x.Equals(c));
                            }

                            if (count == 3)
                            {
                                chars.RemoveAll(x => x.Equals(c));
                                chars.RemoveAll(x => x.Equals("J"));
                                if (chars.Count == 2 && chars[0] == chars[1])
                                    return CardTypes.FullHouse.ToString();
                                else break;
                            }
                        }
                        else if (chars.Count(x => x.Equals(c)) == 3)
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
                            if (cardsHaveJokers && c != "J") // check for special cases with joker, or process normal for part 1.
                                pairs++;
                            else
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
