namespace _11
{
    internal class Part1
    {
        public static List<ulong> Blink(List<ulong> rocksList)
        {
            List<ulong> rocks = rocksList;
            for (int i = 0; i < rocks.Count; i++)
            {

                if (rocks[i] < 0)
                {
                    Console.WriteLine("Rock is empty");
                }
                if (rocks[i] == 0)
                {
                    rocks[i] = 1;
                }
                else if (rocks[i].ToString().Length % 2 == 0)
                {
                    string rockStr = rocks[i].ToString();
                    int mid = rockStr.Length / 2;
                    //Console.WriteLine(rocks[i].ToString());
                    int leftHalf = int.Parse(rockStr.Substring(0, mid));
                    int rightHalf = int.Parse(rockStr.Substring(mid));
                    rocks[i] = (ulong)leftHalf;
                    rocks.Insert(i + 1, (ulong)rightHalf);
                    i++;
                }
                else
                {
                    if (rocks[i] * 2024 < 0)
                    {
                        Console.WriteLine("OVERFLOW");
                    }
                    rocks[i] = rocks[i] * 2024;
                }
            }
            return rocks;
        }

        static void Main(string[] args)
        {
            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));

            List<ulong> rocks = values[0].Split(" ").Select(ulong.Parse).ToList();
            
            for (int i = 0; i < 25; i++)
            {
                rocks = Blink(rocks);
            }

            // Remove any empty rocks
            rocks.RemoveAll(x => x < 0);

            Console.WriteLine(rocks.Count());

        }
    }
}
