using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2019._07
{
    public class OpCodeInterpreter
    {
        private readonly Dictionary<long, long> _program;
        public readonly List<long> AllOutputs = new List<long>();

        public long LastOutput => AllOutputs.Last();

        public long? Input;
        private int? _phase;
        private int _timesAccessedInput;

        private long _idx;
        private long _relativeBaseOffset;

        public OpCodeInterpreter(long[] program, int? input = null, int? phase = null)
        {
            Input = input;
            _program = program.Select((value, index) => new{Key = (long)index, Value = value}).ToDictionary(o => o.Key, o => o.Value);
            _phase = phase;
        }

        public OpCodeInterpreter(int[] program, int? input = null, int? phase = null)
        {
            Input = input;
            _program = program.Select((value, index) => new{Key = (long)index, Value = (long)value}).ToDictionary(o => o.Key, o => o.Value);
            _phase = phase;
        }

        private long GetInput()
        {
            if (_phase.HasValue && _timesAccessedInput++ == 0)
                return _phase.Value;

            var val = Input.Value;
            Input = null;
            return val;
        }

        private bool CanGetInput()
        {
            return _timesAccessedInput == 0 || Input.HasValue;
        }

        public enum Instructions
        {
            None,
            Add,
            Multiply,
            Input,
            Output,
            JIT,
            JIF,
            LT,
            EQ,
            RBO,
            HALT = 99
        }

        private long GetValue(long index, int mode = 1)
        {
            if (mode == 2)
                index = GetValue(index) + _relativeBaseOffset;

            if (mode == 0)
                index = GetValue(index);

            if (!_program.ContainsKey(index))
                _program.Add(index, 0);
            
            return _program[index];
        }

        private void SetValue(long index, long value)
        {
            if (!_program.ContainsKey(index))
                _program.Add(index, value);
            else
                _program[index] = value;
        }

        public bool Run()
        {
            while (true)
            {
                var instruction = (Instructions)(_program[_idx] % 100);
                var modes = GetModes(_idx);

                long index, newValue;
                switch (instruction)
                {
                    case Instructions.Add:
                        index = GetValue(_idx + 3);
                        if (modes[2] == 2)
                            index += _relativeBaseOffset;
                        newValue = GetValue(_idx + 1, modes[0]) + GetValue(_idx + 2, modes[1]);
                        SetValue(index, newValue);
                        _idx += 4;
                        break;
                    
                    case Instructions.Multiply:
                        index = GetValue(_idx + 3);
                        if (modes[2] == 2)
                            index += _relativeBaseOffset;
                        newValue = GetValue(_idx + 1, modes[0]) * GetValue(_idx + 2, modes[1]);
                        SetValue(index, newValue);
                        _idx += 4;
                        break;
                    
                    case Instructions.Input:
                        if (!CanGetInput())
                            return false;
                        
                        index = _program[_idx + 1];
                        if (modes[0] == 2)
                            index += _relativeBaseOffset;
                        SetValue(index, GetInput());
                        _idx += 2;
                        break;
                        
                    case Instructions.Output:
                        AllOutputs.Add(GetValue(_idx + 1, modes[0]));
                        _idx += 2;
                        break;
                    
                    case Instructions.JIT:
                        if (GetValue(_idx + 1, modes[0]) != 0)
                        {
                            _idx = GetValue(_idx + 2, modes[1]);
                        }
                        else
                        {
                            _idx += 3;
                        }
                        break;
                    
                    case Instructions.JIF:
                        if (GetValue(_idx + 1, modes[0]) == 0)
                        {
                            _idx = GetValue(_idx + 2, modes[1]);
                        }
                        else
                        {
                            _idx += 3;
                        }
                        break;
                    
                    case Instructions.LT:
                        if (GetValue(_idx + 1, modes[0]) < GetValue(_idx + 2, modes[1]))
                            newValue = 1;
                        else
                            newValue = 0;
                        index = GetValue(_idx + 3);
                        if (modes[2] == 2)
                            index += _relativeBaseOffset;
                        SetValue(index, newValue);
                        _idx += 4;
                        break;
                    
                    case Instructions.EQ:
                        if (GetValue(_idx + 1, modes[0]) == GetValue(_idx + 2, modes[1]))
                            newValue = 1;
                        else
                            newValue = 0;
                        index = GetValue(_idx + 3);
                        if (modes[2] == 2)
                            index += _relativeBaseOffset;
                        SetValue(index, newValue);
                        _idx += 4;
                        break;
                    
                    case Instructions.RBO:
                        _relativeBaseOffset += GetValue(_idx + 1, modes[0]);
                        _idx += 2;
                        break;
                    
                    case Instructions.HALT:
                        return true;
                }
            }
        }

        private int[] GetModes(long idx)
        {
            var i = (int)_program[idx]; 
            
            return new[]
            {
                (i / 100) % 10,
                (i / 1000) % 10,
                (i / 10000) % 10,
            };
        }
    }
}