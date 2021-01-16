using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;
using Compiler.Event;
using Compiler.Input;
using Compiler.Output;
using CompilerTest.Mock;

namespace CompilerTest.Parser
{
    public class VrpParserTest: AbstractParserTestCase
    {
        public VrpParserTest()
        {
            this.SetInputFileName("EGLL/VRPs.txt");
        }
        
        [Fact]
        public void TestItRaisesASyntaxErrorIfIncorrectNumberOfSegments()
        {
            this.RunParserOnLines(new List<string>(new[] { "abc:def" }));
            
            Assert.Empty(this.sectorElementCollection.Fixes);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesAnErrorIfInvalidCoordinate()
        {
            this.RunParserOnLines(new List<string>(new[] { "Text:abc:def" }));
            
            Assert.Empty(this.sectorElementCollection.Fixes);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsVrpData()
        {
            this.RunParserOnLines(new List<string>(new[] { "Text:N054.28.46.319:W006.15.33.933 ;comment" }));

            Freetext result = this.sectorElementCollection.Freetext[0];
            Assert.Equal(new Coordinate("N054.28.46.319", "W006.15.33.933"), result.Coordinate);
            Assert.Equal("EGLL VRPs", result.Title);
            Assert.Equal("Text", result.Text);
            this.AssertExpectedMetadata(result);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.ESE_VRPS;
        }
    }
}
