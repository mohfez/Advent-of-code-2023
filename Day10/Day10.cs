public class Day10
{
    private enum Direction { UP, DOWN, LEFT, RIGHT }

    public int Answer(bool pt2)
    {
        char[][] input = File.ReadAllLines("Day10/input.txt").Select(line => line.ToCharArray()).ToArray();

        (int y, int x) startPos = FindStart(input);
        (int steps, HashSet<(int, int)> passed)[] stepsArr = new (int steps, HashSet<(int, int)> passed)[] { FindSteps(startPos, Direction.UP, input), FindSteps(startPos, Direction.DOWN, input),
                                                                                                             FindSteps(startPos, Direction.LEFT, input), FindSteps(startPos, Direction.RIGHT, input) };
        Array.Sort(stepsArr, (a, b) => { return b.steps - a.steps; });

        return pt2 ? CountInsideLoop(input, stepsArr[0].passed) : stepsArr[0].steps;
    }

    private (int y, int x) FindStart(char[][] input)
    {
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == 'S') return (y, x);
            }
        }

        return (-1, -1);
    }

    private (int steps, HashSet<(int, int)> passed) FindSteps((int y, int x) startPos, Direction direction, char[][] input)
    {
        HashSet<(int, int)> passed = new HashSet<(int, int)>();
        (int y, int x) current = startPos;

        while (!passed.Contains(current))
        {
            passed.Add(current);
            switch (direction)
            {
                case Direction.UP:
                    if (current.y - 1 < 0) continue;
                    current = (current.y - 1, current.x);
                    switch (input[current.y][current.x])
                    {
                        case '|':
                            continue;
                        case '7':
                            direction = Direction.LEFT;
                            break;
                        case 'F':
                            direction = Direction.RIGHT;
                            break;
                    }
                    break;
                case Direction.DOWN:
                    if (current.y + 1 >= input.Length) continue;
                    current = (current.y + 1, current.x);
                    switch (input[current.y][current.x])
                    {
                        case '|':
                            continue;
                        case 'J':
                            direction = Direction.LEFT;
                            break;
                        case 'L':
                            direction = Direction.RIGHT;
                            break;
                    }
                    break;
                case Direction.LEFT:
                    if (current.x - 1 < 0) continue;
                    current = (current.y, current.x - 1);
                    switch (input[current.y][current.x])
                    {
                        case '-':
                            continue;
                        case 'L':
                            direction = Direction.UP;
                            break;
                        case 'F':
                            direction = Direction.DOWN;
                            break;
                    }
                    break;
                case Direction.RIGHT:
                    if (current.x + 1 >= input[0].Length) continue;
                    current = (current.y, current.x + 1);
                    switch (input[current.y][current.x])
                    {
                        case '-':
                            continue;
                        case 'J':
                            direction = Direction.UP;
                            break;
                        case '7':
                            direction = Direction.DOWN;
                            break;
                    }
                    break;
            }
        }

        return (passed.Count / 2, passed);
    }

    private bool IsInsideLoop(char[][] input, (int y, int x) current)
    {
        if (input[current.y][current.x] != '.') return false;

        bool inside = false;
        current = (current.y, current.x + 1);
        while (current.x < input[0].Length)
        {
            switch (input[current.y][current.x])
            {
                case 'J':
                case 'L':
                case '|':
                    inside = !inside;
                    break;
            }
            current = (current.y, current.x + 1);
        }

        return inside;
    }

    private int CountInsideLoop(char[][] input, HashSet<(int, int)> passed)
    {
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (!passed.Contains((y, x))) input[y][x] = '.';
            }
        }

        int count = 0;
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (IsInsideLoop(input, (y, x))) count++;
            }
        }

        return count;
    }
}