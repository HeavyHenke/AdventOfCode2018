using System.Collections.Generic;
using MoreLinq;

namespace AoC2018
{
    public class Day14
    {
        // 1041411104
        public string CalcA()
        {
            var scoreBoard = new List<int> { 3, 7 };
            int elf1Pos = 0;
            int elf2Pos = 1;

            while(scoreBoard.Count < 10+640441)
            {
                int score = scoreBoard[elf1Pos] + scoreBoard[elf2Pos];
                if (score >= 10)
                {
                    scoreBoard.Add(1);
                    score -= 10;
                }
                scoreBoard.Add(score);

                elf1Pos += 1 + scoreBoard[elf1Pos];
                while (elf1Pos >= scoreBoard.Count)
                    elf1Pos -= scoreBoard.Count;

                elf2Pos += 1 + scoreBoard[elf2Pos];
                while (elf2Pos >= scoreBoard.Count)
                    elf2Pos -= scoreBoard.Count;
            }

            return string.Join("", scoreBoard.TakeLast(10));
        }

        public string CalcB()
        {
            throw new System.NotImplementedException();
        }
    }
}