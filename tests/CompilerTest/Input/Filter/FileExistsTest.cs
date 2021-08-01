using Compiler.Input.Filter;
using Xunit;

namespace CompilerTest.Input.Filter
{
    public class FileExistsTest
    {
        private readonly FileExists filter;

        public FileExistsTest()
        {
            filter = new FileExists();
        }
        
        [Fact]
        public void ItFiltersOnDoesntExist()
        {
            Assert.False(
                filter.Filter("_TestData/FileExistsFilter/NotFoo.txt")
            );
        }
        
        [Fact]
        public void ItDoesntFilterIfExists()
        {
            Assert.True(
                filter.Filter("_TestData/FileExistsFilter/Foo.txt")
            );
        }
    }
}
