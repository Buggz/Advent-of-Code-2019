using System;
using System.Collections.Generic;

namespace AdventOfCode2019._02
{
    public static class Des02
    {
        public static int First(int[] list, int noun, int verb)
        {
            list[1] = noun;
            list[2] = verb;
            
            for (var i = 0; i < list.Length; i += 4)
            {
                if (list[i] == 1)
                {
                    var index1 = list[i + 1];
                    var index2 = list[i + 2];
                    var indexTarget = list[i + 3];
                    var value = list[index1] + list[index2];
                    list[indexTarget] = value;
                }
                else if (list[i] == 2)
                {
                    var index1 = list[i + 1];
                    var index2 = list[i + 2];
                    var indexTarget = list[i + 3];
                    var value = list[index1] * list[index2];
                    list[indexTarget] = value;
                }
                else if (list[i] == 99)
                    break;
                else
                    throw new Exception("This shouldn't happen");
            }

            return list[0];
        }

        public static string Second(List<int> list)
        {
            var expected = 19690720;

            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    var result = First(list.ToArray(), noun, verb);
                    if (result == expected)
                        return $"{noun}{verb}";
                }
            }

            throw new Exception("Couldn't find noun and verb!");
        }
    }
}