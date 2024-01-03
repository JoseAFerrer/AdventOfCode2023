namespace Day05;

public class AlmanacCalculator
{
    private readonly string[] _almanacLines;

    public AlmanacCalculator(string inputPath)
    {
        _almanacLines = File.ReadAllLines(inputPath);

        var seedsAsString = _almanacLines[0].Replace("seeds: ", "");
        // The way to get the seeds in part 1
        // Seeds = GetNumbersFromOneString(seedsAsString).ToList();

        FindAndSaveAllSeeds(seedsAsString);

        FindIndexesAndBuildMaps();
    }
    private void FindAndSaveAllSeeds(string seedsAsString)
    {
        var numbers = GetNumbersFromOneString(seedsAsString);
        for (var i = 0; i < numbers.Length; i += 2)
        {
            var seedRange = new SeedRange()
            {
                FirstSeed = numbers[i],
                LastSeed = numbers[i] + numbers[i + 1],
                Range = numbers[i + 1]
            };
            EncodedSeeds.Add(seedRange);
        }
    }

    private void FindIndexesAndBuildMaps()
    {
        var seedSoilIndex = Array.IndexOf(_almanacLines, "seed-to-soil map:");
        var soilFertilizerIndex = Array.IndexOf(_almanacLines, "soil-to-fertilizer map:");
        var fertilizerWaterIndex = Array.IndexOf(_almanacLines, "fertilizer-to-water map:");
        var waterLightIndex = Array.IndexOf(_almanacLines, "water-to-light map:");
        var lightTemperatureIndex = Array.IndexOf(_almanacLines, "light-to-temperature map:");
        var temperatureHumidityIndex = Array.IndexOf(_almanacLines, "temperature-to-humidity map:");
        var humidityLocationIndex = Array.IndexOf(_almanacLines, "humidity-to-location map:");
        var lastIndex = _almanacLines.Length;

        SoilMap = BuildDesiredXMap(seedSoilIndex, soilFertilizerIndex, Destination.Soil);
        FertilizerMap = BuildDesiredXMap(soilFertilizerIndex, fertilizerWaterIndex, Destination.Fertilizer);
        WaterMap = BuildDesiredXMap(fertilizerWaterIndex, waterLightIndex, Destination.Water);
        LightMap = BuildDesiredXMap(waterLightIndex, lightTemperatureIndex, Destination.Light);
        TemperatureMap = BuildDesiredXMap(lightTemperatureIndex, temperatureHumidityIndex, Destination.Temperature);
        HumidityMap = BuildDesiredXMap(temperatureHumidityIndex, humidityLocationIndex, Destination.Humidity);
        LocationMap = BuildDesiredXMap(humidityLocationIndex, lastIndex + 1, Destination.Location);
    }
    public List<SeedRange> EncodedSeeds { get; set; } = new();
    public XMap SoilMap { get; set; }
    public XMap FertilizerMap { get; set; }
    public XMap WaterMap { get; set; }
    public XMap LightMap { get; set; }
    public XMap TemperatureMap { get; set; }
    public XMap HumidityMap { get; set; }
    public XMap LocationMap { get; set; }

    private IEnumerable<XMap> AllMaps => new List<XMap>{SoilMap, FertilizerMap, WaterMap, LightMap, TemperatureMap, HumidityMap, LocationMap   };


    private long AdvanceGivenAMap(long inputNumber, Destination destination)
    {
        var whatMap = AllMaps.First(x => x.Type == destination);
        var exactRuleToApply = whatMap.Rules.Find(x => x.SourceStart == inputNumber);
        var ruleToApply = exactRuleToApply ?? whatMap.Rules.FindLast(x => x.SourceStart < inputNumber && x.SourceEnd > inputNumber);

        if (ruleToApply is null)
        {
            return inputNumber;
        }
        var difference = inputNumber - ruleToApply.SourceStart;
        var conclusion = ruleToApply.DestinationStart + difference;
        return conclusion;
    }
    
