using Compiler.Output;
using Moq;
using Xunit;

namespace CompilerTest.Output
{
    public class RwyOutputTest
    {
        private RwyOutput output;

        public RwyOutputTest()
        {
            output = new RwyOutput(new Mock<IOutputWriter>().Object);
        }
        
        [Fact]
        public void TestItHasOutputSections()
        {
            OutputSectionKeys[] expected =
            {
                OutputSectionKeys.FILE_HEADER,
                OutputSectionKeys.RWY_ACTIVE_RUNWAYS
            };
            
            Assert.Equal(expected, output.GetOutputSections());
        }

        [Fact]
        public void TestItReturnsAFileDescriptor()
        {
            Assert.Equal("RWY", output.GetFileDescriptor());
        }
    }
}
