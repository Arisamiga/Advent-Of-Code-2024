using System.Linq;

namespace _11
{
    internal class Part2
    {
        public static Dictionary<ulong, ulong> Blink(Dictionary<ulong, ulong> rocksList)
        {
            Dictionary<ulong, ulong> rocks = new Dictionary<ulong, ulong>();
            foreach (var kvp in rocksList)
            {
                ulong rock = kvp.Key;
                ulong occurrences = kvp.Value;

                if (rock == 0)
                {
                    if (rocks.ContainsKey(1))
                    {
                        rocks[1] += occurrences;
                    }
                    else
                    {
                        rocks[1] = occurrences;
                    }
                }
                else if (rock.ToString().Length % 2 == 0)
                {
                    string rockStr = rock.ToString();
                    int mid = rockStr.Length / 2;
                    ulong leftHalf = ulong.Parse(rockStr.Substring(0, mid));
                    ulong rightHalf = ulong.Parse(rockStr.Substring(mid));

                    if (rocks.ContainsKey(leftHalf))
                    {
                        rocks[leftHalf] += occurrences;
                    }
                    else
                    {
                        rocks[leftHalf] = occurrences;
                    }

                    if (rocks.ContainsKey(rightHalf))
                    {
                        rocks[rightHalf] += occurrences;
                    }
                    else
                    {
                        rocks[rightHalf] = occurrences;
                    }
                }
                else
                {
                    ulong newRock = rock * 2024;
                    if (newRock < rock) // Check for overflow
                    {
                        Console.WriteLine("OVERFLOW");
                    }

                    if (rocks.ContainsKey(newRock))
                    {
                        rocks[newRock] += occurrences;
                    }
                    else
                    {
                        rocks[newRock] = occurrences;
                    }
                }
            }
            return rocks;
        }

        static void Main(string[] args)
        {
            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));

            Dictionary<ulong, ulong> initialRocks = values[0].Split(" ").Select(ulong.Parse).ToDictionary(x => x, x => 1UL);

            for (int i = 0; i < 75; i++)
            {
                initialRocks = Blink(initialRocks);
            }

            // Get all occurrences and add them together
            ulong totalRocks = initialRocks.Values.Aggregate((x, y) => x + y);

            Console.WriteLine(totalRocks);
        }
    }
}