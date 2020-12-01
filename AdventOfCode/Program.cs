using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static int MaxNumberOfDigits = 4; // 2020

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! This is Alina's Advent of Code console application");

            string input = File.ReadAllText("input.txt");

            var list = input.Split(null).Where(x => !string.IsNullOrEmpty(x));

            int comparisonCount = 0;

            for (int i = 0; i < list.Count(); i++)
            {
                int a = int.Parse(list.ElementAt(i));

                for (int j = 0; j < list.Count() && j != i; j++)
                {
                    int b = int.Parse(list.ElementAt(j));

                    for (int k = 0; k < list.Count() && k != i && k != j; k++)
                    {
                        if (list.ElementAt(k).Length <= MaxNumberOfDigits - (a + b).ToString().Length + 3)
                        {
                            int c = int.Parse(list.ElementAt(k));

                            comparisonCount++;

                            if (a + b + c == 2020)
                            {
                                Console.WriteLine(
                                    $"We found combination {a} + {b} + {c} = 2020, multiplied {a * b * c}, after {comparisonCount} comparisons");
                            }
                        }
                    }
                }
            }
        }
    }
}
