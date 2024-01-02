namespace AocHelpers;

public static class StringHelpers
{
    public static long[] GetNumbersFromOneString(string numbersAsStrings)
    {
        var numsAsArrayOfStrings = numbersAsStrings.Trim().Split(" ").Where(x => !string.IsNullOrWhiteSpace(x));
        var nums = numsAsArrayOfStrings.Select(long.Parse);
        return nums.ToArray();
    }
}