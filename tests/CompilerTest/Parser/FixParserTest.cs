﻿using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Error;
using Compiler.Model;
using Compiler.Input;

namespace CompilerTest.Parser
{
    public class FixParserTest: AbstractParserTestCase
    {
        public static IEnumerable<object[]> BadData => new List<object[]>
        {
            new object[] { new List<string>{
                "ABCDE N050.57.00.000 W001.21.24.490 MORE"
            }}, // Too many sections
            new object[] { new List<string>{
                "ABCDE N050.57.00.000W001.21.24.490"
            }}, // Too few sections
            new object[] { new List<string>{
                "ABCDE N050.57.00.000 N001.21.24.490"
            }}, // Invalid coordinates
        };

        [Theory]
        [MemberData(nameof(BadData))]
        public void ItRaisesSyntaxErrorsOnBadData(List<string> lines)
        {
            this.RunParserOnLines(lines);

            Assert.Empty(this.sectorElementCollection.Fixes);
            this.logger.Verify(foo => foo.AddEvent(It.IsAny<SyntaxError>()), Times.Once);
        }

        [Fact]
        public void TestItAddsFixData()
        {
            this.RunParserOnLines(new List<string>() {"ABCDE N050.57.00.000 W001.21.24.490;comment"});

            Fix result = this.sectorElementCollection.Fixes[0];
            Assert.Equal("ABCDE", result.Identifier);
            Assert.Equal(new Coordinate("N050.57.00.000", "W001.21.24.490"), result.Coordinate);
            this.AssertExpectedMetadata(result, 1);
        }

        protected override InputDataType GetInputDataType()
        {
            return InputDataType.SCT_FIXES;
        }
    }
}
