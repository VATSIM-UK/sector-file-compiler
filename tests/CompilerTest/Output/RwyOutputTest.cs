using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            
            Assert.Equal(expected, new RwyOutput(new Mock<TextWriter>().Object).GetOutputSections());
        }
    }
}
