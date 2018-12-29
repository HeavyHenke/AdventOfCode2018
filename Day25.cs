using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2018
{
    class Day25
    {
        public string CalcA()
        {
            var constelations = new List<List<(int x, int y, int z, int t)>>();

            foreach (var row in File.ReadAllLines("Day25.txt"))
            {
                var rowArr = row.Split(',').Select(int.Parse).ToList();
                (int x, int y, int z, int t) pt = (rowArr[0], rowArr[1], rowArr[2], rowArr[3]);

                int firstIdx = -1;
                for (int i = 0; i < constelations.Count; i++)
                {
                    for (int j = 0; j < constelations[i].Count; j++)
                    {
                        if (ManhattanDist(pt, constelations[i][j]) <= 3)
                        {
                            if (firstIdx == -1)
                            {
                                firstIdx = i;
                                constelations.Add(new List<(int x, int y, int z, int t)> {pt});
                            }
                            else
                            {
                                constelations[firstIdx].AddRange(constelations[i]);
                                constelations.RemoveAt(i);
                                i--;
                            }

                            break;
                        }
                    }
                }

                if (firstIdx == -1)
                    constelations.Add(new List<(int x, int y, int z, int t)> {pt});
            }

            return constelations.Count.ToString();
        }

        private static int ManhattanDist((int x, int y, int z, int t) a, (int x, int y, int z, int t) b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y) + Math.Abs(a.z - b.z) + Math.Abs(a.t - b.t);
        }
    }
}