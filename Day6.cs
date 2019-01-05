using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace AoC2018
{
    class Day6
    {
        public string CalcA()
        {
            var points = new Dictionary<(int x, int y), int>();
            int numPoints = 0;

            foreach (var line in _input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var lineSplit = line.Split(',');
                var pt = (x: int.Parse(lineSplit[0]) + 1, y: int.Parse(lineSplit[1]) + 1);

                points.Add(pt, ++numPoints);
            }

            int maxX = points.Keys.Max(pt => pt.x) + 2;
            int maxY = points.Keys.Max(pt => pt.y) + 2;
            var matrix = new int[maxX, maxY];

            for (int x = 0; x < maxX; x++)
            for (int y = 0; y < maxY; y++)
            {
                int closestIndex = -1;
                var lengthes = points.Select(k => (ix: k.Value, dist: Abs(k.Key.x - x) + Abs(k.Key.y - y))).OrderBy(o => o.dist).Take(2).ToList();
                if (lengthes[0].dist != lengthes[1].dist)
                    matrix[x, y] = lengthes[0].ix;
            }

            // Disqualify infinite
            var disq = new HashSet<int>();
            for (int x = 0; x < maxX; x++)
            {
                disq.Add(matrix[x, 0]);
                disq.Add(matrix[x, maxY - 1]);
            }
            for (int y = 0; y < maxY; y++)
            {
                disq.Add(matrix[0, y]);
                disq.Add(matrix[maxX - 1, y]);
            }

            var sizes = new int[numPoints];
            for (int x = 0; x < maxX; x++)
            for (int y = 0; y < maxY; y++)
            {
                var pt = matrix[x, y];
                if (pt != 0 && disq.Contains(pt) == false)
                    sizes[pt]++;
            }


            var maxSize = sizes.Max();
            return maxSize.ToString();
        }


        public string CalcB()
        {
            var points = new Dictionary<(int x, int y), int>();
            int numPoints = 0;

            foreach (var line in _input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var lineSplit = line.Split(',');
                var pt = (x: int.Parse(lineSplit[0]), y: int.Parse(lineSplit[1]));

                points.Add(pt, ++numPoints);
            }

            int maxX = points.Keys.Max(pt => pt.x);
            int maxY = points.Keys.Max(pt => pt.y);
            int size = 0;

            // Calc inner positions
            for (int x = -10000 / numPoints; x < maxX + 10000 / numPoints; x++)
            for (int y = -10000 / numPoints; y < maxY + 10000 / numPoints; y++)
            {
                var totManhattanDist = points.Select(k => Abs(k.Key.x - x) + Abs(k.Key.y - y)).Sum();
                if (totManhattanDist < 10000)
                    size++;
            }

            return size.ToString();
        }

        private const string _testInput = @"1, 1
1, 6
8, 3
3, 4
5, 5
8, 9";
        private const string _input = @"249, 60
150, 332
174, 83
287, 329
102, 338
111, 201
259, 96
277, 161
143, 288
202, 311
335, 55
239, 148
137, 224
48, 214
186, 87
282, 334
147, 157
246, 191
241, 181
286, 129
270, 287
79, 119
189, 263
324, 280
316, 279
221, 236
327, 174
141, 82
238, 317
64, 264
226, 151
110, 110
336, 194
235, 333
237, 55
230, 137
267, 44
258, 134
223, 42
202, 76
159, 135
229, 238
197, 83
173, 286
123, 90
314, 165
140, 338
347, 60
108, 76
268, 329";
    }
}