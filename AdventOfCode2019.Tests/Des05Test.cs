using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using AdventOfCode2019._05;
using AdventOfCode2019._07;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2019.Tests
{
    public class Des05Test
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private List<int> _input = new List<int>()
        {
            3, 225, 1, 225, 6, 6, 1100, 1, 238, 225, 104, 0, 1102, 68, 5, 225, 1101, 71, 12, 225, 1, 117, 166, 224,
            1001, 224, -100, 224, 4, 224, 102, 8, 223, 223, 101, 2, 224, 224, 1, 223, 224, 223, 1001, 66, 36, 224, 101,
            -87, 224, 224, 4, 224, 102, 8, 223, 223, 101, 2, 224, 224, 1, 223, 224, 223, 1101, 26, 51, 225, 1102, 11,
            61, 224, 1001, 224, -671, 224, 4, 224, 1002, 223, 8, 223, 1001, 224, 5, 224, 1, 223, 224, 223, 1101, 59, 77,
            224, 101, -136, 224, 224, 4, 224, 1002, 223, 8, 223, 1001, 224, 1, 224, 1, 223, 224, 223, 1101, 11, 36, 225,
            1102, 31, 16, 225, 102, 24, 217, 224, 1001, 224, -1656, 224, 4, 224, 102, 8, 223, 223, 1001, 224, 1, 224, 1,
            224, 223, 223, 101, 60, 169, 224, 1001, 224, -147, 224, 4, 224, 102, 8, 223, 223, 101, 2, 224, 224, 1, 223,
            224, 223, 1102, 38, 69, 225, 1101, 87, 42, 225, 2, 17, 14, 224, 101, -355, 224, 224, 4, 224, 102, 8, 223,
            223, 1001, 224, 2, 224, 1, 224, 223, 223, 1002, 113, 89, 224, 101, -979, 224, 224, 4, 224, 1002, 223, 8,
            223, 1001, 224, 7, 224, 1, 224, 223, 223, 1102, 69, 59, 225, 4, 223, 99, 0, 0, 0, 677, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 1105, 0, 99999, 1105, 227, 247, 1105, 1, 99999, 1005, 227, 99999, 1005, 0, 256, 1105, 1, 99999,
            1106, 227, 99999, 1106, 0, 265, 1105, 1, 99999, 1006, 0, 99999, 1006, 227, 274, 1105, 1, 99999, 1105, 1,
            280, 1105, 1, 99999, 1, 225, 225, 225, 1101, 294, 0, 0, 105, 1, 0, 1105, 1, 99999, 1106, 0, 300, 1105, 1,
            99999, 1, 225, 225, 225, 1101, 314, 0, 0, 106, 0, 0, 1105, 1, 99999, 7, 677, 677, 224, 1002, 223, 2, 223,
            1006, 224, 329, 1001, 223, 1, 223, 1007, 226, 226, 224, 1002, 223, 2, 223, 1006, 224, 344, 1001, 223, 1,
            223, 1108, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 359, 1001, 223, 1, 223, 1107, 226, 677, 224, 1002,
            223, 2, 223, 1006, 224, 374, 101, 1, 223, 223, 1107, 677, 226, 224, 1002, 223, 2, 223, 1006, 224, 389, 101,
            1, 223, 223, 7, 226, 677, 224, 1002, 223, 2, 223, 1005, 224, 404, 101, 1, 223, 223, 1008, 677, 226, 224,
            102, 2, 223, 223, 1005, 224, 419, 101, 1, 223, 223, 1008, 226, 226, 224, 102, 2, 223, 223, 1006, 224, 434,
            101, 1, 223, 223, 107, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 449, 1001, 223, 1, 223, 108, 226, 677,
            224, 102, 2, 223, 223, 1005, 224, 464, 101, 1, 223, 223, 1108, 677, 226, 224, 102, 2, 223, 223, 1005, 224,
            479, 101, 1, 223, 223, 1007, 226, 677, 224, 102, 2, 223, 223, 1006, 224, 494, 101, 1, 223, 223, 107, 677,
            677, 224, 102, 2, 223, 223, 1005, 224, 509, 101, 1, 223, 223, 108, 677, 677, 224, 102, 2, 223, 223, 1006,
            224, 524, 1001, 223, 1, 223, 8, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 539, 101, 1, 223, 223, 107, 677,
            226, 224, 102, 2, 223, 223, 1005, 224, 554, 1001, 223, 1, 223, 8, 226, 226, 224, 102, 2, 223, 223, 1006,
            224, 569, 1001, 223, 1, 223, 7, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 584, 1001, 223, 1, 223, 1108,
            226, 226, 224, 102, 2, 223, 223, 1005, 224, 599, 1001, 223, 1, 223, 1107, 677, 677, 224, 1002, 223, 2, 223,
            1006, 224, 614, 1001, 223, 1, 223, 1007, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 629, 1001, 223, 1,
            223, 108, 226, 226, 224, 102, 2, 223, 223, 1005, 224, 644, 1001, 223, 1, 223, 8, 677, 226, 224, 1002, 223,
            2, 223, 1005, 224, 659, 1001, 223, 1, 223, 1008, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 674, 1001,
            223, 1, 223, 4, 223, 99, 226
        };

        public Des05Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void First()
        {
            var input = 1;

            var output = Des05.First(_input, input);
            _testOutputHelper.WriteLine(output.ToString());
        }

        [Fact]
        public void Second()
        {
            var input = 5;

            var output = Des05.Second(_input, input);
            _testOutputHelper.WriteLine(output.ToString());
        }
        
        [Theory]
        [InlineData(1, typeof(Add))]
        [InlineData(2, typeof(Multiply))]
        [InlineData(3, typeof(SaveValue))]
        [InlineData(4, typeof(OutputValue))]
        public void InstructionParser_Chooses_Correct_Type(int input, Type expectedType)
        {
             var actual = Instruction.Parse(new List<int>() { input, 0, 0, 0, 0 });
             Assert.Equal(expectedType, actual.GetType());
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(2, 3)]
        [InlineData(3, 1)]
        [InlineData(4, 1)]
        public void Instructions_Have_The_Correct_Number_Of_Parameters(int input, int expectedCount)
        {
            var actual = Instruction.Parse(new List<int>() { input, 0, 0, 0, 0 });
            Assert.Equal(expectedCount, actual.ParameterCount);
        }

        [Theory]
        [InlineData(1001, Modes.Position, Modes.Immediate)]
        [InlineData(1101, Modes.Immediate, Modes.Immediate)]
        public void Parameters_Have_Correct_Mode(int input, Modes expectedMode1, Modes expectedMode2)
        {
            var actual = Instruction.Parse(new List<int>() { input, 0, 0, 0, 0 });
            Assert.Equal(expectedMode1, actual.Parameters[0].Mode);
            Assert.Equal(expectedMode2, actual.Parameters[1].Mode);
        }

       
        [Fact]
        public void Instruction_Performs_Correctly()
        {
            var list = new List<int>() {3, 0, 4, 0, 99};
            var output = Des05.First(list, 42);
            _testOutputHelper.WriteLine(output.ToString());
        }

        [Theory]
        [InlineData(5, 999)]
        [InlineData(8, 1000)]
        [InlineData(9, 1001)]
        public void Instruction_With_Jumps_Work_Correctly(int input, int expected)
        {
            var list = new List<int>()
            {
                3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31,
                1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104,
                999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99
            };

            var cpu = new OpCodeInterpreter(list.ToArray(), input);
            cpu.Run();
            Assert.Equal(expected, cpu.LastOutput);
        }

        [Fact]
        public void Test()
        {
            var list = new List<int> { 3,9,8,9,10,9,4,9,99,-1,8 };
            var output = Des05.Second(list, 8);
            Assert.Equal(1, output);
            
            output = Des05.Second(list, 9);
            Assert.Equal(0, output);
        }
    }
}