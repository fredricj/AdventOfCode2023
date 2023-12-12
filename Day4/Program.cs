part1(File.ReadLines("day4-test1.txt").ToList());
Console.WriteLine("Part1");
part1(File.ReadLines("day4-input.txt").ToList());

part2(File.ReadLines("day4-test2.txt").ToList());
Console.WriteLine("Part2");
part2(File.ReadLines("day4-input.txt").ToList());

static void part1(List<string> lines)
{
    int sumPartNumbers = 0;
    for (int i = 0; i < lines.Count; i++)
    {
        string[] sides = lines[i].Split(":")[1].Split("|");
        string[] winningNumbers = sides[0].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        string[] yourNumbers = sides[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var winCount = winningNumbers.Intersect(yourNumbers).Count();
        if (winCount>0)
        {
            sumPartNumbers += (int)Math.Pow(2, winCount - 1);
        }
    }

    Console.WriteLine("Sum partNumber: {0}", sumPartNumbers);
}

static void part2(List<string> lines)
{
    int[] winsPerCard = new int[lines.Count];
    for (int i = 0; i < lines.Count; i++)
    {
        string[] sides = lines[i].Split(":")[1].Split("|");
        string[] winningNumbers = sides[0].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        string[] yourNumbers = sides[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        winsPerCard[i] = winningNumbers.Intersect(yourNumbers).Count();
    }
    Dictionary<int, int> memo = new Dictionary<int, int>();
    for (int i =  winsPerCard.Length - 1; i >= 0; i--)
    {
        memo[i] = 0;
        if (winsPerCard[i] > 0)
        {
            for (int j = 1; j <= winsPerCard[i]; j++)
            {
                if (memo.ContainsKey(i+j))
                {
                    memo[i] += memo[i+j] + 1;
                }            
            }
        }
    }
    int memoSum = 0;
    foreach (int i in memo.Keys)
    {
        memoSum += memo[i];
    }
    Console.WriteLine("Total cards: {0}", memoSum + winsPerCard.Length);
}