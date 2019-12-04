using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace AdventOfCode2019._03
{
    public static class Des03
    {
        public static int First(List<string> line1, List<string> line2)
        {
            var grid = CalculateGrid(line1, line2);

            return grid.Where(x => x.Value.Count == 2).Select(x => Math.Abs(x.Key.X) + Math.Abs(x.Key.Y)).Min();
        }

        public static int Second(List<string> line1, List<string> line2)
        {
            var grid = CalculateGrid(line1, line2);

            return grid
                .Where(x => x.Value.Count == 2)
                .Select(x => x.Value.Sum(y => y.Steps)).Min();
        }

        private static Dictionary<Point, List<StepsCount>> CalculateGrid(List<string> line1, List<string> line2)
        {
            var instructions1 = line1.Select(x => new Instruction(x)).ToList();
            var instructions2 = line2.Select(x => new Instruction(x)).ToList();

            var grid = new Dictionary<Point, List<StepsCount>>();
            DrawLine(grid, instructions1, 1);
            DrawLine(grid, instructions2, 2);

            if (grid.Any(x => x.Value.Count > 2))
                throw new Exception("Points have been visited more than 2 times");
            return grid;
        }

        private static void DrawLine(Dictionary<Point, List<StepsCount>> grid, List<Instruction> instructions, int lineId)
        {
            var current = new Point(0, 0);
            var steps = 0;
            
            foreach (var instruction in instructions)
            {
                (current, steps) = DrawSection(grid, current, instruction, lineId, steps);
            }
        }

        private static Tuple<Point, int> DrawSection(Dictionary<Point, List<StepsCount>> grid, Point origin, Instruction instruction, int lineId, int steps)
        {
            var deltaX = 0;
            var deltaY = 0;
            if (instruction.Direction == Direction.Up)
                deltaY = -1;
            else if (instruction.Direction == Direction.Down)
                deltaY = 1;
            else if (instruction.Direction == Direction.Left)
                deltaX = -1;
            else
                deltaX = 1;

            for (var i = 0; i < instruction.Distance; i++)
            {
                steps++;
                origin.X += deltaX;
                origin.Y += deltaY;

                if (grid.ContainsKey(origin))
                {
                    if (!grid[origin].Any(x => x.LineId == lineId))
                        grid[origin].Add(new StepsCount(){LineId = lineId, Steps = steps});    
                    
                }
                else
                {
                    grid.Add(origin, new List<StepsCount>() { new StepsCount(){ LineId = lineId, Steps = steps } } );
                }
            }

            return new Tuple<Point, int>(origin, steps);
        }
    }
}