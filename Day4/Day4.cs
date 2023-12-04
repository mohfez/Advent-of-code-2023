using System.Text.RegularExpressions;

public class Day4
{
    public int Answer(bool pt2)
    {
        string[] input = File.ReadAllLines("Day4/input.txt");
        int points = 0;
        
        int[] copies = new int[input.Length];
        Array.Fill(copies, 1);

        string getNumsRegex = @"\d+|\|";
        foreach (string line in input)
        {
            List<int> winningNumbers = new List<int>();
            bool readingMyNumbers = false;
            int valid = 0;

            MatchCollection matches = Regex.Matches(line, getNumsRegex);
            int cardNum = int.Parse(matches[0].Value);

            for (int i = 1; i < matches.Count; i++)
            {
                string match = matches[i].Value;

                if (match == "|")
                {
                    readingMyNumbers = true;
                    continue;
                }
                
                int num = int.Parse(match);
                if (readingMyNumbers && winningNumbers.Contains(num)) valid++;
                else if (!readingMyNumbers) winningNumbers.Add(num);
            }

            // pt1
            points += (int) Math.Pow(2, valid - 1);

            // pt2
            for (int i = 0; i < copies[cardNum - 1]; i++)
            {
                for (int k = cardNum; k - cardNum < valid; k++)
                {
                    copies[k]++;
                }
            }
        }

        return pt2 ? copies.Sum() : points;
    }
}