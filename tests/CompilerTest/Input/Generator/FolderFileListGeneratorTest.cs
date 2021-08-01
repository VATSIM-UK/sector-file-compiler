using Compiler.Input.Generator;
using Xunit;

namespace CompilerTest.Input.Generator
{
    public class FolderFileListGeneratorTest
    {
        [Fact]
        public void ItReturnsPathList()
        {
            Assert.Single(new FolderFileListGenerator("_TestData/FolderFileListGenerator").GetPaths());
            Assert.Contains(
                new FolderFileListGenerator("_TestData/FolderFileListGenerator").GetPaths(),
                filePath => filePath.Contains("Foo.txt")
            );
        }
    }
}
