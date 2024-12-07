using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _05
{
    internal class Part2
    {
        static void Main(string[] args)
        {
            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));

            List<String[]> Rules = new List<String[]>();

            int Seperator = 0;

            int total = 0;

            for (int i = 0; i < values.Length; i++)
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

                List<String> elements = new List<string>(Instructions[e].Split(","));
                bool edited = false;
                for (int j = 0;  j < elements.Count; j++)
                {
                    var element = elements[j];

                    var first = Rules.FindAll(x => x[1] == element && elements.Contains(x[0]) && !elements.Take(j).Contains(x[0]));

                    if (first != null)
                    {
                        foreach (String[] firstelm in first)
                        {
                            if (elements.Contains(firstelm[0]) && !elements.Take(j).Contains(firstelm[0]))
                            {
                                var indexes = new List<int>();
                                for (int i = 0; i < elements.Count; i++)
                                {
                                    if (elements[i] == firstelm[0])
                                    {
                                        indexes.Add(i);
                                    }
                                }

                                foreach (var index in indexes)
                                {
                                    var secondIndex = elements.IndexOf(firstelm[1]);
                                    elements[index] = firstelm[1];
                                    elements[secondIndex] = firstelm[0];
                                }

                                j = -1;
                                edited = true;
                            }
                        }
                    }
                }
                if (edited)
                {
                    //Console.WriteLine(String.Join(",", elements));
                    total += Int32.Parse(elements[elements.Count / 2]);
                }
            }
            Console.WriteLine(total);
        }
    }
}