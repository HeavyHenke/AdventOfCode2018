using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MoreLinq;

namespace AoC2018
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            DateTime start = DateTime.Now;
            string result = new Day18().CalcB();
            DateTime stop = DateTime.Now;

            Console.WriteLine("It took " + (stop - start).TotalSeconds);

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }

    public class Day18
    {
        private char[,] _area;

        public string CalcA()
        {
            _area = new char[50, 50];
            var allLines = File.ReadAllLines("Day18.txt");
            for (int y = 0; y < allLines.Length; y++)
                for (int x = 0; x < allLines[y].Length; x++)
                    _area[x, y] = allLines[y][x];

            for (int minutes = 0; minutes < 10; minutes++)
            {
                char[,] nextArea = (char[,]) _area.Clone();
                for (int y = 0; y < _area.GetLength(1); y++)
                for (int x = 0; x < _area.GetLength(0); x++)
                {

                    switch (_area[x, y])
                    {
                        case '.':
                            if (GetNeighbours(x, y).Where(n => _area[n.x, n.y] == '|').Take(3).Count() == 3)
                                nextArea[x, y] = '|';
                            break;
                        case '|':
                            if (GetNeighbours(x, y).Where(n => _area[n.x, n.y] == '#').Take(3).Count() == 3)
                                nextArea[x, y] = '#';
                            break;
                        case '#':
                            if (!GetNeighbours(x, y).Any(n => _area[n.x, n.y] == '#') ||
                                !GetNeighbours(x, y).Any(n => _area[n.x, n.y] == '|'))
                                nextArea[x, y] = '.';
                            break;
                        default:
                            throw new Exception("Knas");
                    }
                }

                _area = nextArea;
            }


            // Print();

            int wood = 0;
            int lumber = 0;
            for (int y = 0; y < _area.GetLength(1); y++)
            for (int x = 0; x < _area.GetLength(0); x++)
            {
                char chr = _area[x, y];
                if (chr == '|')
                    wood++;
                else if (chr == '#')
                    lumber++;
            }

            return (wood*lumber).ToString();
        }

        public string CalcB()
        {
            _area = new char[50, 50];
            var allLines = File.ReadAllLines("Day18.txt");
            for (int y = 0; y < allLines.Length; y++)
                for (int x = 0; x < allLines[y].Length; x++)
                    _area[x, y] = allLines[y][x];

            for (int minutes = 0; minutes < 10; minutes++)
            {
                char[,] nextArea = (char[,]) _area.Clone();
                for (int y = 0; y < _area.GetLength(1); y++)
                for (int x = 0; x < _area.GetLength(0); x++)
                {

                    switch (_area[x, y])
                    {
                        case '.':
                            if (GetNeighbours(x, y).Where(n => _area[n.x, n.y] == '|').Take(3).Count() == 3)
                                nextArea[x, y] = '|';
                            break;
                        case '|':
                            if (GetNeighbours(x, y).Where(n => _area[n.x, n.y] == '#').Take(3).Count() == 3)
                                nextArea[x, y] = '#';
                            break;
                        case '#':
                            if (!GetNeighbours(x, y).Any(n => _area[n.x, n.y] == '#') ||
                                !GetNeighbours(x, y).Any(n => _area[n.x, n.y] == '|'))
                                nextArea[x, y] = '.';
                            break;
                        default:
                            throw new Exception("Knas");
                    }
                }

                _area = nextArea;
            }


            // Print();

            int wood = 0;
            int lumber = 0;
            for (int y = 0; y < _area.GetLength(1); y++)
            for (int x = 0; x < _area.GetLength(0); x++)
            {
                char chr = _area[x, y];
                if (chr == '|')
                    wood++;
                else if (chr == '#')
                    lumber++;
            }

            return (wood*lumber).ToString();
        }


        private void Print()
        {
            for (int y = 0; y < _area.GetLength(1); y++)
            {
                for (int x = 0; x < _area.GetLength(0); x++)
                    Console.Write(_area[x, y]);
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private IEnumerable<(int x, int y)> GetNeighbours(int x, int y)
        {
            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;
                    if (IsValid(x + dx, y + dy))
                        yield return (x + dx, y + dy);
                }
        }

        private bool IsValid(int x, int y)
        {
            return x >= 0 && x < _area.GetLength(0) && y >= 0 && y < _area.GetLength(1);
        }
    }
}
