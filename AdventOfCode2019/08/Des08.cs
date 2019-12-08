using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019._08
{
    public class Des08
    {
        public static int First(string input)
        {
            var list = new List<string>();
            for (var i = 0; i < input.Length; i += 25*6)
            {
                list.Add(input.Substring(i, 25*6));
            }

            var mostZeroes = list.OrderBy(x => x.Length - x.Replace("0", "").Length).First();
            return (mostZeroes.Length - mostZeroes.Replace("1", "").Length) *
                   (mostZeroes.Length - mostZeroes.Replace("2", "").Length);
        }

        public static List<string> Second(string input)
        {
            var layers = new List<string>();
            for (var i = 0; i < input.Length; i += 25*6)
            {
                layers.Add(input.Substring(i, 25*6));
            }

            var finalImage = new List<string>();
            
            for (var i = 0; i < 25 * 6; i++)
            {
                finalImage.Add(GetColor(layers, i));
            }

            return finalImage;
        }

        public static string GetColor(List<string> layers, int i)
        {
            foreach (var layer in layers)
            {
                if (layer.Substring(i, 1) != "2")
                    return layer.Substring(i, 1);
            }
            
            throw new Exception("Shouldn't happen");
        }
    }
}