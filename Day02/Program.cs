// See https://aka.ms/new-console-template for more information

using Day02;

var redMax = 12;
var greenMax = 13;
var blueMax = 14;
var pathToExample = "Inputs/example1.txt";
var pathToInput = "Inputs/input.txt";

var exampleSumOfValidIds = FindSumOfValidIdsForFile(pathToExample, redMax, greenMax, blueMax);
var sumOfValidIds = FindSumOfValidIdsForFile(pathToInput, redMax, greenMax, blueMax);


Console.WriteLine("The example sum of valid ids is: " + exampleSumOfValidIds);
Console.WriteLine("The real sum of valid ids is: " + sumOfValidIds);

//
//
//

int FindSumOfValidIdsForFile(string pathToInput1, int redMax1, int greenMax1, int blueMax1)
{
    var lines = File.ReadAllLines(pathToInput1);
    var games = lines.Select(line =>
    {
        var x = line.Replace(" ", "");
        var id = GetGameIdFromLine(x);

        var handfulsLine = x.Split(":")[1];
        var handfuls = handfulsLine.Split(";");
        var handfulsSets = handfuls.SelectMany(y => y.Split(","));
        var areColorsValid = new ColorValidator(handfulsSets, redMax1, greenMax1, blueMax1).AreColorsValid;

        return new Game()
        {
            Id = id,
            IsValid = areColorsValid
        };
    });

    var sumOfValidIds1 = 0;
    foreach (var game in games.Where(x => x.IsValid))
    {
        sumOfValidIds1 += game.Id;
    }

    return sumOfValidIds1;

    int GetGameIdFromLine(string s)
    {
        var gameWithId = s.Split(":")[0];
        var i = int.Parse(gameWithId.Replace("Game", ""));
        return i;
    }
}