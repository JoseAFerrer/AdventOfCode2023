namespace Day05;

public class XMap
{
    public Destination Type { get; set; }
    public List<Rule> Rules { get; set; } = null!;
}

public class Rule
{
    public Rule(long destinationStart, long sourceStart, long range)
    {
        DestinationStart = destinationStart;
        SourceStart = sourceStart;
        SourceEnd = sourceStart + range;
        DestinationEnd = destinationStart + range;
        Range = range;
    }
    public long DestinationStart { get; set; }
    public long SourceStart { get; set; }
    public long SourceEnd { get; set; }
    public long DestinationEnd { get; set; }
    public long Range { get; set; }
}

public enum Destination
{
    Soil,
    Fertilizer,
    Water,
    Light,
    Temperature,
    Humidity,
    Location
}

public class SeedRange
{
    public long FirstSeed { get; set; }
    public long LastSeed { get; set; }
    public long Range { get; set; }
}