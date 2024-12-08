using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _06
{
    internal class Part2
    {
        List<string> pos = new List<string> { "^", ">", "v", "<" };
        public String[] moveGuard(String[] tiles, int index, int position, String direction)
        {
            string[] newTiles = (string[])tiles.Clone();
            switch (direction)
            {
                case "^":
                    newTiles[index - 1] = newTiles[index - 1].Remove(position, 1).Insert(position, "^");
                    break;
                case ">":
                    newTiles[index] = newTiles[index].Remove(position + 1, 1).Insert(position + 1, ">");
                    break;
                case "<":
                    newTiles[index] = newTiles[index].Remove(position - 1, 1).Insert(position - 1, "<");
                    break;
                case "v":
                    newTiles[index + 1] = newTiles[index + 1].Remove(position, 1).Insert(position, "v");
                    break;
            }
            newTiles[index] = newTiles[index].Remove(position, 1).Insert(position, "*");
            return newTiles;
        }

        public String FindMovement(char direction, String Front)
        {
            if (Front == "." || Front == "*")
            {
                return direction.ToString();
            }
            else if (Front == "#")
            {
                int currentIndex = pos.IndexOf(direction.ToString());
                if (currentIndex + 1 > pos.Count - 1)
                {
                    return pos[0];

                }
                else
                {
                    return pos[currentIndex + 1];
                }
            }
            return "";
        }

        public (String[], Point?, String) returnTileofDirection(String[] tiles)
        {
            if (tiles == null)
                return (tiles, null, "");

            var direction = String.Join("", tiles).Where(x => x == '^' || x == '>' || x == '<' || x == 'v').First();
            int index = Array.IndexOf(tiles, tiles.Where(x => x.Contains(direction)).First());
            int position = tiles[index].IndexOf(direction);

            switch (direction)
            {
                case '^':
                    if (index - 1 < 0)
                    {
                        return (null, null, "");
                    }
                    return (moveGuard(tiles, index, position, FindMovement(direction, tiles[index - 1][position].ToString())), new Point(index, position), direction.ToString());
                case '>':
                    if (position + 1 >= tiles[index].Length)
                    {
                        return (null, null, "");
                    }
                    return (moveGuard(tiles, index, position, FindMovement(direction, tiles[index][position + 1].ToString())), new Point(index, position), direction.ToString());
                case '<':
                    if (position - 1 < 0)
                    {
                        return (null, null, "");
                    }
                    return (moveGuard(tiles, index, position, FindMovement(direction, tiles[index][position - 1].ToString())), new Point(index, position), direction.ToString());
                case 'v':
                    if (index + 1 >= tiles.Length)
                    {
                        return (null, null, "");
                    }
                    return (moveGuard(tiles, index, position, FindMovement(direction, tiles[index + 1][position].ToString())), new Point(index, position), direction.ToString());
            }
            return (tiles, new Point(index, position), direction.ToString());
        }

        public bool isGuardLooping(String[] tiles)
        {
            if (tiles == null)
                return false;

            List<(Point, String)> obstructionPoints = new List<(Point, String)>();

            while (true)
            {
                var direction = String.Join("", tiles).Where(x => x == '^' || x == '>' || x == '<' || x == 'v').First();
                int index = Array.IndexOf(tiles, tiles.Where(x => x.Contains(direction)).First());
                int position = tiles[index].IndexOf(direction);
                Point currentPoint = new Point(index, position);
                if (obstructionPoints.Any(x => x.Item1 == currentPoint && x.Item2 == direction.ToString()))
                {
                    return true;
                }

                switch (direction)
                {
                    case '^':
                        if (index - 1 < 0)
                        {
                            return false;
                        }
                        tiles = moveGuard(tiles, index, position, FindMovement(direction, tiles[index - 1][position].ToString()));
                        break;
                    case '>':
                        if (position + 1 >= tiles[index].Length)
                        {
                            return false;
                        }
                        tiles = moveGuard(tiles, index, position, FindMovement(direction, tiles[index][position + 1].ToString()));
                        break;
                    case '<':
                        if (position - 1 < 0)
                        {
                            return false;
                        }
                        tiles = moveGuard(tiles, index, position, FindMovement(direction, tiles[index][position - 1].ToString()));
                        break;
                    case 'v':
                        if (index + 1 >= tiles.Length)
                        {
                            return false;
                        }
                        tiles = moveGuard(tiles, index, position, FindMovement(direction, tiles[index + 1][position].ToString()));
                        break;
                }
                obstructionPoints.Add((currentPoint, direction.ToString()));
            }
        }

        static async Task Main(string[] args)
        {
            using var inputReader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));
            Part2 part2 = new Part2();
            string result = await part2.SolveAsync(inputReader);
            Console.WriteLine("Obstructions: " + result);
        }

        public async Task<string> SolveAsync(StreamReader inputReader)
        {
            List<string> lines = new();
            while (await inputReader.ReadLineAsync() is { } line)
            {
                if (_width == 0)
                {
                    _width = line.Length;
                }
                else if (line.Length != _width)
                {
                    Console.WriteLine($"Warning: Skipping line with incorrect length: {line}");
                    continue;
                }

                _height++;
                lines.Add(line);
            }

            _map = new char[_width, _height];
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _map[x, y] = lines[y][x];
                    if (_map[x, y] == '^')
                    {
                        _startingPoint = new Point(x, y);
                    }
                }
            }

            var potentialObstructions = GetPotentialObstructionPositions(_startingPoint);

            int obstructionCount = 0;
            foreach (var potentialObstruction in potentialObstructions.Except(new[] { _startingPoint }))
            {
                if (DoesGuardLoop(_startingPoint, potentialObstruction))
                {
                    obstructionCount++;
                }
            }

            return obstructionCount.ToString();
        }

        private bool DoesGuardLoop(Point start, Point newObstruction)
        {
            HashSet<(Point point, Point direction)> visited = new();

            var currentDirection = new Point(0, -1);
            var currentPoint = start;

            while (true)
            {
                if (visited.Contains((currentPoint, currentDirection)))
                {
                    return true;
                }

                visited.Add((currentPoint, currentDirection));
                var nextPosition = currentPoint + currentDirection;
                if (IsOutOfBounds(nextPosition))
                {
                    break;
                }

                if (_map[nextPosition.X, nextPosition.Y] == '#' ||
                    (nextPosition.X == newObstruction.X && nextPosition.Y == newObstruction.Y))
                {
                    // Turn right
                    currentDirection = new Point(-currentDirection.Y, currentDirection.X);
                    nextPosition = currentPoint;
                }

                if (IsOutOfBounds(nextPosition))
                {
                    break;
                }

                currentPoint = nextPosition;
            }

            return false;
        }

        private HashSet<Point> GetPotentialObstructionPositions(Point start)
        {
            HashSet<Point> visited = new();

            var currentDirection = new Point(0, -1);
            var currentPoint = start;
            while (true)
            {
                visited.Add(currentPoint);
                var nextPosition = currentPoint + currentDirection;
                if (IsOutOfBounds(nextPosition))
                {
                    break;
                }

                if (_map[nextPosition.X, nextPosition.Y] == '#')
                {
                    // Turn right
                    currentDirection = new Point(-currentDirection.Y, currentDirection.X);
                    nextPosition = currentPoint;
                }

                if (IsOutOfBounds(nextPosition))
                {
                    break;
                }

                currentPoint = nextPosition;
            }

            return visited;
        }

        private bool IsOutOfBounds(Point position)
        {
            return position.X < 0 || position.Y < 0 || position.X >= _width || position.Y >= _height;
        }

        private int _width;
        private int _height;
        private char[,] _map;
        private Point _startingPoint;
    }

    public struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
        public static bool operator ==(Point a, Point b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Point a, Point b) => !(a == b);

        public override bool Equals(object obj)
        {
            if (obj is Point point)
            {
                return this == point;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}