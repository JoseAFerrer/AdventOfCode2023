using Day06;

var pathToDemoInput = "Inputs/sample.txt";
WorkOutSolution(pathToDemoInput);

var pathToInput = "Inputs/input.txt";
WorkOutSolution(pathToInput);

void WorkOutSolution(string s)
{
    var calculator = new RaceCalculator(s);
    var marginOfError = calculator.FindMarginOfError();
    var winningCasesForLongRace = calculator.FindWinningCasesForLongRace();
    Console.WriteLine("The margin of error is: " + marginOfError);
    Console.WriteLine("The winning cases for the long race are: " + winningCasesForLongRace);
}