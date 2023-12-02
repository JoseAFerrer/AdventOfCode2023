namespace Day02;

public class ColorValidator
{
    private bool IsRedValid { get; set; }
    private bool IsGreenValid { get; set; }
    private bool IsBlueValid { get; set; }
    public bool AreColorsValid => IsRedValid && IsGreenValid && IsBlueValid;

    public ColorValidator(IEnumerable<string> sets, int maxRed, int maxGreen, int maxBlue)
    {
        IsRedValid = FindIfValidColor(sets, "red", maxRed);
        IsGreenValid = FindIfValidColor(sets, "green", maxGreen);
        IsBlueValid = FindIfValidColor(sets, "blue", maxBlue);
    }
    private static bool FindIfValidColor(IEnumerable<string> sets, string color, int max)
    {
        var blocks = sets.Where(y => y.Contains(color));
        var onlyNumbers = blocks.Select(x => int.Parse(x.Replace(color, "")));
        var isColorInvalid = onlyNumbers.Any(x => x > max);
        return !isColorInvalid;
    }
}