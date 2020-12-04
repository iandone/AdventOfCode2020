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

            Console.WriteLine($"Number of valid passports: {Load().Sum(p => p.IsValid() ? 1 : 0)}");
        }

        private static IList<Passport> Load()
        {
            return File.ReadAllText("input.txt").Split("\r\n\r\n").Select(p => new Passport(p)).ToList();
        }
    }
}