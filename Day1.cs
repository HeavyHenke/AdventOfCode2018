using System.Collections.Generic;
using System.IO;

namespace AoC2018
{
    class Day1
    {
        public string CalcA()
        {
            var data = File.ReadAllLines("Day1.data");
            int freq = 0;
            foreach (var d in data)
            {
                var delta = int.Parse(d);
                freq += delta;
            }

            return freq.ToString();
        }

        public string CalcB()
        {
            var data = File.ReadAllLines("Day1.data");
            int freq = 0;
            var visited = new HashSet<int>{0};

            while (true)
            {
                foreach (var d in data)
                {
                    var delta = int.Parse(d);
                    freq += delta;

                    if (visited.Add(freq) == false)
                        return freq.ToString();
                }
            }
        }

    }
}
