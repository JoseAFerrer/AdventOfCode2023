namespace Day02;

public class MinColorFinder
{
    public int MinRed { get; }
    public int MinGreen { get; }
    public int MinBlue { get; }

    public MinColorFinder(IEnumerable<string> sets)
    {
        var enumerable = sets as string[] ?? sets.ToArray();
        MinRed = FindMinOfColor(enumerable, "red");
        MinGreen = FindMinOfColor(enumerable, "green");
        MinBlue = FindMinOfColor(enumerable, "blue");
    }
    private static int FindMinOfColor(IEnumerable<string> sets, string color)
    {
        var blocks = sets.Where(y => y.Contains(color));
        var onlyNumbers = blocks.Select(x => int.Parse(x.Replace(color, "")));
        var minValidColor = onlyNumbers.Max();
        return minValidColor;
    }
}