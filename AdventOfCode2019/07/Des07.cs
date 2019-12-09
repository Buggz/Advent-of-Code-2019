using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019._05;

namespace AdventOfCode2019._07
{
    public static class Des07
    {
        public static long First(int[] opcodeProgram)
        {
            var results = new List<long>();

            foreach (var permutation in Permutations)
            {
                results.Add(RunIntCodeComputer(opcodeProgram, permutation));
            }

            return results.Max();
        }

        public static long Second(int[] opcodeProgram)
        {
            var results = new List<long>();

            foreach (var permutation in Permutations)
            {
                results.Add(RunIntCodeComputerFeedback(opcodeProgram, permutation));
            }

            return results.Max();
        }

        private static long RunIntCodeComputerFeedback(int[] opcodeProgram, int[] permutation)
        {
            long output = 0;
            var cpus = new[]
            {
                new OpCodeInterpreter(opcodeProgram, phase: permutation[0] + 4),
                new OpCodeInterpreter(opcodeProgram, phase: permutation[1] + 4),
                new OpCodeInterpreter(opcodeProgram, phase: permutation[2] + 4),
                new OpCodeInterpreter(opcodeProgram, phase: permutation[3] + 4),
                new OpCodeInterpreter(opcodeProgram, phase: permutation[4] + 4)
            };


            for (var i = 0; i < permutation.Length; i = (i + 1) % 5 )
            {
                cpus[i].Input = output;
                var halted = cpus[i].Run();
                output = cpus[i].LastOutput;
                
                if (halted && i == 4)
                    break;
            }

            return output;
        }

        public static long RunIntCodeComputer(int[] opcodeProgram, int[] permutation)
        {
            long output = 0;
            
            for (var i = 0; i < permutation.Length; i++)
            {
                var cpu = new OpCodeInterpreter(opcodeProgram, (int)output, permutation[i] - 1);
                cpu.Run();
                output = cpu.LastOutput;
            }

            return output;
        }

