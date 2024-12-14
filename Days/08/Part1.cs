﻿using System.Drawing;

namespace _08
{
    internal class Part1
    {
        static void Main1(string[] args)
        {
            int width = 0;
            int height = 0;

            Dictionary<char, List<Point>> antennas = new();

            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));

            foreach (String value in values)
            {
                var locations = value.ToList().Where(a => !String.IsNullOrWhiteSpace(a.ToString())).ToArray();
                width = locations.Length;
                height++;
            }

            Console.WriteLine(width + " " + height);

            var matrix = new char[width, height];

            for (int y = 0; y < height; y++)
            {
                var line = values[y];

                for (int x = 0; x < width; x++)
                {
                    matrix[x, y] = line[x];

                    if (line[x] != '.')
                    {
                        if (!antennas.ContainsKey(line[x]))
                        {
                            antennas.Add(line[x], new List<Point>());
                        }
                        antennas[line[x]].Add(new Point(x, y));
                    }
                }
            }

            HashSet<Point> antinodes = new();

            foreach (var antennaChar in antennas)
            {
                var points = antennaChar.Value;
                for (int antenna = 0; antenna < points.Count; antenna++)
                {
                    var point = points[antenna];

                    for (int secondAntenna = antenna + 1; secondAntenna < points.Count; secondAntenna++)
                    {
                        var difference = new Point(
                            points[secondAntenna].X - points[antenna].X,
                            points[secondAntenna].Y - points[antenna].Y
                        );

                        antinodes.Add(new Point(points[antenna].X - difference.X, points[antenna].Y - difference.Y));
                        antinodes.Add(new Point(points[secondAntenna].X + difference.X, points[secondAntenna].Y + difference.Y));
                    }
                }
            }

            Console.WriteLine("---- " + antinodes.Where(p => p.X >= 0 && p.X < width && p.Y >= 0 && p.Y < height).Count());
        }
    }
}