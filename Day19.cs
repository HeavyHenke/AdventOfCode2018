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
            var machine = new Machine("Day19.txt");
            return machine.Calc(0);
        }

        public string CalcB()
        {
            var machine = new Machine("Day19.txt");
            return machine.Calc(1);
        }
    }
}