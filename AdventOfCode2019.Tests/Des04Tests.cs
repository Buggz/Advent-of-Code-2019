using System;
using AdventOfCode2019._04;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2019.Tests
{
    public class Des04Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Des04Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData(122345, true)]
        [InlineData(111123, true)]
        [InlineData(223450, false)]
        [InlineData(123789, false)]
        public void Validates_Correctly(int number, bool shouldValidate)
        {
            var test = new Password(number);
            Assert.Equal(shouldValidate, test.IsValid());
        }

        [Theory]
        [InlineData(112233, true)]
        [InlineData(123444, false)]
        [InlineData(111122, true)]
        [InlineData(122345, true)]
        [InlineData(111123, false)]
        [InlineData(223450, false)]
        [InlineData(123789, false)]
        public void Validates_Correctly2(int number, bool shouldValidate)
        {
            var password = new Password(number);
            Assert.Equal(shouldValidate, password.IsValid2());
        }

        [Fact]
        public void First()
        {
            var result = Des04.First(137683, 596253);
            _testOutputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void Second()
        {
            var result = Des04.Second(137683, 596253);
            _testOutputHelper.WriteLine(result.ToString());
        }
    }
}