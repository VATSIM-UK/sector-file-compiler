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
        public void TestItDoesntRemoveNormalLines()
        {
            List<string> lines = new List<string>(new string[] { "a", "b", "c" });
            Assert.Equal(lines, this.transformer.Transform(lines));
        }

        [Fact]
        public void TestItRemovesCommentsFromAndOfLines()
        {
            List<string> lines = new List<string>(new string[] { "a ;acomment", "b;bcomment", "c  ; comment " });
            Assert.Equal(new List<string>(new string[] { "a", "b", "c" }), this.transformer.Transform(lines));
        }

        [Fact]
        public void TestItRemovesCommentLinesEntirely()
        {
            List<string> lines = new List<string>(new string[] { "a", ";bcomment", "c " });
            Assert.Equal(new List<string>(new string[] { "a", "c " }), this.transformer.Transform(lines));
        }
    }
}
