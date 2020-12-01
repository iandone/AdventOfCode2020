using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static int MaxNumberOfDigits = 4; // 2020.Length

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! This is Li's Advent of Code console application");

            var input = LoadInput();

            int comparison = 0;

            for (int i = 0; i < input.Count(); i++)
            {
                int a = int.Parse(input.ElementAt(i));

                for (int j = 0; j < input.Count() && j != i; j++)
                {
                    int b = int.Parse(input.ElementAt(j));

                    for (int k = 0; k < input.Count() && k != i && k != j; k++)
                    {
                        if (input.ElementAt(k).Length <= MaxNumberOfDigits - (a + b).ToString().Length + 3)
                        {
                            int c = int.Parse(input.ElementAt(k));

                            comparison++;

                            if (a + b + c == 2020)
                            {
                                Console.WriteLine($"We found combination {a} + {b} + {c} = 2020, multiplied {a * b * c}, after {comparison} comparisons");
                            }
                        }
                    }
                }
            }
        }

        private static IEnumerable<string> LoadInput()
        {
            return File.ReadAllText("input.txt").Split(null).Where(x => !string.IsNullOrEmpty(x));
        }
    }
}
