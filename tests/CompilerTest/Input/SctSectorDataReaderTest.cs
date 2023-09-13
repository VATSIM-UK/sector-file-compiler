﻿using System.Collections.Generic;
using Compiler.Input;
using Compiler.Model;
using Xunit;

namespace CompilerTest.Input
{
    public class SctSectorDataReaderTest
    {
        private readonly SctSectorDataReader reader;

        public SctSectorDataReaderTest()
        {
            this.reader = new SctSectorDataReader();
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
        [InlineData("@ARC", false)]
        public void ItDetectsACommentLine(string line, bool expected) {
            Assert.Equal(expected, this.reader.IsCommentLine(line));
        }

        [Fact]
        public void ItIgnoresShortComments() {
            Assert.False(this.reader.IsArcGenLine("@AR"));
        }

        [Fact]
        public void ItRecognisesArcGenLines() {
            Assert.True(this.reader.IsArcGenLine("@ARC(xxx)"));
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
            Assert.Equal(new Comment(expected), this.reader.GetCommentSegment(line));
        }

        public static IEnumerable<object[]> DataSegmentData =>
            new List<object[]>
            {
                    new object[] { "a b c", new List<string> { "a", "b", "c" } },
                    new object[] { "a b;c", new List<string> { "a", "b"} },
                    new object[] { "a b   c", new List<string> { "a", "b", "c"} },
                    new object[] { "a:b  ;c", new List<string> { "a:b" } },
            };

        [Theory]
        [MemberData(nameof(DataSegmentData))]
        public void ItReturnsDataSegments(string line, List<string> expected)
        {
            Assert.Equal(expected, this.reader.GetDataSegments(line));
        }

        [Theory]
        [InlineData("abc ; ", "abc")]
        [InlineData("abc", "abc")]
        [InlineData(";abc", "")]
        [InlineData(" abc ", "abc")]
        public void ItReturnsRawData(string line, string expected)
        {
            Assert.Equal(expected, this.reader.GetRawData(line));
        }
    }
}
