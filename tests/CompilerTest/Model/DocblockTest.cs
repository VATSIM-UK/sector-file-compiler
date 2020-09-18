using Xunit;
using Compiler.Model;
using System.Collections.Generic;

namespace CompilerTest.Model
{
    public class DocblockTest
    {
        [Fact]
        public void TestItAddsLines()
        {
            Comment line1 = new Comment("Line 1");
            Comment line2 = new Comment("Line 2");
            Comment line3 = new Comment("Line 3");
            Comment line4 = new Comment("Line 4");
            Docblock docblock = new Docblock();
            docblock.AddLine(line1);
            docblock.AddLine(line2);
            docblock.AddLine(line3);
            docblock.AddLine(line4);

            IEnumerator<Comment> enumerator = docblock.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(line1, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(line2, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(line3, enumerator.Current);
            enumerator.MoveNext();
            Assert.Equal(line4, enumerator.Current);
        }
    }
}
