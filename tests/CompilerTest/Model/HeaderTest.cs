using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Model;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Model
{
    public class HeaderTest
    {
        private Header header;
        private List<HeaderLine> headerLines;
        private Definition definition;

        public HeaderTest()
        {
            this.definition = DefinitionFactory.Make();
            this.headerLines = new List<HeaderLine>()
            {
                new HeaderLine(CommentFactory.Make(), DefinitionFactory.Make()),
                new HeaderLine(CommentFactory.Make(), DefinitionFactory.Make()),
                new HeaderLine(CommentFactory.Make(), DefinitionFactory.Make()),
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
