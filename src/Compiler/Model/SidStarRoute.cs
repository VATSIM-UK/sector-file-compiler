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
            List<ICompilableElement> elements = new List<ICompilableElement>();
            elements.Add(this);
            elements.Concat(this.Segments);
            return elements;
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return string.Format(
                "{0} {1}{2}{3}",
                this.Identifier.PadRight(26, ' '),
                this.InitialSegment.Start.ToString(),
                this.InitialSegment.End.ToString(),
                this.InitialSegment.Colour == null ? "" : " " + this.InitialSegment.Colour
            );
        }
    }
}
