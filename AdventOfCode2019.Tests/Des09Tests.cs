using AdventOfCode2019._07;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class Des09Tests
    {
        [Fact]
        public void Example2()
        {
            var opcodes = new[] {1102, 34915192, 34915192, 7, 4, 7, 99, 0};
            var cpu = new OpCodeInterpreter(opcodes);
            cpu.Run();
            Assert.True(cpu.LastOutput > 1000000000000000); //16 digit number
        }
        
        [Fact]
        public void Example3()
        {
            var opcodes = new long[] {104, 1125899906842624, 99};
            var cpu = new OpCodeInterpreter(opcodes);
            cpu.Run();
            Assert.Equal(1125899906842624, cpu.LastOutput);
        }
    }
}