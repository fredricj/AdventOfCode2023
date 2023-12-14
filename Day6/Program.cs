using System.Linq;

Console.WriteLine("Test part1:");
part1("day6-test1.txt");
Console.WriteLine("Part1:");
part1("day6-input.txt");

Console.WriteLine("Test part2:");
part2("day6-test1.txt");
Console.WriteLine("Part2:");
part2("day6-input.txt");

static void part1(string filename)
{
    List<string> lines = File.ReadLines(filename).ToList<string>();
    List<int> raceTimes = lines[0].Split(':')[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(Int32.Parse).ToList<int>();
    List<int> raceRecord = lines[1].Split(':')[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(Int32.Parse).ToList<int>();
    int comb = 1;
    for (int i = 0; i < raceTimes.Count; i++)
    {
        int record = raceRecord[i];
        Console.WriteLine("Racetime: {0}, Record: {1}", raceTimes[i], record);
        int combinations = 0;
        for (int j = 0; j < raceTimes[i]; j++)
        {
            if (j * (raceTimes[i] - j) > record)
            {
                //Console.WriteLine("Holding for {0} sec: {1}", j, j * (raceTimes[i] - j));
                combinations++;
            }
        }
        Console.WriteLine("Combinations: {0}", combinations);
        comb *= combinations;
    }
    Console.WriteLine("Combination result: {0}", comb);
}

static void part2(string filename)
{
    List<string> lines = File.ReadLines(filename).ToList<string>();
    long raceTime = Int64.Parse(lines[0].Split(':')[1].Replace(" ", ""));
    long raceRecord = Int64.Parse(lines[1].Split(':')[1].Replace(" ", ""));
    Console.WriteLine("Racetime: {0}, Record: {1}", raceTime, raceRecord);
    long combinations = 0;
    for (long j = 0; j < raceTime; j++)
    {
        if (j * (raceTime - j) > raceRecord)
        {
            //Console.WriteLine("Holding for {0} sec: {1}", j, j * (raceTime - j));
            combinations++;
        }
    }
    Console.WriteLine("Combination result: {0}", combinations);
}