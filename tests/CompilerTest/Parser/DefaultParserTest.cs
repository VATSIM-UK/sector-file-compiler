using System.Collections.Generic;
using Xunit;
using Compiler.Parser;
using Compiler.Model;
using Compiler.Output;

namespace CompilerTest.Parser
{
    public class DefaultParserTest
    {
        private readonly SectorElementCollection sectorElements;
        private readonly DefaultParser parser;

        public DefaultParserTest()
        {
            this.sectorElements = new SectorElementCollection();
            this.parser = new DefaultParser(
                new MetadataParser(this.sectorElements, OutputSections.ESE_HEADER)
            );
        }

        [Fact]
        public void TestItParsesComments()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { ";comment" })
            );

            this.parser.ParseData(data);
            Assert.IsType<CommentLine>(
                this.sectorElements.Compilables[OutputSections.ESE_HEADER][0]
            );
        }

        [Fact]
        public void TestItParsesBlankLines()
        {
            SectorFormatData data = new SectorFormatData(
                "test.txt",
                "test",
                "test",
                new List<string>(new string[] { "\r\n" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(
                this.sectorElements.Compilables[OutputSections.ESE_HEADER][0]
            );
        }
    }
}
