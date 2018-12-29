using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MoreLinq;

namespace AoC2018
{
    class Day4
    {
        public string CalcA()
        {
            var stuff = new SortedList<DateTime, string>();
            foreach (var line in File.ReadAllLines("Day4.txt"))
            {
                DateTime time = DateTime.Parse(line.Substring(1, 16));
                string str = line.Substring(19);
                stuff.Add(time, str);
            }

            var guardBegins = new Regex(@"^Guard #(?<guard>\d+) begins shift");
            var fallsAsleep = new Regex(@"^falls asleep");
            var wakesUp = new Regex("^wakes up");
            int currentGuard = -1;
            DateTime fellAsleepTime = DateTime.MinValue;

            var guardSleep = new Dictionary<int, int[]>();
            foreach (var kvp in stuff)
            {
                var match = guardBegins.Match(kvp.Value);
                if (match.Success)
                {
                    if(fellAsleepTime != DateTime.MinValue)
                        throw new Exception("Guard didnt wake up");

                    currentGuard = int.Parse(match.Groups["guard"].Value);
                    fellAsleepTime = DateTime.MinValue;
                }
                else if (fallsAsleep.IsMatch(kvp.Value))
                {
                    if(fellAsleepTime != DateTime.MinValue)
                        throw new Exception("Cant fall asleep when already asleep");
                    fellAsleepTime = kvp.Key;
                }
                else if (wakesUp.IsMatch(kvp.Value))
                {
                    if(fellAsleepTime == DateTime.MinValue)
                        throw new Exception("Cant wake when not asleep");

                    if (guardSleep.ContainsKey(currentGuard) == false)
                        guardSleep[currentGuard] = new int[60];
                    for (int sleepMinute = fellAsleepTime.Minute; sleepMinute < kvp.Key.Minute; sleepMinute++)
                    {
                        guardSleep[currentGuard][sleepMinute]++;
                    }

                    fellAsleepTime = DateTime.MinValue;
                }
                else
                    throw new Exception("Unexpected line " + kvp.Value);
            }

            var maxSleepGuard = guardSleep.MaxBy(pair => pair.Value.Sum()).Select(p => p.Key).Single();

            var maxTime = guardSleep[maxSleepGuard].Max();
            var maxMinute = Array.IndexOf(guardSleep[maxSleepGuard], maxTime);

            return (maxMinute * maxSleepGuard).ToString();
        }

        public string CalcB()
        {
           var stuff = new SortedList<DateTime, string>();
            foreach (var line in File.ReadAllLines("Day4.txt"))
            {
                DateTime time = DateTime.Parse(line.Substring(1, 16));
                string str = line.Substring(19);
                stuff.Add(time, str);
            }

            var guardBegins = new Regex(@"^Guard #(?<guard>\d+) begins shift");
            var fallsAsleep = new Regex(@"^falls asleep");
            var wakesUp = new Regex("^wakes up");
            int currentGuard = -1;
            DateTime fellAsleepTime = DateTime.MinValue;

            var guardDutyCount = new Dictionary<int ,int>();
            var guardSleep = new Dictionary<int, int[]>();
            foreach (var kvp in stuff)
            {
                var match = guardBegins.Match(kvp.Value);
                if (match.Success)
                {
                    if(fellAsleepTime != DateTime.MinValue)
                        throw new Exception("Guard didnt wake up");

                    currentGuard = int.Parse(match.Groups["guard"].Value);
                    if (guardDutyCount.TryGetValue(currentGuard, out var preDutyCnt))
                        guardDutyCount[currentGuard] = preDutyCnt + 1;
                    else
                        guardDutyCount[currentGuard] = 1;

                    fellAsleepTime = DateTime.MinValue;
                }
                else if (fallsAsleep.IsMatch(kvp.Value))
                {
                    if(fellAsleepTime != DateTime.MinValue)
                        throw new Exception("Cant fall asleep when already asleep");
                    fellAsleepTime = kvp.Key;
                }
                else if (wakesUp.IsMatch(kvp.Value))
                {
                    if(fellAsleepTime == DateTime.MinValue)
                        throw new Exception("Cant wake when not asleep");

                    if (guardSleep.ContainsKey(currentGuard) == false)
                        guardSleep[currentGuard] = new int[60];
                    for (int sleepMinute = fellAsleepTime.Minute; sleepMinute < kvp.Key.Minute; sleepMinute++)
                    {
                        guardSleep[currentGuard][sleepMinute]++;
                    }

                    fellAsleepTime = DateTime.MinValue;
                }
                else
                    throw new Exception("Unexpected line " + kvp.Value);
            }

            var q = from g in guardSleep
                select (g.Key, g.Value.Max(), Array.IndexOf(g.Value, g.Value.Max()));

            var max = q.MaxBy(q2 => q2.Item2).Single();

            return (max.Item1 * max.Item3).ToString();
        }
    }
}