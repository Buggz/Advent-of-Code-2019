using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019._04
{
    public class Password
    {
        private readonly int _input;
        private Dictionary<int, int> _repeatedDigits = new Dictionary<int, int>();

        public Password(int input)
        {
            _input = input;
        }

        public bool IsValid()
        {
            return IsIncreasing() && HasDoubleDigit();
        }

        private bool HasDoubleDigit()
        {
            return _repeatedDigits.Keys.Any(x => x > 1);
        }

        public bool IsValid2()
        {
            return IsIncreasing() && HasAnExactDoubleDigit();
        }

        private bool HasAnExactDoubleDigit()
        {
            return _repeatedDigits.Any(x => x.Value == 2);
        }

        private bool IsIncreasing()
        {
            var splitNumber = _input.ToString().ToCharArray();

            for (var i = 1; i < splitNumber.Length; i++)
            {
                if (splitNumber[i] == splitNumber[i - 1])
                {
                    var num = splitNumber[i];
                    if (_repeatedDigits.ContainsKey(num))
                        _repeatedDigits[num]++;
                    else
                        _repeatedDigits.Add(num, 2);
                }
                    
                if (splitNumber[i] < splitNumber[i - 1])
                    return false;
            }
            
            return true;
        }
    }
}