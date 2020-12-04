using System;
using System.Collections.Generic;

namespace AoC2018
{
    internal class Day22
    {
        private int _targetX;
        private int _targetY;
        private int _depth;

        public string CalcA()
        {
            //_depth = 510;
            //(_targetX, _targetY) = (10, 10);
            _depth = 6969;
            (_targetX, _targetY) = (9, 796);

            var totalRisk = 0;
            for (int y = 0; y <= _targetY; y++)
            {
                for (int x = 0; x <= _targetX; x++)
                {
                    var risk = GetRisk(x, y);
                    totalRisk += risk;
                }
            }

            return totalRisk.ToString();
        }

        public string CalcB()
        {
            //_depth = 510;
            //(_targetX, _targetY) = (10, 10);
            _depth = 6969;
            (_targetX, _targetY) = (9, 796);

            var map = new Dictionary<(int x, int y), Node>();
            var startNode = new Node
            {
                X = 0, Y=0,
                DistTourch = 0,
                DistClimbing = 7,
                DistNeither = 7,
                Type = Terrain.StartPos
            };
            map.Add((0,0), startNode);

            Node GetNodeAt(int x, int y)
            {
                if (map.TryGetValue((x, y), out var node))
                    return node;

                node = new Node
                {
                    X = x,
                    Y = y,
                    Type = (Terrain) GetRisk(x, y)
                };
                map[(x, y)] = node;
                return node;
            }

            var searchQueue = new OrderedList<SearchOrder>(new SearhOrderComparer());
            searchQueue.Add(new SearchOrder
            {
                Distance = 0,
                Node = startNode,
                Tool = Tool.Torch
            });
            searchQueue.Add(new SearchOrder
            {
                Distance = 7,
                Node = startNode,
                Tool = Tool.Neither
            }); searchQueue.Add(new SearchOrder
            {
                Distance = 7,
                Node = startNode,
                Tool = Tool.Climbing
            });
            
            while (searchQueue.Count > 0)
            {
                var order = searchQueue[0];
                searchQueue.RemoveAt(0);

                if (order.Node.X == _targetX && order.Node.Y == _targetY && order.Tool == Tool.Torch)
                    return order.Distance.ToString();

                // Console.WriteLine($"In ({order.Node.X}, {order.Node.Y})");

                // Change tool
                switch (order.Node.Type)
                {
                    case Terrain.StartPos:
                        break;
                    case Terrain.Rocky:
                        if (order.Tool == Tool.Climbing)
                        {
                            if (order.Node.DistTourch > order.Distance + 7)
                            {
                                order.Node.DistTourch = order.Distance + 7;
                                searchQueue.Add(new SearchOrder
                                {
                                    Distance = order.Distance+7,
                                    Node = order.Node,
                                    Tool = Tool.Torch
                                });
                            }
                        }
                        else if (order.Tool == Tool.Torch)
                        {
                            if (order.Node.DistClimbing > order.Distance + 7)
                            {
                                order.Node.DistClimbing = order.Distance + 7;
                                searchQueue.Add(new SearchOrder
                                {
                                    Distance = order.Distance + 7,
                                    Node = order.Node,
                                    Tool = Tool.Climbing
                                });
                            }
                        }
                        else throw new Exception("Invalid tool: " + order.Node.Type + " " + order.Tool);
                        break;
                    case Terrain.Wet:
                        if (order.Tool == Tool.Climbing)
                        {
                            if (order.Node.DistNeither > order.Distance + 7)
                            {
                                order.Node.DistNeither = order.Distance + 7;
                                searchQueue.Add(new SearchOrder
                                {
                                    Distance = order.Distance + 7,
                                    Node = order.Node,
                                    Tool = Tool.Neither
                                });
                            }
                        }
                        else if (order.Tool == Tool.Neither)
                        {
                            if (order.Node.DistClimbing > order.Distance + 7)
                            {
                                order.Node.DistClimbing = order.Distance + 7;
                                searchQueue.Add(new SearchOrder
                                {
                                    Distance = order.Distance + 7,
                                    Node = order.Node,
                                    Tool = Tool.Climbing
                                });
                            }
                        }
                        else throw new Exception("Invalid tool: " + order.Node.Type + " " + order.Tool);
                        break;
                    case Terrain.Narrow:
                        if (order.Tool == Tool.Torch)
                        {
                            if (order.Node.DistNeither > order.Distance + 7)
                            {
                                order.Node.DistNeither = order.Distance + 7;
                                searchQueue.Add(new SearchOrder
                                {
                                    Distance = order.Distance + 7,
                                    Node = order.Node,
                                    Tool = Tool.Neither
                                });
                            }
                        }
                        else if (order.Tool == Tool.Neither)
                        {
                            if (order.Node.DistTourch > order.Distance + 7)
                            {
                                order.Node.DistTourch = order.Distance + 7;
                                searchQueue.Add(new SearchOrder
                                {
                                    Distance = order.Distance + 7,
                                    Node = order.Node,
                                    Tool = Tool.Torch
                                });
                            }
                        }
                        else throw new Exception("Invalid tool: " + order.Node.Type + " " + order.Tool);

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                // Move
                foreach (var newPos in new (int x, int y)[]
                {
                    (order.Node.X - 1, order.Node.Y), (order.Node.X + 1, order.Node.Y),
                    (order.Node.X, order.Node.Y - 1), (order.Node.X, order.Node.Y + 1)
                })
                {
                    if(newPos.x < 0 ||newPos.y < 0)
                        continue;
                    
                    var destNode = GetNodeAt(newPos.x, newPos.y);
                    switch (order.Tool)
                    {
                        case Tool.Neither:
                            if ((destNode.Type == Terrain.Wet || destNode.Type == Terrain.Narrow ) && destNode.DistNeither > order.Distance + 1)
                            {
                                destNode.DistNeither = order.Distance + 1;
                                searchQueue.Add(new SearchOrder
                                {
                                    Distance = order.Distance + 1,
                                    Node = destNode,
                                    Tool = order.Tool
                                });
                            }
                            break;
                        case Tool.Climbing:
                            if ((destNode.Type == Terrain.Rocky || destNode.Type == Terrain.Wet) && destNode.DistClimbing > order.Distance + 1)
                            {
                                destNode.DistClimbing = order.Distance + 1;
                                searchQueue.Add(new SearchOrder
                                {
                                    Distance = order.Distance + 1,
                                    Node = destNode,
                                    Tool = order.Tool
                                });
                            }
                            break;
                        case Tool.Torch:
                            if ((destNode.Type == Terrain.Rocky || destNode.Type == Terrain.Narrow) && destNode.DistTourch > order.Distance + 1)
                            {
                                destNode.DistTourch = order.Distance + 1;
                                searchQueue.Add(new SearchOrder
                                {
                                    Distance = order.Distance + 1,
                                    Node = destNode,
                                    Tool = order.Tool
                                });
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                }
            }

            throw new Exception("Got lost");
        }

        enum Tool
        {
            Neither = 0,
            Climbing = 1,
            Torch = 2,
        }

        enum Terrain
        {
            Rocky = 0,
            Wet = 1,
            Narrow = 2,
            StartPos = 3
        }

        class Node
        {
            public int X;
            public int Y;
            public Terrain Type;
            public int DistNeither = int.MaxValue;
            public int DistClimbing = int.MaxValue;
            public int DistTourch = int.MaxValue;
        }

        class SearchOrder
        {
            public Node Node;
            public Tool Tool;
            public int Distance;
        }

        class SearhOrderComparer : IComparer<SearchOrder>
        {
            public int Compare(SearchOrder x, SearchOrder y)
            {
                return x.Distance.CompareTo(y.Distance);
            }
        }


        private byte GetRisk(int x, int y)
        {
            var hepp = (GetGeologicalIndex(x, y) + _depth) % 20183;
            return (byte) (hepp % 3);
        }

        private readonly Dictionary<(int x, int y), long> _geoIndexes = new Dictionary<(int x, int y), long>();

        private long GetGeologicalIndex(int x, int y)
        {
            if (x == 0 && y == 0)
                return 0;
            if (x == _targetX && y == _targetY)
                return 0;
            if (_geoIndexes.TryGetValue((x, y), out var geoIx))
                return geoIx;

            if (y == 0)
            {
                var ix = x * 16807L;
                _geoIndexes[(x, y)] = ix;
                return ix;
            }

            if (x == 0)
            {
                var ix = y * 48271L;
                _geoIndexes[(x, y)] = ix;
                return ix;
            }

            var ero1 = (GetGeologicalIndex(x - 1, y) + _depth) % 20183L;
            var ero2 = (GetGeologicalIndex(x, y - 1) + _depth) % 20183L;
            var geoIx2 = ero1 * ero2;
            _geoIndexes[(x, y)] = geoIx2;

            return geoIx2;
        }
    }
}