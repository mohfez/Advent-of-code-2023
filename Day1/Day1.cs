using System.Text.RegularExpressions;

public class Day1
{
    private int ToDigit(string input)
    {
        switch (input)
        {
            case "one": return 1;
            case "two": return 2;
            case "three": return 3;
            case "four": return 4;
            case "five": return 5;
            case "six": return 6;
            case "seven": return 7;
            case "eight": return 8;
            case "nine": return 9;
        }

        return int.Parse(input);
    }

    public int Answer(bool pt1)
    {
        string[] input = File.ReadAllLines("Day1/input.txt");
        int ans = 0;

        foreach (string line in input)
        {
            if (pt1)
            {
                MatchCollection nums = Regex.Matches(line, @"\d");
                ans += ToDigit(nums[0].Value) * 10 + ToDigit(nums[^1].Value);
            }
            else
            {
                string firstNum = Regex.Match(line, @"one|two|three|four|five|six|seven|eight|nine|\d").Value; // eightwo -> eight
                string secondNum = Regex.Match(line, @"one|two|three|four|five|six|seven|eight|nine|\d", RegexOptions.RightToLeft).Value; // eightwo -> two
                ans += ToDigit(firstNum) * 10 + ToDigit(secondNum);
            }
        }

        return ans;
    }
}