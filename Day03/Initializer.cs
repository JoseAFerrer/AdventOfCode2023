namespace Day03;

public static class Initializer
{
    public static (bool[,], bool[,]) InitializeArrays(string[] strings, int maxWidth2, int maxLength2)
    {
        var allValidPositionsFromSpecialChars = new bool[maxLength2, maxWidth2];
        var numbersPositions = new bool[maxLength2, maxWidth2];
        var lineNr = 0;
        foreach (var line in strings)
        {
            var colNr = 0;

            foreach (var character in line)
            {
                if (character != '.' && !char.IsDigit(character))
                {
                    allValidPositionsFromSpecialChars[lineNr, colNr] = true;

                    HandleEasyBorders(lineNr, colNr, maxLength2, maxWidth2, allValidPositionsFromSpecialChars);
                    HandleHardBorders(lineNr, colNr, maxLength2, maxWidth2, allValidPositionsFromSpecialChars);
                }

                if (char.IsDigit(character)) numbersPositions[lineNr, colNr] = true;

                colNr++;
            }

            lineNr++;
        }
        return (allValidPositionsFromSpecialChars, numbersPositions);
    }
    
    public static (bool[,], bool[,], bool[,]) InitializeArraysPart2(string[] input, int maxWidth, int maxLength)
    {
        var validPositionsFromGears = new bool[maxLength, maxWidth];
        var numbersPositions = new bool[maxLength, maxWidth];
        var gears = new bool[maxLength, maxWidth];
        var parsedInputAsArray = input.ToArray();

        var i = 0;
        foreach (var line in input)
        {
            var j = 0;

            foreach (var character in line)
            {
                if (char.IsDigit(character)) numbersPositions[i, j] = true;
                
                if (character == '*')
                {
                    var amountOfAdjacentNumbers = CountAdjacentNumbers(maxWidth, maxLength, j, parsedInputAsArray, i);

                    if (amountOfAdjacentNumbers == 2) 
                    { 
                        gears[i, j] = true;
                        HandleEasyBorders(i, j, maxLength, maxWidth, validPositionsFromGears);
                        HandleHardBorders(i, j, maxLength, maxWidth, validPositionsFromGears);
                    }
                }
                j++;
            }
            i++;
        }
        return (validPositionsFromGears, numbersPositions, gears);
    }
    private static int CountAdjacentNumbers(int maxWidth2, int maxLength2, int j, string[] parsedInputAsArray, int i)
    {
        var amountOfAdjacentNumbers = 0;

        if (EastCondition(j, maxWidth2) && IsDigit(parsedInputAsArray[i][j + 1]))
        {
            amountOfAdjacentNumbers++;
        }

        if (WestCondition(j) && IsDigit(parsedInputAsArray[i][j - 1]))
        {
            amountOfAdjacentNumbers++;
        }

        if (SouthCondition(i, maxWidth2) && IsDigit(parsedInputAsArray[i + 1][j]))
        {
            amountOfAdjacentNumbers++;
        }

        if (NorthCondition(i) && IsDigit(parsedInputAsArray[i - 1][j]))
        {
            amountOfAdjacentNumbers++;
        }

        if (EastCondition(j, maxWidth2) && SouthCondition(i, maxLength2) && IsDigit(parsedInputAsArray[i + 1][j + 1])&& !IsDigit(parsedInputAsArray[i + 1][j]))
        {
            amountOfAdjacentNumbers++;
        }

        if (EastCondition(j, maxWidth2) && NorthCondition(i) && IsDigit(parsedInputAsArray[i - 1][j + 1]) && !IsDigit(parsedInputAsArray[i - 1][j]))
        {
            amountOfAdjacentNumbers++;
        }

        if (WestCondition(j) && SouthCondition(i, maxLength2) && IsDigit(parsedInputAsArray[i + 1][j - 1]) && !IsDigit(parsedInputAsArray[i + 1][j]))
        {
            amountOfAdjacentNumbers++;
        }

        if (WestCondition(j) && NorthCondition(i) && IsDigit(parsedInputAsArray[i - 1][j - 1]) && !IsDigit(parsedInputAsArray[i - 1][j]))
        {
            amountOfAdjacentNumbers++;
        }

        return amountOfAdjacentNumbers;
    }
    private static bool IsDigit(char character)
    {
        return char.IsDigit(character);
    }


    private static void HandleEasyBorders(int lineNr, int colNr, int maxLength, int maxWidth, bool[,] validPositions)
    {
        // West
        if (EastCondition(colNr, maxWidth)) validPositions[lineNr, colNr + 1] = true;

        // East
        if (WestCondition(colNr)) validPositions[lineNr, colNr - 1] = true;

        // South
        if (SouthCondition(lineNr, maxLength)) validPositions[lineNr + 1, colNr] = true;

        // North
        if (NorthCondition(lineNr)) validPositions[lineNr - 1, colNr] = true;
    }
    private static bool NorthCondition(int lineNr1)
    {
        return lineNr1 > 0;
    }
    private static bool SouthCondition(int lineNr1, int maxLength1)
    {
        return lineNr1 + 1 < maxLength1;
    }
    private static bool WestCondition(int i)
    {
        return i > 0;
    }
    private static bool EastCondition(int i, int maxWidth1)
    {
        return i + 1 < maxWidth1;
    }

    private static void HandleHardBorders(int lineNr, int colNr, int maxLength, int maxWidth, bool[,] validPositions)
    {
        // North-West
        if (colNr + 1 < maxWidth && lineNr > 0) validPositions[lineNr - 1, colNr + 1] = true;

        // South-West
        if (colNr + 1 < maxWidth && lineNr + 1 < maxLength) validPositions[lineNr + 1, colNr + 1] = true;

        // North-East
        if (colNr > 0 && lineNr > 0) validPositions[lineNr - 1, colNr - 1] = true;

        // South-East
        if (colNr > 0 && lineNr + 1 < maxLength) validPositions[lineNr + 1, colNr - 1] = true;
    }
}