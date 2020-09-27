using System.Collections.Generic;
using Compiler.Input;
using Xunit;

namespace CompilerTest.Input
{
    public class EseSectorDataReaderTest
    {
        private readonly EseSectorDataReader reader;

        public EseSectorDataReaderTest()
        {
            this.reader = new EseSectorDataReader();
        }

        [Theory]
        [InlineData("", true)]
        [InlineData("    ", true)]
        [InlineData("\n", true)]
        [InlineData("\r\n", true)]
        [InlineData("  \r\n  ", true)]
        [InlineData("\r", true)]
        [InlineData("a\r\n", false)]
        [InlineData(";comment\r\n", false)]
        [InlineData(";comment", false)]
        [InlineData(";", false)]
        [InlineData("abcde", false)]
        public void ItDetectsABlankLine(string line, bool expected)
        {
            Assert.Equal(expected, this.reader.IsBlankLine(line));
        }

        [Theory]
        [InlineData(";", true)]
        [InlineData(";comment", true)]
        [InlineData("     ;comment", true)]
        [InlineData(";comment      ", true)]
        [InlineData(";comment \r\n", true)]
        [InlineData("\r\n ;comment \r\n", true)]
        [InlineData("abc", false)]
        [InlineData("abc ;comment", false)]
        [InlineData("", false)]
        [InlineData("// comment", false)]
        [InlineData("/* comment */", false)]
        public void ItDetectsACommentLine(string line, bool expected)
        {
            Assert.Equal(expected, this.reader.IsCommentLine(line));
        }

        [Theory]
        [InlineData("abc ;", "")]
        [InlineData("abc ;comment", "comment")]
        [InlineData("abc;comment", "comment")]
        [InlineData("abc;comment      ", "comment")]
        [InlineData("abc //comment", "")]
        [InlineData("abc ; comment\r\n", "comment")]
        public void ItReturnsACommentSegment(string line, string expected)
        {
            Assert.Equal(expected, this.reader.GetCommentSegment(line));
        }

        public static IEnumerable<object[]> DataSegmentData =>
            new List<object[]>
            {
                    new object[] { "a:b:c", new List<string> { "a", "b", "c" } },
                    new object[] { "a:b;c", new List<string> { "a", "b"} },
                    new object[] { "a:b   c", new List<string> { "a", "b   c"} },
                    new object[] { "a:b    ;c", new List<string> { "a", "b    "} },
            };

        [Theory]
        [MemberData(nameof(DataSegmentData))]
        public void ItReturnsDataSegments(string line, List<string> expected)
        {
            Assert.Equal(expected, this.reader.GetDataSegments(line));
        }
    }
}
