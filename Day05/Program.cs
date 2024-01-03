using Day05;

var pathToDemoInput = "Inputs/example1.txt";
WorkOutSolution(pathToDemoInput);

var pathToInput = "Inputs/input.txt";
WorkOutSolution(pathToInput);

void WorkOutSolution(string s)
{
    var calculator = new AlmanacCalculator(s);
    var closest = calculator.FindClosestLocationComplex();
    Console.WriteLine("The closest location is: " + closest);
}