using System.Collections.Generic;
using System.Linq;

namespace Compiler.Model
{
    public class SidStarRoute : AbstractCompilableElement
    {
        public SidStarRoute(
            SidStarType type,
            string identifier,
            RouteSegment initialSegment,
            List<RouteSegment> segments,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base (definition, docblock, inlineComment)
        {
            Type = type;
            Identifier = identifier;
            InitialSegment = initialSegment;
            Segments = segments;
        }

        public SidStarType Type { get; }
        public string Identifier { get; }
        public RouteSegment InitialSegment { get; }
        public List<RouteSegment> Segments { get; }

        public override IEnumerable<ICompilableElement> GetCompilableElements()
        {
            List<ICompilableElement> elements = new List<ICompilableElement>() {this};
            return elements.Concat(this.Segments);
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"{this.Identifier?.PadRight(26, ' ')} {this.InitialSegment.Start}{this.InitialSegment.End}{(this.InitialSegment.Colour == null ? "" : " " + this.InitialSegment.Colour)}";
        }
    }
}
