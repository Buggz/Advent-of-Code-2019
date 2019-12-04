using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019._04
{
    public static class Des04
    {
        public static int First(int start, int end)
        {
            var list = new List<Password>();
            for (var i = start; i <= end; i++)
                list.Add(new Password(i));

            return list.Count(x => x.IsValid());
        }

        public static int Second(int start, int end)
        {
            var list = new List<Password>();
            for (var i = start; i <= end; i++)
                list.Add(new Password(i));

            return list.Count(x => x.IsValid2());
        }
    }
}