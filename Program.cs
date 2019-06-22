using System;
using System.Collections;
using System.Diagnostics;
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
            string result = new Day21().CalcA();
            DateTime stop = DateTime.Now;

            Console.WriteLine("It took " + (stop - start).TotalSeconds);

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }

    class Day21
    {
        public string CalcA()
        {
            return " ";
        }
    }
}

