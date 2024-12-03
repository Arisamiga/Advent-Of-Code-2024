namespace _02
{
    internal class Part1
    {
        static void Main2(string[] args)
        {

            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));

            

            int safeSpots = 0;
            foreach (String value in values)
            {
                int safeness = 0; // 0 = unsafe 1=safe
                var locations = value.Split(" ");
                locations = locations.Where(a => !string.IsNullOrWhiteSpace(a)).ToArray();
                int previousNum = -1;
                var state = "null"; 
                foreach (String location in locations)
                {
                    if (previousNum == -1)
                    {
                        previousNum = Int32.Parse(location);
                        continue;
                    }
                    if (Int32.Parse(location) > previousNum)
                    {
                        if (state != "Increase" && state != "null")
                        {
                            //no
                            //Console.WriteLine(Int32.Parse(location) + " / " + previousNum);
                            safeness = 0;
                            break;
                        }

                        state = "Increase";
                    }
                    else if (Int32.Parse(location) < previousNum)
                    {
                        if (state != "Decreases" && state != "null")
                        {
                            //no
                            //Console.WriteLine(Int32.Parse(location) + " | " +  previousNum);
                            safeness = 0;
                            break;
                        }
                        state = "Decreases";
                    }

                    int change = Math.Abs(Int32.Parse(location) - previousNum);
                    //Console.WriteLine("Change: "+ change);

                    // Suddent Change check
                    if (change > 3 || change == 0)
                    {
                        //uNSAFE
                        safeness = 0;
                        break;
                    }

                    safeness = 1;
                    previousNum = Int32.Parse(location);
                }
                //Console.WriteLine(safeness);
                if (safeness != 0)
                {
                    safeSpots++;
                    safeness = 0;
                }
            };

            Console.WriteLine(safeSpots);
        }
    }
}