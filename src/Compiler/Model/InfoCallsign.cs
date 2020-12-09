namespace Compiler.Model
{
    public class InfoCallsign : AbstractCompilableElement
    {
        public InfoCallsign(string callsign, Definition definition, Docblock docblock, Comment inlineComment)
            : base(definition, docblock, inlineComment)
        {
            this.Callsign = callsign;
        }

        public string Callsign { get; }

        public override string GetCompileData()
        {
            return this.Callsign;
        }
    }
}
