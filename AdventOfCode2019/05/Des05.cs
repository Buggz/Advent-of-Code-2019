using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019._05
{
    public class Des05
    {
        public static List<int> First(List<int> list, int input)
        {
            Globals.List = list;
            Globals.Input = input;
            
            for (var i = 0; i < list.Count;)
            {
                try
                {
                    var instruction = Instruction.Parse(Globals.List.Skip(i).ToList());
                    if (instruction is Halt)
                        break;
                    instruction.Run();
                    i += instruction.ParameterCount + 1;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            
            return Globals.Output;
        }

        public static List<int> Second(List<int> list, int input)
        {
            Globals.List = list;
            Globals.Input = input;
            
            for (var i = 0; i < list.Count;)
            {
                try
                {
                    var instruction = Instruction.Parse(Globals.List.Skip(i).ToList());
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
            
            return Globals.Output;
        }
    }
}