﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        private static IList<Instruction> _instructions = LoadInstructions();

        static void Main()
        {
            Console.WriteLine("Hello World! This is Li's Advent of Code console app");

            FixInfiniteLoop();

            Console.WriteLine($"P2 = {Execute()}");
        }

        private static IList<Instruction> LoadInstructions()
        {
            return File.ReadAllLines("input.txt").Select(i => new Instruction(i)).ToList();
        }

        public static int Execute()
        {
            int accumulator = 0;
            int iterations = 0;
            int index = 0;

            while (index < _instructions.Count && iterations++ < _instructions.Count * 2)
            {
                var currentInstruction = _instructions.ElementAt(index);

                switch (currentInstruction.Move)
                {
                    case "acc":
                        accumulator += currentInstruction.Step;
                        break;
                    case "jmp":
                        index += currentInstruction.Step - 1;
                        break;
                }
                index++;
            }

            return index == _instructions.Count ? accumulator : 0;
        }

        public static void FixInfiniteLoop()
        {
            foreach (var instruction in _instructions)
            {
                var currentInstruction = instruction;

                if (new[] {"nop", "jmp"}.Contains(currentInstruction.Move))
                {
                    instruction.Switch();

                    if (Execute() > 0)
                    {
                        break;
                    }
                    else
                    {
                        instruction.Switch();
                    }
                }
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