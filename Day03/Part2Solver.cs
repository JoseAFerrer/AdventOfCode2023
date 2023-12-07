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
            var parsedInputAsArray = strings.ToArray();
            for (var i = 0; i < maxLength; i++)
            {
                var row = new StringBuilder();
                for (var j = 0; j < maxWidth; j++)
                {
                    var resultingChar = complexValidDigits[i, j]
                        ? parsedInputAsArray[i][j]
                        : gears[i, j]
                            ? '*' : 
                            '/';
                    row.Append(resultingChar);
                }

                onlyPartNumbers[i] = row.ToString();
                Console.WriteLine(onlyPartNumbers[i]);
            }
            

            var sumOfProductOfGearNumbers = 0;
            foreach (var line in onlyPartNumbers)
            {
                foreach (var character in line)
                {
                    if (character != '*') continue;
                    var gearNumber1 = 2;
                    var gearNumber2 = 2;
                }
                
                
                
                var blocks = line.Split("/");
                var numbersAsStrings = blocks.Where(x => !string.IsNullOrWhiteSpace(x));
                var numbersInThisLine = numbersAsStrings.Select(int.Parse).Sum();
                sumOfProductOfGearNumbers += numbersInThisLine;
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

        void AssignAndPropagate(int maxLength3, int i3, bool[,] simpleValidDigits2, bool[,] complexValidDigits1, bool[,] bools2)
        {
            for (var i = 0; i < maxLength3; i++)
            for (var j = 0; j < i3; j++)
                if (simpleValidDigits2[i, j])
                {
                    complexValidDigits1[i, j] = true;
                    // Propagate to both sides if there are numbers there
                    for (var k = j; k < i3; k++)
                        if (bools2[i, k])
                            complexValidDigits1[i, k] = true;
                        else
                            break;

                    for (var k = j; k + 1 > 0; k--)
                        if (bools2[i, k])
                            complexValidDigits1[i, k] = true;
                        else
                            break;
                }
        }
    }
}