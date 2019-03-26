using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2018
{
    class Day8
    {
        public string CalcA()
        {
            string test = File.ReadAllText("Day8.txt");

            int sum = ReadNode(test.Split(' ').Select(int.Parse).GetEnumerator()).Sum();
            return sum.ToString();
        }

        private IEnumerable<int> ReadNode(IEnumerator<int> input)
        {
            input.MoveNext();
            int numChild = input.Current;

            input.MoveNext();
            int numMeta = input.Current;

            for (int i = 0; i < numChild; i++)
                foreach (var meta in ReadNode(input))
                    yield return meta;

            for (int i = 0; i < numMeta; i++)
            {
                input.MoveNext();
                yield return input.Current;
            }
        }


        public string CalcB()
        {
            string test = File.ReadAllText("Day8.txt");

            int sum = ReadNodeB(test.Split(' ').Select(int.Parse).GetEnumerator());
            return sum.ToString();
        }

        private static int ReadNodeB(IEnumerator<int> input)
        {
            input.MoveNext();
            int numChild = input.Current;

            input.MoveNext();
            int numMeta = input.Current;

            var children = new int[numChild];
            for (int i = 0; i < numChild; i++)
                children[i] = ReadNodeB(input);

            int sum = 0;
            for (int i = 0; i < numMeta; i++)
            {
                input.MoveNext();

                if (numChild > 0)
                {
                    var childIx = input.Current - 1;
                    if (childIx >= 0 && childIx < children.Length)
                        sum += children[childIx];
                }
                else
                {
                    sum += input.Current;
                }
            }

            return sum;
        }
    }
}