var path = "input.txt";

var readText = File.ReadAllLines(path);
var totalSum = 0;
foreach (var s in readText)
{
    var onlyNumbers = s.Where(char.IsNumber).Select(x => x.ToString());
    var firstLast = onlyNumbers.First() + onlyNumbers.Last();
    var refinedNumber = int.Parse(firstLast);
    totalSum += refinedNumber;
}

Console.WriteLine("The total sum is " + totalSum);