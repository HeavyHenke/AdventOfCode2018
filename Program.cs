using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AoC2018
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            string result = new Day8().CalcB();

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }
}
