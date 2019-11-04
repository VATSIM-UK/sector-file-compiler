using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class BlankLineTest
    {
        private readonly BlankLine blankLine;

        public BlankLineTest()
        {
            this.blankLine = new BlankLine();
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("\r\n", this.blankLine.Compile());
        }
    }
}
