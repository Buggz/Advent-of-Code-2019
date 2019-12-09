using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2019._07
{
    public class OpCodeInterpreter
    {
        private List<int> _program;
        public int LastOutput;
        public int? Input;
        private int? _phase;
        private int _timesAccessedInput;

        private int _idx;

        public OpCodeInterpreter(int[] program, int? input = null, int? phase = null)
        {
            Input = input;
            _program = program.ToList();
            _phase = phase;
        }

        private int GetInput()
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
            HALT = 99
        }

        private int GetValue(int value, int mode)
        {
            if (mode == 1)
                return _program[value];
            return _program[_program[value]];
        }

        public bool Run()
        {
            while (true)
            {
                var instruction = (Instructions)(_program[_idx] % 100);
                var modes = GetModes(_program[_idx]);
                switch (instruction)
                {
                    case Instructions.Add:
                        _program[_program[_idx + 3]] = GetValue(_idx + 1, modes[0]) + GetValue(_idx + 2, modes[1]);
                        _idx += 4;
                        break;
                    
                    case Instructions.Multiply:
                        _program[_program[_idx + 3]] = GetValue(_idx + 1, modes[0]) * GetValue(_idx + 2, modes[1]);
                        _idx += 4;
                        break;
                    
                    case Instructions.Input:
                        if (!CanGetInput())
                            return false;
                        _program[_program[_idx + 1]] = GetInput();
                        _idx += 2;
                        break;
                        
                    case Instructions.Output:
                        LastOutput = GetValue(_idx + 1, modes[0]);
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
                            _program[_program[_idx + 3]] = 1;
                        else
                            _program[_program[_idx + 3]] = 0;
                        _idx += 4;
                        break;
                    
                    case Instructions.EQ:
                        if (GetValue(_idx + 1, modes[0]) == GetValue(_idx + 2, modes[1]))
                            _program[_program[_idx + 3]] = 1;
                        else
                            _program[_program[_idx + 3]] = 0;
                        _idx += 4;
                        break;
                    
                    case Instructions.HALT:
                        return true;
                }
            }
        }

        public static int[] GetModes(int i)
        {
            return new[]
            {
                (i / 100) % 10 == 1 ? 1 : 0,
                (i / 1000) % 10 == 1 ? 1 : 0,
                (i / 10000) % 10 == 1 ? 1 : 0,
            };
        }
    }
}