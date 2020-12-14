using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        private static Matrix _testMatrix;
        private static Matrix _matrix;

        static void Main()
        {
            Console.WriteLine("Hello World! This is Li's Advent of Code console app");

             // P1
            _testMatrix = Load("input-test.txt");
            _matrix = Load("input.txt");

            _testMatrix.Stabilise(deepView:false);
            _matrix.Stabilise(deepView:false);

            Console.WriteLine($"Test 1: ({_testMatrix.OccupiedSeats()})"); //37
            Console.WriteLine($"Part 1: ({_matrix.OccupiedSeats()})"); // 2289

            // P2
            _testMatrix = Load("input-test.txt");
            _matrix = Load("input.txt");

            _testMatrix.Stabilise(deepView:true);
            _matrix.Stabilise(deepView:true);

            Console.WriteLine($"Test 2: ({_testMatrix.OccupiedSeats()})"); // 26
            Console.WriteLine($"Part 2: ({_matrix.OccupiedSeats()})"); // 2059
        }

        private static Matrix Load(string filename)
        {
            string[] input = File.ReadAllLines(filename);

            char[,] contents = new char[input.Length, input[0].Length];

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    contents[i, j] = input[i][j];
                }
            }

            return new Matrix(contents);
        }
    }

#pragma warning disable CS0659
    public class Matrix
#pragma warning restore CS0659
    {
        private int Rows => Contents.GetLength(0);
        private int Cols => Contents.GetLength(1);

        private char[,] Contents;

        public Matrix(char[,] contents)
        {
            Contents = contents;
        }

        public void Stabilise(bool deepView = false, bool printStep = false)
        {
            char[,] pos;
            do
            {
                if (printStep) { Display(); }
                pos = Contents;
                OccupySeats(deepView);
                if (printStep) { Display(); }
                RearrangeSeats(deepView);
            } while (!Equals(pos));
        }

        public int OccupiedSeats()
        {
            int sum = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (HelperFunctions.IsSeatOccupied(Contents[i, j]))
                    {
                        sum++;
                    }
                }
            }

            return sum;
        }

        public void Display() => HelperFunctions.Display(Contents);

        public override bool Equals(object obj)
        {
            if (!(obj is char[,] pos) || Contents.GetLength(0) != pos.GetLength(0) || Contents.GetLength(1) != pos.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (Contents[i, j] != pos[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void OccupySeats(bool deepView = false) => Contents = Occupy(deepView);
        private void RearrangeSeats(bool deepView = false) => Contents = Rearrange(deepView);

        /*
         * If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
         */
        private char[,] Occupy(bool deepView = false)
        {
            char[,] positions = new char[Rows, Cols];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (HelperFunctions.IsSeatEmpty(Contents[i, j]) && !HasOccupiedAdjacent(i, j, deepView))
                    {
                        positions[i, j] = '#';
                    }
                    else
                    {
                        positions[i, j] = Contents[i, j];
                    }
                }
            }
            return positions;
        }

        /*
         * If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
         */
        private char[,] Rearrange(bool deepView = false)
        {
            char[,] positions = new char[Rows, Cols];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (HelperFunctions.IsSeatOccupied(Contents[i, j]) && HasOccupiedSeatsAdjacent(i, j, deepView))
                    {
                        positions[i, j] = 'L';
                    }
                    else
                    {
                        positions[i, j] = Contents[i, j];
                    }
                }
            }
            return positions;
        }

        private List<char> GetSurroundings(int i, int j)
        {
            var positions = new List<char>();

            if (i > 0)
            {
                if (j > 0)
                {
                    positions.Add(Contents[i - 1, j - 1]);
                }
                positions.Add(Contents[i - 1, j]);
                if (j < Cols - 1)
                {
                    positions.Add(Contents[i - 1, j + 1]);
                }
            }

            if (j > 0)
            {
                positions.Add(Contents[i, j - 1]);
            }

            if (j < Cols - 1)
            {
                positions.Add(Contents[i, j + 1]);
            }

            if (i < Rows - 1)
            {
                if (j > 0)
                {
                    positions.Add(Contents[i + 1, j - 1]);
                }

                positions.Add(Contents[i + 1, j]);

                if (j < Cols - 1)
                {
                    positions.Add(Contents[i + 1, j + 1]);
                }
            }

            return positions;
        }

        private List<char> GetDeepSurroundings(int i, int j)
        {
            var positions = new List<char>();

            int column, row;

            row = i - 1;
            column = j;
            while (row >= 0)
            {
                if (!HelperFunctions.IsFloor(Contents[row, column]))
                {
                    positions.Add(Contents[row, column]);
                    break;
                }
                row--;
            }

            row = i + 1;
            column = j;
            while (row < Rows)
            {
                if (!HelperFunctions.IsFloor(Contents[row, column]))
                {
                    positions.Add(Contents[row, column]);
                    break;
                }
                row++;
            }

            row = i;
            column = j - 1;
            while (column >= 0)
            {
                if (!HelperFunctions.IsFloor(Contents[row, column]))
                {
                    positions.Add(Contents[row, column]);
                    break;
                }
                column--;
            }

            row = i;
            column = j + 1;
            while (column < Cols)
            {
                if (!HelperFunctions.IsFloor(Contents[row, column]))
                {
                    positions.Add(Contents[row, column]);
                    break;
                }
                column++;
            }

            row = i - 1;
            column = j - 1;
            while (row >= 0 && column >= 0)
            {
                if (!HelperFunctions.IsFloor(Contents[row, column]))
                {
                    positions.Add(Contents[row, column]);
                    break;
                }
                row--;
                column--;
            }

            row = i + 1;
            column = j + 1;
            while (row < Rows && column < Cols)
            {
                if (!HelperFunctions.IsFloor(Contents[row, column]))
                {
                    positions.Add(Contents[row, column]);
                    break;
                } 
                row++;
                column++;
            }

            row = i + 1;
            column = j - 1;
            while (row < Rows && column >= 0)
            {
                if (!HelperFunctions.IsFloor(Contents[row, column]))
                {
                    positions.Add(Contents[row, column]);
                    break;
                }
                row++;
                column--;
            }

            row = i - 1;
            column = j + 1;
            while (row >= 0 && column < Cols)
            {
                if (!HelperFunctions.IsFloor(Contents[row, column]))
                {
                    positions.Add(Contents[row, column]);
                    break;
                }
                row--;
                column++;
            }

            return positions;
        }

        private bool HasOccupiedAdjacent(int i, int j, bool deepView = false) =>
            deepView ?
                GetDeepSurroundings(i, j).Any(p => HelperFunctions.IsSeatOccupied(p)) :
                GetSurroundings(i, j).Any(p => HelperFunctions.IsSeatOccupied(p));

        private bool HasOccupiedSeatsAdjacent(int i, int j, bool deepView = false) =>
            deepView ?
                GetDeepSurroundings(i, j).Sum(p => HelperFunctions.IsSeatOccupied(p) ? 1 : 0) >= 5 : 
                GetSurroundings(i, j).Sum(p => HelperFunctions.IsSeatOccupied(p) ? 1 : 0) >= 4;
    }

    static class HelperFunctions
    {
        public static bool IsSeatOccupied(char s) => s == '#';
        public static bool IsSeatEmpty(char c) => c == 'L';
        public static bool IsFloor(char c) => c == '.';

        public static void Display(char[,] contents)
        {
            Console.WriteLine();
            for (int i = 0; i < contents.GetLength(0); i++)
            {
                for (int j = 0; j < contents.GetLength(1); j++)
                {
                    Console.Write($"{contents[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}