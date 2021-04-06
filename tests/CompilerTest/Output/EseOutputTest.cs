using Compiler.Output;
using Moq;
using Xunit;

namespace CompilerTest.Output
{
    public class EseOutputTest
    {
        private EseOutput output;

        public EseOutputTest()
        {
            output = new EseOutput(new Mock<IOutputWriter>().Object);
        }
        
        [Fact]
        public void TestItHasOutputSections()
        {
            OutputSectionKeys[] expected = {
                OutputSectionKeys.FILE_HEADER,
                OutputSectionKeys.ESE_POSITIONS,
                OutputSectionKeys.ESE_FREETEXT,
                OutputSectionKeys.ESE_SIDSSTARS,
                OutputSectionKeys.ESE_AIRSPACE,
                OutputSectionKeys.ESE_GROUND_NETWORK,
                OutputSectionKeys.ESE_RADAR
            };
            
            Assert.Equal(expected, output.GetOutputSections());
        }
        
        [Fact]
        public void TestItReturnsAFileDescriptor()
        {
            Assert.Equal("ESE", output.GetFileDescriptor());
        }
    }
}
