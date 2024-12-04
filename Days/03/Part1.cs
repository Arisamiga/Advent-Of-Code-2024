using System.Text.RegularExpressions;

namespace _03
{
    internal class Part1
    {
        static void Main(string[] args)
        {
            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));

            String word = string.Join("\n", values);

            Regex regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)", RegexOptions.Compiled);
            int total = 0;
            foreach (Match match in regex.Matches(word))
            {
                var nums = match.Value.Replace("mul(", "").Replace(")", "").Split(",");

                total += (Int32.Parse(nums[0]) * Int32.Parse(nums[1]));
            }
            Console.WriteLine("Total: " + total);
        }
    }
}