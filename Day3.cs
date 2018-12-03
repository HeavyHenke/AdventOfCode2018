using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2018
{
    public class Day3
    {
        public string CalcA()
        {
            var taken = new HashSet<(int x, int y)>();
            var addedToNumTaken = new HashSet<(int x, int y)>();
            int numTaken = 0;

            var regex = new Regex("#(?<id>\\d+) @ (?<x>\\d+),(?<y>\\d+): (?<with>\\d+)x(?<height>\\d+)", RegexOptions.Compiled);

            //var testDataLines = _testData.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            //foreach (var line in testDataLines)
            foreach (var line in File.ReadAllLines("day3.data"))
            {
                var match = regex.Match(line);
                if (!match.Success)
                    throw new Exception("Unable to pars: " + line);

                int startX = int.Parse(match.Groups["x"].Value);
                int startY = int.Parse(match.Groups["y"].Value);
                int width = int.Parse(match.Groups["with"].Value);
                int height = int.Parse(match.Groups["height"].Value);

                for (int y = startY; y < startY + height; y++)
                {
                    for (int x = startX; x < startX + width; x++)
                    {
                        if (taken.Add((x, y)) == false && addedToNumTaken.Add((x, y)))
                            numTaken++;
                    }
                }
            }

            return numTaken.ToString();
        }

        public string CalcB()
        {
            var taken = new Dictionary<(int, int), int>();
            var sizes = new Dictionary<int, int>();


            var regex = new Regex("#(?<id>\\d+) @ (?<x>\\d+),(?<y>\\d+): (?<with>\\d+)x(?<height>\\d+)", RegexOptions.Compiled);


            //var testDataLines = _testData.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            //foreach (var line in testDataLines)
            foreach (var line in File.ReadAllLines("day3.data"))
            {
                var match = regex.Match(line);
                if (!match.Success)
                    throw new Exception("Unable to pars: " + line);

                int id = int.Parse(match.Groups["id"].Value);
                int startX = int.Parse(match.Groups["x"].Value);
                int startY = int.Parse(match.Groups["y"].Value);
                int width = int.Parse(match.Groups["with"].Value);
                int height = int.Parse(match.Groups["height"].Value);

                sizes.Add(id, width * height);

                for (int y = startY; y < startY + height; y++)
                {
                    for (int x = startX; x < startX + width; x++)
                    {
                        if (taken.TryGetValue((x, y), out _))
                        {
                            taken[(x, y)] = -1;
                        }
                        else
                        {
                            taken.Add((x, y), id);
                        }
                    }
                }
            }

            foreach (var kvp in sizes)
            {
                if (taken.Values.Count(v => v == kvp.Key) == kvp.Value)
                    return kvp.Key.ToString();
            }

            throw new Exception("Not found!");
        }


        private string _testData = @"#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2";
    }
}