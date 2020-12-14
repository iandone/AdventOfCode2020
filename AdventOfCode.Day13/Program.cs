using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day13
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World! This is Li's Advent of Code console app");

            // P1
            var testScenario1 = Load("input-test.txt");
            Console.WriteLine($"Test 1: ({testScenario1.Part1Result})"); //295

            var realScenario1 = Load("input.txt");
            Console.WriteLine($"Part 1: ({realScenario1.Part1Result})"); // 2045

            // P2
            var testScenario2 = Load("input-test.txt"); ;
            Console.WriteLine($"Test 2: ({testScenario2.Part2Result()})"); // 1068781

            var realScenario2 = Load("input.txt");
            Console.WriteLine($"Part 2: ({realScenario2.Part2Result()})"); // 402251700208309
        }

        private static Scenario Load(string filename)
        {
            var input = File.ReadAllLines(filename).Where(line => !string.IsNullOrEmpty(line)).ToList();

            var busIds =  new List<int>();

            foreach (string entry in input[1].Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                if (int.TryParse(entry, out int id))
                {
                    busIds.Add(id);
                }
                else
                {
                    // this shall serve us well
                    busIds.Add(0);
                }
            }

            return new Scenario(earliestDepartureTime: int.Parse(input[0]), busIds: busIds);
        }
    }

    public class Scenario
    {
        private readonly int _earliestDepartureTime;
        private readonly List<int> _busIds;
        private readonly Dictionary<int, int> _waitTimes;

        public Scenario(int earliestDepartureTime, List<int> busIds)
        {
            _earliestDepartureTime = earliestDepartureTime;
            _busIds = busIds;
            _waitTimes = GetWaitTimes();
        }

        public long Part1Result => EarliestBusId * _waitTimes[EarliestBusId];

        public long Part2Result()
        {
            long time = 0;

            long step = _busIds.First();
            int offsetIndex = 1;

            while (offsetIndex < _busIds.Count)
            {
                if (_busIds[offsetIndex] > 0)
                {
                    time += step;

                    if ((time + offsetIndex) % _busIds[offsetIndex] > 0)
                    {
                        // should divide perfectly
                        continue;
                    }
                    step *= _busIds[offsetIndex];
                }

                offsetIndex++;
            }

            return time;
        }

        private int EarliestBusId => _waitTimes.Where(r => r.Value == _waitTimes.Values.Min()).Single().Key;

        private Dictionary<int, int> GetWaitTimes() => _busIds.Where(id => id > 0).ToDictionary(k => k,
            v => _earliestDepartureTime - (_earliestDepartureTime % v) + v - _earliestDepartureTime);
    }
}
