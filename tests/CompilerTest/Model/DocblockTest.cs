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
            Comment line1 = new("Line 1");
            Comment line2 = new("Line 2");
            Comment line3 = new("Line 3");
            Comment line4 = new("Line 4");
            Docblock docblock = new();
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
