using System.Linq;
using Compiler.Input.Generator;
using Xunit;

namespace CompilerTest.Input.Generator
{
    public class RecursiveFolderFileListGeneratorTest
    {
        [Fact]
        public void ItReturnsPathList()
        {
            Assert.Equal(
                2,
                new RecursiveFolderFileListGenerator("_TestData/FolderFileListGenerator").GetPaths().Count()
            );
            Assert.Contains(
                new RecursiveFolderFileListGenerator("_TestData/FolderFileListGenerator").GetPaths(),
                filePath => filePath.Contains("Foo.txt")
            );
            Assert.Contains(
                new RecursiveFolderFileListGenerator("_TestData/FolderFileListGenerator").GetPaths(),
                filePath => filePath.Contains("Bar.txt")
            );
        }
    }
}
