using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2019.Tests
{
    public class Other
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private class MyClass
        {
            public string TheValue = "Unchanged";
        }

        private void Foo(List<MyClass> list)
        {
            list = null;
        }

        private void Bar(List<MyClass> list)
        {
            list[0].TheValue = "Changed!";
        }
        
        [Fact]
        public void Baz()
        {
            var list = new List<MyClass>(){new MyClass()};
            
            Foo(list);
            
            Assert.NotNull(list);
        }

        [Fact]
        public void Bazz()
        {
            var list = new List<MyClass>(){new MyClass()};
            
            Bar(list);
            
            Assert.Equal("Changed!", list[0].TheValue);
        }

        [Fact]
        public void Bazzz()
        {
            var list = new List<MyClass>(){new MyClass()};
            var list2 = list;
            list2[0].TheValue = "Changed!";
            Assert.Equal("Changed!", list[0].TheValue);
        }

        [Fact]
        public void Baze()
        {
            //42f82ae6e57626768c5f525f03085decfdc5c6fe
            var list = new[]
            {
                0x42, 0xf8, 0x2a, 0xe6, 0xe5, 0x76, 0x26, 0x76, 0x8c, 0x5f, 0x52, 0x5f, 0x03, 0x08, 0x5d, 0xec, 0xfd,
                0xc5, 0xc6, 0xfe
            };

            var chars = list
                .OrderBy(x => x)
                .Select(x => ((char) x).ToString())
                .ToList();
            _testOutputHelper.WriteLine(string.Join("", chars));
            _testOutputHelper.WriteLine((list.Sum(x => (int)x) % 128).ToString());
        }

        public Other(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void AsciiValues()
        {
            var input = new[]{'#', '1', 'R', 'r'};
            var sum = 0;
            foreach (var c in input)
            {
                sum += c;
            }

            _testOutputHelper.WriteLine($"Sum: {sum}, sum%128: {sum % 128}");
        }

    }
}