using System.Collections.Generic;
using System.Linq;

namespace AoC2018
{
    class Day9
    {
        public string CalcA()
        {
            int numPlayers = 468;
            int lastMarble = 71843;

            var circle = new LinkedList<int>();
            var scores = new int[numPlayers];

            int curPlayer = 0;
            var curMarbleIx = circle.AddFirst(0);
            int nextMarble = 1;
            while (nextMarble <= lastMarble)
            {
                if (nextMarble % 23 == 0)
                {
                    var removeIx = curMarbleIx;
                    for (int i = 0; i < 7; i++) 
                        removeIx = removeIx.Previous ?? circle.Last;

                    curMarbleIx = removeIx.Next ?? circle.First;
                    circle.Remove(removeIx);

                    scores[curPlayer] += removeIx.Value + nextMarble;
                }
                else
                {
                    var nextIx = curMarbleIx.Next ?? circle.First;
                    nextIx = nextIx.Next ?? circle.First;

                    curMarbleIx = circle.AddBefore(nextIx, nextMarble);
                }

                nextMarble++;
                curPlayer++;
                if (curPlayer == numPlayers)
                    curPlayer = 0;
            }

            return scores.Max().ToString();
        }

        public string CalcB()
        {
            int numPlayers = 468;
            int lastMarble = 7184300;

            var circle = new LinkedList<int>();
            var scores = new long[numPlayers];

            int curPlayer = 0;
            var curMarbleIx = circle.AddFirst(0);
            int nextMarble = 1;
            while (nextMarble <= lastMarble)
            {
                if (nextMarble % 23 == 0)
                {
                    var removeIx = curMarbleIx;
                    for (int i = 0; i < 7; i++) 
                        removeIx = removeIx.Previous ?? circle.Last;

                    curMarbleIx = removeIx.Next ?? circle.First;
                    circle.Remove(removeIx);

                    scores[curPlayer] += removeIx.Value + nextMarble;
                }
                else
                {
                    var nextIx = curMarbleIx.Next ?? circle.First;
                    nextIx = nextIx.Next ?? circle.First;

                    curMarbleIx = circle.AddBefore(nextIx, nextMarble);
                }

                nextMarble++;
                curPlayer++;
                if (curPlayer == numPlayers)
                    curPlayer = 0;
            }

            return scores.Max().ToString();
        }

    }
}