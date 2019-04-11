using System;
using System.Collections.Generic;

namespace AoC2018
{
    public class Day11
    {
        public string CalcA()
        {
            const int serial = 7347;
            var grid = new int[300, 300];

            for (int y = 0; y < 300; y++)
            for (int x = 0; x < 300; x++)
            {
                int rackId = (x + 10);
                int temp = (rackId * y + serial) * rackId;
                int power = 0;
                if (temp > 100)
                {
                    temp = temp / 100;
                    power = temp % 10 - 5;
                }

                grid[y, x] = power;
            }

            int maxX = 0;
            int maxY = 0;
            int maxPower = 0;
            int maxSize = 0;

            var preCaledGrids = new List<int[,]>();
            preCaledGrids.Add(new int[300, 300]);   // 0-size grid

            for (int size = 1; size <= 300; size++)
            {
                var sizeGrid = new int[300 - size + 1, 300 - size + 1];
                for (int y = 0; y < 300 - size; y++)
                for (int x = 0; x < 300 - size; x++)
                {
                    int power = preCaledGrids[size - 1][y, x] + grid[y + size - 1, x + size - 1];
                    for (int i = 0; i < size - 1; i++)
                        power += grid[y + size - 1, x + i] + grid[y + i, x + size - 1];

                    if (power > maxPower)
                    {
                        maxPower = power;
                        maxX = x;
                        maxY = y;
                        maxSize = size;
                    }

                    sizeGrid[y, x] = power;
                }
                preCaledGrids.Add(sizeGrid);
                Console.WriteLine("Size " + size);
            }

            Console.WriteLine("Total power " + maxPower);
            return $"{maxX},{maxY},{maxSize}"; // 233,228,12
        }
    }
}