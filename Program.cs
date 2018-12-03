using System;
using System.Windows.Forms;

namespace AoC2018
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string result = new Day3().CalcB();

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }
}
