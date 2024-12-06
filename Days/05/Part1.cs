using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _05
{
    internal class Part1
    {
        static void Main2(string[] args)
        {
            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));
            
            List<String[]> Rules = new List<String[]>();

            int Seperator = 0;

            int total = 0;

            for (int i  = 0; i < values.Length; i++)
            {
                if (values[i] == "")
                {
                    Seperator = i;
                    break;
                }

                Rules.Add(values[i].Split("|"));
            }

            String[] Instructions = values.Skip(Seperator + 1).ToArray();

            for (int e = 0; e < Instructions.Length; e++)
            {

                var elements = Instructions[e].Split(",");
                List<String> ValuesAdded = new List<String>();
                bool standingGood = true;
                foreach (String element in elements)
                {
                    var first = Rules.FindAll(x => x[1] == element);

                    if (first != null)
                    {
                        foreach (String[] firstelm  in first)
                        {
                            if (elements.Contains(firstelm[0]) && !ValuesAdded.Contains(firstelm[0]))
                            {
                                //Console.WriteLine("Bad at " + e + " As " + first[0] + " with " + first[1]);
                                standingGood = false;
                                break;
                            }
                        }
                    }

                    ValuesAdded.Add(element);
                }

                if (standingGood)
                {
                    //Get middle value of array
                    //Console.WriteLine("Adding: " + Int32.Parse(elements[elements.Length / 2]));
                    total += Int32.Parse(elements[elements.Length / 2]);
                }
            }
            Console.WriteLine(total);
        }
    }
}