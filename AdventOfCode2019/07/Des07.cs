using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019._05;

namespace AdventOfCode2019._07
{
    public static class Des07
    {
        public static int First(int[] opcodeProgram)
        {
            var results = new List<int>();

            foreach (var permutation in Permutations)
            {
                results.Add(RunIntCodeComputer(opcodeProgram, permutation));
            }

            return results.Max();
        }

        public static int RunIntCodeComputer(int[] opcodeProgram, int[] permutation)
        {
            int output = 0;
            
            for (var i = 0; i < permutation.Length; i++)
            {
                output = new OpCodeInterpreter(opcodeProgram).Run(output, permutation[i] - 1); 
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