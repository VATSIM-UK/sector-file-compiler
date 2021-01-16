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
    public class EseOutputTest
    {
        [Fact]
        public void TestItHasOutputSections()
        {
            OutputSectionKeys[] expected = {
                OutputSectionKeys.FILE_HEADER,
                OutputSectionKeys.ESE_POSITIONS,
                OutputSectionKeys.ESE_FREETEXT,
                OutputSectionKeys.ESE_SIDSSTARS,
                OutputSectionKeys.ESE_AIRSPACE,
            };
            
            Assert.Equal(expected, new EseOutput(new Mock<TextWriter>().Object).GetOutputSections());
        }
    }
}
