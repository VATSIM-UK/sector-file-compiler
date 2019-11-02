using System.Collections.Generic;
using Xunit;
using Compiler.Parser;
using Compiler.Error;
using Compiler.Model;
using Compiler.Event;

namespace CompilerTest.Parser
{
    public class SidStarParserTest
    {
        private readonly SidStarParser parser;

        private readonly SectorElementCollection collection;

        private readonly EventTracker log;

        public SidStarParserTest()
        {
            this.log = new EventTracker();
            this.parser = new SidStarParser(this.log);
            this.collection = new SectorElementCollection();
        }

        [Fact]
        public void TestItRaisesASyntaxErrorIfIncorrectNumberOfSegments()
        {
            this.parser.ParseElements("test.txt", new List<string>(new string[] { "abc:def:ghi" }), this.collection);
            Assert.IsType<SyntaxError>(this.log.GetLastEvent());
        }

        [Fact]
        public void TestItRaisesAnErrorIfUnknownType()
        {
            this.parser.ParseElements("test.txt", new List<string>(new string[] { "abc:def:ghi:jkl:mno" }), this.collection);
            Assert.IsType<SyntaxError>(this.log.GetLastEvent());
        }

        [Fact]
        public void TestItAddsSidStarData()
        {
            this.parser.ParseElements(
                "test.txt",
                new List<string>(new string[] { "SID:EGKK:26L:ADMAG2X:FIX1 FIX2" }),
                this.collection
            );

            Assert.Equal(0, this.log.CountEvents());
            SidStar result = this.collection.SidStars[0];
            Assert.Equal("SID", result.Type);
            Assert.Equal("EGKK", result.Airport);
            Assert.Equal("26L", result.Runway);
            Assert.Equal("ADMAG2X", result.Identifier);
            Assert.Equal(new List<string>(new string[] { "FIX1", "FIX2" }), result.Route);
        }
    }
}
