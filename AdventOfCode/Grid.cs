using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Grid
    {
        private readonly int _depth;
        private readonly int _length;
        private readonly IDictionary<int, string> _map;

        public Grid(IDictionary<int, string> input)
        {
            _depth = input.Count;
            _length = input.First().Value.Length;
            _map = input;
        }

        private bool IsTree(int depth, int position) => _map[depth].ToCharArray()[position] == '#';

        public int Traverse(int right, int down)
        {
            int numberOfTrees = 0;

            int currentDepth = 1;
            int currentPosition = 0;

            do
            {
                currentDepth += down;
                currentPosition += right;

                if (currentPosition >= _length)
                {
                    currentPosition -= _length;
                }

                numberOfTrees += IsTree(currentDepth, currentPosition) ? 1 : 0;
            } while (currentDepth < _depth - down + 1);

            return numberOfTrees;
        }
    }
}