    private long GoBackGivenAMap(long inputNumber, Destination destination)
    {
        var myMap = AllMaps.First(x => x.Type == destination);
        var exactRuleToApply = myMap.Rules.Find(x => x.DestinationStart == inputNumber);
        var ruleToApply = exactRuleToApply ?? myMap.Rules.FindLast(x => x.DestinationStart < inputNumber && x.DestinationEnd > inputNumber);

        if (ruleToApply is null)
        {
            return inputNumber;
        }
        var difference = inputNumber - ruleToApply.DestinationStart;
        var conclusion = ruleToApply.SourceStart + difference;
        return conclusion;
    }

    public long FindLocationFromSeed(long seed)
    {
        var soil = AdvanceGivenAMap(seed, Destination.Soil);
        var fertilizer = AdvanceGivenAMap(soil, Destination.Fertilizer);
        var water = AdvanceGivenAMap(fertilizer, Destination.Water);
        var light = AdvanceGivenAMap(water, Destination.Light);
        var temperature = AdvanceGivenAMap(light, Destination.Temperature);
        var humidity = AdvanceGivenAMap(temperature, Destination.Humidity);
        var location = AdvanceGivenAMap(humidity, Destination.Location);
        Console.WriteLine(location);
        return location;
    }
    
    public long FindSeedFromLocation(long location)
    {
        var humidity = GoBackGivenAMap(location, Destination.Location);
        var temperature = GoBackGivenAMap(humidity, Destination.Humidity);
        var light = GoBackGivenAMap(temperature, Destination.Temperature);
        var water = GoBackGivenAMap(light, Destination.Light);
        var fertilizer = GoBackGivenAMap(water, Destination.Water);
        var soil = GoBackGivenAMap(fertilizer, Destination.Fertilizer);
        var seed = GoBackGivenAMap(soil, Destination.Soil);
        return seed;
    }

    private long FindClosestLocation(long top, long division)
    {
        for (long i = 0; i < top; i += division)
        {
            var seed = FindSeedFromLocation(i);
            if (EncodedSeeds.Any(x => x.FirstSeed <= seed && seed <= x.LastSeed ))
            {
                return i;
            }
        }

        return top;
    }

    public long FindClosestLocationComplex()
    {
        var maxSeedNr = EncodedSeeds.Max(x => x.LastSeed);
        var top = maxSeedNr;
        var division = (long)Math.Ceiling((double) top / 100000);
        
        long initialReturnable = top;
        for (var i = 0; i < 10; i++)
        {
            top = FindClosestLocation(top, division);
            Console.WriteLine("Current top: " + top);
            if (division > 1)
            {
                division = (long)Math.Ceiling((double)division / 2);
            }
            else
            {
                return top;
            }
        }
        
        return top;
    }
    
    
    private XMap BuildDesiredXMap(int startIndex, int endIndex, Destination destination)
    {
        var rulesAsStrings = _almanacLines[(startIndex + 1)..(endIndex - 1)];
        var rulesAsLongs = rulesAsStrings.Select(GetNumbersFromOneString).ToList();
        var unorderedRules = rulesAsLongs.Select(x => new Rule(x[0], x[1], x[2])).ToList();
        var orderedRules = unorderedRules.OrderBy(x => x.SourceStart).ToList();
        var firstRule = orderedRules.First();
        if (firstRule.SourceStart != 0)
        {
            orderedRules.Reverse();
            orderedRules.Add(new Rule(0, 0, firstRule.SourceStart));
            orderedRules.Reverse();
        }
        
        var soilMap = new XMap
        {
            Type = destination,
            Rules = orderedRules
        };
        return soilMap;
    }
    private static long[] GetNumbersFromOneString(string seedsAsStrings)
    {
        var numsAsArrayOfStrings = seedsAsStrings.Split(" ");
        var nums = numsAsArrayOfStrings.Select(long.Parse);
        return nums.ToArray();
    }


// Use almanac

// Find lowest location number
}