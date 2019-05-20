using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AoC2018
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            DateTime start = DateTime.Now;
            string result = new Day17().CalcA();
            DateTime stop = DateTime.Now;

            Console.WriteLine("It took " + (stop - start).TotalSeconds);

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }

    public class Day17
    {
        public string CalcA()
        {
            var clay = new HashSet<(int x, int y)>();

            foreach (var line in File.ReadAllLines("Day17Test.txt"))
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


            minY = 0;
            Console.SetBufferSize(Math.Max(maxX - minX + 1, Console.BufferWidth) , Math.Max(maxY - minY + 1, Console.BufferHeight));

            var stillWater = new HashSet<(int x, int y)>();
            var springWater = new HashSet<(int x, int y)>();

            var springQueue = new Queue<(int x, int y)>();
            springQueue.Enqueue((500, 0));
            while (springQueue.Count > 0)
            {
                var node = springQueue.Dequeue();
                if (node.y > maxY)
                    continue;

                int y;
                bool foundCollition = false;
                for (y = node.y + 1; clay.Contains((node.x, y)) == false && stillWater.Contains((node.x, y)) == false && y <= maxY; y++)
                {
                    if (springWater.Add((node.x, y)) == false)
                    {
                        foundCollition = true;
                        break;
                    }
                }
                y--;

                if (y == maxY)
                    continue;

                if (foundCollition)
                    continue;

                // Find border
                int leftBorder = minX - 1;
                int leftFloor = node.x;
                bool floorEnded = false;
                for (int x = node.x - 1; x >= minX; x--)
                {
                    if (!floorEnded && (clay.Contains((x, y + 1)) || stillWater.Contains((x, y + 1))))
                    {
                        leftFloor = x;
                    }
                    else
                    {
                        floorEnded = true;
                    }

                    if (clay.Contains((x, y)))
                    {
                        leftBorder = x;
                        break;
                    }
                }

                int rightBorder = maxX + 2;
                int rightFloor = node.x;
                floorEnded = false;
                for (int x = node.x + 1; x <= maxX; x++)
                {
                    if (!floorEnded && (clay.Contains((x, y + 1)) || stillWater.Contains((x, y + 1))))
                    {
                        rightFloor = x;
                    }
                    else
                    {
                        floorEnded = true;
                    }
                    if (clay.Contains((x, y)))
                    {
                        rightBorder = x;
                        break;
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
                        if (x >= leftFloor)
                        {
                            continue;
                        }

                        springQueue.Enqueue((x, y));
                        break;
                    }

                    for (int x = node.x; x < rightBorder; x++)
                    {
                        springWater.Add((x, y));
                        if (x <= rightFloor)
                        {
                            continue;
                        }

                        springQueue.Enqueue((x, y));
                        break;
                    }
                }
            }

            Print(minY, maxY, minX, maxX, clay, stillWater, springWater);
            //Console.ReadKey();

            // 30488 to low
            return (stillWater.Count + springWater.Count).ToString();
        }

        private void Print(int minY, int maxY, int minX, int maxX, HashSet<(int x, int y)> clay, HashSet<(int x, int y)> stillWater, HashSet<(int x, int y)> springWater)
        {
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (clay.Contains((x, y)))
                        Console.Write("#");
                    else if (springWater.Contains((x, y)))
                        Console.Write("|");
                    else if (stillWater.Contains((x, y)))
                        Console.Write("~");
                    else
                        Console.Write(".");
                }

                Console.WriteLine();
                if (y % 100 == 99)
                    Console.ReadKey();
            }
        }
    }
}
