using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        private static long _accumulator;
        private static IList<Instruction> _instructions = LoadInstructions();

        static void Main()
        {
            Console.WriteLine("Hello World! This is Li's Advent of Code console app");

            FixInfiniteLoop();

            Console.WriteLine($"P2 = {(CanExecuteInFull() ? _accumulator : 0)}");
        }

        private static IList<Instruction> LoadInstructions()
        {
            return File.ReadAllLines("input.txt").Select(i => new Instruction(i)).ToList();
        }

        public static bool CanExecuteInFull()
        {
            _accumulator = 0;
            int iterations = 0;
            int index = 0;

            while (index < _instructions.Count && iterations++ < _instructions.Count * 2)
            {
                var currentInstruction = _instructions.ElementAt(index);

                switch (currentInstruction.Move)
                {
                    case "acc":
                        _accumulator += currentInstruction.Step;
                        break;
                    case "jmp":
                        index += currentInstruction.Step - 1;
                        break;
                }
                index++;
            }

            return index == _instructions.Count;
        }

        public static void FixInfiniteLoop()
        {
            int index = 0;

            while (index < _instructions.Count)
            {
                var currentInstruction = _instructions.ElementAt(index);

                if (new[] { "nop", "jmp" }.Contains(currentInstruction.Move))
                {
                    _instructions.ElementAt(index).Switch();

                    if (CanExecuteInFull())
                    {
                        break;
                    }
                    else
                    {
                        _instructions.ElementAt(index).Switch();
                    }
                }
                index++;
            }
        }

        public class Instruction
        {
            public string Move { get; private set; }
            public int Step { get; }
            public int NumberOfTimes { get; set; } = 0;

            public Instruction(string instruction)
            {
                Move = new Regex("[^\\s]+").Match(instruction).Value;
                Step = int.Parse(new Regex("-?\\d+").Match(instruction).Value);
            }

            public void Switch()
            {
                switch (Move)
                {
                    case "nop":
                        Move = "jmp";
                        break;
                    case "jmp":
                        Move = "nop";
                        break;
                    default: 
                        break;
                }
            }
        }
    }
}