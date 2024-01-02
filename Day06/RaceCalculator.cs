using AocHelpers;

namespace Day06;

public class RaceCalculator
{
    public RaceCalculator(string path)
    {
        var lines = File.ReadAllLines(path);
        var timesAsStrings = lines[0].Replace("Time: ", "");
        var recordsAsStrings = lines[1].Replace("Distance: ", "");

        Times = StringHelpers.GetNumbersFromOneString(timesAsStrings);
        Records = StringHelpers.GetNumbersFromOneString(recordsAsStrings);
    }

    public long[] Times { get; set; }
    public long[] Records { get; set; }
    public long FindMarginOfError()
    {
        long winningCases = 1;
        foreach (var time in Times)
        {
            var index = Array.IndexOf(Times, time);
            var record = Records[index];
            var winningCase = FindWinningCasesForRace(time, record);
            winningCases *= winningCase;
        }

        return winningCases;
    }
    
    public long FindWinningCasesForLongRace()
    {
        var originalTimes = Times.Select(x => x.ToString());
        var realTime = long.Parse(string.Concat(originalTimes));
        
        var originalRecords = Records.Select(x => x.ToString());
        var realRecord = long.Parse(string.Concat(originalRecords));
        
        var winningCase = FindWinningCasesForRace(realTime, realRecord);
        return winningCase;
    }
    
    private static long FindWinningCasesForRace(long time, long record)
    {
        long winningCases = 0;
        for (long i = 1; i < time; i++)
        {
            var remainingTime = time - i;
            var distanceReached = i * remainingTime;
            if (distanceReached > record) winningCases++;
        }

        return winningCases;
    }
}