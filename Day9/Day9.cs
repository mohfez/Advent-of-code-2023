public class Day9
{
    public int Answer(bool pt2)
    {
        int[][] input = File.ReadAllLines("Day9/input.txt").Select(line => line.Split(" ").Select(num => int.Parse(num)).ToArray()).ToArray();
        return input.Select(vals => ExtrapolateNext(pt2 ? vals.Reverse().ToArray() : vals)).Sum();
    }

    private int ExtrapolateNext(int[] vals) => !vals.Any(v => v != 0) ? 0 : ExtrapolateNext(GetDifference(vals)) + vals[^1];
    
    private int[] GetDifference(int[] vals)
    {
        int[] diff = new int[vals.Length - 1];
        for (int k = 0; k < vals.Length - 1; k++)
        {
            diff[k] = vals[k + 1] - vals[k];
        }

        return diff;
    }
}