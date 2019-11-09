using Compiler.Model;
using Xunit;

namespace CompilerTest.Model
{
    public class NullElementTest
    {
        [Fact]
        public void TestItReturnsEmptyString()
        {
            Assert.Equal("", (new NullElement()).Compile());
        }
    }
}
