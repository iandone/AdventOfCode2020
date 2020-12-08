using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        private static int _accumulator = 0;
        private static IList<Instruction> _instructions = LoadInstructions();

        static void Main()
        {
            Execute();
            
            Console.WriteLine("Hello World! This is Li's Advent of Code console app");

            Console.WriteLine($"P1 = {_accumulator}");
        }

        private static IList<Instruction> LoadInstructions()
        {
            return File.ReadAllLines("input.txt").Select(i => new Instruction(i)).ToList();
        }

        public static void Execute()
        {
            int index = 0;

            while (index < _instructions.Count)
            {
                var currentInstruction = _instructions.ElementAt(index);

                if (currentInstruction.HasRun)
                {
                    break;
                }

                switch (currentInstruction.Move)
                {
                    case "nop":
                        index++;
                        break;
                    case "acc":
                        _accumulator += currentInstruction.Step;
                        index++;
                        break;
                    case "jmp":
                        index += currentInstruction.Step;
                        break;
                }

                currentInstruction.HasRun = true;
            }
        }

        public class Instruction
        {
            public string Move { get; }
            public int Step { get; }
            public bool HasRun { get; set; }

        
            public Instruction(string instruction)
            {
                Move = instruction.Split(" ")[0];
                Step = int.Parse(new Regex("-?\\d+").Match(instruction).Value);
                HasRun = false;
            }
        }
    }
}