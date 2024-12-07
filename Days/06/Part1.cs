using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _06
{
    internal class Part1
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
            //Console.WriteLine("Direction: " + direction);
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
                if (currentIndex + 1 > pos.Count-1)
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
        public String[] returnTileofDirection(String[] tiles)
        {
            if (tiles == null)
                return tiles;

            var direction = String.Join("", tiles).Where(x => x == '^' || x == '>' || x == '<' || x == 'v').First();
            //Console.WriteLine(direction);
            // Get position of char in the lists
            int index = Array.IndexOf(tiles, tiles.Where(x => x.Contains(direction)).First());
            int position = tiles[index].IndexOf(direction);

            //Console.WriteLine("Direction: " + direction + " Index: " + index + " Position " + position);
            switch (direction)
            {
                case '^':
                    if (index - 1 < 0)
                    {
                        return null;
                    }
                    return moveGuard(tiles, index, position, FindMovement(direction, tiles[index - 1][position].ToString()));

                case '>':
                    if (position + 1 >= tiles[index].Length)
                    {
                        return null;
                    }
                    return moveGuard(tiles, index, position, FindMovement(direction, tiles[index][position + 1].ToString()));
                case '<':
                    if (position - 1 < 0)
                    {
                        return null;
                    }
                    return moveGuard(tiles, index, position, FindMovement(direction, tiles[index][position - 1].ToString()));
                case 'v':
                    if (index + 1 >= tiles.Length)
                    {
                        return null;
                    }
                    return moveGuard(tiles, index, position, FindMovement(direction, tiles[index + 1][position].ToString()));
            }
            return tiles;
        }
        static void Main2(string[] args)
        {

            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));

            Part1 part1 = new Part1();
            int totalMoves = 0;
            String[] moves;
            while ((moves = part1.returnTileofDirection(values)) != null)
            {
                values = moves;
                totalMoves++;
            }
            Console.WriteLine("Moved: " + totalMoves);
            Console.WriteLine("Counted *'s: " + (values.Sum(v => v.Count(c => c == '*')) + 1));
        }
    }
}