using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2018
{
    public class Day10
    {
        public string CalcA()
        {
            var fileLines = File.ReadAllLines("day10.txt");
            var regex = new Regex(@"position=<\s*(?<xpos>-?\d+),\s*(?<ypos>-?\d+)> velocity=<\s*(?<dx>-?\d+),\s*(?<dy>-?\d+)>", RegexOptions.Compiled);

            var positions = new List<(int x, int y, int dx, int dy)>();
            foreach (var line in fileLines)
            {
                var m = regex.Match(line);
                if(m.Success == false)
                    throw new Exception("Knasig regex");

                var pos = (int.Parse(m.Groups["xpos"].Value), int.Parse(m.Groups["ypos"].Value), int.Parse(m.Groups["dx"].Value), int.Parse(m.Groups["dy"].Value));
                positions.Add(pos);
            }

            Display(positions);
            int seconds = 0;

            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    return seconds.ToString();

                Console.WriteLine();

                if (key.Key == ConsoleKey.RightArrow)
                {
                    do
                    {
                        positions = positions.Select(p => (p.x + p.dx, p.y + p.dy, p.dx, p.dy)).ToList();
                        seconds++;
                    } while (Display(positions) == false);
                }

                if (key.Key == ConsoleKey.LeftArrow)
                {
                    positions = positions.Select(p => (p.x - p.dx, p.y - p.dy, p.dx, p.dy)).ToList();
                    seconds--;
                    Display(positions);
                }

            }
        }

        private static bool Display(List<(int x, int y, int dx, int dy)> positions)
        {
            int minX = positions.Min(p => p.x);
            int maxX = positions.Max(p => p.x);
            int xLength = Math.Abs(maxX - minX);
            int minY = positions.Min(p => p.y);
            int maxY = positions.Max(p => p.y);
            int yLength = Math.Abs(maxY - minY);

            if (xLength > 100 || yLength > 100)
                return false;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (positions.Any((p => p.x == x && p.y == y)))
                        Console.Write('#');
                    else
                        Console.Write('.');
                }

                Console.WriteLine();
            }

            return true;
        }
    }
}