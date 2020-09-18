using System.Collections.Generic;
using Xunit;
using Compiler.Parser;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Mock;

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
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { ";comment" })
            );

            this.parser.ParseData(data);
            Assert.IsType<Comment>(
                this.sectorElements.Compilables[OutputSections.ESE_HEADER][0]
            );
        }

        [Fact]
        public void TestItParsesBlankLines()
        {
            MockSectorDataFile data = new MockSectorDataFile(
                "test.txt",
                new List<string>(new string[] { "\r\n" })
            );

            this.parser.ParseData(data);
            Assert.IsType<BlankLine>(
                this.sectorElements.Compilables[OutputSections.ESE_HEADER][0]
            );
        }
    }
}
