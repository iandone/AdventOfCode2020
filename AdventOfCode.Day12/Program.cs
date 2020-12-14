using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day12
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World! This is Li's Advent of Code console app");

            // P1
            var testInstructions = Load("input-test.txt");
            var instructions = Load("input.txt");

            var testFerry = new Ferry(testInstructions);
            var realFerry = new Ferry(instructions);

            testFerry.Move();
            realFerry.Move();

            Console.WriteLine($"Test 1: ({testFerry.ManhattanDistance()})"); //25
            Console.WriteLine($"Part 1: ({realFerry.ManhattanDistance()})"); // 1589

            // P2
            testInstructions = Load("input-test.txt");
            instructions = Load("input.txt");

            testFerry = new Ferry(testInstructions);
            realFerry = new Ferry(instructions);

            testFerry.MoveWithWaypoint();
            realFerry.MoveWithWaypoint();

            Console.WriteLine($"Test 2: ({testFerry.ManhattanDistance()})"); // 286
            Console.WriteLine($"Part 2: ({realFerry.ManhattanDistance()})"); // 23960
        }

        private static List<Instruction> Load(string filename)
        {
            var instructions = new List<Instruction>();
            var input = File.ReadAllLines(filename).Where(line => !string.IsNullOrEmpty(line)).ToList();

            return input.Select(i =>
                    new Instruction(new Regex("[A-Z]").Match(i).Value.ToCharArray()[0], int.Parse(new Regex("\\d+").Match(i).Value)))
                .ToList();
        }
    }

    public class Ferry
    {
        private readonly List<Instruction> Instructions;

        private static readonly Dictionary<int, char> ClockwiseDirections =
            new Dictionary<int, char>() {{1, 'N'}, {2, 'E'}, {3, 'S'}, {4, 'W'}};

        private readonly Dictionary<char, long> ShipPosition;
        private Dictionary<char, long> WaypointPosition;
        private char Direction;

        public long ManhattanDistance() => Math.Abs(ShipPosition['N'] - ShipPosition['S']) + Math.Abs(ShipPosition['W'] - ShipPosition['E']);

        public Ferry(List<Instruction> instructions)
        {
            ShipPosition = ClockwiseDirections.ToDictionary(k => k.Value, v => 0L);
            Direction = 'E';

            Instructions = instructions;

            WaypointPosition = ClockwiseDirections.ToDictionary(k => k.Value, v => 0L);

            //offset
            WaypointPosition['N'] = 1;
            WaypointPosition['E'] = 10;
        }

        public void Move()
        {
            foreach (var instruction in Instructions)
            {
                switch (instruction.Direction)
                {
                    case 'F':
                        ShipPosition[Direction] += instruction.Step;
                        break;
                    case 'L':
                    case 'R':
                        int step = instruction.Direction == 'L' ? -1 : 1;
                        int index = ClockwiseDirections.Keys.Where(k => ClockwiseDirections[k] == Direction).Single();
                        int numberOfTurns = instruction.Step / 90;
                        while (numberOfTurns > 0)
                        {
                            index+=step;
                            if (index < 1)
                            {
                                index = ClockwiseDirections.Count;
                            }
                            else if(index > ClockwiseDirections.Count)
                            {
                                index = 1;
                            }
                            Direction = ClockwiseDirections[index];
                            numberOfTurns--;
                        }
                        break;
                    default:
                        ShipPosition[instruction.Direction] += instruction.Step;
                        break;
                }
            }
        }

        public void MoveWithWaypoint()
        {
            foreach (var instruction in Instructions)
            {
                switch (instruction.Direction)
                {
                    case 'F':
                        ShipPosition['N'] += WaypointPosition['N'] * instruction.Step;
                        ShipPosition['E'] += WaypointPosition['E'] * instruction.Step;
                        ShipPosition['S'] += WaypointPosition['S'] * instruction.Step;
                        ShipPosition['W'] += WaypointPosition['W'] * instruction.Step;
                        break;
                    case 'L':
                    case 'R':
                        RotateWaypoint(instruction, step: instruction.Direction == 'L' ? -1 : 1);
                        break;
                    default:
                        WaypointPosition[instruction.Direction] += instruction.Step;
                        break;
                }
            }
        }

        private void RotateWaypoint(Instruction instruction, int step)
        {
            int index;
            var positions = new Dictionary<char, long>(WaypointPosition);
            WaypointPosition.Clear();
            foreach (var pos in positions)
            {
                index = ClockwiseDirections.Keys.Where(k => ClockwiseDirections[k] == pos.Key).Single();
                int numberOfTurns = instruction.Step / 90;
                while (numberOfTurns > 0)
                {
                    index += step;
                    if (index < 1)
                    {
                        index = ClockwiseDirections.Count;
                    }
                    else if (index > ClockwiseDirections.Count)
                    {
                        index = 1;
                    }
                    numberOfTurns--;
                }
                WaypointPosition[ClockwiseDirections[index]] = pos.Value;
            }
        }
    }

    public class Instruction
    {
        public char Direction { get; private set; }
        public int Step { get; private set; }

        public Instruction(char direction, int step)
        {
            Direction = direction;
            Step = step;
        }
    }
}