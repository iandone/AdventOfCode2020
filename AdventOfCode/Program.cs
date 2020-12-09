using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        private static readonly int _preamble = 25;
        private static long[] _input;
        private static long[] _checklist;

        static void Main()
        {
            Load();

            long p1Result = FindWeakness();
            Console.WriteLine($"P1 = ({(p1Result > 0 ? p1Result.ToString() : "No such number found")})"); // 36845998

            long p2Result = FindEncryptionWeakness(p1Result);
            Console.WriteLine($"P2 = ({p2Result})"); // 4830226
        }

        private static long FindWeakness()
        {
            for (int i = 0; i < _checklist.Length; i++)
            {
                if (!IsSumOfTwoElements(_checklist[i], _input[i..(i + _preamble)]))
                {
                    return _checklist[i];
                }
            }

            return -1;
        }

        private static bool IsSumOfTwoElements(long number, long[] range)
        {
            for (int i = 0; i < range.Length; i++)
            {
                for (int j = 0; j < range.Length && i != j; j++)
                {
                    if (number == range[i] + range[j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static long FindEncryptionWeakness(long number)
        {
            for (int i = 0; i < _checklist.Length; i++)
            {
                int j = i;

                while (_checklist[i..j].Sum() < number && j < _checklist.Length)
                {
                    j++;
                }

                if (_checklist[i..j].Sum() == number)
                {
                    return _checklist[i..j].Min() + _checklist[i..j].Max();
                }
            }

            return -1;
        }

        private static void Load()
        {
            Console.WriteLine("Hello World! This is Li's Advent of Code console app");
            _input = File.ReadAllLines("input.txt").Select(num => long.Parse(num)).ToArray();
            _checklist = _input[_preamble..];
        }
    }
}