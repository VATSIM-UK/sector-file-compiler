﻿using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class VrpParserTest: AbstractParserTestCase
    {
        public VrpParserTest()
        {
            SetInputFileName("EGLL/VRPs.txt");
        }
        
        [Fact]
        public void TestItRaisesASyntaxErrorIfIncorrectNumberOfSegments()
        {
            RunParserOnLines(new List<string>(new[] { "abc:def" }));
            
            Assert.Empty(sectorElementCollection.Fixes);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItRaisesAnErrorIfInvalidCoordinate()
        {
            RunParserOnLines(new List<string>(new[] { "Text:abc:def" }));
            
            Assert.Empty(sectorElementCollection.Fixes);
            logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsVrpData()
        {
            RunParserOnLines(new List<string>(new[] { "Text:N054.28.46.319:W006.15.33.933 ;comment" }));

            Freetext result = sectorElementCollection.Freetext[0];
            Assert.Equal(new Coordinate("N054.28.46.319", "W006.15.33.933"), result.Coordinate);
            Assert.Equal("EGLL VRPs", result.Title);
            Assert.Equal("Text", result.Text);
            AssertExpectedMetadata(result);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.ESE_VRPS;
        }
    }
}
