using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AoC2018
{
    public class Day12
    {
        public string CalcA()
        {
            var transitions = new Dictionary<string, char>();
            foreach (var row in File.ReadAllLines("Day12.txt"))
            {
                var state = row.Substring(0, 5);
                var result = row[9];
                transitions.Add(state, result);
            }

            var initialState = "##..#..##....#..#..#..##.#.###.######..#..###.#.#..##.###.#.##..###..#.#..#.##.##..###.#.#...#.##..";
            int offset = 20 * 2;

            var preState = new string('.', offset) + initialState + new string('.', offset);

            for (int gen = 0; gen < 20; gen++)
            {
                var next = new StringBuilder();
                next.Append("..");
                for (int ix = 0; ix < preState.Length - 4; ix++)
                {
                    next.Append(transitions[preState.Substring(ix, 5)]);
                }

                next.Append("..");

                preState = next.ToString();
                Console.WriteLine(preState);
            }

            int ret = 0;
            for (int i = 0; i < preState.Length; i++)
            {
                if (preState[i] == '#')
                    ret += i - offset;
            }

            return ret.ToString();
        }
        
        public string CalcB()
        {
            var transitions = new Dictionary<(bool, bool, bool, bool, bool), bool>();
            foreach (var row in File.ReadAllLines("Day12.txt"))
            {
                var bState = (row[0] == '#', row[1] == '#', row[2] == '#', row[3] == '#', row[4] == '#');
                var result = row[9];
                transitions.Add(bState, result == '#');
            }

            var initialStateStr = "##..#..##....#..#..#..##.#.###.######..#..###.#.#..##.###.#.##..###..#.#..#.##.##..###.#.#...#.##..";
            var preState = new HashSet<int>();
            for (int i = 0; i < initialStateStr.Length; i++)
            {
                if (initialStateStr[i] == '#')
                    preState.Add(i);
            }

            var mem = new Dictionary<int, long>();
            for (long gen = 0; gen < 50000000000; gen++)
            {
                var nextState = new HashSet<int>();
                var toVisit = new HashSet<int>();
                foreach (var ix in preState)
                {
                    toVisit.Add(ix-2);
                    toVisit.Add(ix-1);
                    toVisit.Add(ix);
                    toVisit.Add(ix+1);
                    toVisit.Add(ix+2);
                }

                foreach (var v in toVisit)
                {
                    var state = (preState.Contains(v-2), preState.Contains(v-1), preState.Contains(v), preState.Contains(v+1), preState.Contains(v+2));
                    if (transitions[state])
                        nextState.Add(v);
                }

                preState = nextState;

                int sum = preState.Sum();
                if (mem.ContainsKey(sum))
                {
                    long diff = gen - mem[sum];
                    long numDiffsLeft = (50000000000 - gen) / diff;
                    gen += numDiffsLeft * diff;
                    mem.Clear();
                }
                mem.Add(sum, gen);

                if(gen % 1000 == 0)
                    Console.WriteLine(gen);
            }

            int ret = preState.Sum();

            return ret.ToString(); // 7067 to low
        }

        public string CalcB2()
        {
            var transitions = new Dictionary<string, char>();
            foreach (var row in File.ReadAllLines("Day12.txt"))
            {
                var state = row.Substring(0, 5);
                var result = row[9];
                transitions.Add(state, result);
            }

            var initialState = "##..#..##....#..#..#..##.#.###.######..#..###.#.#..##.###.#.##..###..#.#..#.##.##..###.#.#...#.##..";
            int offset = 2;

            var preState = new string('.', 2) + initialState + new string('.', 2);

            var visited = new Dictionary<string, long>();
            for (long gen = 0; gen < 20; gen++)
            {
                var next = new StringBuilder();
                next.Append("..");
                for (int ix = 0; ix < preState.Length - 4; ix++)
                {
                    next.Append(transitions[preState.Substring(ix, 5)]);
                }

                next.Append("..");

                preState = next.ToString();

                int i;
                for (i = 0; i < preState.Length; i++)
                {
                    if(preState[i] == '#')
                        break;
                }
                offset += i;

                preState = preState.Trim('.');
                if (visited.ContainsKey(preState))
                {
                    Console.WriteLine(preState);
                    return "Found it at iter " + gen;
                }
                visited.Add(preState, gen);

                Console.WriteLine(gen);
                Console.WriteLine(preState);
                Console.WriteLine();

                preState = ".." + preState + "..";
            }

            int ret = 0;
            for (int i = 0; i < preState.Length; i++)
            {
                if (preState[i] == '#')
                    ret += i - offset;
            }

            return ret.ToString();
        }
    }
}