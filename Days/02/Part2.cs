namespace _02
{
    internal class Part2
    {
        static void Main(string[] args)
        {
            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));
            Part2 part2 = new Part2();
            int safeSpots = 0;
            for (int e = 0; e < values.Length; e++)
            {
                var value = values[e];
                var report = value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

               
                if (part2.isSafe(report))
                {
                    safeSpots++;
                }
                else
                {
                    for (int i = 0; i < report.Count; i++)
                    {
                        var reportCopy = report.ToList();
                        reportCopy.RemoveAt(i);
                        if (part2.isSafe(reportCopy))
                        {
                            safeSpots++;
                            break;
                        }
                    }

                }
            }

            Console.WriteLine(safeSpots);
        }
        public bool isSafe(List<int> locations)
        {
            if (locations.Count < 2)
            {
                return true;
            }

            var firstChange = locations[1] - locations[0];

            if (firstChange == 0 || Math.Abs(firstChange) > 3)
            {
                return false;
            }

            var direction = firstChange / Math.Abs(firstChange);

            for (int i = 1; i < locations.Count - 1; i++)
            {

                var change = locations[i + 1] - locations[i];
                //Console.WriteLine("Change: " + change);

                // Sudden Change check
                if (change == 0 || Math.Abs(change) > 3)
                {
                    return false;
                }

                var InDirection = change / Math.Abs(change);

                if (InDirection != direction)
                {
                    return false;
                }
            }
            return true;
        }
    }
}