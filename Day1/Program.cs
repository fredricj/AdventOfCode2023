using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");
using (StreamReader sr = new StreamReader("day1-input.txt"))
{
    part1(sr);
    sr.DiscardBufferedData();
    sr.BaseStream.Seek(0, SeekOrigin.Begin);
    part2(sr);
}
using (StreamReader sr = new StreamReader("day1-test1.txt"))
{
    try { part1(sr); } catch { Console.WriteLine("Error"); }
}
using (StreamReader sr = new StreamReader("day1-test2.txt"))
{
    part2(sr);
}

static int GetNumber(string value)
{
    int firstNum;
    try
    {
        firstNum = Int32.Parse(value);
    }
    catch
    {
        switch (value)
        {
            case "one":
                firstNum = 1; break;
            case "two":
                firstNum = 2; break;
            case "three":
                firstNum = 3; break;
            case "four":
                firstNum = 4; break;
            case "five":
                firstNum = 5; break;
            case "six":
                firstNum = 6; break;
            case "seven":
                firstNum = 7; break;
            case "eight":
                firstNum = 8; break;
            case "nine":
                firstNum = 9; break;
            default: throw new InvalidDataException(String.Format("Unknown value: '{0}'", value));
        }
    }

    return firstNum;
}

static void part1(StreamReader sr)
{
    int adjustment = 0;
    Regex rx = new Regex(@"[0-9]", RegexOptions.Compiled);
    while (sr.Peek() >= 0)
    {
        String line = sr.ReadLine();
        if (line == null) break;
        MatchCollection matches = rx.Matches(line);
        int firstNum = Int32.Parse(matches[0].Value);
        int lastNum = Int32.Parse(matches[matches.Count - 1].Value);
        adjustment += firstNum * 10 + lastNum;
    }
    Console.WriteLine("Adjustment value: {0}", adjustment);
}

static void part2(StreamReader sr)
{
    Regex rx2 = new Regex(@"(one|two|three|four|five|six|seven|eight|nine|[0-9])", RegexOptions.Compiled);
    Regex rx2back = new Regex(@"(one|two|three|four|five|six|seven|eight|nine|[0-9])", RegexOptions.Compiled | RegexOptions.RightToLeft);
    int adjustment2 = 0;
    while (sr.Peek() >= 0)
    {
        String line = sr.ReadLine();
        if (line == null) break;
        MatchCollection matches = rx2.Matches(line);
        MatchCollection matches2 = rx2back.Matches(line);
        int firstNum = GetNumber(matches[0].Value);
        int lastNum = GetNumber(matches2[0].Value);
        adjustment2 += firstNum * 10 + lastNum;
    }
    Console.WriteLine("Adjustment value2: {0}", adjustment2);
}