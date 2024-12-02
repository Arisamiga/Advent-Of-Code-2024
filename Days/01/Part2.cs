namespace _01
{
    internal class Part2
    {
        static void Main(string[] args)
        {

            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));

            List<string> left = new List<string>();
            List<string> right = new List<string>();
            var totalNum = 0;

            foreach (String value in values)
            {
                var locations = value.Split(" ");
                locations = locations.Where(a => !string.IsNullOrWhiteSpace(a)).ToArray();
                left.Add(locations[0]);
                right.Add(locations[1]);
            };


            foreach (String leftV in left)
            {
                var counts = right.Where(a => a == leftV).ToArray();
                totalNum += Int32.Parse(leftV) * counts.Length;
                
            };
            Console.WriteLine(totalNum);
        }
    }
}
