using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static int MaxNumberOfDigits = 4;

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
                    if (list.ElementAt(j).Length <= MaxNumberOfDigits - list.ElementAt(i).Length + 3)
                    {
                        int b = int.Parse(list.ElementAt(j));

                        comparisonCount++;

                        if (a + b == 2020)
                        {
                            Console.WriteLine($"We found pair {a} + {b} = 2020, multiplied {a * b}, after {comparisonCount} comparisons");
                        }
                    }
                }
            }
        }
    }
}
