using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World! This is Li's Advent of Code console app");

            Console.WriteLine($"Valid Password Count = {Load().Sum(rule => rule.IsValid() ? 1 : 0)}");
        }

        private static IEnumerable<Rule> Load()
        {
            return File.ReadAllLines("input-day2.txt").Select(line => new Rule(line));
        }
    }
}