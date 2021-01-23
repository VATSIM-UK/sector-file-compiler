using Compiler.Model;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Model
{
    /*
     * Mock class for testing.
     */
    class MockSectorElement : AbstractCompilableElement
    {
        public MockSectorElement(Definition definition, Docblock docblock, Comment inlineComment)
            : base(definition, docblock, inlineComment)
        {

        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return "";
        }
    }

    public class AbstractSectorElementTest
    {
        private readonly MockSectorElement element;
        private readonly Docblock docblock;
        private readonly Comment comment;
        private readonly Definition definition;

        public AbstractSectorElementTest()
        {
            this.docblock = DocblockFactory.Make();
            this.comment = CommentFactory.Make();
            this.definition = DefinitionFactory.Make();

            this.element = new MockSectorElement(
                this.definition,
                this.docblock,
                this.comment
            );
        }

        [Fact]
        public void TestItReturnsComment()
        {
            Assert.Equal(this.comment, this.element.InlineComment);
        }

        [Fact]
        public void TestItReturnsDefinition()
        {
            Assert.Equal(this.definition, this.element.GetDefinition());
        }
        
        [Fact]
        public void TestItReturnsDocblock()
        {
            Assert.Equal(this.docblock, this.element.Docblock);
        }
    }
}
