using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Rules
    {
        public static List<Rule> Definitions = new List<Rule>()
        {
            new NumericRule(name:"byr", 4, 1920, 2002),
            new NumericRule(name:"iyr", 4, 2010, 2020),
            new NumericRule(name:"eyr", 4, 2020, 2030),
            new HeightRule(new List<(int, int)>{(150, 193), (59, 76)}),
            new HairColourRule(),
            new EyeColourRule(),
            new PassportIdRule(),
            new CountryIdRule()
        };

        public static bool Evaluate(string fieldName, string fieldValue) =>
            Definitions.Where(d => d.Name.Equals(fieldName)).Single().Check(fieldValue);
    }

    public abstract class Rule
    {
        public readonly string Name;

        public readonly bool IsMandatory;

        public Rule(string name, bool isMandatory)
        {
            Name = name;
            IsMandatory = isMandatory;
        }

        public virtual bool Check(string value) => false;
    }

    public class NumericRule : Rule
    {
        private readonly int _numberOfDigits;

        private readonly int _minValue;

        private readonly int _maxValue;

        public NumericRule(string name, int numberOfDigits, int minValue, int maxValue) : base(name, true)
        {
            _numberOfDigits = numberOfDigits;
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public override bool Check(string value) => value.Length == _numberOfDigits && int.Parse(value) >= _minValue && int.Parse(value) <= _maxValue;
    }
 
    public class HeightRule : Rule
    {
        private readonly List<(int, int)> _ranges;

        public HeightRule(List<(int, int)> ranges) : base("hgt", true)
        {
            _ranges = ranges;
        }

        public override bool Check(string value)
        {
            if (value.Contains("cm")) 
            {
                return int.Parse(value.Split("cm")[0]) >= _ranges[0].Item1 && long.Parse(value.Split("cm")[0]) <= _ranges[0].Item2;
            } 
            else if (value.Contains("in"))
            {
                return int.Parse(value.Split("in")[0]) >= _ranges[1].Item1 && long.Parse(value.Split("in")[0]) <= _ranges[1].Item2;
            }
            return false;
        }
    }

    public class HairColourRule : Rule
    {
        public HairColourRule() : base("hcl", true)
        {
        }

        public override bool Check(string value)
        {
            if (value.Length == 7 && value.StartsWith("#"))
            {
                return new Regex(@"[0-9a-f]{6}", RegexOptions.IgnoreCase).IsMatch(value[1..]);
            }

            return false;
        }
    }

    public class PassportIdRule : Rule
    {
        public PassportIdRule() : base("pid", true)
        {
        }

        public override bool Check(string value)
        {
            if (value.Length == 9)
            {
                return new Regex(@"[0-9]{9}", RegexOptions.IgnoreCase).IsMatch(value);
            }

            return false;
        }
    }

    public class EyeColourRule : Rule
    {
        public EyeColourRule() : base("ecl", true)
        {
        }

        public override bool Check(string value)
        {
            return new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(value);
        }
    }

    public class CountryIdRule : Rule
    {
        public CountryIdRule() : base("cid", false)
        {
        }

        public override bool Check(string value) => true;
    }
}
