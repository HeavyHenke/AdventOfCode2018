using System.Collections.Generic;
using MoreLinq;

namespace AoC2018
{
    public class Day14
    {
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
            var scoreBoard = new List<byte> { 3, 7 };
            int elf1Pos = 0;
            int elf2Pos = 1;

            var toFind = new byte[] { 6, 4, 0, 4, 4};

            while(true)
            {
                var elf1CurScore = scoreBoard[elf1Pos];
                var elf2CurScore = scoreBoard[elf2Pos];
                byte score = (byte) (elf1CurScore + elf2CurScore);
                if (score >= 10)
                {
                    scoreBoard.Add(1);
                    score -= 10;
                }
                scoreBoard.Add(score);

                elf1Pos += 1 + elf1CurScore;
                while (elf1Pos >= scoreBoard.Count)
                    elf1Pos -= scoreBoard.Count;

                elf2Pos += 1 + elf2CurScore;
                while (elf2Pos >= scoreBoard.Count)
                    elf2Pos -= scoreBoard.Count;

                if (scoreBoard.Count >= toFind.Length)
                {
                    bool correct = true;
                    for (int i = 0; i < toFind.Length && correct; i++)
                    {
                        if (scoreBoard[scoreBoard.Count - toFind.Length + i] != toFind[i])
                            correct = false;
                    }
                    if(correct)
                        break;
                }
            }

            return (scoreBoard.Count - toFind.Length).ToString();
        }
    }
}