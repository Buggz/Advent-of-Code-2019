using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019._07;

namespace AdventOfCode2019._05
{
    public class Des05
    {
        public static long First(List<int> list, int input)
        {
            var cpu = new OpCodeInterpreter(list.Select(x => (long)x).ToArray(), input);
            cpu.Run();
            return cpu.LastOutput;
        }
        
        public static long Second(List<int> list, int input)
        {
            var cpu = new OpCodeInterpreter(list.Select(x => (long)x).ToArray(), input);
            cpu.Run();
            return cpu.LastOutput;
        }
    }
}