using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class FreetextParserTest: AbstractParserTestCase
    {
        [Fact]
        public void TestItRaisesASyntaxErrorIfIncorrectNumberOfSegments()
        {
            RunParserOnLines(new List<string>(new[] { "abc:def:ghi" }));
            
            Assert.Empty(sectorElementCollection.Fixes);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesAnErrorIfInvalidCoordinate()
        {
            RunParserOnLines(new List<string>(new[] { "abc:def:Title:Text" }));
            
            Assert.Empty(sectorElementCollection.Fixes);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsFreetextData()
        {
            RunParserOnLines(new List<string>(new[] { "N054.28.46.319:W006.15.33.933:Title:Text ;comment" }));

            Freetext result = sectorElementCollection.Freetext[0];
            Assert.Equal(new Coordinate("N054.28.46.319", "W006.15.33.933"), result.Coordinate);
            Assert.Equal("Title", result.Title);
            Assert.Equal("Text", result.Text);
            AssertExpectedMetadata(result);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.ESE_FREETEXT;
        }
    }
}
