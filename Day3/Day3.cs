using System.Text.RegularExpressions;

public class Day3
{
    private struct Symbol { public string symbol; public int row; public int col; }
    private struct Number { public int num; public int row; public int col; public int length; }

    public int Answer(bool pt2)
    {
        string[] input = File.ReadAllLines("Day3/input.txt");
        string getNumsRegex = @"\d+";
        string getSymbolsRegex = @"[^.\d]";

        List<Symbol> symbols = new List<Symbol>();
        List<Number> numbers = new List<Number>();
        for (int row = 0; row < input.Length; row++)
        {
            MatchCollection symbolMatches = Regex.Matches(input[row], getSymbolsRegex);
            MatchCollection numberMatches = Regex.Matches(input[row], getNumsRegex);

            foreach (Match symbol in symbolMatches)
            {
                symbols.Add(new Symbol { symbol = symbol.Value, row = row, col = symbol.Index });
            }

            foreach (Match number in numberMatches)
            {
                numbers.Add(new Number { num = int.Parse(number.Value), row = row, col = number.Index, length = number.Length });
            }
        }

        int pt1Ans = 0;
        foreach (Number number in numbers)
        {
            foreach (Symbol symbol in symbols)
            {
                if (Near(symbol, number)) pt1Ans += number.num;
            }
        }

        int pt2Ans = 0;
        foreach (Symbol symbol in symbols)
        {
            if (symbol.symbol != "*") continue;
            int found = 0;
            int gears = 1;

            foreach (Number number in numbers)
            {
                if (Near(symbol, number))
                {
                    gears *= number.num;
                    found++;

                    if (found == 2)
                    {
                        pt2Ans += gears;
                        break;
                    }
                }
            }
        }

        return pt2 ? pt2Ans : pt1Ans;
    }

    private bool Near(Symbol symbol, Number number)
    {
        return !(number.row - symbol.row < -1 || number.row - symbol.row > 1) && (symbol.col >= number.col - 1 && symbol.col <= number.col + number.length);
    }
}