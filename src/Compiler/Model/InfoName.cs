namespace Compiler.Model
{
    public class InfoName : AbstractCompilableElement
    {
        public InfoName(string name, Definition definition, Docblock docblock, Comment inlineComment)
            : base(definition, docblock, inlineComment)
        {
            this.Name = name;
        }

        public string Name { get; }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return this.Name;
        }
    }
}
