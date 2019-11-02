using System.Collections.Generic;
using Xunit;
using Compiler.Output;

namespace CompilerTest.Output
{
    public class OuputStringGeneratorTest
    {
        [Fact]
        public void TestItReturnsString()
        {
            string expected = "a\r\nb\r\nc\r\n";
            Assert.Equal(expected, OutputStringGenerator.GenerateOutput(new List<string>(new string[] { "a", "b", "c"})));
        }
    }
}
