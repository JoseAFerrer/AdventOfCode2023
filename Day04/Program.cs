// See https://aka.ms/new-console-template for more information

using Day04;

var calculator = new Calculator();

var pathToDemoInput = "Inputs/example1.txt";
var demoPoints = calculator.CalculatePointsForInput(pathToDemoInput);
Console.WriteLine("The demo cards add up to " + demoPoints + " points");


var pathToRealInput = "Inputs/input.txt";
var realPoints = calculator.CalculatePointsForInput(pathToRealInput);
Console.WriteLine("The cards add up to " + realPoints + " points");

var demoNrOfScratchCards = calculator.CalculateScratchCardsAmount(pathToDemoInput);
Console.WriteLine("The demo cards add up to " + demoNrOfScratchCards + " scratchcards");


var realNrOfScratchCards = calculator.CalculateScratchCardsAmount(pathToRealInput);
Console.WriteLine("The cards add up to " + realNrOfScratchCards + " scratchcards");
