using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Compiler.Transformer;

namespace CompilerTest.Transformer
{
    public class RemoveAllCommentsTest
    {
        private readonly RemoveAllComments transformer;

        public RemoveAllCommentsTest()
        {
            this.transformer = new RemoveAllComments();
        }

        [Fact]
        public void TestItDoesntAlterNonCommentLines()
        {
            Assert.Equal("abc", this.transformer.Transform("abc"));
        }

        [Fact]
        public void TestItRemovesCommentsFromEndOfLines()
        {
            Assert.Equal("abc", this.transformer.Transform("abc; comment"));
        }

        [Fact]
        public void TestItRemovesCommentLinesEntirely()
        {
            Assert.Equal("", this.transformer.Transform(" ; comment"));
        }
    }
}
