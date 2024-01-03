using System.Diagnostics;

namespace Day07;

public class HandCalculator
{
    private Hand[] Hands { get; set; }
    private List<Hand> OrderedHands { get; set; } = new List<Hand>();
    private IComparer<string> CardOrderer { get; set; } = new CardComparer();
    private IComparer<string> CardOrdererWithJokers { get; set; } = new CardComparer(true);
    public HandCalculator(string path)
    {
        var lines = AocHelpers.StringHelpers.GetLinesFromFileInPath(path);
        Hands = lines.Select(x =>
        {
            var separated = x.Split(" ");
            return new Hand()
            {
                Cards = separated.First(),
                Bid = int.Parse(separated.Last()),
                Rank = Array.IndexOf(lines, x)+1,
                Type = HandType.HighCard
            };
        }).ToArray();
    }

    public int GetTotalWinnings()
    {
        FindTypeForEachHand();
        OrderByBlocks();
        AssignRightRanks();
        return FindWinnings();
    }
    
    public int GetTotalWinningsWithJokers()
    {
        FindTypeForEachHand(true);
        OrderByBlocks(true);
        AssignRightRanks();
        return FindWinnings();
    }

    private int FindWinnings()
    {
        return OrderedHands.Sum(x => x.Bid * x.Rank);
    }
    
    private void AssignRightRanks()
    {
            for (var i = 0; i < OrderedHands.Count; i++)
            {
                OrderedHands[i].Rank = i+1;
            }
    }
    
    private void OrderByBlocks(bool jokers = false)
    {
        var comparerToUse = jokers
            ? CardOrdererWithJokers
            : CardOrderer;
        var blocks = Hands.GroupBy(x => x.Type);
        var orderedBlocks = new Dictionary<HandType, IEnumerable<Hand>>();
        foreach (var block in blocks)
        {
            var group = block.Select(x => x);
            var ordered = group.OrderBy(x => x.Cards, comparerToUse); 
            orderedBlocks.Add(block.Key, ordered);
        }
        
        SaveOrderedHands(orderedBlocks, HandType.HighCard);
        SaveOrderedHands(orderedBlocks, HandType.OnePair);
        SaveOrderedHands(orderedBlocks, HandType.TwoPairs);
        SaveOrderedHands(orderedBlocks, HandType.ThreeOfAKind);
        SaveOrderedHands(orderedBlocks, HandType.FullHouse);
        SaveOrderedHands(orderedBlocks, HandType.FourOfAKind);
        SaveOrderedHands(orderedBlocks, HandType.FiveOfAKind);
    }
    private void SaveOrderedHands(Dictionary<HandType, IEnumerable<Hand>> orderedBlocks, HandType handType)
    {
        var couldGetValue = orderedBlocks.TryGetValue(handType, out var hands);
        if (couldGetValue)
        {
            OrderedHands.AddRange(hands!);
        }
    }

    private class CardComparer : IComparer<string>
    {
        private bool Jokers { get; }
        public CardComparer(bool jokers = false)
        {
            Jokers = jokers;
        }
        
        public int Compare(string? x, string? y)
        {
            for (var i = 0; i < 5; i++)
            {
                var xValue = GetValueForCard(x![i], Jokers);
                var yValue = GetValueForCard(y![i], Jokers);
                
                if (xValue > yValue)
                {
                    return 1;
                }

                if (yValue > xValue)
                {
                    return -1;
                }
            }

            return 0;
        }
        private static int GetValueForCard(char x, bool usingJokers)
        {
            var xvalue = x switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 11,
                'T' => 10,
                _ => 0
            };
            if (xvalue == 0) xvalue = int.Parse(x.ToString());
            if (usingJokers && xvalue == 11) xvalue = 1;
            return xvalue;
        }
    }

    private void FindTypeForEachHand(bool usingJokers = false)
    {
        foreach (var hand in Hands)
        {
            ClassifyHand(hand, usingJokers);
        }
    }
    
    private static void ClassifyHand(Hand hand, bool usingJokers)
    {
        if (usingJokers && hand.Cards.Contains('J'))
        {
            ClassifyHandWithJokers(hand);
            return;
        }
        
        var cardsAsChars = hand.Cards.ToArray();
        var grouped = cardsAsChars.GroupBy(x => x).ToArray();
        var nrOfGroups = grouped.Length;

        switch (nrOfGroups)
        {
            case 5:
                return;
            case 1:
                hand.Type = HandType.FiveOfAKind;
                return;
            case 2:
                hand.Type = grouped.Any(x => x.Count() == 4)
                    ? HandType.FourOfAKind
                    : HandType.FullHouse;
                return;
            case 3:
                hand.Type = grouped.Any(x => x.Count() == 3)
                    ? HandType.ThreeOfAKind
                    : HandType.TwoPairs;
                return;
            case 4:
                hand.Type = HandType.OnePair;
                return;
            default:
                throw new UnreachableException();
        }
    }

    private static void ClassifyHandWithJokers(Hand hand)
    {
        var cardsAsChars = hand.Cards.ToArray();
        var grouped = cardsAsChars.GroupBy(x => x).ToArray();
        var nrOfGroups = grouped.Length;

        switch (nrOfGroups)
        {
            case 5:
                hand.Type = HandType.OnePair;
                return;
            case 1:
                hand.Type = HandType.FiveOfAKind;
                return;
            case 2:
                hand.Type = HandType.FiveOfAKind;
                return;
            case 4:
                hand.Type = HandType.ThreeOfAKind;
                return;
            case 3:
            {
                var anyGroupOfThree = grouped.Any(x => x.Count() == 3);
                if (anyGroupOfThree)
                {
                    hand.Type = HandType.FourOfAKind;
                    return;
                }

                if (grouped.FirstOrDefault(x => x.Key == 'J')!.Count() == 2)
                {
                    hand.Type = HandType.FourOfAKind;
                    return;
                }
            
                hand.Type = HandType.FullHouse;
                return;
            }
            default:
                throw new UnreachableException();
        }
    }
}
