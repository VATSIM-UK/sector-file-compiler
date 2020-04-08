using Xunit;
using Compiler.Error;

namespace CompilerTest.Error
{
    public class FileNotFoundErrorTest
    {
        private readonly FileNotFoundError error;
        public FileNotFoundErrorTest()
        {
            this.error = new FileNotFoundError("foo.txt");
        }

        [Fact]
        public void TestItIsFatal()
        {
            Assert.True(error.IsFatal());
        }

        [Fact]
        public void TestItHasAMessage()
        {
            Assert.Equal("File not found: foo.txt", this.error.GetMessage());
        }
    }
}
