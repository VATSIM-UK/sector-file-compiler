using System.Collections.Generic;
using Xunit;
using Compiler.Parser;
using Compiler.Model;
using Compiler.Output;

namespace CompilerTest.Parser
{
    public class MetadataParserTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly MetadataParser parser;

        public MetadataParserTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.parser = new MetadataParser(this.sectorElements, OutputSections.ESE_HEADER);
        }

        [Fact]
        public void TestItParsesComments()
        {
            this.parser.ParseCommentLine(";comment");
            Assert.Equal(
                "; comment\r\n",
                this.sectorElements.Compilables[OutputSections.ESE_HEADER][Subsections.DEFAULT][0].Compile()
            );
        }

        [Fact]
        public void TestItReturnsTrueOnCommentParse()
        {
            Assert.True(this.parser.ParseCommentLine(";comment"));
        }

        [Fact]
        public void TestItReturnsFalseOnNoComment()
        {
            Assert.False(this.parser.ParseCommentLine("nocomment"));
        }

        [Fact]
        public void TestItParsesBlankLines()
        {
            this.parser.ParseBlankLine("   ");
            Assert.Equal(
                "\r\n",
                this.sectorElements.Compilables[OutputSections.ESE_HEADER][Subsections.DEFAULT][0].Compile()
            );
        }

        [Fact]
        public void TestItReturnsTrueOnBlankParse()
        {
            Assert.True(this.parser.ParseBlankLine("   "));
        }

        [Fact]
        public void TestItReturnsFalseOnNoBlank()
        {
            Assert.False(this.parser.ParseBlankLine("a"));
        }
    }
}
