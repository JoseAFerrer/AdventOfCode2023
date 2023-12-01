var path = "input.txt";

var readText = File.ReadAllLines(path);
var aggregatedText = string.Join(",", readText);

var refinedText = aggregatedText
                            .Replace("one", "one1one")
                            .Replace("two", "two2two")
                            .Replace("three", "three3three")
                            .Replace("four", "four4four")
                            .Replace("five", "five5five")
                            .Replace("six", "six6six")
                            .Replace("seven", "seven7seven")
                            .Replace("eight", "eight8eight")
                            .Replace("nine", "nine9nine");

var refinedLines = refinedText.Split(",");

var totalSum = 0;
foreach (var s in refinedLines)
{
    var onlyNumbers = s.Where(char.IsNumber).Select(x => x.ToString());
    var firstLast = onlyNumbers.First() + onlyNumbers.Last();
    var refinedNumber = int.Parse(firstLast);
    totalSum += refinedNumber;
}

Console.WriteLine("The total sum is " + totalSum);