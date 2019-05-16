using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2018
{
    public class Day13
    {
        public string CalcA()
        {
            string[] allLines = File.ReadAllLines("Day13.txt");
            var carts = new Dictionary<(int x, int y), (char dir, int turnCount)>();

            for (int y = 0; y < allLines.Length; y++)
            {
                for (int x = 0; x < allLines[y].Length; x++)
                {
                    if (allLines[y][x] == '>' || allLines[y][x] == '<' || allLines[y][x] == '^' || allLines[y][x] == 'v')
                    {
                        carts.Add((x, y), (allLines[y][x], 0));
                    }
                }

                allLines[y] = allLines[y].Replace('<', '-')
                    .Replace('>', '-')
                    .Replace('^', '|')
                    .Replace('v', '|');
            }

            Print(allLines, carts);

            while (true)
            {
                foreach (var cart in carts.OrderBy(c => c.Key.y).ThenBy(c => c.Key.y).ToList())
                {
                    carts.Remove(cart.Key);

                    int x, y;
                    char dir;
                    int turnCount;

                    switch (cart.Value.dir)
                    {
                        case '>':
                            x = cart.Key.x + 1;
                            y = cart.Key.y;
                            turnCount = cart.Value.turnCount;

                            if (carts.ContainsKey((x, y)))
                                return $"{x},{y}";

                            dir = cart.Value.dir;
                            if (allLines[y][x] == '\\')
                                dir = 'v';
                            else if (allLines[y][x] == '/')
                                dir = '^';
                            else if (allLines[y][x] == '+')
                            {
                                if (turnCount % 3 == 0)
                                    dir = '^';
                                else if (turnCount % 3 == 2)
                                    dir = 'v';
                                turnCount++;
                            }

                            break;
                        case '<':
                            x = cart.Key.x - 1;
                            y = cart.Key.y;
                            turnCount = cart.Value.turnCount;

                            if (carts.ContainsKey((x, y)))
                                return $"{x},{y}";

                            dir = cart.Value.dir;
                            if (allLines[y][x] == '\\')
                                dir = '^';
                            else if (allLines[y][x] == '/')
                                dir = 'v';
                            else if (allLines[y][x] == '+')
                            {
                                if (turnCount % 3 == 0)
                                    dir = 'v';
                                else if (turnCount % 3 == 2)
                                    dir = '^';
                                turnCount++;
                            }

                            break;

                        case '^':
                            x = cart.Key.x;
                            y = cart.Key.y - 1;
                            turnCount = cart.Value.turnCount;

                            if (carts.ContainsKey((x, y)))
                                return $"{x},{y}";

                            dir = cart.Value.dir;
                            if (allLines[y][x] == '\\')
                                dir = '<';
                            else if (allLines[y][x] == '/')
                                dir = '>';
                            else if (allLines[y][x] == '+')
                            {
                                if (turnCount % 3 == 0)
                                    dir = '<';
                                else if (turnCount % 3 == 2)
                                    dir = '>';
                                turnCount++;
                            }

                            break;

                        case 'v':
                            x = cart.Key.x;
                            y = cart.Key.y + 1;
                            turnCount = cart.Value.turnCount;

                            if (carts.ContainsKey((x, y)))
                                return $"{x},{y}";

                            dir = cart.Value.dir;
                            if (allLines[y][x] == '\\')
                                dir = '>';
                            else if (allLines[y][x] == '/')
                                dir = '<';
                            else if (allLines[y][x] == '+')
                            {
                                if (turnCount % 3 == 0)
                                    dir = '>';
                                else if (turnCount % 3 == 2)
                                    dir = '<';
                                turnCount++;
                            }

                            break;

                        default:
                            throw new Exception("Unknown direction");
                    }

                    carts.Add((x, y), (dir, turnCount));
                }
            }
        }

        public string CalcB()
        {
            string[] allLines = File.ReadAllLines("Day13.txt");
            var carts = new Dictionary<(int x, int y), (char dir, int turnCount)>();

            for (int y = 0; y < allLines.Length; y++)
            {
                for (int x = 0; x < allLines[y].Length; x++)
                {
                    if (allLines[y][x] == '>' || allLines[y][x] == '<' || allLines[y][x] == '^' || allLines[y][x] == 'v')
                    {
                        carts.Add((x, y), (allLines[y][x], 0));
                    }
                }

                allLines[y] = allLines[y].Replace('<', '-')
                    .Replace('>', '-')
                    .Replace('^', '|')
                    .Replace('v', '|');
            }

            //Print(allLines, carts);
            //Console.ReadKey();

            while (carts.Count > 1)
            {
                var positions = carts.Keys.OrderBy(k => k.y).ThenBy(k => k.x).ToList();

                foreach (var pos in positions)
                {
                    if (carts.TryGetValue(pos, out var cart) == false)
                        continue;

                    carts.Remove(pos);

                    int x = pos.x, y = pos.y;
                    char dir = cart.dir;
                    int turnCount = cart.turnCount;

                    switch (cart.dir)
                    {
                        case '>':
                            x++;

                            if (carts.ContainsKey((x, y)))
                            {
                                carts.Remove((x, y));
                                continue;
                            }

                            if (allLines[y][x] == '\\')
                                dir = 'v';
                            else if (allLines[y][x] == '/')
                                dir = '^';
                            else if (allLines[y][x] == '+')
                            {
                                if (turnCount % 3 == 0)
                                    dir = '^';
                                else if (turnCount % 3 == 2)
                                    dir = 'v';
                                turnCount++;
                            }

                            break;
                        case '<':
                            x--;

                            if (carts.ContainsKey((x, y)))
                            {
                                carts.Remove((x, y));
                                continue;
                            }

                            if (allLines[y][x] == '\\')
                                dir = '^';
                            else if (allLines[y][x] == '/')
                                dir = 'v';
                            else if (allLines[y][x] == '+')
                            {
                                if (turnCount % 3 == 0)
                                    dir = 'v';
                                else if (turnCount % 3 == 2)
                                    dir = '^';
                                turnCount++;
                            }

                            break;

                        case '^':
                            y--;

                            if (carts.ContainsKey((x, y)))
                            {
                                carts.Remove((x, y));
                                continue;
                            }

                            if (allLines[y][x] == '\\')
                                dir = '<';
                            else if (allLines[y][x] == '/')
                                dir = '>';
                            else if (allLines[y][x] == '+')
                            {
                                if (turnCount % 3 == 0)
                                    dir = '<';
                                else if (turnCount % 3 == 2)
                                    dir = '>';
                                turnCount++;
                            }

                            break;

                        case 'v':
                            y++;

                            if (carts.ContainsKey((x, y)))
                            {
                                carts.Remove((x, y));
                                continue;
                            }

                            if (allLines[y][x] == '\\')
                                dir = '>';
                            else if (allLines[y][x] == '/')
                                dir = '<';
                            else if (allLines[y][x] == '+')
                            {
                                if (turnCount % 3 == 0)
                                    dir = '>';
                                else if (turnCount % 3 == 2)
                                    dir = '<';
                                turnCount++;
                            }

                            break;

                        default:
                            throw new Exception("Unknown direction");
                    }

                    carts.Add((x, y), (dir, turnCount));
                }

                //Print(allLines, carts);
                //Console.ReadKey();
            }

            return $"{carts.Single().Key.x},{carts.Single().Key.y}";
        }


        private static void Print(string[] allLines, Dictionary<(int x, int y), (char dir, int turnCount)> carts)
        {
            Console.Clear();
            Console.ResetColor();
            foreach (var line in allLines)
                Console.WriteLine(line);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            foreach (var cart in carts)
            {
                Console.SetCursorPosition(cart.Key.x, cart.Key.y);
                Console.Write(cart.Value.dir);
            }

            Console.ResetColor();
            Console.SetCursorPosition(0, carts.Max(c => c.Key.y) + 1);
        }
    }
}