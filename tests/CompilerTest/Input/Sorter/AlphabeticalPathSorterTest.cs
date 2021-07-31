using System.Collections.Generic;
using Compiler.Input.Sorter;
using Xunit;

namespace CompilerTest.Input.Sorter
{
    public class AlphabeticalPathSorterTest
    {
        [Fact]
        public void ItSortsFiles()
        {
            List<string> expected = new() {"baa", "boo", "foo"};
            List<string> input = new() {"foo", "boo", "baa"};
            input.Sort(new AlphabeticalPathSorter());
            Assert.Equal(expected, input);
        }
    }
}
