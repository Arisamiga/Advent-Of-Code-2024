namespace _07
{
    internal class Part1
    {
        static public bool Solve (ulong result, int index, ulong expected, ulong[] numbers)
        {
           if (result > expected)
            {
                return false;
            }
            if (index == numbers.Length)
            {
                return result == expected;
            }
            return Solve(result + numbers[index], index + 1, expected, numbers) || Solve(result * numbers[index], index + 1, expected, numbers);
        }

        static public bool ableToSolve(ulong[] numbers, ulong expected)
        {
            return Solve(numbers[0], 1, expected, numbers);
        }
        static void Main(string[] args)
        {

            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));
            ulong total = 0;
            foreach (String value in values)
            {
                var locations = value.Split(" ");
                locations = locations.Where(a => !string.IsNullOrWhiteSpace(a)).ToArray();
                locations = locations.Select(a => a.Replace(":", "")).ToArray();
                
                var expected = ulong.Parse(locations[0].Trim());

                var numbers = locations.Skip(1).Select(a => ulong.Parse(a)).ToArray();
                
                if (ableToSolve(numbers, expected))
                {
                    total += expected;
                }
            };
            Console.WriteLine(total);
        }
    }
}
