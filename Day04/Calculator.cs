namespace Day04;

public class Calculator
{
    public int CalculatePointsForInput(string path)
    {
        var parsedInput = File.ReadAllLines(path);

        var cards = parsedInput.Select(x => new Card(x));
        var points = cards.Sum(x => x.GetPoints());
        return points;
    }
    
    public int CalculateScratchCardsAmount(string path)
    {
        var parsedInput = File.ReadAllLines(path);

        var originalCards = parsedInput.Select(x => new Card(x));
        var finalPileOfCards = originalCards.Select(x => x).ToArray();

        foreach (var currentCard in originalCards)
        {
            var currentId = currentCard.CardId;
            var matching = currentCard.HowManyMatching();
            var finalCopiesOfCurrentCard = finalPileOfCards[currentId-1].Copies;
            for (var i = currentId; i < currentId + matching; i++)
            {
                    finalPileOfCards[i].Copies += finalCopiesOfCurrentCard;
            }
        }


        var totalAmount = finalPileOfCards.Sum(x => x.Copies);
        return totalAmount;
    }
}