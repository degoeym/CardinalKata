using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CardinalKata
{
    class Program
    {
        private static readonly Regex nonDigits = new Regex(@"[^\d]+");
        static void Main(string[] args)
        {
            CalculateSpread("weather.dat", 0, 1, 2);
            CalculateSpread("football.dat", 1, 6, 8);
        }

        private static void CalculateSpread(string fileName, int keyPosition, int valueOne, int valueTwo)
        {
            var data = new List<KeyValuePair<string, int>>();

            using (var reader = new StreamReader(fileName)) 
            {
                var firstLine = reader.ReadLine(); // Read the first line to remove the column headers
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line)) // Just skip empty lines
                    {
                        var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        if (parts[0].Any(char.IsDigit)) // Lines we care about start with a number of some sort, so only care about
                        {
                            var day = parts[keyPosition];
                            Int32.TryParse(nonDigits.Replace(parts[valueOne], ""), out int firstValue);
                            Int32.TryParse(nonDigits.Replace(parts[valueTwo], ""), out int secondValue);
                            var spread = Math.Abs(firstValue - secondValue);
                            data.Add(new KeyValuePair<string, int>(day, spread));
                        }
                    }
                }
            }

            var target = data.OrderBy(x => x.Value).FirstOrDefault(); // Order from smallest to larget, take the first.
            Console.WriteLine(target.Key);
        }
    }
}
