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
            this.RunParserOnLines(new List<string>(){"abc ;comment"});
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItReadsHeaderData()
        {
            this.RunParserOnLines(new List<string>(){";comment1", ";comment2"});

            Assert.Single(this.sectorElementCollection.FileHeaders);
            Header header = this.sectorElementCollection.FileHeaders[0];
            Assert.Equal("comment1", header.Lines[0].Line.CommentString);
            Assert.Equal("comment2", header.Lines[1].Line.CommentString);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.FILE_HEADERS;
        }
    }
}
