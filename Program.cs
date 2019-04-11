using System;
using System.Windows.Forms;

namespace AoC2018
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            string result = new Day11().CalcA();

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }
}
