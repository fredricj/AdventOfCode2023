using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");
List<string> lines;
part1(File.ReadLines("day3-test1.txt").ToList());
part1(File.ReadLines("day3-test2.txt").ToList());
part1(File.ReadLines("day3-test3.txt").ToList());

lines = File.ReadLines("day3-input.txt").ToList();
part1(lines);

part2(File.ReadLines("day3-test1.txt").ToList());
part2(lines);

static void part1(List<string> lines)
{
    int sumPartNumbers = 0;
    Regex rx = new(@"[^0-9.]");
    for (int i = 0; i < lines.Count; i++)
    {
        MatchCollection matches = rx.Matches(lines[i]);
        foreach (Match match in matches)
        {
            sumPartNumbers += getPartNumbers(lines, i, match.Index).Sum();
        }
    }

    Console.WriteLine("Sum partNumber: {0}", sumPartNumbers);
}

static List<int> getPartNumbers(List<string> lines, int symbolLineIndex, int symbolColIndex)
{
    List<int> partNumbers = new List<int>();
    Regex rx = new(@"[0-9]+");

    for (int j = -1; j <= 1; j++)
    {
        int lineNo = symbolLineIndex + j;
        if (lineNo >= 0 && lineNo < lines.Count)
        {
            MatchCollection matches = rx.Matches(lines[lineNo]);
            foreach (Match match in matches)
            {
                if (match.Index <= symbolColIndex+1 && symbolColIndex-1 <= match.Index+match.Length-1)
                {
                    partNumbers.Add(Int32.Parse(match.Value));
                }
            }
        } 
    }
    return partNumbers;
}
static void part2(List<string> lines)
{
    int sumPartNumbers = 0;
    Regex rx = new(@"\*");
    for (int i = 0; i < lines.Count; i++)
    {
        var row = lines[i];
        MatchCollection matches = rx.Matches(row);
        foreach (Match match in matches)
        {
            List<int> numbers = getPartNumbers(lines, i, match.Index);
            if (numbers.Count == 2) {
                sumPartNumbers += numbers[0] * numbers[1];
            }
            
        }
    }
    Console.WriteLine("Sum partNumber gear: {0}", sumPartNumbers);
}