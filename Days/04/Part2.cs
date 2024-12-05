using System;
using System.IO;
using System.Text.RegularExpressions;

namespace _04
{
    internal class Part2
    {
        static void Main(string[] args)
        {
            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));
            int total = 0;
            Regex mas = new Regex(@"(MAS|SAM)", RegexOptions.Compiled);
            for (int i = 0; i < values.Length - 2; i++)
            {
                for (int j = 0; j < values[i].Length - 2; j++)
                {

                    string leftToRight = $"{values[i][j]}{values[i + 1][j + 1]}{values[i + 2][j + 2]}";
                    string TopToLeft = $"{values[i][j + 2]}{values[i + 1][j + 1]}{values[i + 2][j]}";

                    if (mas.IsMatch(leftToRight) && mas.IsMatch(TopToLeft))
                    {
                        total++;
                    }


                }
            }

            Console.WriteLine(total);
        }
    }
}