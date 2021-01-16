using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Model;
using CompilerTest.Bogus.Factory;
using Moq;
using Xunit;

namespace CompilerTest.Model
{
    public class HeaderLineTest
    {
        private Definition definition;
        private Comment comment;
        private HeaderLine headerLine;

        public HeaderLineTest()
        {
            this.definition = DefinitionFactory.Make();
            this.comment = CommentFactory.Make();
            this.headerLine = new HeaderLine(this.comment, this.definition);
        }

        [Fact]
        public void TestItReturnsDefinition()
        {
            Assert.Equal(this.definition, this.headerLine.GetDefinition());
        }

        [Fact]
        public void TestItCompilesHeaderLines()
        {
            Mock<TextWriter> mockWriter = new();
            this.headerLine.Compile(new SectorElementCollection(), mockWriter.Object);
            mockWriter.Verify(foo => foo.WriteLine(this.comment.ToString()), Times.Once());
        }
    }
}
