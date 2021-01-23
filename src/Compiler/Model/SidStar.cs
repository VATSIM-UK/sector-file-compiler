using System.Collections.Generic;

namespace Compiler.Model
{
    public class SidStar : AbstractCompilableElement
    {
        public string Type { get; }
        public string Airport { get; }
        public string Runway { get; }
        public string Identifier { get; }
        public List<string> Route { get; }

        public SidStar(
            string type,
            string airport,
            string runway,
            string identifier,
            List<string> route,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            this.Type = type;
            this.Airport = airport;
            this.Runway = runway;
            this.Identifier = identifier;
            this.Route = route;
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"{this.Type}:{this.Airport}:{this.Runway}:{this.Identifier}:{string.Join(' ', this.Route)}";
        }
    }
}
