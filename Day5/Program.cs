Console.WriteLine("Test part1:");
part1("day5-test1.txt");
Console.WriteLine("Part1:");
part1("day5-input.txt");

Console.WriteLine("Test part2:");
part2("day5-test1.txt");
Console.WriteLine("Part2:");
part2("day5-input.txt");

static void part1(string filename)
{
    (List<long> seeds, Dictionary<string, List<TypeMap>> maps) = parseFile(filename);
    long lowestLocation = Int64.MaxValue;
    var watch = System.Diagnostics.Stopwatch.StartNew();
    foreach (var seed in seeds)
    {
        var locationId = mapSeedToLocation(seed, maps);
        //Console.WriteLine("Location: {0}", locationId);
        if (locationId < lowestLocation)
        {
            lowestLocation = locationId;
        }
    }

    Console.WriteLine("Lowest location: {0}", lowestLocation);
    watch.Stop();
    Console.WriteLine("Runtime: {0}", watch.ElapsedMilliseconds);
}

static long mapSeedToLocation(long seed, Dictionary<string, List<TypeMap>> maps)
{
    var destId = getNextFromMap(seed, maps["sts"]);
    destId = getNextFromMap(destId, maps["stf"]);
    destId = getNextFromMap(destId, maps["ftw"]);
    destId = getNextFromMap(destId, maps["wtl"]);
    destId = getNextFromMap(destId, maps["ltt"]);
    destId = getNextFromMap(destId, maps["tth"]);
    var locationId = getNextFromMap(destId, maps["htl"]);
    return locationId;
}
{

}
static (List<long>, Dictionary<string, List<TypeMap>>) parseFile(string filename)
{
    List<string> lines = File.ReadLines(filename).ToList();
    List<long> seeds = [];
    string section = "";
    Dictionary<string, List<TypeMap>> maps = new Dictionary<string, List<TypeMap>>();
    foreach (string? line in lines)
    {
        if (line.Contains(':'))
        {
            if (line.StartsWith("seeds"))
            {
                seeds = line.Split(':')[1].Trim().Split(' ').Select(Int64.Parse).ToList<long>();
            } else
            {
                if (line.StartsWith("seed-to-soil"))
                {
                    section = "sts";
                }
                else if (line.StartsWith("soil-to-fertilizer"))
                {
                    section = "stf";
                }
                else if (line.StartsWith("fertilizer-to-water"))
                {
                    section = "ftw";
                }
                else if (line.StartsWith("water-to-light"))
                {
                    section = "wtl";
                }
                else if (line.StartsWith("light-to-temperature"))
                {
                    section = "ltt";
                }
                else if (line.StartsWith("temperature-to-humidity"))
                {
                    section = "tth";
                }
                else if (line.StartsWith("humidity-to-location"))
                {
                    section = "htl";
                }
                maps.Add(section, new List<TypeMap>());
            }
        }
        else if (line.Length == 0)
        {
            continue;
        }
        else
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(Int64.Parse).ToArray<long>();
            TypeMap map = new TypeMap();
            map.destNumber = parts[0];
            map.sourceNumber = parts[1];
            map.range = parts[2];

            maps[section].Add(map);
        }
    }
    foreach (var map in maps)
    {
        map.Value.Sort();
    }
    return (seeds, maps);
}

static void part2(string filename)
{
    var watch = System.Diagnostics.Stopwatch.StartNew();
    (List<long> seeds, Dictionary<string, List<TypeMap>> maps) = parseFile(filename);
    long lowestLocation = Int64.MaxValue;
    for (int i = 0; i < seeds.Count; i=i+2)
    {
        var lowSeedId = seeds[i];
        var highSeedId = seeds[i + 1] + seeds[i]-1;
        
        for (long seedId = lowSeedId; seedId <= highSeedId; seedId++)
        {
            var locationId = mapSeedToLocation(seedId, maps);
            if (locationId < lowestLocation)
            {
                lowestLocation = locationId;
            }
        }
        Console.WriteLine("Round done: {0}", watch.ElapsedMilliseconds/1000);
    }
    Console.WriteLine("Lowest location: {0}", lowestLocation);
    watch.Stop();
    Console.WriteLine("Runtime: {0}", watch.ElapsedMilliseconds / 1000);
    //var elapsedMs = watch.ElapsedMilliseconds;
}
static long getNextFromMap(long sourceId, List<TypeMap> map)
{
    foreach (var conv in map)
    {
        var v = sourceId - conv.sourceNumber;
        if (v < 0) {
            break;
        }
        if (v >= 0 && v < conv.range)
        {
            return v + conv.destNumber;
        }
    }
    return sourceId;
}

class TypeMap : IComparable<TypeMap>
{
    public long sourceNumber = 0, destNumber = 0, range = 0;

    public int CompareTo(TypeMap other)
    {
        //if (other == null)
        //{
        //    return 1;
        //}
        return this.sourceNumber.CompareTo(other.sourceNumber);
    }
}

