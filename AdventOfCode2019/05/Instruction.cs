using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace AdventOfCode2019._05
{
    public enum Modes
    {
        Position,
        Immediate
    }

    public class Parameter
    {
        public int Value { private get; set; }
        public Modes Mode = Modes.Position;

        public int GetValue()
        {
            if (Mode == Modes.Immediate)
                return Value;

            return Globals.List[Value];
        }
    }

    public static class Globals
    {
        public static List<int> List;
        public static int Input;
        public static List<int> Output = new List<int>();
    }
    
    public abstract class Instruction
    {
        public List<Parameter> Parameters { get; }
        public abstract int ParameterCount { get; }
        public abstract void Run();

        protected Instruction(List<int> list)
        {
            Parameters = list.Skip(1).Take(ParameterCount).Select(x => new Parameter(){Value = x}).ToList();
            var opCode = list.First();

            if (((opCode % 1000) / 100) > 0)
                Parameters[0].Mode = Modes.Immediate;
            if (((opCode % 10000) / 1000) > 0)
                Parameters[1].Mode = Modes.Immediate;
//            if ((opCode / 10000) > 0)
//                Parameters[2].Mode = Modes.Immediate;
        }
        
        private static readonly Dictionary<int, Func<List<int>, Instruction>> OpcodeTypes = new Dictionary<int, Func<List<int>, Instruction>>()
        {
            {1, Add.Create },
            {2, Multiply.Create },
            {3, SaveValue.Create },
            {4, OutputValue.Create },
            {5, JumpIfTrue.Create },
            {6, JumpIfFalse.Create },
            {7, LessThan.Create },
            {8, Equal.Create },
        };
        
        public static Instruction Parse(List<int> list)
        {
            var opcode = list[0];

            if (opcode == 99)
                return Halt.Create(list);
            
            return OpcodeTypes[opcode % 10].Invoke(list);
        }
    }

    public class Halt : Instruction
    {
        private Halt(List<int> list) : base(list)
        {
        }

        public static Instruction Create(List<int> list)
        {
            return new Halt(list);
        }

        public override int ParameterCount { get; } = 0;
        public override void Run()
        {
            
        }
    }

    public class Add : Instruction
    {
        private Add(List<int> list): base(list) { }
        
        public sealed override int ParameterCount { get; } = 3;
        public static Instruction Create(List<int> list)
        {
            return new Add(list);
        }

        public override void Run()
        {
            var index = Parameters[2].GetValue();
            Globals.List[index] = Parameters[0].GetValue() + Parameters[1].GetValue();
        }
    }
    
    public class Multiply : Instruction
    {
        private Multiply(List<int> list): base(list) { }
        
        public override int ParameterCount { get; } = 3;

        public static Instruction Create(List<int> list)
        {
            return new Multiply(list);
        }

        public override void Run()
        {
            var index = Parameters[2].GetValue();
            Globals.List[index] = Parameters[0].GetValue() * Parameters[1].GetValue();
        }
    }

    public class SaveValue : Instruction
    {
        private SaveValue(List<int> list): base(list){}
        
        public override int ParameterCount { get; } = 1;
        public static Instruction Create(List<int> list)
        {
            return new SaveValue(list);
        }

        public override void Run()
        {
            var index = Parameters[0].GetValue();
            Globals.List[index] = Globals.Input;
        }
    }

    public class OutputValue : Instruction
    {
        private OutputValue(List<int> list): base(list) { }
        
        public override int ParameterCount { get; } = 1;
        public static Instruction Create(List<int> list)
        {
            return new OutputValue(list);
        }

        public override void Run()
        {
            Globals.Output.Add(Parameters[0].GetValue());
        }
    }

    public class JumpIfTrue : Instruction, IMayJump
    {
        private int _jumpTo;
        private bool _shouldJump;
        public JumpIfTrue(List<int> list) : base(list)
        {
        }

        public override int ParameterCount { get; } = 2;
        public override void Run()
        {
            if (Parameters[0].GetValue() != 0)
            {
                _jumpTo = Parameters[1].GetValue();
                _shouldJump = true;
            }
        }

        int IMayJump.JumpTo()
        {
            return _jumpTo;
        }

        bool IMayJump.ShouldJump()
        {
            return _shouldJump;
        }

        public static Instruction Create(List<int> list)
        {
            return new JumpIfTrue(list);
        }
    }

    public interface IMayJump
    {
        int JumpTo();
        bool ShouldJump();
    }

    public class JumpIfFalse : Instruction, IMayJump
    {
        private int _jumpTo;
        private bool _shouldJump;
        
        public JumpIfFalse(List<int> list) : base(list)
        {
        }

        public override int ParameterCount { get; } = 2;
        public override void Run()
        {
            if (Parameters[0].GetValue() == 0)
            {
                _jumpTo = Parameters[1].GetValue();
                _shouldJump = true;
            }
        }

        public int JumpTo()
        {
            return _jumpTo;
        }

        public bool ShouldJump()
        {
            return _shouldJump;
        }

        public static Instruction Create(List<int> list)
        {
            return new JumpIfFalse(list);
        }
    }
    
    public class LessThan : Instruction
    {
        public LessThan(List<int> list) : base(list)
        {
        }

        public override int ParameterCount { get; } = 3;
        public override void Run()
        {
            if (Parameters[0].GetValue() < Parameters[1].GetValue())
            {
                Globals.List[Parameters[2].GetValue()] = 1;
            }
            else
            {
                Globals.List[Parameters[2].GetValue()] = 0;
            }
        }

        public static Instruction Create(List<int> list)
        {
            return new LessThan(list);
        }
    }

    public class Equal : Instruction
    {
        public Equal(List<int> list) : base(list)
        {
        }

        public override int ParameterCount { get; } = 3;
        public override void Run()
        {
            if (Parameters[0].GetValue() == Parameters[1].GetValue())
            {
                Globals.List[Parameters[2].GetValue()] = 1;
            }
            else
            {
                Globals.List[Parameters[2].GetValue()] = 0;
            }
        }

        public static Instruction Create(List<int> list)
        {
            return new Equal(list);
        }
    }
}