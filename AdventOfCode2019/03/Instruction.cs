using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019._03
{
    public class Instruction
    {
        public Direction Direction { get; set; }
        public int Distance { get; set; }
        
        private readonly Dictionary<string, Direction> _directionTable = new Dictionary<string, Direction>()
        {
            { "U", Direction.Up },
            { "D", Direction.Down },
            { "L", Direction.Left },
            { "R", Direction.Right },
            
        };
        
        public Instruction(string code)
        {
            var directionCode = code.Substring(0, 1);
            
            if (!_directionTable.Keys.Contains<string>(directionCode))
                throw new ArgumentException("Invalid direction code!");

            Direction = _directionTable[directionCode];
            Distance = int.Parse(code.Substring(1));
        }
    }
}