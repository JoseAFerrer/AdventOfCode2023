using Day07;

var pathToDemoInput = "Inputs/sample.txt";
WorkOutSolution(pathToDemoInput);

var pathToInput = "Inputs/input.txt";
WorkOutSolution(pathToInput);

void WorkOutSolution(string s)
{
    var calculator = new HandCalculator(s);
    var winnings = calculator.GetTotalWinnings();
    Console.WriteLine("The winnings are: " + winnings);
    var calculatorJ = new HandCalculator(s);
    var winningsJ = calculatorJ.GetTotalWinningsWithJokers();
    Console.WriteLine("The winnings accounting for jokers are: " + winningsJ);
}