using System.Collections.Generic;
using Compiler.Model;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Model
{
    public class HeaderTest
    {
        private readonly Header header;
        private readonly List<HeaderLine> headerLines;
        private readonly Definition definition;

        public HeaderTest()
        {
            this.definition = DefinitionFactory.Make();
            this.headerLines = new List<HeaderLine>()
            {
                new(CommentFactory.Make(), DefinitionFactory.Make()),
                new(CommentFactory.Make(), DefinitionFactory.Make()),
                new(CommentFactory.Make(), DefinitionFactory.Make()),
            };
            this.header = new Header(
                this.definition,
                this.headerLines
            );
        }

        [Fact]
        public void TestItReturnsDefinition()
        {
            Assert.Equal(this.definition, this.header.GetDefinition());
        }

        [Fact]
        public void TestItReturnsHeaderLinesAsCompilableElements()
        {
            Assert.Equal(this.headerLines, this.header.GetCompilableElements());
        }
    }
}
