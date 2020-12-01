using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static int MaxNumberOfDigits = 4; // 2020.Length
        public static int MinNumberOfDigits = 2; // based on the input file

        static void Main()
        {
            Console.WriteLine("Hello World! This is Li's Advent of Code console app");

            var input = LoadInput();

            int comparison = 0;

            for (int i = 0; i < input.Count(); i++)
            {
                int a = input.ElementAt(i);

                for (int j = 0; j < input.Count() && j != i; j++)
                {
                    if (GetNumberOfDigits(input.ElementAt(j)) <= MaxNumberOfDigits - GetNumberOfDigits(input.ElementAt(i)) + MinNumberOfDigits)
                    {
                        int b = input.ElementAt(j);

                        for (int k = 0; k < input.Count() && k != i && k != j; k++)
                        {
                            if (GetNumberOfDigits(input.ElementAt(k)) <= MaxNumberOfDigits - GetNumberOfDigits(a + b) + MinNumberOfDigits + 1)
                            {
                                int c = input.ElementAt(k);

                                comparison++;

                                if (a + b + c == 2020)
                                {
                                    Console.WriteLine(
                                        $"The result is combination {a} + {b} + {c} = 2020, multiplied {a * b * c}, after {comparison} comparisons");
                                }
                            }
                        }
                    }
                }
            }
        }

        private static double GetNumberOfDigits(int number) => Math.Floor(Math.Log10(number) + 1);

        private static IEnumerable<int> LoadInput()
        {
            return File.ReadAllLines("input.txt").Select(x => int.Parse(x));
        }
    }
}
