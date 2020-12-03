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
            string result = new Day22().CalcA();
            DateTime stop = DateTime.Now;

            Console.WriteLine("It took " + (stop - start).TotalSeconds);

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }

    class Day22
    {
        private int _targetX;
        private int _targetY;
        private int _depth;

        public string CalcA()
        {
            //_depth = 510;
            //(_targetX, _targetY) = (10, 10);
            _depth = 6969;
            (_targetX, _targetY) = (9, 796);

            var totalRisk = 0;
            for (int y = 0; y <= _targetY; y++)
            {
                for (int x = 0; x <= _targetX; x++)
                {
                    var risk = GetRisk(x, y);
                    totalRisk += risk;
                }
            }

            return totalRisk.ToString();
        }


        private byte GetRisk(int x, int y)
        {
            var hepp = (GetGeologicalIndex(x, y) + _depth) % 20183;
            return (byte) (hepp % 3);
        }

        private readonly Dictionary<(int x, int y), long> _geoIndexes = new Dictionary<(int x, int y), long>();

        private long GetGeologicalIndex(int x, int y)
        {
            if (x == 0 && y == 0)
                return 0;
            if (x == _targetX && y == _targetY)
                return 0;
            if (_geoIndexes.TryGetValue((x, y), out var geoIx))
                return geoIx;

            if (y == 0)
            {
                var ix = x * 16807L;
                _geoIndexes[(x, y)] = ix;
                return ix;
            }

            if (x == 0)
            {
                var ix = y * 48271L;
                _geoIndexes[(x, y)] = ix;
                return ix;
            }

            var ero1 = (GetGeologicalIndex(x - 1, y) + _depth) % 20183L;
            var ero2 = (GetGeologicalIndex(x, y - 1) + _depth) % 20183L;
            var geoIx2 = ero1 * ero2;
            _geoIndexes[(x, y)] = geoIx2;

            return geoIx2;
        }
    }

    class Day21
    {
        public string CalcA()
        {
            var machine = new Machine("Day21.txt");

            machine.Calc(212115);   // Max found thru debugging
            return machine.NumInstructionsExecuted.ToString();
        }
    }



    public class Machine
    {
        private readonly List<Action> _program;
        private readonly int[] _registers;
        private readonly int _ipRegister;

        public int NumInstructionsExecuted { get; private set; }

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
                //if (_registers[_ipRegister] == 13)
                //    _registers[_ipRegister] = 28;
                if (_registers[_ipRegister] == 8)
                    ;

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

