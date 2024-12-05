using System.Linq;
using System.Text.RegularExpressions;

namespace _04
{
    internal class Part1
    {
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        static void Main2(string[] args)
        {
            String[] values = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));
            int total = 0;
            //Horizontal
            Regex xmas = new Regex(@"XMAS", RegexOptions.Compiled);
            for (int e = 0; e < values.Length; e++)
            {
                var value = values[e];
                
                

                total += xmas.Matches(value).Count;

                //Rev Horizontal
                var valueR = Reverse(value);

                total += xmas.Matches(valueR).Count;

            }

            //Verticle
            for (int i = 0; i < values.Length; i++)
            {
                var value = values[i].ToArray();
                var verticalString = "";
                for (int j = 0; j < value.Length; j++)
                {
                    verticalString += values[j][i];
                }

                
                total += xmas.Matches(verticalString).Count;

                //Rev Verticle
                var vertR = Reverse(verticalString);
                total += xmas.Matches(vertR).Count;

            }

            //Diagonal (top-left to bottom-right)
            for (int i = 0; i < values.Length; i++)
            {
                var diagonalString = "";
                for (int j = 0; j < values.Length - i; j++)
                {
                    diagonalString += values[i + j][j];
                }

                total += xmas.Matches(diagonalString).Count;

                //Rev Diagonal
                var diagR = Reverse(diagonalString);
                total += xmas.Matches(diagR).Count;
            }

            for (int i = 1; i < values.Length; i++)
            {
                var diagonalString = "";
                for (int j = 0; j < values.Length - i; j++)
                {
                    diagonalString += values[j][i + j];
                }

                total += xmas.Matches(diagonalString).Count;

                //Rev Diagonal
                var diagR = Reverse(diagonalString);
                total += xmas.Matches(diagR).Count;
            }

            //Backwards Diagonal (top-right to bottom-left)
            for (int i = 0; i < values.Length; i++)
            {
                var backDiagonalString = "";
                for (int j = 0; j < values.Length - i; j++)
                {
                    backDiagonalString += values[i + j][values.Length - 1 - j];
                }

                total += xmas.Matches(backDiagonalString).Count;

                //Rev Backwards Diagonal
                var backDiagR = Reverse(backDiagonalString);
                total += xmas.Matches(backDiagR).Count;
            }

            for (int i = 1; i < values.Length; i++)
            {
                var backDiagonalString = "";
                for (int j = 0; j < values.Length - i; j++)
                {
                    backDiagonalString += values[j][values.Length - 1 - (i + j)];
                }

                total += xmas.Matches(backDiagonalString).Count;

                // Rev Backwards Diagonal
                var backDiagR = Reverse(backDiagonalString);
                total += xmas.Matches(backDiagR).Count;
            }


            Console.WriteLine(total);
        }
    }
}