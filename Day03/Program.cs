using Day03;

var pathToDemoInput = "Inputs/example1.txt";
var parsedDemoInput = File.ReadAllLines(pathToDemoInput);

var sumOfDemoPartNumbers = Part1Solver.FindValidPartNumbersAndTheirSum(parsedDemoInput);
Console.WriteLine("The sum of the part numbers in the example schematic is " + sumOfDemoPartNumbers);

var pathToRealInput = "Inputs/input.txt";
var parsedRealInput = File.ReadAllLines(pathToRealInput);
var sumOfRealPartNumbers = Part1Solver.FindValidPartNumbersAndTheirSum(parsedRealInput);
Console.WriteLine("The sum of the part numbers in the real schematic is " + sumOfRealPartNumbers);

var productOfGearsExample = Part2Solver.FindGearsAndMultiplyThem(parsedDemoInput);
Console.WriteLine("The product of the part numbers for gears in the example is " + productOfGearsExample);

var productOfGearsReal = Part2Solver.FindGearsAndMultiplyThem(parsedRealInput);
Console.WriteLine("The product of the part numbers for gears in the real schematic is " + productOfGearsReal);