using Xunit;
using Compiler.Model;
using System.Collections.Generic;
using System.Linq;

namespace CompilerTest.Model
{
    public class DocblockTest
    {
        [Fact]
        public void TestItAddsLines()
        {
            Comment line1 = new("Line 1");
            Comment line2 = new("Line 2");
            Comment line3 = new("Line 3");
            Comment line4 = new("Line 4");
            Docblock docblock = new();
            docblock.AddLine(line1);
            docblock.AddLine(line2);
            docblock.AddLine(line3);
            docblock.AddLine(line4);

            List<Comment> comments = docblock.ToList();
            Assert.Equal(4, comments.Count);
            Assert.Equal(line1, comments[0]);
            Assert.Equal(line2, comments[1]);
            Assert.Equal(line3, comments[2]);
            Assert.Equal(line4, comments[3]);
        }
    }
}
