using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World! This is Li's Advent of Code console app");

            var groups = LoadGroups();

            Console.WriteLine($"P1 = {groups.Sum(g => g.NumberOfQuestionsAnswered())}");
            Console.WriteLine($"P2 = {groups.Sum(g => g.NumberOfQuestionsAnsweredByAll())}");
        }

        private static IList<Group> LoadGroups()
        {
            return File.ReadAllText("input.txt").Split("\n\n").Select(g => new Group(g)).ToList();
        }

        public class Group
        {
            private readonly List<HashSet<char>> _answers;
            private readonly HashSet<char> _combinedAnswers;

        
            public Group(string group)
            {
                _answers = group.Split("\n").Select(a => a.ToCharArray().ToHashSet()).ToList();
                _combinedAnswers = _answers.SelectMany(answer => answer).ToHashSet();
            }

            public int NumberOfQuestionsAnsweredByAll()
            {
                if (_answers.Count == 1)
                {
                    return _answers.Single().Count;
                }

                return _combinedAnswers
                    .ToDictionary(key => key, val => _answers.Sum(answer => answer.Count(c => c == val)))
                    .Count(a => a.Value == _answers.Count);
            }

            public int NumberOfQuestionsAnswered()
            {
                if (_answers.Count == 1)
                {
                    return _answers.Single().Count;
                }
                
                return _combinedAnswers.ToHashSet().Count;
            }
        }
    }
}