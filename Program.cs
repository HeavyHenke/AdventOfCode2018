using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MoreLinq;

namespace AoC2018
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            DateTime start = DateTime.Now;
            string result = new Day22().CalcB();
            DateTime stop = DateTime.Now;

            Console.WriteLine("It took " + (stop - start).TotalSeconds);

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }


    public class Machine
    {
        private readonly List<Action> _program;
        private readonly int[] _registers;
        private readonly int _ipRegister;

        public int NumInstructionsExecuted { get; private set; }

        public HashSet<int> _day21Hash = new HashSet<int>();
        public int _lastDay21Val;

        public Machine(string programFileName)
        {
            _registers = new int[6];
            var instr = new Dictionary<string, Action<int, int, int>>
            {
                {"addr", (a, b, c) => { _registers[c] = _registers[a] + _registers[b]; }},
                {"addi", (a, b, c) => { _registers[c] = _registers[a] + b;}},
                {"mulr", (a, b, c) => { _registers[c] = _registers[a] * _registers[b];}},
                {"muli", (a, b, c) => { _registers[c] = _registers[a] * b;}},
                {"banr", (a, b, c) => { _registers[c] = _registers[a] & _registers[b];}},
                {"bani", (a, b, c) => { _registers[c] = _registers[a] & b;}},
                {"borr", (a, b, c) => { _registers[c] = _registers[a] | _registers[b];}},
                {"bori", (a, b, c) => { _registers[c] = _registers[a] | b;}},
                {"setr", (a, b, c) => { _registers[c] = _registers[a];}},
                {"seti", (a, b, c) => { _registers[c] = a;}},
                {"gtir", (a, b, c) => { _registers[c] = (a > _registers[b]) ? 1 : 0;}},
                {"gtri", (a, b, c) => { _registers[c] = (_registers[a] > b) ? 1 : 0;}},
                {"gtrr", (a, b, c) => { _registers[c] = (_registers[a] > _registers[b]) ? 1 : 0;}},
                {"eqir", (a, b, c) => { _registers[c] = (a == _registers[b]) ? 1 : 0; }},
                {"eqri", (a, b, c) => { _registers[c] = (_registers[a] == b) ? 1 : 0; }},
                {"eqrr", (a, b, c) => { _registers[c] = (_registers[a] == _registers[b]) ? 1 : 0; }},
            };
            var programLines = File.ReadAllLines(programFileName);
            _ipRegister = int.Parse(programLines[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last());

            _program = new List<Action>();
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
                void Command() => curIns(arg0, arg1, arg2);
                _program.Add(Command);

                progDescr.Add(Describe(instruction, pos, arg0, arg1, arg2));
                pos++;
            }

            var sourceCode = string.Join("\r\n", progDescr);
        }

        public string Calc(int startReg0At)
        {
            NumInstructionsExecuted = 0;
            _registers[0] = startReg0At;

            while (_registers[_ipRegister] >= 0 && _registers[_ipRegister] < _program.Count)
            {
                if (_registers[_ipRegister] == 28 && _registers[3] > 0)
                {
                    if(_day21Hash.Count % 100 == 0)
                        Console.WriteLine(_day21Hash.Count);
                    if (_day21Hash.Add(_registers[3]) == false)
                    {
                        return _lastDay21Val.ToString();
                    }

                    _lastDay21Val = _registers[3];
                }

                _program[_registers[_ipRegister]]();
                _registers[_ipRegister]++;
                NumInstructionsExecuted++;
            }

            return _registers[0].ToString();
        }

        private string Describe(string instr, int pos, int a, int b, int c)
        {
            string IndexToRegister(int ix)
            {
                if (ix == _ipRegister)
                    return "IP";
                return ((char)('A' + ix)).ToString();
            }

            if (instr == "addi")
            {
                var result = IndexToRegister(c);
                var reg = IndexToRegister(a);
                return $"{pos:00}: {result} = {reg} + {b}";
            }

            if (instr == "addr")
            {
                var result = IndexToRegister(c);
                var reg1 = IndexToRegister(a);
                var reg2 = IndexToRegister(b);
                return $"{pos:00}: {result} = {reg1} + {reg2}";
            }

            if (instr == "banr")
            {
                var result = IndexToRegister(c);
                var reg1 = IndexToRegister(a);
                var reg2 = IndexToRegister(b);
                return $"{pos:00}: {result} = {reg1} & {reg2}";
            }

            if (instr == "bani")
            {
                var result = IndexToRegister(c);
                var reg1 = IndexToRegister(a);
                return $"{pos:00}: {result} = {reg1} & {b}";
            }

            if (instr == "borr")
            {
                var result = IndexToRegister(c);
                var reg1 = IndexToRegister(a);
                var reg2 = IndexToRegister(b);
                return $"{pos:00}: {result} = {reg1} | {reg2}";
            }

            if (instr == "bori")
            {
                var result = IndexToRegister(c);
                var reg1 = IndexToRegister(a);
                return $"{pos:00}: {result} = {reg1} | {b}";
            }

            if (instr == "seti")
            {
                var result = IndexToRegister(c);
                return $"{pos:00}: {result} = {a}";
            }

            if (instr == "mulr")
            {
                var result = IndexToRegister(c);
                var reg1 = IndexToRegister(a);
                var reg2 = IndexToRegister(b);
                return $"{pos:00}: {result} = {reg1} * {reg2}";
            }

            if (instr == "eqrr")
            {
                var result = IndexToRegister(c);
                var reg1 = IndexToRegister(a);
                var reg2 = IndexToRegister(b);
                return $"{pos:00}: {result} = ({reg1} == {reg2}) ? 1 : 0";
            }

            if (instr == "gtrr")
            {
                var result = IndexToRegister(c);
                var reg1 = IndexToRegister(a);
                var reg2 = IndexToRegister(b);
                return $"{pos:00}: {result} = ({reg1} > {reg2}) ? 1 : 0";
            }

            if (instr == "gtri")
            {
                var result = IndexToRegister(c);
                var reg1 = IndexToRegister(a);
                return $"{pos:00}: {result} = ({reg1} > {b}) ? 1 : 0";
            }

            if (instr == "gtir")
            {
                var result = IndexToRegister(c);
                var reg2 = IndexToRegister(b);
                return $"{pos:00}: {result} = ({a} > {reg2}) ? 1 : 0";
            }

            if (instr == "eqrr")
            {
                var result = IndexToRegister(c);
                var reg1 = IndexToRegister(a);
                var reg2 = IndexToRegister(b);
                return $"{pos:00}: {result} = ({reg1} == {reg2}) ? 1 : 0";
            }

            if (instr == "eqri")
            {
                var result = IndexToRegister(c);
                var reg1 = IndexToRegister(a);
                return $"{pos:00}: {result} = ({reg1} == {b}) ? 1 : 0";
            }

            if (instr == "muli")
            {
                var result = IndexToRegister(c);
                var reg1 = IndexToRegister(a);
                return $"{pos:00}: {result} = {reg1} * {b}";
            }

            if (instr == "setr")
            {
                var result = IndexToRegister(c);
                var reg1 = IndexToRegister(a);
                return $"{pos:00}: {result} = {reg1}";
            }

            return $"{pos:00}: {instr} {a} {b} {c}";
        }
    }
}

