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
            TemperatureSpread();
            FootballSpread();
        }

        private static void TemperatureSpread()
        {
            var days = new List<TemperatureData>();

            using (var reader = new StreamReader("weather.dat")) 
            {
                var firstLine = reader.ReadLine(); // Read the first line to remove the column headers
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line)) // Just skip empty lines
                    {
                        var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        if (parts[0].Any(char.IsDigit)) // Both files' lines start with a number of some sort, so only target those
                        {
                            var day = parts[0];
                            Int32.TryParse(nonDigits.Replace(parts[1], ""), out int maxTemp);
                            Int32.TryParse(nonDigits.Replace(parts[2], ""), out int minTemp);
                            days.Add(new TemperatureData(day, maxTemp, minTemp));
                        }
                    }
                }
            }

            var target = days.OrderBy(x => x.Spread).FirstOrDefault(); // Order from smallest to larget, take the first.
            Console.WriteLine(target.Day);
        }

        private static void FootballSpread()
        {
            var teams = new List<TeamData>();

            using (var reader = new StreamReader("football.dat")) 
            {
                var firstLine = reader.ReadLine(); // Read the first line to remove the column headers
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line)) // Just skip empty lines
                    {
                        var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        if (parts[0].Any(char.IsDigit)) // Both files' lines start with a number of some sort, so only target those
                        {
                            var team = parts[1];
                            Int32.TryParse(nonDigits.Replace(parts[6], ""), out int goalsFor);
                            Int32.TryParse(nonDigits.Replace(parts[8], ""), out int goalsAgainst);
                            teams.Add(new TeamData(team, goalsFor, goalsAgainst));
                        }
                    }
                }
            }

            var target = teams.OrderBy(x => x.Spread).FirstOrDefault(); // Order from smallest to larget, take the first.
            Console.WriteLine(target.Team);
        }

        class TemperatureData
        {
            public string Day { get; set; }

            public int Spread { get; set; }

            public TemperatureData(string day, int maxTemp, int minTemp)
            {
                Day = day;
                Spread = maxTemp - minTemp;
            }
        }

        class TeamData
        {
            public string Team { get; set; }

            public int Spread { get; set; }

            public TeamData(string team, int goalsFor, int goalsAgainst)
            {
                Team = team;
                Spread = Math.Abs(goalsFor - goalsAgainst);
            }
        }
    }
}
