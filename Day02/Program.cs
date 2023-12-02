// See https://aka.ms/new-console-template for more information

using Day02;

var redMax = 12;
var greenMax = 13;
var blueMax = 14;
var pathToExample = "Inputs/example1.txt";
var pathToInput = "Inputs/input.txt";

var exampleSumOfValidIds = FindSumOfValidIdsForFile(pathToExample, redMax, greenMax, blueMax);
var realSumOfValidIds = FindSumOfValidIdsForFile(pathToInput, redMax, greenMax, blueMax);

Console.WriteLine("The example sum of valid ids is: " + exampleSumOfValidIds);
Console.WriteLine("The real sum of valid ids is: " + realSumOfValidIds);

var exampleSumOfPowers = FindPowerOfSetsForFile(pathToExample);
var realSumOfPowers = FindPowerOfSetsForFile(pathToInput);

Console.WriteLine("The example sum of powers is: " + exampleSumOfPowers);
Console.WriteLine("The real sum of powers is: " + realSumOfPowers);

//
//
//

int FindSumOfValidIdsForFile(string pathToInput1, int redMax1, int greenMax1, int blueMax1)
{
    var lines = File.ReadAllLines(pathToInput1);
    var games = lines.Select(line =>
    {
        var x = line.Replace(" ", "");

        var handfulSets = FilterOutChars(x);
        var areColorsValid = new ColorValidator(handfulSets, redMax1, greenMax1, blueMax1).AreColorsValid;

        return new Game
        {
            Id = GetGameIdFromLine(x),
            IsValid = areColorsValid
        };
    });

    var sumOfValidIds1 = 0;
    foreach (var game in games.Where(x => x.IsValid))
    {
        sumOfValidIds1 += game.Id;
    }

    return sumOfValidIds1;
}

int FindPowerOfSetsForFile(string pathToInput1)
{
    var lines = File.ReadAllLines(pathToInput1);
    var games = lines.Select(line =>
    {
        var x = line.Replace(" ", "");

        var handfulSets = FilterOutChars(x);
        var minColorFinder = new MinColorFinder(handfulSets);

        return new Game
        {
            Id = GetGameIdFromLine(x),
            Power = minColorFinder.MinRed * minColorFinder.MinGreen * minColorFinder.MinBlue
        };
    });

    var sumOfPowers = 0;
    foreach (var game in games)
    {
        sumOfPowers += game.Power;
    }

    return sumOfPowers;
}

int GetGameIdFromLine(string s)
{
    var gameWithId = s.Split(":")[0];
    var i = int.Parse(gameWithId.Replace("Game", ""));
    return i;
}

IEnumerable<string> FilterOutChars(string s)
{
    var handfulsLine = s.Split(":")[1];
    var handfuls = handfulsLine.Split(";");
    var enumerable = handfuls.SelectMany(y => y.Split(","));
    return enumerable;
}