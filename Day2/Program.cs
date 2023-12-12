using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");
using (StreamReader sr = new StreamReader("day2-test1.txt"))
{
    part1(sr);
    sr.DiscardBufferedData();
    sr.BaseStream.Seek(0, SeekOrigin.Begin);
    part2(sr);
}
using (StreamReader sr = new StreamReader("day2-input.txt"))
{
    part1(sr);
    sr.DiscardBufferedData();
    sr.BaseStream.Seek(0, SeekOrigin.Begin);
    part2(sr);
}

static void part1(StreamReader sr)
{
    List<int> parts = new List<int>();
    Regex rx = new Regex(@"(?<count>[0-9]+) (?<color>blue|red|green)", RegexOptions.Compiled);
    int sumGoodGames = 0;
    while (sr.Peek() >= 0)
    {
        String line = sr.ReadLine();
        if (line == null) break;
        string[] subs = line.Split(':');
        bool isValid = true;
        foreach (string s in subs[1].Split(";"))
        {
            MatchCollection matches = rx.Matches(s);
            foreach (Match m in matches)
            {
                GroupCollection g = m.Groups;
                if ((g["color"].Value == "green" && Int32.Parse(g["count"].Value) > 13) ||
                    (g["color"].Value == "red" && Int32.Parse(g["count"].Value) > 12) ||
                    (g["color"].Value == "blue" && Int32.Parse(g["count"].Value) > 14)
                    )
                {
                    isValid = false;
                    break;
                }
                
            }
            if (!isValid) {  break; }
        }
        if (isValid)
        {
            string goodGames = subs[0];
            sumGoodGames += Int32.Parse(goodGames.Split(" ")[1]);
            Console.WriteLine(goodGames);
        }
    }
    Console.WriteLine("Sum bad games: {0}", sumGoodGames);
}

static void part2(StreamReader sr)
{
    List<int> parts = new List<int>();
    Regex rx = new Regex(@"(?<count>[0-9]+) (?<color>blue|red|green)", RegexOptions.Compiled);
    int sumGoodGames = 0;
    while (sr.Peek() >= 0)
    {
        Dictionary<string, int> map = new Dictionary<string, int>();
        String line = sr.ReadLine();
        if (line == null) break;
        string[] subs = line.Split(':');
        foreach (string s in subs[1].Split(";"))
        {
            MatchCollection matches = rx.Matches(s);
            foreach (Match m in matches)
            {
                
                GroupCollection g = m.Groups;
                map.TryGetValue(g["color"].Value, out int currMax);
                map[g["color"].Value] = Int32.Max(currMax, Int32.Parse(g["count"].Value));
            }
        }
        map.TryGetValue("green", out int green);
        map.TryGetValue("blue", out int blue);
        map.TryGetValue("red", out int red);
        sumGoodGames += green * red * blue;
    }
    Console.WriteLine("Power of games: {0}", sumGoodGames);
}