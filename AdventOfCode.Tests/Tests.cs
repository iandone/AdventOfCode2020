using System.Collections.Generic;
using NUnit.Framework;

namespace AdventOfCode.Tests
{
    public class Tests
    {
        private static readonly IDictionary<int, string> _input = new Dictionary<int, string>()
        {
            { 1, "..##......."},
            { 2, "#...#...#.."},
            { 3, ".#....#..#."},
            { 4, "..#.#...#.#"},
            { 5, ".#...##..#."},
            { 6, "..#.##....."},
            { 7, ".#.#.#....#"},
            { 8, ".#........#"},
            { 9, "#.##...#..."},
            {10, "#...##....#"},
            {11, ".#..#...#.#"}
        };

        private readonly Grid _grid = new Grid(_input);

        [TestCase(1, 1, 2)]
        [TestCase(3, 1, 7)]
        [TestCase(5, 1, 3)]
        [TestCase(7, 1, 4)]
        [TestCase(1, 2, 2)]
        public void TraverseReturnsCorrectNumberOfTrees(int right, int down, int expected)
        {
            Assert.Equals(expected, _grid.Traverse(right, down));
        }
    }
}
