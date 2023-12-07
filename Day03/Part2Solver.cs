using System.Text;

namespace Day03;

public static class Part2Solver
{
    public static int FindGearsAndMultiplyThem(string[] strings)
    {
        {
            var maxLength = strings.Length;
            var maxWidth = strings.FirstOrDefault().Length;

            var (allValidPositionsFromGears, numbersPositions, gears) = Initializer.InitializeArraysPart2(strings, maxWidth, maxLength);
            Console.WriteLine("All valid positions extracted from gears are:");
            PrintMatrix(maxLength, allValidPositionsFromGears);
            Console.WriteLine("The numbers positions:");
            PrintMatrix(maxLength, numbersPositions);
            
            var simpleValidDigits = new bool[maxLength, maxWidth];
            FindSimpleValidDigits(maxLength, maxWidth, simpleValidDigits, allValidPositionsFromGears, numbersPositions);
            Console.WriteLine("The redeeming positions for part numbers:");
            PrintMatrix(maxLength, simpleValidDigits);

            var complexValidDigits = new bool[maxLength, maxWidth];
            AssignAndPropagate(maxLength, maxWidth, simpleValidDigits, complexValidDigits, numbersPositions);
            Console.WriteLine("The positions for part numbers:");
            PrintMatrix(maxLength, complexValidDigits);

            var onlyPartNumbers = new string[maxLength];
            RefineNumericMatrix(strings, maxLength, maxWidth, complexValidDigits, gears, onlyPartNumbers);


            var sumOfProductOfGearNumbers = 0;
            var i = 0;
            foreach (var line in onlyPartNumbers)
            {
                var j = 0;
                foreach (var character in line)
                {
                    if (character != '*')
                    {
                        j++;
                        continue;
                    }

                    var simpleValidDigitsForThisGear = new bool[maxLength, maxWidth];
                    var complexValidDigitsForThisGear = new bool[maxLength, maxWidth];
                    var rowWithOnlyCharsSurroundingGearToTrue = AssignThreeTruesAroundGear(maxWidth, j);

                    for (var k = 0; k < maxWidth; k++)
                    {
                        if (!rowWithOnlyCharsSurroundingGearToTrue[k]) continue;
                        simpleValidDigitsForThisGear[i - 1, k] = simpleValidDigits[i - 1, k];
                        simpleValidDigitsForThisGear[i, k] = simpleValidDigits[i, k];
                        simpleValidDigitsForThisGear[i + 1, k] = simpleValidDigits[i + 1, k];
                    }
                    
                    AssignAndPropagate(maxLength, maxWidth, simpleValidDigitsForThisGear, complexValidDigitsForThisGear, numbersPositions);
                    
                    var gearsWithOnlyThisGear = new bool[maxLength, maxWidth];
                    gearsWithOnlyThisGear[i, j] = true;
                    var onlyPartNumbersForThisGear = new string[maxLength];
                    RefineNumericMatrix(strings, maxLength, maxWidth, complexValidDigitsForThisGear, gearsWithOnlyThisGear, onlyPartNumbersForThisGear);
                    var partNumbersAsBigString = string.Concat(onlyPartNumbersForThisGear).Replace("*", "/");
                    var partNumbersInBlocks = partNumbersAsBigString.Split("/").Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                    if (partNumbersInBlocks.Count < 2)
                    {
                        continue;
                    }
                    var gearNumber1 = int.Parse(partNumbersInBlocks.First());
                    var gearNumber2 =int.Parse(partNumbersInBlocks.Last());
                    
                    var product = gearNumber1 * gearNumber2;
                    sumOfProductOfGearNumbers += product;
                    j++;
                }
                i++;
                
            }

            return sumOfProductOfGearNumbers;
        }

        void PrintRow(bool[,] validPositions1, int i1)
        {
            var rowElementsSelected = Enumerable.Range(0, validPositions1.GetLength(1))
                .Select(x => validPositions1[i1, x]
                    ? 1
                    : 0)
                .ToArray();
            var stringOfRow = string.Concat(rowElementsSelected);
            Console.WriteLine(stringOfRow);
        }

        void PrintMatrix(int maxLength1, bool[,] bools)
        {
            for (var i = 0; i < maxLength1; i++) PrintRow(bools, i);
        }

        void FindSimpleValidDigits(int i2, int maxWidth3, bool[,] simpleValidDigits1, bool[,] validPositions2, bool[,] numbersPositions2)
        {
            for (var i = 0; i < i2; i++)
            for (var j = 0; j < maxWidth3; j++)
                simpleValidDigits1[i, j] = validPositions2[i, j] && numbersPositions2[i, j];
        }

        void AssignAndPropagate(int maxLength, int maxWidth, bool[,] simpleValidDigits, bool[,] complexValidDigits, bool[,] numbersPositions)
        {
            for (var i = 0; i < maxLength; i++)
            for (var j = 0; j < maxWidth; j++)
                if (simpleValidDigits[i, j])
                {
                    complexValidDigits[i, j] = true;
                    // Propagate to both sides if there are numbers there
                    for (var k = j; k < maxWidth; k++)
                        if (numbersPositions[i, k])
                            complexValidDigits[i, k] = true;
                        else
                            break;

                    for (var k = j; k + 1 > 0; k--)
                        if (numbersPositions[i, k])
                            complexValidDigits[i, k] = true;
                        else
                            break;
                }
        }
    }
    private static bool[] AssignThreeTruesAroundGear(int maxWidth, int j)
    {
        var rowBeforeWithOnlyGearSurroundingCharsToTrue = new bool[maxWidth];
        rowBeforeWithOnlyGearSurroundingCharsToTrue[j - 1] = true;
        rowBeforeWithOnlyGearSurroundingCharsToTrue[j] = true;
        rowBeforeWithOnlyGearSurroundingCharsToTrue[j + 1] = true;
        return rowBeforeWithOnlyGearSurroundingCharsToTrue;
    }
    private static void RefineNumericMatrix(string[] strings,
        int maxLength,
        int maxWidth,
        bool[,] complexValidDigits,
        bool[,] gears,
        string[] onlyPartNumbers)
    {
        var parsedInputAsArray = strings.ToArray();
        for (var i = 0; i < maxLength; i++)
        {
            var row = new StringBuilder();
            for (var j = 0; j < maxWidth; j++)
            {
                var resultingChar = complexValidDigits[i, j]
                    ? parsedInputAsArray[i][j]
                    : gears[i, j]
                        ? '*'
                        : '/';
                row.Append(resultingChar);
            }

            onlyPartNumbers[i] = row.ToString();
            Console.WriteLine(onlyPartNumbers[i]);
        }
    }
}