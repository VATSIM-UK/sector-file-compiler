using Compiler.Output;
using Moq;
using Xunit;

namespace CompilerTest.Output
{
    public class RwyOutputTest
    {
        [Fact]
        public void TestItHasOutputSections()
        {
            OutputSectionKeys[] expected =
            {
                OutputSectionKeys.FILE_HEADER,
                OutputSectionKeys.RWY_ACTIVE_RUNWAYS
            };
            
            Assert.Equal(expected, new RwyOutput(new Mock<IOutputWriter>().Object).GetOutputSections());
        }
    }
}
