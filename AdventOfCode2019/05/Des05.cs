using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019._07;

namespace AdventOfCode2019._05
{
    public class Des05
    {
        public static int First(List<int> list, int input)
        {
            var cpu = new OpCodeInterpreter(list.ToArray(), input);
            cpu.Run();
            return cpu.LastOutput;
        }
        
        public static List<int> Second(List<int> list, int input)
        {
            return RunOpcodes(list, input);
        }
        
        public static List<int> RunOpcodes(List<int> list, int input, int? phaseSetting = null)
        {
            IntcodeGlobals.List = list;
            IntcodeGlobals.Input = input;
            IntcodeGlobals.PhaseSetting = phaseSetting;
            IntcodeGlobals.Output.Clear();
            
            for (var i = 0; i < list.Count;)
            {
                try
                {
                    var instruction = Instruction.Parse(IntcodeGlobals.List.Skip(i).ToList());
                    if (instruction is Halt)
                        break;
                    instruction.Run();

                    var mayJump = instruction as IMayJump;
                    if (mayJump != null && mayJump.ShouldJump())
                        i = mayJump.JumpTo();
                    else
                        i += instruction.ParameterCount + 1;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            
            return IntcodeGlobals.Output;
        }
    }
}