        private static readonly List<int[]> Permutations = new List<int[]>
        {
            new[] {1, 2, 3, 4, 5},
            new[] {2, 1, 3, 4, 5},
            new[] {3, 1, 2, 4, 5},
            new[] {1, 3, 2, 4, 5},
            new[] {2, 3, 1, 4, 5},
            new[] {3, 2, 1, 4, 5},
            new[] {3, 2, 4, 1, 5},
            new[] {2, 3, 4, 1, 5},
            new[] {4, 3, 2, 1, 5},
            new[] {3, 4, 2, 1, 5},
            new[] {2, 4, 3, 1, 5},
            new[] {4, 2, 3, 1, 5},
            new[] {4, 1, 3, 2, 5},
            new[] {1, 4, 3, 2, 5},
            new[] {3, 4, 1, 2, 5},
            new[] {4, 3, 1, 2, 5},
            new[] {1, 3, 4, 2, 5},
            new[] {3, 1, 4, 2, 5},
            new[] {2, 1, 4, 3, 5},
            new[] {1, 2, 4, 3, 5},
            new[] {4, 2, 1, 3, 5},
            new[] {2, 4, 1, 3, 5},
            new[] {1, 4, 2, 3, 5},
            new[] {4, 1, 2, 3, 5},
            new[] {5, 1, 2, 3, 4},
            new[] {1, 5, 2, 3, 4},
            new[] {2, 5, 1, 3, 4},
            new[] {5, 2, 1, 3, 4},
            new[] {1, 2, 5, 3, 4},
            new[] {2, 1, 5, 3, 4},
            new[] {2, 1, 3, 5, 4},
            new[] {1, 2, 3, 5, 4},
            new[] {3, 2, 1, 5, 4},
            new[] {2, 3, 1, 5, 4},
            new[] {1, 3, 2, 5, 4},
            new[] {3, 1, 2, 5, 4},
            new[] {3, 5, 2, 1, 4},
            new[] {5, 3, 2, 1, 4},
            new[] {2, 3, 5, 1, 4},
            new[] {3, 2, 5, 1, 4},
            new[] {5, 2, 3, 1, 4},
            new[] {2, 5, 3, 1, 4},
            new[] {1, 5, 3, 2, 4},
            new[] {5, 1, 3, 2, 4},
            new[] {3, 1, 5, 2, 4},
            new[] {1, 3, 5, 2, 4},
            new[] {5, 3, 1, 2, 4},
            new[] {3, 5, 1, 2, 4},
            new[] {4, 5, 1, 2, 3},
            new[] {5, 4, 1, 2, 3},
            new[] {1, 4, 5, 2, 3},
            new[] {4, 1, 5, 2, 3},
            new[] {5, 1, 4, 2, 3},
            new[] {1, 5, 4, 2, 3},
            new[] {1, 5, 2, 4, 3},
            new[] {5, 1, 2, 4, 3},
            new[] {2, 1, 5, 4, 3},
            new[] {1, 2, 5, 4, 3},
            new[] {5, 2, 1, 4, 3},
            new[] {2, 5, 1, 4, 3},
            new[] {2, 4, 1, 5, 3},
            new[] {4, 2, 1, 5, 3},
            new[] {1, 2, 4, 5, 3},
            new[] {2, 1, 4, 5, 3},
            new[] {4, 1, 2, 5, 3},
            new[] {1, 4, 2, 5, 3},
            new[] {5, 4, 2, 1, 3},
            new[] {4, 5, 2, 1, 3},
            new[] {2, 5, 4, 1, 3},
            new[] {5, 2, 4, 1, 3},
            new[] {4, 2, 5, 1, 3},
            new[] {2, 4, 5, 1, 3},
            new[] {3, 4, 5, 1, 2},
            new[] {4, 3, 5, 1, 2},
            new[] {5, 3, 4, 1, 2},
            new[] {3, 5, 4, 1, 2},
            new[] {4, 5, 3, 1, 2},
            new[] {5, 4, 3, 1, 2},
            new[] {5, 4, 1, 3, 2},
            new[] {4, 5, 1, 3, 2},
            new[] {1, 5, 4, 3, 2},
            new[] {5, 1, 4, 3, 2},
            new[] {4, 1, 5, 3, 2},
            new[] {1, 4, 5, 3, 2},
            new[] {1, 3, 5, 4, 2},
            new[] {3, 1, 5, 4, 2},
            new[] {5, 1, 3, 4, 2},
            new[] {1, 5, 3, 4, 2},
            new[] {3, 5, 1, 4, 2},
            new[] {5, 3, 1, 4, 2},
            new[] {4, 3, 1, 5, 2},
            new[] {3, 4, 1, 5, 2},
            new[] {1, 4, 3, 5, 2},
            new[] {4, 1, 3, 5, 2},
            new[] {3, 1, 4, 5, 2},
            new[] {1, 3, 4, 5, 2},
            new[] {2, 3, 4, 5, 1},
            new[] {3, 2, 4, 5, 1},
            new[] {4, 2, 3, 5, 1},
            new[] {2, 4, 3, 5, 1},
            new[] {3, 4, 2, 5, 1},
            new[] {4, 3, 2, 5, 1},
            new[] {4, 3, 5, 2, 1},
            new[] {3, 4, 5, 2, 1},
            new[] {5, 4, 3, 2, 1},
            new[] {4, 5, 3, 2, 1},
            new[] {3, 5, 4, 2, 1},
            new[] {5, 3, 4, 2, 1},
            new[] {5, 2, 4, 3, 1},
            new[] {2, 5, 4, 3, 1},
            new[] {4, 5, 2, 3, 1},
            new[] {5, 4, 2, 3, 1},
            new[] {2, 4, 5, 3, 1},
            new[] {4, 2, 5, 3, 1},
            new[] {3, 2, 5, 4, 1},
            new[] {2, 3, 5, 4, 1},
            new[] {5, 3, 2, 4, 1},
            new[] {3, 5, 2, 4, 1},
            new[] {2, 5, 3, 4, 1},
            new[] {5, 2, 3, 4, 1}
        };
    }
}