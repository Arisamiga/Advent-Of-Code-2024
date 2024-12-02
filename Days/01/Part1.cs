namespace _01
{
    internal class Part1
    {
        static void Main(string[] args)
        {

            String [] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));

            List<string> left = new List<string>();
            List<string> right = new List<string>();

            foreach (String value in values)
            {
                var locations = value.Split(" ");
                locations = locations.Where(a => !string.IsNullOrWhiteSpace(a)).ToArray();
                left.Add(locations[0]);
                right.Add(locations[1]);
            };
            right.Sort();
            left.Sort();

            var TotalNum = 0;
            for (int i = 0; i < right.Count; i++)
            {
                var leftV = Int32.Parse(left[i]);
                var rightV = Int32.Parse(right[i]);

                TotalNum += Math.Abs(leftV - rightV);
            }

            Console.WriteLine("Total: " +  TotalNum);
        }
    }
}
