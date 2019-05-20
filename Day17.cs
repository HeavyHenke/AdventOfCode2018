using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC2018
{
    public class Day17
    {
        public string CalcA()
        {
            var clay = new HashSet<(int x, int y)>();

            foreach (var line in File.ReadAllLines("Day17.txt"))
            {
                var m = Regex.Match(line, @"x=(\d+), y=(\d+)..(\d+)");
                if (m.Success)
                {
                    int x = int.Parse(m.Groups[1].Value);
                    int y1 = int.Parse(m.Groups[2].Value);
                    int y2 = int.Parse(m.Groups[3].Value);
                    for (int y = y1; y <= y2; y++)
                        clay.Add((x, y));
                    continue;
                }

                m = Regex.Match(line, @"y=(\d+), x=(\d+)..(\d+)");
                if (m.Success)
                {
                    int y = int.Parse(m.Groups[1].Value);
                    int x1 = int.Parse(m.Groups[2].Value);
                    int x2 = int.Parse(m.Groups[3].Value);
                    for (int x = x1; x <= x2; x++)
                        clay.Add((x, y));
                    continue;
                }

                throw new Exception("Knas!");
            }

            int maxX, minX, maxY, minY;
            minX = minY = int.MaxValue;
            maxX = maxY = int.MinValue;
            foreach (var pos in clay)
            {
                maxX = Math.Max(maxX, pos.x);
                maxY = Math.Max(maxY, pos.y);
                minX = Math.Min(minX, pos.x);
                minY = Math.Min(minY, pos.y);
            }

            Console.SetBufferSize(Math.Max(maxX - minX + 1, Console.BufferWidth), Math.Max(maxY - minY + 1, Console.BufferHeight));

            var stillWater = new HashSet<(int x, int y)>();
            var springWater = new HashSet<(int x, int y)>();

            var springQueue = new Queue<(int x, int y)>();
            springQueue.Enqueue((499, 0));
            while (springQueue.Count > 0)
            {
                var node = springQueue.Dequeue();
                if (node.y > maxY)
                    continue;

                int y;
                bool foundCollision = false;
                for (y = node.y + 1; clay.Contains((node.x, y)) == false && stillWater.Contains((node.x, y)) == false && y <= maxY; y++)
                {
                    if (y >= minY && springWater.Add((node.x, y)) == false)
                    {
                        foundCollision = true;
                        break;
                    }
                }
                y--;

                if (y == maxY)
                    continue;

                if (foundCollision)
                    continue;

                // Find border
                int leftBorder = int.MinValue;
                int leftFloor = node.x;
                bool done = false;
                for (int x = node.x; !done; x--)
                {
                    if (clay.Contains((x, y)))
                    {
                        leftBorder = x;
                        done = true;
                    }

                    if (clay.Contains((x, y + 1)) || stillWater.Contains((x, y + 1)))
                    {
                        leftFloor = x;
                    }
                    else
                    {
                        done = true;
                    }
                }

                int rightBorder = int.MaxValue;
                int rightFloor = node.x;
                done = false;
                for (int x = node.x; !done; x++)
                {
                    if (clay.Contains((x, y)))
                    {
                        rightBorder = x;
                        done = true;
                    }

                    if (clay.Contains((x, y + 1)) || stillWater.Contains((x, y + 1)))
                    {
                        rightFloor = x;
                    }
                    else
                    {
                        done = true;
                    }
                }

                if (leftFloor == leftBorder && rightFloor == rightBorder)
                {
                    for (int x = leftBorder + 1; x < rightBorder; x++)
                    {
                        stillWater.Add((x, y));
                        springWater.Remove((x, y));
                    }

                    springQueue.Enqueue((node.x, y - 1));
                }
                else
                {
                    for (int x = node.x; x > leftBorder; x--)
                    {
                        springWater.Add((x, y));
                        if (x < leftFloor)
                        {
                            springQueue.Enqueue((x, y));
                            break;
                        }
                    }

                    for (int x = node.x; x < rightBorder; x++)
                    {
                        springWater.Add((x, y));
                        if (x > rightFloor)
                        {
                            springQueue.Enqueue((x, y));
                            break;
                        }
                    }
                }
            }

            foreach (var xy in stillWater)
            {
                minX = Math.Min(xy.x, minX);
                maxX = Math.Max(xy.x, maxX);
            }
            foreach (var xy in springWater)
            {
                minX = Math.Min(xy.x, minX);
                maxX = Math.Max(xy.x, maxX);
            }

            // var board = Print(minY, maxY, minX, maxX, clay, stillWater, springWater);

            return (stillWater.Count + springWater.Count).ToString();
        }

        private string Print(int minY, int maxY, int minX, int maxX, HashSet<(int x, int y)> clay, HashSet<(int x, int y)> stillWater, HashSet<(int x, int y)> springWater)
        {
            var sb = new StringBuilder();
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (clay.Contains((x, y)))
                        sb.Append("#");
                    else if (springWater.Contains((x, y)))
                        sb.Append("|");
                    else if (stillWater.Contains((x, y)))
                        sb.Append("~");
                    else
                        sb.Append(".");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}