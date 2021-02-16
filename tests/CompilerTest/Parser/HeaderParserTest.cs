using System.Collections.Generic;
using Compiler.Error;
using Compiler.Input;
using Compiler.Model;
using Moq;
using Xunit;

namespace CompilerTest.Parser
{
    public class HeaderParserTest: AbstractParserTestCase
    {
        [Fact]
        public void TestItThrowsSyntaxErrorIfHasData()
        {
            RunParserOnLines(new List<string>(){"abc ;comment"});
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItReadsHeaderData()
        {
            RunParserOnLines(new List<string>(){";comment1", ";comment2"});

            Assert.Single(sectorElementCollection.FileHeaders);
            Header header = sectorElementCollection.FileHeaders[0];
            Assert.Equal("comment1", header.Lines[0].Line.CommentString);
            Assert.Equal("comment2", header.Lines[1].Line.CommentString);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.FILE_HEADERS;
        }
    }
}
