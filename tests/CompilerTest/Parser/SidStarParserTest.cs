using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;

namespace CompilerTest.Parser
{
    public class SidStarParserTest
    {
        private readonly SidStarParser parser;

        private readonly SectorElementCollection collection;

        private readonly ErrorLog log;

        public SidStarParserTest()
        {
            this.log = new ErrorLog();
            this.parser = new SidStarParser(this.log);
            this.collection = new SectorElementCollection();
        }

        [Fact]
        public void TestItRaisesAnErrorIfIncorrectNumberOfSegments()
        {
            this.parser.ParseElements("test.txt", new List<string>(new string[] { "abc:def:ghi" }), this.collection);

            CompilerError error = this.log.GetLastError();
            Assert.Equal(ErrorType.SyntaxError, error.type);
            Assert.Equal(ErrorCode.SidStarSegments, error.code);
            Assert.Equal("test.txt", error.fileName);
            Assert.Equal(0, error.itemNumber);
        }

        [Fact]
        public void TestItRaisesAnErrorIfUnknownType()
        {
            this.parser.ParseElements("test.txt", new List<string>(new string[] { "abc:def:ghi:jkl:mno" }), this.collection);

            CompilerError error = this.log.GetLastError();
            Assert.Equal(ErrorType.SyntaxError, error.type);
            Assert.Equal(ErrorCode.SidStarType, error.code);
            Assert.Equal("test.txt", error.fileName);
            Assert.Equal(0, error.itemNumber);
        }

        [Fact]
        public void TestItAddsSidStarData()
        {
            this.parser.ParseElements(
                "test.txt",
                new List<string>(new string[] { "SID:EGKK:26L:ADMAG2X:FIX1 FIX2" }),
                this.collection
            );

            Assert.Equal(0, this.log.CountErrors());
            SidStar result = this.collection.SidStars[0];
            Assert.Equal("SID", result.Type);
            Assert.Equal("EGKK", result.Airport);
            Assert.Equal("26L", result.Runway);
            Assert.Equal("ADMAG2X", result.Identifier);
            Assert.Equal(new List<string>(new string[] { "FIX1", "FIX2" }), result.Route);
        }
    }
}
