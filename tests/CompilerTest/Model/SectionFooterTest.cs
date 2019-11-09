using Compiler.Model;
using Xunit;

namespace CompilerTest.Model
{
    public class SectionFooterTest
    {
        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal("\r\n", (new SectionFooter()).Compile());
        }
    }
}
