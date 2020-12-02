using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Rule
    {
        private readonly string _positioning;
        private readonly string _password;

        public Rule(string input)
        {
            string[] ruleParts = input.Split(":");
            _positioning = ruleParts[0];
            _password = ruleParts[1];
        }

        private int RuleCharacter => _positioning[^1];

        private IEnumerable<int> Positioning => Regex.Split(_positioning, "\\D+").Where(p => !string.IsNullOrEmpty(p)).Select(p => int.Parse(p));

        private bool Compare(int position) => _password.ToCharArray()[position] == RuleCharacter;

        public bool IsValid() => Positioning.Sum(position => Compare(position) ? 1 : 0) == 1;
    }
}
