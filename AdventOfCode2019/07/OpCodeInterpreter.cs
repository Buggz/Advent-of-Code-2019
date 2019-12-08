using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019._07
{
    public class OpCodeInterpreter
    {
        private readonly Action<int> _output;
        private List<int> _program;
        private int _lastOutput;
        private int _input;
        private int? _phase;
        private int _timesAccessedInput;

        public OpCodeInterpreter(int[] program, Action<int> output = null)
        {
            _output = output;
            _program = program.ToList();
        }

        private int GetInput()
        {
            if (_phase.HasValue && _timesAccessedInput++ == 0)
                return _phase.Value;

            return _input;
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

        public int Run(int input, int? phase = null)
        {
            _input = input;
            _phase = phase;
            
            for (var i = 0;;)
            {
                var instruction = (Instructions)(_program[i] % 100);
                var modes = GetModes(_program[i]);
                switch (instruction)
                {
                    case Instructions.Add:
                        _program[_program[i + 3]] = GetValue(i + 1, modes[0]) + GetValue(i + 2, modes[1]);
                        i += 4;
                        break;
                    
                    case Instructions.Multiply:
                        _program[_program[i + 3]] = GetValue(i + 1, modes[0]) * GetValue(i + 2, modes[1]);
                        i += 4;
                        break;
                    
                    case Instructions.Input:
                        _program[_program[i + 1]] = GetInput();
                        i += 2;
                        break;
                        
                    case Instructions.Output:
                        _lastOutput = GetValue(i + 1, modes[0]);
                        i += 2;
                        break;
                    
                    case Instructions.JIT:
                        if (GetValue(i + 1, modes[0]) != 0)
                        {
                            i = GetValue(i + 2, modes[1]);
                        }
                        else
                        {
                            i += 3;
                        }
                        break;
                    
                    case Instructions.JIF:
                        if (GetValue(i + 1, modes[0]) == 0)
                        {
                            i = GetValue(i + 2, modes[1]);
                        }
                        else
                        {
                            i += 3;
                        }
                        break;
                    
                    case Instructions.LT:
                        if (GetValue(i + 1, modes[0]) < GetValue(i + 2, modes[1]))
                            _program[_program[i + 3]] = 1;
                        else
                            _program[_program[i + 3]] = 0;
                        i += 4;
                        break;
                    
                    case Instructions.EQ:
                        if (GetValue(i + 1, modes[0]) == GetValue(i + 2, modes[1]))
                            _program[_program[i + 3]] = 1;
                        else
                            _program[_program[i + 3]] = 0;
                        i += 4;
                        break;
                    
                    case Instructions.HALT:
                        return _lastOutput;
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