using System;
using System.Collections.Generic;
using AdventOfCode2019._07;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2019.Tests
{
    public class Des07Test
    {
        private readonly ITestOutputHelper _testOutputHelper;

        [Fact]
        public void First()
        {
            var output = Des07.First(_opcodeProgram);
            _testOutputHelper.WriteLine(output.ToString());
        }

        [Fact]
        public void Second()
        {
            var output = Des07.Second(_opcodeProgram);
            _testOutputHelper.WriteLine(output.ToString());
        }

        [Fact]
        public void Example1_Part2()
        {
            var opcode = new[]
            {
                3, 26, 1001, 26, -4, 26, 3, 27, 1002, 27, 2, 27, 1, 27, 26,
                27, 4, 27, 1001, 28, -1, 28, 1005, 28, 6, 99, 0, 0, 5
            };
            var output = Des07.Second(opcode);
            Assert.Equal(139629729, output);
        }

        [Fact]
        public void ExampleProgram1()
        {
            var opcode = new [] {3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0};
            var output = Des07.First(opcode);
            Assert.Equal(43210, output);
        }
        
        [Fact]
        public void ExampleProgram2()
        {
            var opcode = new[]
            {
                3, 23, 3, 24, 1002, 24, 10, 24, 1002, 23, -1, 23,
                101, 5, 23, 23, 1, 24, 23, 23, 4, 23, 99, 0, 0
            };
            var output = Des07.First(opcode);
            Assert.Equal(54321, output);
        }
        
        [Fact]
        public void ExampleProgram3()
        {
            var opcode = new[]
            {
                3, 31, 3, 32, 1002, 32, 10, 32, 1001, 31, -2, 31, 1007, 31, 0, 33,
                1002, 33, 7, 33, 1, 33, 31, 31, 1, 32, 31, 31, 4, 31, 99, 0, 0, 0
            };
            var output = Des07.First(opcode);
            Assert.Equal(65210, output);
        }
        
        [Theory]
        [InlineData(1001, 0, 1)]
        [InlineData(1101, 1, 1)]
        public void Correctly_Gets_Modes(int input, int expectedMode1, int expectedMode2)
        {
            var modes = OpCodeInterpreter.GetModes(input);
            Assert.Equal(expectedMode1, modes[0]);
            Assert.Equal(expectedMode2, modes[1]);
        }


        private readonly int[] _opcodeProgram = new []
        {
            3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 34, 55, 68, 93, 106, 187, 268, 349, 430, 99999, 3, 9, 102, 5, 9, 9,
            1001, 9, 2, 9, 4, 9, 99, 3, 9, 1001, 9, 5, 9, 102, 2, 9, 9, 101, 2, 9, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9, 101,
            2, 9, 9, 102, 4, 9, 9, 4, 9, 99, 3, 9, 101, 4, 9, 9, 102, 3, 9, 9, 1001, 9, 2, 9, 102, 4, 9, 9, 1001, 9, 2,
            9, 4, 9, 99, 3, 9, 101, 2, 9, 9, 1002, 9, 5, 9, 4, 9, 99, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4,
            9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4,
            9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9,
            99, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4,
            9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9,
            3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9,
            3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9,
            3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 99,
            3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9,
            3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3,
            9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3,
            9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9,
            1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 99
        };

        public Des07Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}