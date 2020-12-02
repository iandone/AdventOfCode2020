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

            int validPasswordCount = LoadInput().Sum(rule => rule.IsValid() ? 1 : 0);

            Console.WriteLine("Valid Password Count = "+validPasswordCount);
        }

        class Input
        {
            private readonly string _rule;
            private readonly string _password;

            private int LastCharIndex => _rule.Length - 1;

            private IEnumerable<int> Limits => Regex.Split(_rule, "\\D+").Where(limit => !string.IsNullOrEmpty(limit)).Select(limit => int.Parse(limit));

            public Input(string rule, string password)
            {
                _rule = rule;
                _password = password;
            }

            public bool IsValid()
            {
                char character = _rule[LastCharIndex];

                int frequency = _password.ToCharArray().Count(c => c == character);

                return frequency >= Limits.Min() && frequency <= Limits.Max();
            }
        }

        private static IEnumerable<Input> LoadInput()
        {
            return File.ReadAllLines("input-day2.txt").Select(s => new Input(rule: s.Split(":")[0], password: s.Split(":")[1]));
        }
    }
}
