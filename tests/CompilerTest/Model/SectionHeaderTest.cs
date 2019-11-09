using Xunit;
using Compiler.Model;

namespace CompilerTest.Model
{
    public class SectionHeaderTest
    {
        private readonly SectionHeader header;

        public SectionHeaderTest()
        {
            this.header = new SectionHeader("SIDSSTARS");
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("[SIDSSTARS]\r\n\r\n", this.header.Compile());
        }
    }
}
