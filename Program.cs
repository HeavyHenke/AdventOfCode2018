using System;
using System.IO;
using System.Windows.Forms;

namespace AoC2018
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            string result = new Day9().CalcB();

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }
}
