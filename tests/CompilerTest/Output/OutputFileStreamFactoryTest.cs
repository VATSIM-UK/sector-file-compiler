using System.IO;
using Compiler.Output;
using Xunit;

namespace CompilerTest.Output
{
    public class OutputFileStreamFactoryTest
    {
        [Fact]
        public void TestItReturnsStreamWriter()
        {
            Assert.IsType<StreamWriter>(new OutputFileStreamFactory().Make("foo/test"));
        }
    }
}
