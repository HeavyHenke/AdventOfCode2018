using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MoreLinq;

namespace AoC2018
{
    class Day7
    {
        public string CalcA()
        {
            var lines = File.ReadAllLines("Day7.txt");

            var regex = new Regex("Step (?<preStep>\\w) must be finished before step (?<step>\\w) can begin.", RegexOptions.Compiled);

            var dependencies = new Dictionary<char, string>();
            var stepsLeft = new HashSet<char>();
            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success == false)
                    throw new Exception("Knasig regex");

                var preStep = match.Groups["preStep"].Value[0];
                var step = match.Groups["step"].Value[0];
                if (dependencies.TryGetValue(step, out var d))
                    dependencies[step] = d + preStep;
                else
                    dependencies[step] = preStep.ToString();

                stepsLeft.Add(preStep);
                stepsLeft.Add(step);
            }

            string ret = "";
            while (stepsLeft.Count > 0)
            {
                var validMoves = new List<char>();
                foreach (var step in stepsLeft)
                {
                    if (dependencies.TryGetValue(step, out var preSteps) == false)
                    {
                        validMoves.Add(step);
                        continue;
                    }
                }

                var move = validMoves.OrderBy(o => o).First();
                stepsLeft.Remove(move);
                ret += move;

                foreach (var dependency in dependencies.ToList())
                {
                    if (dependency.Value.Contains(move))
                    {
                        var newStr = dependency.Value.Replace(move.ToString(), "");
                        if (newStr.Length > 0)
                            dependencies[dependency.Key] = newStr;
                        else
                            dependencies.Remove(dependency.Key);
                    }
                }
            }

            return ret;
        }

        public string CalcB()
        {
            var lines = File.ReadAllLines("Day7.txt");

            var regex = new Regex("Step (?<preStep>\\w) must be finished before step (?<step>\\w) can begin.", RegexOptions.Compiled);

            var dependencies = new Dictionary<char, string>();
            var stepsLeft = new HashSet<char>();
            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success == false)
                    throw new Exception("Knasig regex");

                var preStep = match.Groups["preStep"].Value[0];
                var step = match.Groups["step"].Value[0];
                if (dependencies.TryGetValue(step, out var d))
                    dependencies[step] = d + preStep;
                else
                    dependencies[step] = preStep.ToString();

                stepsLeft.Add(preStep);
                stepsLeft.Add(step);
            }

            int timePassed = 0;
            var wokerBusy = new List<(int timeLeft, char work)>();
            for (int i = 0; i < 5; i++)
                wokerBusy.Add((0, ' '));

            while (stepsLeft.Count > 0)
            {
                var move = FindWork(stepsLeft, dependencies, wokerBusy.Select(w => w.work));
                if (move == default(char))
                {
                    timePassed += FinishWork(wokerBusy, stepsLeft, dependencies);
                    continue;
                }

                // start work on move
                var availableWorker = wokerBusy.FindIndex(p => p.timeLeft == 0);
                if (availableWorker < 0)
                {
                    timePassed += FinishWork(wokerBusy, stepsLeft, dependencies);
                    availableWorker = wokerBusy.FindIndex(p => p.timeLeft == 0);
                }

                int workTime = move - 'A' + 61;
                wokerBusy[availableWorker] = (workTime, move);
            }

            timePassed += wokerBusy.Max(w => w.timeLeft);
            return timePassed.ToString();
        }

        private static int FinishWork(List<(int timeLeft, char work)> wokerBusy, HashSet<char> stepsLeft, Dictionary<char, string> dependencies)
        {
            var timeToRemove = wokerBusy.Where(w => w.timeLeft > 0).Min(w => w.timeLeft); 
            
            for (int i = 0; i < wokerBusy.Count; i++)
            {
                var time = wokerBusy[i].timeLeft - timeToRemove;
                if (time > 0)
                {
                    wokerBusy[i] = (time, wokerBusy[i].work);
                }
                else
                {
                    RemoveWork(stepsLeft, wokerBusy[i].work, dependencies);
                    wokerBusy[i] = (0, ' ');
                }
            }

            return timeToRemove;
        }

        private static char FindWork(HashSet<char> stepsLeft, Dictionary<char, string> dependencies, IEnumerable<char> workingOnNow)
        {
            var validMoves = new List<char>();
            foreach (var step in stepsLeft)
            {
                if (dependencies.ContainsKey(step) == false)
                {
                    validMoves.Add(step);
                }
            }

            var onGoingWork = workingOnNow.ToHashSet();
            var move = validMoves.Where(m => onGoingWork.Contains(m) == false).OrderBy(o => o).FirstOrDefault();
            return move;
        }

        private static void RemoveWork(HashSet<char> stepsLeft, char move, Dictionary<char, string> dependencies)
        {
            stepsLeft.Remove(move);

            foreach (var dependency in dependencies.ToList())
            {
                if (dependency.Value.Contains(move))
                {
                    var newStr = dependency.Value.Replace(move.ToString(), "");
                    if (newStr.Length > 0)
                        dependencies[dependency.Key] = newStr;
                    else
                        dependencies.Remove(dependency.Key);
                }
            }
        }
    }
}