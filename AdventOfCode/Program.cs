using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    class Program
    {
        private static readonly Grid _grid = new Grid(Load());

        static void Main()
        {
            Console.WriteLine("Hello World! This is Li's Advent of Code console app");

            var results = new List<int>
            {
                GetResults(1, 1),
                GetResults(3, 1),
                GetResults(5, 1),
                GetResults(7, 1),
                GetResults(1, 2)
            };

            long product = 1;
            foreach (var result in results)
            {
                product *= result;
            }

            Console.WriteLine($"\nProduct result = {product}");
        }

        public static int GetResults(int right, int down)
        {
            int result = _grid.Traverse(right, down);
            Console.WriteLine($"Result for ({right},{down}): {result}");
            return result;
        }

        private static IDictionary<int, string> Load()
        {
            var dictionary = new Dictionary<int, string>();

            var file = new StreamReader("input.txt");
            string line;
            int index = 1;

            Console.WriteLine("Input:");
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine($"{index,3}: {line}");
                dictionary.Add(index, line);
                index++;
            }

            Console.WriteLine();

            file.Close();

            return dictionary;
        }
    }
}