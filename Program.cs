﻿using System;
using System.Windows.Forms;

namespace AoC2018
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            DateTime start = DateTime.Now;
            string result = new Day15().CalcA();
            DateTime stop = DateTime.Now;

            Console.WriteLine("It took " + (stop - start).TotalSeconds);

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }

    public class Day15
    {
        public string CalcA()
        {
            return "";
        }
    }
}
