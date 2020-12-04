using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Passport
    {
        private readonly Dictionary<string, string> _fields;

        public Passport(string passportFields)
        {
            _fields = passportFields.Split(new char[0], StringSplitOptions.RemoveEmptyEntries)
                .Where(f => f.Contains(":"))
                .ToDictionary(k => k.Split(":")[0], v => v.Split(":")[1]);
        }

        private int MinNumOfRulesToApply => _fields.ContainsKey("cid") ? Rules.Definitions.Count : Rules.Definitions.Count - 1;

        public bool IsValid()
        {
            if (_fields.Count < MinNumOfRulesToApply)
            {
                return false;
            }

            return _fields.Sum(f => Rules.Evaluate(f.Key, f.Value) ? 1 : 0) == MinNumOfRulesToApply;
        }
    }
}
