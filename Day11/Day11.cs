public class Day11
{
    public long Answer(bool pt2)
    {
        char[][] input = File.ReadAllLines("Day11/input.txt").Select(line => line.ToCharArray()).ToArray();

        List<int> emptyRows = GetEmptyRows(input);
        List<int> emptyCols = GetEmptyCols(input);

        long sum = 0;
        HashSet<(int, int)> passed = new HashSet<(int, int)>();
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '#')
                {
                    foreach ((int y, int x) other in GetOtherGalaxies(input, (y, x)))
                    {
                        if (passed.Contains(other)) continue;
                        sum += FindSteps(input, (y, x), other, emptyRows, emptyCols, pt2 ? 1000000 : 2);
                    }

                    passed.Add((y, x));
                }
            }
        }

        return sum;
    }

    private long FindSteps(char[][] input, (int y, int x) start, (int y, int x) end, List<int> emptyRows, List<int> emptyCols, int expansion)
    {
        int[] xLimits = { Math.Min(start.x, end.x), Math.Max(start.x, end.x) };
        int[] yLimits = { Math.Min(start.y, end.y), Math.Max(start.y, end.y) };
        
        IEnumerable<int> xRange = Enumerable.Range(xLimits[0], xLimits[1] - xLimits[0]);
        IEnumerable<int> yRange = Enumerable.Range(yLimits[0], yLimits[1] - yLimits[0]);

        int emptyR = xRange.Count(x => emptyCols.Contains(x));
        int emptyC = yRange.Count(y => emptyRows.Contains(y));

        return xRange.Count() + yRange.Count() + emptyC * (expansion - 1) + emptyR * (expansion - 1);
    }

    private List<int> GetEmptyRows(char[][] input)
    {
        List<int> empty = new List<int>();
        for (int y = 0; y < input.Length; y++)
        {
            if (input[y].All(c => c == '.')) empty.Add(y);
        }

        return empty;
    }

    private List<int> GetEmptyCols(char[][] input)
    {
        List<int> empty = new List<int>();
        for (int x = 0; x < input[0].Length; x++)
        {
            int dotsFound = 0;
            for (int y = 0; y < input.Length; y++)
            {
                if (input[y][x] == '.') dotsFound++;
            }

            if (dotsFound == input.Length) empty.Add(x);
        }

        return empty;
    }

    private List<(int y, int x)> GetOtherGalaxies(char[][] input, (int y, int x) coords)
    {
        List<(int y, int x)> other = new List<(int y, int x)>();
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '#' && (y, x) != coords)
                {
                    other.Add((y, x));
                }
            }
        }

        return other;
    }
}