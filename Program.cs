using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Math;

namespace AoC2018
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            string result = new Day25().CalcA();

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }

    class Day25
    {
        public string CalcA()
        {
            var constelations = new List<List<(int x, int y, int z, int t)>>();

            foreach (var row in File.ReadAllLines("Day25.txt"))
            {
                var rowArr = row.Split(',').Select(int.Parse).ToList();
                (int x, int y, int z, int t) pt = (rowArr[0], rowArr[1], rowArr[2], rowArr[3]);

                var partOfConstilations = new List<int>();
                for (int i = 0; i < constelations.Count; i++)
                {
                    bool isPartOf = false;
                    for (int j = 0; j < constelations[i].Count; j++)
                    {
                        if (ManhattanDist(pt, constelations[i][j]) <= 3)
                        {
                            isPartOf = true;
                            break;
                        }
                    }
                    if(isPartOf)
                        partOfConstilations.Add(i);
                }

                if (partOfConstilations.Count == 0)
                {
                    constelations.Add(new List<(int x, int y, int z, int t)> {pt});
                }
                else if (partOfConstilations.Count == 1)
                {
                    constelations[partOfConstilations[0]].Add(pt);
                }
                else
                {
                    constelations[partOfConstilations[0]].Add(pt);

                    foreach (var constil in partOfConstilations.Skip(1))
                    {
                        constelations[partOfConstilations[0]].AddRange(constelations[constil]);
                    }

                    foreach (var constil in partOfConstilations.Skip(1).Reverse())
                    {
                        constelations.RemoveAt(constil);
                    }
                }
            }

            return constelations.Count.ToString();
        }

        private static int ManhattanDist((int x, int y, int z, int t) a, (int x, int y, int z, int t) b)
        {
            return Abs(a.x - b.x) + Abs(a.y - b.y) + Abs(a.z - b.z) + Abs(a.t - b.t);
        }
    }
}
