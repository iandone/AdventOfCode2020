using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World! This is Li's Advent of Code console app");

            Answer(LoadBoardingPasses());
        }

        private static void Answer(IList<BoardingPass> boardingPasses)
        {
            var seats = boardingPasses.Select(pass => pass.SeatId);

            int id = boardingPasses.Min(pass => pass.SeatId);

            int maxId = boardingPasses.Max(pass => pass.SeatId);
            Console.WriteLine($"P1. Max Seat Id: {boardingPasses.Max(pass => pass.SeatId)}");

            do
            {
                if (!seats.Contains(id) && seats.Contains(id - 1) && seats.Contains(id + 1))
                {
                    break;
                }

                id++;
            } while (id < boardingPasses.Max(pass => pass.SeatId));

            Console.WriteLine($"P2. My Seat Id: {id}");
        }

        private static IList<BoardingPass> LoadBoardingPasses()
        {
            return File.ReadAllLines("input.txt").Select(b => new BoardingPass(b)).ToList();
        }
    }

    public class BoardingPass
    {
        private readonly int _aisle;
        private readonly int _column;

        public BoardingPass(string input)
        {
            var matches = new Regex(@"(B|F)").Matches(input);
            _aisle = SetAisle(input[..matches.Count]);
            _column = SetColumn(input[matches.Count..]);
        }

        private int SetAisle(string description) => GetValue(description: description, new Tuple<char, char>('F', 'B'), new List<int>() { 0, 127}, takeMax: false);

        private int SetColumn(string description) => GetValue(description: description, new Tuple<char, char>('L', 'R'), new List<int>() {0, 7}, takeMax: true);

        private int GetValue(string description, Tuple<char,char> moves, List<int> boundaries, bool takeMax)
        {
            if (string.IsNullOrEmpty(description))
            {
                return takeMax ? boundaries.Max() : boundaries.Min();
            }

            int step = (int) Math.Floor((boundaries.Max() - boundaries.Min() + 1) / 2d);

            boundaries = description[0] == moves.Item1
                ? new List<int>() {boundaries.Min(), boundaries.Max() - step}
                : new List<int>() {boundaries.Min() + step, boundaries.Max()};

            return GetValue(description[1..], moves, boundaries, takeMax);
        }

        public int SeatId => (_aisle * 8) + _column;
    }
}