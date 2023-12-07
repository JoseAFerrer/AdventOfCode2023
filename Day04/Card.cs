namespace Day04;

public class Card
{
    public int CardId { get; set; }
    public int Copies { get; set; } = 1;
    public List<int> WinningNumbers { get; set; }
    public List<int> PlayedNumbers { get; set; }

    public Card(string cardInfo)
    {
        var gameString = cardInfo.Split(":")[0];
        var gameIdString = gameString.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray()[1];
        CardId = int.Parse(gameIdString);
        var onlyNumbers = cardInfo.Split(":")[1];
        var winningString = onlyNumbers.Split("|")[0];
        var playedString = onlyNumbers.Split("|")[1];
        WinningNumbers = winningString.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();
        PlayedNumbers = playedString.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();
    }

    public int GetPoints()
    {
        var intersectionCount = HowManyMatching();
        if (intersectionCount == 0) return 0;

        var points = Math.Pow(2, intersectionCount-1);
        return (int) points;
    }
    
    public int HowManyMatching()
    {
        var intersection = PlayedNumbers.Intersect(WinningNumbers).ToList();
        return intersection.Count;
    }
}