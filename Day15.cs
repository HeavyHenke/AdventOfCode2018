using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2018
{
    public class Day15
    {
        private readonly HashSet<(int x, int y)> _walls = new HashSet<(int x, int y)>();
        private readonly Dictionary<(int x, int y), int> _goblins = new Dictionary<(int x, int y), int>();
        private readonly Dictionary<(int x, int y), int> _elves = new Dictionary<(int x, int y), int>();
        private int _maxX;
        private int _maxY;


        public string CalcA()
        {
            var lines = File.ReadAllLines("Day15.txt");
            _maxX = lines.Length;
            _maxY = lines[0].Length;

            for (int y = 0; y < lines.Length; y++)
            for (int x = 0; x < lines[y].Length; x++)
            {
                switch (lines[y][x])
                {
                    case '#':
                        _walls.Add((x, y));
                        break;
                    case 'G':
                        _goblins.Add((x, y), 200);
                        break;
                    case 'E':
                        _elves.Add((x, y), 200);
                        break;
                }
            }

            int completedTurns = 0;
            Print(_maxY, _maxX, _walls, _goblins, _elves, completedTurns);
            Console.ReadKey();
            Console.WriteLine();

            while (true)
            {
                if (CalcCompletedTurn(completedTurns, 3))
                {
                    completedTurns++;
                }
                else
                {
                    break;
                }
            }


            Print(_maxY, _maxX, _walls, _goblins, _elves, completedTurns);

            return (completedTurns * (_elves.Values.Sum() + _goblins.Values.Sum())).ToString();
        }

        public string CalcB()
        {
            int elvesPower = 4;

            while (true)
            {
                _walls.Clear();
                _goblins.Clear();
                _elves.Clear();

                var lines = File.ReadAllLines("Day15.txt");
                _maxX = lines.Length;
                _maxY = lines[0].Length;

                for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[y].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '#':
                            _walls.Add((x, y));
                            break;
                        case 'G':
                            _goblins.Add((x, y), 200);
                            break;
                        case 'E':
                            _elves.Add((x, y), 200);
                            break;
                    }
                }


                //Print(_maxY, _maxX, _walls, _goblins, _elves, 0);
                //Console.ReadKey();
                //Console.WriteLine();

                int numStartElves = _elves.Count;
                int completedTurns = 0;

                while (true)
                {
                    if (CalcCompletedTurn(completedTurns, elvesPower))
                    {
                        completedTurns++;
                    }
                    else
                    {
                        break;
                    }
                    if (_elves.Count != numStartElves)
                        break;
                }

                if (_elves.Count == numStartElves)
                {
                    Console.WriteLine("Combat ends after " + completedTurns + " full rounds.");
                    Console.WriteLine(_elves.Values.Sum() + " hp left");
                    Print(_maxY, _maxX, _walls, _goblins, _elves, completedTurns);
                    return (completedTurns * (_elves.Values.Sum() + _goblins.Values.Sum())).ToString();
                }

                elvesPower++;
            }
        }


        private bool CalcCompletedTurn(int completedTurns, int elvesPower)
        {
            var turnOrder = _goblins.Keys.Concat(_elves.Keys).OrderBy(p => p.y).ThenBy(p => p.x).ToList();
            var hasMoved = new HashSet<(int x, int y)>();
            foreach (var turnPos in turnOrder)
            {
                if (hasMoved.Contains(turnPos))
                    continue;

                if (_goblins.ContainsKey(turnPos))
                {
                    if (_elves.Count == 0 || _goblins.Count == 0)
                        return false;
                    DoATurn(turnPos, hasMoved, _elves, _goblins, 3);
                }
                else if (_elves.ContainsKey(turnPos))
                {
                    if (_elves.Count == 0 || _goblins.Count == 0)
                        return false;
                    DoATurn(turnPos, hasMoved, _goblins, _elves, elvesPower);
                }
            }

            //Print(_maxY, _maxX, _walls, _goblins, _elves, completedTurns + 1);
            //Console.ReadKey();
            //Console.WriteLine();
            return true;
        }

        private void DoATurn((int x, int y) turnPos, HashSet<(int x, int y)> hasMoved, Dictionary<(int x, int y), int> enemies, Dictionary<(int x, int y), int> friendlies, int attackPower)
        {
            // Need to move?
            if (PositionAdjacentTo(turnPos, enemies.Keys).Any() == false)
            {
                var positionsAdjacentToEnemies = enemies.SelectMany(e => GetAllEmptyAdjacentTo(e.Key.x, e.Key.y)).ToList();
                if (positionsAdjacentToEnemies.Count == 0)
                    return;

                var movePos = FindClosesPathTo(turnPos, positionsAdjacentToEnemies);
                if (movePos.HasValue)
                {
                    int health = friendlies[turnPos];
                    friendlies.Remove(turnPos);
                    turnPos = movePos.Value;
                    friendlies.Add(turnPos, health);
                    hasMoved.Add(turnPos);
                }
            }

            // Attack
            var target = PositionAdjacentTo(turnPos, enemies.Keys).OrderBy(p => enemies[p]).ThenBy(p => p.y).ThenBy(p => p.x).FirstOrDefault();
            if (target != default)
            {
                enemies[target] -= attackPower;
                if (enemies[target] < 1)
                    enemies.Remove(target);
            }
        }

        private (int x, int y)? FindClosesPathTo((int x, int y) fromPos, IEnumerable<(int x, int y)> destinationPos)
        {
            var distances = new int[_maxX, _maxY];
            var searchQueue = new Queue<(int steps, int x, int y)>();

            foreach (var pos in destinationPos)
            {
                searchQueue.Enqueue((1, pos.x, pos.y));
                distances[pos.x, pos.y] = 1;
            }


            while (searchQueue.Count > 0)
            {
                var node = searchQueue.Dequeue();

                foreach (var adj in GetAllEmptyAdjacentTo(node.x, node.y))
                {
                    var distToAdj = distances[adj.x, adj.y];
                    if (distToAdj == 0 || node.steps + 1 < distToAdj)
                    {
                        distances[adj.x, adj.y] = node.steps + 1;
                        searchQueue.Enqueue((node.steps + 1, adj.x, adj.y));
                    }
                }
            }

            var dest = GetAllEmptyAdjacentTo(fromPos.x, fromPos.y)
                .Select(p => (p, distances[p.x, p.y]))
                .Where(p => p.Item2 != 0)
                .OrderBy(p => p.Item2).ThenBy(p => p.p.y).ThenBy(p => p.p.x)
                .FirstOrDefault();
            if (dest == default)
                return null;
            return dest.p;
        }

        private IEnumerable<(int x, int y)> PositionAdjacentTo((int x, int y) pos, IEnumerable<(int x, int y)> characters)
        {
            foreach (var charPos in characters)
            {
                if (Math.Abs(charPos.x - pos.x) + Math.Abs(charPos.y - pos.y) == 1)
                    yield return charPos;
            }
        }

        private IEnumerable<(int x, int y)> GetAllEmptyAdjacentTo(int x, int y)
        {
            if (IsEmpty((x + 1, y)))
                yield return (x + 1, y);
            if (IsEmpty((x - 1, y)))
                yield return (x - 1, y);
            if (IsEmpty((x, y + 1)))
                yield return (x, y + 1);
            if (IsEmpty((x, y - 1)))
                yield return (x, y - 1);
        }

        private bool IsEmpty((int x, int y) pos)
        {
            if (pos.x < 0 || pos.x >= _maxX)
                return false;
            if (pos.y < 0 || pos.y >= _maxY)
                return false;

            if (_walls.Contains(pos)) return false;
            if (_goblins.ContainsKey(pos)) return false;
            if (_elves.ContainsKey(pos)) return false;
            return true;
        }


        private void Print(int maxX, int maxY, HashSet<(int x, int y)> walls, Dictionary<(int x, int y), int> goblins, Dictionary<(int x, int y), int> elves, int turn)
        {
            Console.WriteLine("Turn " + turn);
            for (int y = 0; y < maxY; y++)
            {
                List<string> extraInfo = new List<string>();
                for (int x = 0; x < maxX; x++)
                {
                    if (goblins.ContainsKey((x, y)))
                    {
                        Console.Write('G');
                        extraInfo.Add($"G({goblins[(x, y)]})");
                    }
                    else if (elves.ContainsKey((x, y)))
                    {
                        Console.Write('E');
                        extraInfo.Add($"E({elves[(x, y)]})");
                    }
                    else if (walls.Contains((x, y)))
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }


                Console.WriteLine("\t\t" + string.Join(", ", extraInfo));
            }
        }
    }
}