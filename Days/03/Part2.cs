using System.Text.RegularExpressions;

namespace _03
{
    internal class Part2
    {
        static void Main(string[] args)
        {
            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));

            String word = string.Join("\n", values);

            Regex regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)", RegexOptions.Compiled);
            int total = 0;
            bool ableToMultiply = true;
            foreach (Match match in regex.Matches(word))
            {
                if (match.Value.IndexOf("mul") != -1 && ableToMultiply)
                {
                    var nums = match.Value.Replace("mul(", "").Replace(")", "").Split(",");
                    total += (Int32.Parse(nums[0]) * Int32.Parse(nums[1]));
                }
                else if (match.Value.IndexOf("don") != -1)
                {
                    ableToMultiply = false;
                }
                else if (match.Value.IndexOf("do") != -1)
                {
                    ableToMultiply = true;
                }
            }
            Console.WriteLine("Total: " + total);
        }
    }
}