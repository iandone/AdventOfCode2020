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

            Console.WriteLine($"Valid Password Count = {LoadInput().Sum(rule => rule.IsValid() ? 1 : 0)}");
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

            private int LastChar => _rule[^1];

            private IEnumerable<int> Limits => Regex.Split(_rule, "\\D+").Where(limit => !string.IsNullOrEmpty(limit)).Select(limit => int.Parse(limit));

            private bool CompareToLastChar(int pos) => _password.ToCharArray()[pos] == LastChar;

            public bool IsValid()
            {
                int frequency = Limits.Sum(l => CompareToLastChar(l) ? 1 : 0);

                return frequency == 1;
            }
        }

        private static IEnumerable<Input> LoadInput()
        {
            return File.ReadAllLines("input-day2.txt").Select(s => new Input(rule: s.Split(":")[0], password: s.Split(":")[1]));
        }
    }
}
