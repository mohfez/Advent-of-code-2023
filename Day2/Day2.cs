using System.Text.RegularExpressions;

public class Day2
{
    private struct Game { public int gameID, red, green, blue; }

    public int Answer(bool pt2)
    {
        List<Game> games = new List<Game>();
        string[] input = File.ReadAllLines("Day2/input.txt");

        string gameNumRegex = @"Game (\d+):";
        string getColours = @"(\d+) (red|green|blue)";

        foreach (string line in input)
        {
            Game currentGame = new Game();
            currentGame.gameID = int.Parse(Regex.Match(line, gameNumRegex).Groups[1].Value);

            foreach (Match match in Regex.Matches(line, getColours))
            {
                int colourAmount = int.Parse(match.Groups[1].Value);
                string colour = match.Groups[2].Value;

                switch (colour)
                {
                    case "red":
                        if (colourAmount > currentGame.red) currentGame.red = colourAmount;
                        break;
                    case "green":
                        if (colourAmount > currentGame.green) currentGame.green = colourAmount;
                        break;
                    case "blue":
                        if (colourAmount > currentGame.blue) currentGame.blue = colourAmount;
                        break;
                }
            }

            games.Add(currentGame);
        }

        int idSum = 0;
        int power = 0;
        foreach (Game game in games)
        {
            if (game.red <= 12 && game.green <= 13 && game.blue <= 14) idSum += game.gameID;
            power += game.red * game.green * game.blue;
        }

        return pt2 ? power : idSum;
    }
}