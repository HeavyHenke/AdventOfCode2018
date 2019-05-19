using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2018
{
    public class Day16
    {
        public string CalcA()
        {
            var registers = new int[4];
            var instructions = new List<Action<int, int, int>>
            {
                (a, b, c) => { registers[c] = registers[a] + registers[b];},
                (a, b, c) => { registers[c] = registers[a] + b;},
                (a, b, c) => { registers[c] = registers[a] * registers[b];},
                (a, b, c) => { registers[c] = registers[a] * b;},
                (a, b, c) => { registers[c] = registers[a] & registers[b];},
                (a, b, c) => { registers[c] = registers[a] & b;},
                (a, b, c) => { registers[c] = registers[a] | registers[b];},
                (a, b, c) => { registers[c] = registers[a] | b;},
                (a, b, c) => { registers[c] = registers[a];},
                (a, b, c) => { registers[c] = a;},
                (a, b, c) => { registers[c] = (a > registers[b]) ? 1 : 0;},
                (a, b, c) => { registers[c] = (registers[a] > b) ? 1 : 0;},
                (a, b, c) => { registers[c] = (registers[a] > registers[b]) ? 1 : 0;},
                (a, b, c) => { registers[c] = (a == registers[b]) ? 1 : 0; },
                (a, b, c) => { registers[c] = (registers[a] == b) ? 1 : 0; },
                (a, b, c) => { registers[c] = (registers[a] == registers[b]) ? 1 : 0; }
            };


            var lines = File.ReadAllLines("Day16_1.txt");
            int threeOrMore = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                var before = Regex.Match(lines[i++], @"Before: \[(\d+), (\d+), (\d+), (\d+)\]");
                var op = Regex.Match(lines[i++], @"(\d+) (\d+) (\d+) (\d+)");
                var after = Regex.Match(lines[i++], @"After:  \[(\d+), (\d+), (\d+), (\d+)\]");

                int matches = 0;
                foreach (var instr in instructions)
                {
                    registers[0] = int.Parse(before.Groups[1].Value);
                    registers[1] = int.Parse(before.Groups[2].Value);
                    registers[2] = int.Parse(before.Groups[3].Value);
                    registers[3] = int.Parse(before.Groups[4].Value);

                    instr(int.Parse(op.Groups[2].Value), int.Parse(op.Groups[3].Value), int.Parse(op.Groups[4].Value));

                    if (registers[0] == int.Parse(after.Groups[1].Value) &&
                        registers[1] == int.Parse(after.Groups[2].Value) &&
                        registers[2] == int.Parse(after.Groups[3].Value) &&
                        registers[3] == int.Parse(after.Groups[4].Value))
                        matches++;
                }

                if (matches >= 3)
                    threeOrMore++;
            }

            return threeOrMore.ToString();
        }

        public string CalcB()
        {
            var registers = new int[4];
            var instructions = new List<Action<int, int, int>>
            {
                (a, b, c) => { registers[c] = registers[a] + registers[b];},
                (a, b, c) => { registers[c] = registers[a] + b;},
                (a, b, c) => { registers[c] = registers[a] * registers[b];},
                (a, b, c) => { registers[c] = registers[a] * b;},
                (a, b, c) => { registers[c] = registers[a] & registers[b];},
                (a, b, c) => { registers[c] = registers[a] & b;},
                (a, b, c) => { registers[c] = registers[a] | registers[b];},
                (a, b, c) => { registers[c] = registers[a] | b;},
                (a, b, c) => { registers[c] = registers[a];},
                (a, b, c) => { registers[c] = a;},
                (a, b, c) => { registers[c] = (a > registers[b]) ? 1 : 0;},
                (a, b, c) => { registers[c] = (registers[a] > b) ? 1 : 0;},
                (a, b, c) => { registers[c] = (registers[a] > registers[b]) ? 1 : 0;},
                (a, b, c) => { registers[c] = (a == registers[b]) ? 1 : 0; },
                (a, b, c) => { registers[c] = (registers[a] == b) ? 1 : 0; },
                (a, b, c) => { registers[c] = (registers[a] == registers[b]) ? 1 : 0; }
            };
            var validOps = new HashSet<int>[16];
            for (int i = 0; i < validOps.Length; i++)
                validOps[i] = new HashSet<int>(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16});

            var lines = File.ReadAllLines("Day16_1.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                var before = Regex.Match(lines[i++], @"Before: \[(\d+), (\d+), (\d+), (\d+)\]");
                var op = Regex.Match(lines[i++], @"(\d+) (\d+) (\d+) (\d+)");
                var after = Regex.Match(lines[i++], @"After:  \[(\d+), (\d+), (\d+), (\d+)\]");

                int matches = 0;
                for (var instrIx = 0; instrIx < instructions.Count; instrIx++)
                {
                    var instr = instructions[instrIx];
                    registers[0] = int.Parse(before.Groups[1].Value);
                    registers[1] = int.Parse(before.Groups[2].Value);
                    registers[2] = int.Parse(before.Groups[3].Value);
                    registers[3] = int.Parse(before.Groups[4].Value);

                    instr(int.Parse(op.Groups[2].Value), int.Parse(op.Groups[3].Value), int.Parse(op.Groups[4].Value));

                    if (registers[0] != int.Parse(after.Groups[1].Value) ||
                        registers[1] != int.Parse(after.Groups[2].Value) ||
                        registers[2] != int.Parse(after.Groups[3].Value) ||
                        registers[3] != int.Parse(after.Groups[4].Value))
                    {
                        validOps[instrIx].Remove(int.Parse(op.Groups[1].Value));
                    }
                }
            }

            var opByCode = new Dictionary<int, Action<int, int, int>>();

            while (opByCode.Count < 16)
            {
                for (int op = 0; op < 16; op++)
                {
                    if(opByCode.ContainsKey(op))
                        continue;

                    int foundNum = 0;
                    int foundIx = -1;
                    for (int validOpIx = 0; validOpIx < validOps.Length; validOpIx++)
                    {
                        if (validOps[validOpIx].Contains(op))
                        {
                            foundIx = validOpIx;
                            foundNum++;
                        }
                    }

                    if (foundNum == 1)
                    {
                        validOps[foundIx] = new HashSet<int> {op};
                        opByCode.Add(op, instructions[foundIx]);
                    }
                }
            }

            registers[0] = registers[1] = registers[2] = registers[3] = 0;
            foreach (var line in File.ReadAllLines("Day16_2.txt"))
            {
                var lineInts = line.Split(' ').Select(int.Parse).ToList();
                opByCode[lineInts[0]](lineInts[1], lineInts[2], lineInts[3]);
            }


            return registers[0].ToString();
        }

    }
}