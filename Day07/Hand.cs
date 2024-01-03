namespace Day07;


public class Hand
{
    public string Cards { get; set; } = null!;
    public int Rank { get; set; }
    public HandType Type { get; set; }
    public int Bid { get; set; }
}

public enum HandType
{
    FiveOfAKind,
    FourOfAKind,
    FullHouse,
    ThreeOfAKind,
    TwoPairs,
    OnePair,
    HighCard
}

