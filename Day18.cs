using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2018
{
    public class Day18
    {
        private char[,] _area;

        private int _maxX;
        private int _maxY;

        public string CalcA()
        {
            _area = new char[50, 50];
            var allLines = File.ReadAllLines("Day18.txt");
            for (int y = 0; y < allLines.Length; y++)
            for (int x = 0; x < allLines[y].Length; x++)
                _area[x, y] = allLines[y][x];

            for (int minutes = 0; minutes < 10; minutes++)
            {
                char[,] nextArea = (char[,])_area.Clone();
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

            return (wood * lumber).ToString();
        }

        public string CalcB()
        {
            _maxX = _maxY = 50;
            MyBitArr arr = new MyBitArr(_maxX * _maxY * 2);
            var allLines = File.ReadAllLines("Day18.txt");
            for (int y = 0; y < allLines.Length; y++)
            for (int x = 0; x < allLines[y].Length; x++)
                SetCharAtPos(arr, x, y, allLines[y][x]);

            //Print(arr);
            //Console.ReadKey();

            int wood;
            int lumber;
            var seen = new Dictionary<MyBitArr, int>();
            int targetMinutes = 1000000000;
            for (int minutes = 0; minutes < targetMinutes; minutes++)
            {
                MyBitArr nextArea = new MyBitArr(_maxX * _maxY * 2);
                wood = lumber = 0;

                for (int y = 0; y < _maxY; y++)
                for (int x = 0; x < _maxX; x++)
                {
                    switch (GetCharAt(arr, x, y))
                    {
                        case '.':
                            if (HasAtLeastNeighbours(arr, x, y, '|', 3))
                            {
                                SetCharAtPos(nextArea, x, y, '|');
                                wood++;
                            }
                            else
                            {
                                // SetCharAtPos(nextArea, x, y, '.');
                            }
                            break;
                        case '|':
                            if (HasAtLeastNeighbours(arr, x, y, '#', 3))
                            {
                                SetCharAtPos(nextArea, x, y, '#');
                                lumber++;
                            }
                            else
                            {
                                SetCharAtPos(nextArea, x, y, '|');
                                wood++;
                            }
                            break;
                        case '#':
                            if (!(HasAtLeastNeighbours(arr, x, y, '#', 1) && HasAtLeastNeighbours(arr, x, y, '|',1)))
                            {
                                // SetCharAtPos(nextArea, x, y, '.');
                            }
                            else
                            {
                                SetCharAtPos(nextArea, x, y, '#');
                                lumber++;
                            }
                            break;
                        default:
                            throw new Exception("Knas");
                    }
                }

                arr = nextArea;
                //Print(arr);
                //Console.ReadKey();

                if (seen.TryGetValue(arr, out int otherMinutes))
                {
                    Console.WriteLine($"Found duplicate at {minutes} with {otherMinutes}");

                    var minDiff = minutes - otherMinutes;
                    int remainingMinutes = targetMinutes - minutes;
                    int toAdd = (remainingMinutes / minDiff) * minDiff;
                    minutes += toAdd;
                }
                else
                {
                    seen[arr] = minutes;
                }
            }


            // Print(arr);

            wood = 0;
            lumber = 0;
            for (int y = 0; y < _maxY; y++)
            for (int x = 0; x < _maxX; x++)
            {
                char chr = GetCharAt(arr, x, y);
                if (chr == '|')
                    wood++;
                else if (chr == '#')
                    lumber++;
            }

            return (wood * lumber).ToString();
        }

        private void SetCharAtPos(MyBitArr arr, int x, int y, char chr)
        {
            int ix = (y * _maxX + x) * 2;
            if (chr == '.')
                return;
            if (chr == '#')
                ix++;
            arr.Set(ix, true);
        }

        private char GetCharAt(MyBitArr arr, int x, int y)
        {
            int ix = (y * _maxX + x) * 2;
            var b1 = arr.Get(ix);
            if (b1)
                return '|';
            var b2 = arr.Get(ix + 1);
            if (b2)
                return '#';
            return '.';
        }

        private bool IsCharAt(MyBitArr arr, int x, int y, char chr)
        {
            int ix = (y * _maxX + x) * 2;
            if (chr == '|')
                return arr.Get(ix);
            if (chr == '#')
                return arr.Get(ix + 1);
            return (arr.Get(ix) | arr.Get(ix + 1)) == false;
        }


        private static void Print(char[,] area)
        {
            for (int y = 0; y < area.GetLength(1); y++)
            {
                for (int x = 0; x < area.GetLength(0); x++)
                    Console.Write(area[x, y]);
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private void Print(MyBitArr area)
        {
            Console.WriteLine();
            for (int y = 0; y < _maxY; y++)
            {
                for (int x = 0; x < _maxX; x++)
                    Console.Write(GetCharAt(area, x, y));
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private bool HasAtLeastNeighbours(MyBitArr arr, int x, int y, char chr, int num)
        {
            int numFound = 0;
            foreach (var n in GetNeighbours(x, y))
            {
                if (IsCharAt(arr, n.x, n.y, chr))
                    numFound++;
                if (numFound == num)
                    return true;
            }

            return false;
        }


        private IEnumerable<(int x, int y)> GetNeighbours(int x, int y)
        {
            if (IsValid(x - 1, y - 1))
                yield return (x - 1, y - 1);
            if (IsValid(x, y - 1))
                yield return (x, y - 1);
            if (IsValid(x + 1, y - 1))
                yield return (x + 1, y - 1);

            if (IsValid(x - 1, y))
                yield return (x - 1, y);
            if (IsValid(x + 1, y))
                yield return (x + 1, y);

            if (IsValid(x - 1, y + 1))
                yield return (x - 1, y + 1);
            if (IsValid(x, y + 1))
                yield return (x, y + 1);
            if (IsValid(x + 1, y + 1))
                yield return (x + 1, y + 1);

            //for (int dx = -1; dx <= 1; dx++)
            //    for (int dy = -1; dy <= 1; dy++)
            //    {
            //        if (dx == 0 && dy == 0)
            //            continue;
            //        if (IsValid(x + dx, y + dy))
            //            yield return (x + dx, y + dy);
            //    }
        }

        private bool IsValid(int x, int y)
        {
            return x >= 0 && x < _maxX && y >= 0 && y < _maxY;
        }
    }

    public class MyBitArr
    {
        private readonly long[] _bits;

        public MyBitArr(int size)
        {
            _bits = new long[(size + 63) / 64];
        }

        private MyBitArr(long[] bits)
        {
            _bits = bits;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                long hash = 17;
                for (int i = 0; i < _bits.Length; i++)
                    hash = hash * 31 + _bits[i];
                return (int) hash;
            }
        }

        public override bool Equals(object obj)
        {
            MyBitArr other = obj as MyBitArr;
            if (other == null) return false;

            var arr1 = _bits;
            var arr2 = _bits;
            for(int i = 0; i < arr1.Length; i++)
                if (arr1[i] != arr2[i])
                    return false;
            return true;
        }

        public void Set(int ix, bool b)
        {
            int longIx = ix >> 6;
            long mask = 1L << (ix & 63);
            _bits[longIx] |= mask;
        }

        public bool Get(int ix)
        {
            int longIx = ix >> 6;
            long mask = 1L << (ix & 63);
            return (_bits[longIx] & mask) != 0;
        }

        public MyBitArr Clone()
        {
            return new MyBitArr((long[]) _bits.Clone());
        }
    }
}
