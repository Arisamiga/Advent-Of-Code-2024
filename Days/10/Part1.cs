using System.Drawing;
namespace _01
{

    internal class Part1
    {
        public static int GetScoreFromPoint(Point point, int[,] map, int width, int height)
        {
            int score = 0;
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(point);

            HashSet<Point> visited = new HashSet<Point>();

            while (queue.Count > 0)
            {
                Point p = queue.Dequeue();
                
                var value = map[p.X, p.Y];
                if (value == 9)
                {
                    score++;
                    continue;
                }
                foreach(var direction in new Point[] { new Point(0, 1), new Point(1, 0), new Point(0, -1), new Point(-1, 0) })
                {
                    var newPoint = new Point(p.X + direction.X, p.Y + direction.Y);
                    if ((newPoint.X >= 0 && newPoint.X < width && newPoint.Y >= 0 && newPoint.Y < height) 
                        && !visited.Contains(newPoint) 
                        && map[newPoint.X, newPoint.Y] == (value + 1))
                    {
                        visited.Add(newPoint);
                        queue.Enqueue(newPoint);
                    }
                }

            }
            return score;
        }

        static void Main1(string[] args)
        {
            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));

            int width = values[0].Length;
            int height = values.Count();

            int[,] map = new int[width, height];

            Console.WriteLine(width + " " + height);

            List<Point> startingPoints = new List<Point>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = values[y][x] - '0';

                    if (map[x, y] == 0)
                    {
                        startingPoints.Add(new Point(x, y));
                    }
                }

            }

            var total = 0;

            foreach (var point in startingPoints)
            {
                total += GetScoreFromPoint(point, map, width, height);
            }
            Console.WriteLine(total);
        }
    }
}