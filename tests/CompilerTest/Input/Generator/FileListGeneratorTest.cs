using System.Collections.Generic;
using Compiler.Input.Generator;
using Xunit;

namespace CompilerTest.Input.Generator
{
    public class FileListGeneratorTest
    {
        [Fact]
        public void ItReturnsPathList()
        {
            List<string> paths = new() {"a", "b", "c"};
            Assert.Equal(paths, new FileListGenerator(paths).GetPaths());
        }
    }
}
