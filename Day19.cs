using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2018
{
    public class Day19
    {
        public string CalcA()
        {
            return Calc(0);
        }

        public string CalcB()
        {
            return Calc(1);
        }

        private string Calc(int startReg0At)
        {
            var registers = new int[6];
            var instr = new Dictionary<string, Action<int, int, int>>
            {
                {"addr", (a, b, c) => { registers[c] = registers[a] + registers[b]; }},
                {"addi", (a, b, c) => { registers[c] = registers[a] + b;}},
                {"mulr", (a, b, c) => { registers[c] = registers[a] * registers[b];}},
                {"muli", (a, b, c) => { registers[c] = registers[a] * b;}},
                {"banr", (a, b, c) => { registers[c] = registers[a] & registers[b];}},
                {"bani", (a, b, c) => { registers[c] = registers[a] & b;}},
                {"borr", (a, b, c) => { registers[c] = registers[a] | registers[b];}},
                {"bori", (a, b, c) => { registers[c] = registers[a] | b;}},
                {"setr", (a, b, c) => { registers[c] = registers[a];}},
                {"seti", (a, b, c) => { registers[c] = a;}},
                {"gtir", (a, b, c) => { registers[c] = (a > registers[b]) ? 1 : 0;}},
                {"gtri", (a, b, c) => { registers[c] = (registers[a] > b) ? 1 : 0;}},
                {"gtrr", (a, b, c) => { registers[c] = (registers[a] > registers[b]) ? 1 : 0;}},
                {"eqir", (a, b, c) => { registers[c] = (a == registers[b]) ? 1 : 0; }},
                {"eqri", (a, b, c) => { registers[c] = (registers[a] == b) ? 1 : 0; }},
                {"eqrr", (a, b, c) => { registers[c] = (registers[a] == registers[b]) ? 1 : 0; }},
            };
            var programLines = File.ReadAllLines("Day19.txt");
            int ipRegister = int.Parse(programLines[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last());

            registers[0] = startReg0At;

            var prog = new List<Action>();
            var progDescr = new List<string>();
            int pos = 0;
            foreach (var line in programLines.Skip(1))
            {
                var parts = line.Split(' ');
                var instruction = parts[0];
                var arg0 = int.Parse(parts[1]);
                var arg1 = int.Parse(parts[2]);
                var arg2 = int.Parse(parts[3]);
                var curIns = instr[instruction];
                Action command = () => curIns(arg0, arg1, arg2);
                prog.Add(command);

                progDescr.Add(Describe(instruction, pos, arg0, arg1, arg2));
                pos++;
            }

            int numIter = 0;
            DateTime start = DateTime.Now;

            while (registers[ipRegister] >= 0 && registers[ipRegister] < prog.Count)
            {
                if (registers[ipRegister] == 1)
                {
                    int a = registers[0];
                    int e = registers[4];
                    int f;

                    for (f = 1; f <= e; f++)
                    {
                        if (e % f == 0)
                            a += f;
                    }

                    return a.ToString();
                }

                if (registers[ipRegister] == 3)
                {
                    registers[1] = registers[5] * (registers[2] - 1);
                    do
                    {
                        registers[1] += registers[5];
                        if (registers[1] == registers[4])
                            registers[0] += registers[5];
                        registers[2]++;
                    } while (registers[2] <= registers[4]);

                    registers[ipRegister] = 12;
                    numIter += 8 * registers[4];
                    continue;
                }
                
                prog[registers[ipRegister]]();
                registers[ipRegister]++;

                numIter++;
                if (numIter > 2000000000)
                {   
                    var duration = DateTime.Now - start;
                    var instrPerSecond = numIter / duration.TotalSeconds;
                    Console.WriteLine($"IPS {instrPerSecond}");
                    numIter = 0;
                    start = DateTime.Now;
                }
            }

            return registers[0].ToString();
        }

        private static string Describe(string instr, int pos, int a, int b, int c)
        {
            if (instr == "addi")
            {
                var result = (char)('A' + c);
                var reg = (char)('A' + a);
                return $"{pos}: {result} = {reg} + {b}";
            }

            if (instr == "addr")
            {
                var result = (char)('A' + c);
                var reg1 = (char)('A' + a);
                var reg2 = (char)('A' + b);
                return $"{pos}: {result} = {reg1} + {reg2}";
            }

            if (instr == "seti")
            {
                var result = (char)('A' + c);
                return $"{pos}: {result} = {a}";
            }

            if (instr == "mulr")
            {
                var result = (char)('A' + c);
                var reg1 = (char)('A' + a);
                var reg2 = (char)('A' + b);
                return $"{pos}: {result} = {reg1} * {reg2}";
            }

            if (instr == "eqrr")
            {
                var result = (char)('A' + c);
                var reg1 = (char)('A' + a);
                var reg2 = (char)('A' + b);
                return $"{pos}: {result} = ({reg1} == {reg2}) ? 1 : 0";
            }

            if (instr == "gtrr")
            {
                var result = (char)('A' + c);
                var reg1 = (char)('A' + a);
                var reg2 = (char)('A' + b);
                return $"{pos}: {result} = ({reg1} > {reg2}) ? 1 : 0";
            }

            if (instr == "muli")
            {
                var result = (char)('A' + c);
                var reg1 = (char)('A' + a);
                return $"{pos}: {result} = {reg1} * {b}";
            }

            if (instr == "setr")
            {
                var result = (char)('A' + c);
                var reg1 = (char)('A' + a);
                return $"{pos}: {result} = {reg1}";
            }

            return $"{pos}: {instr} {a} {b} {c}";
        }
    }
}