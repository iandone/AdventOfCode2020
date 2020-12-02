using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World! This is Li's Advent of Code console app");

            Console.WriteLine($"Valid Password Count = {Load().Sum(rule => rule.IsValid() ? 1 : 0)}");
        }

        class Input
        {
            private readonly string _rule;
            private readonly string _password;

            public Input(string rule, string password)
            {
                _rule = rule;
                _password = password;
            }

            private int RuleChar => _rule[^1];

            private IEnumerable<int> Positioning => Regex.Split(_rule, "\\D+").Where(limit => !string.IsNullOrEmpty(limit)).Select(limit => int.Parse(limit));

            private bool Compare(int pos) => _password.ToCharArray()[pos] == RuleChar;

            public bool IsValid() => Positioning.Sum(pos => Compare(pos) ? 1 : 0) == 1;
        }

        private static IEnumerable<Input> Load()
        {
            return File.ReadAllLines("input-day2.txt").Select(s => new Input(rule: s.Split(":")[0], password: s.Split(":")[1]));
        }
    }
}
