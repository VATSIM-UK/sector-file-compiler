using Compiler.Model;

namespace CompilerTest.Model
{
    public class AbstractModelTestCase
    {
        protected Comment GetInlineComment()
        {
            return new Comment("comment");
        }

        protected Docblock GetDocbock()
        {
            Docblock docblock = new Docblock();
            docblock.AddLine(this.GetInlineComment());
            return docblock;
        }

        protected Definition GetDefinition()
        {
            return new Definition("testfile.txt", 55);
        }
    }
}
