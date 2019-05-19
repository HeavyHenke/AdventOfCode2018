using System;
using System.Windows.Forms;

namespace AoC2018
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            DateTime start = DateTime.Now;
            string result = new Day17().CalcA();
            DateTime stop = DateTime.Now;

            Console.WriteLine("It took " + (stop - start).TotalSeconds);

            Clipboard.SetText(result);
            Console.WriteLine(result);
        }
    }

    public class Day17
    {
        public string CalcA()
        {
            return "";
        }
    }
}
