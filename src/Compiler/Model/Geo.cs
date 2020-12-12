using System.Collections.Generic;
using System.Linq;

namespace Compiler.Model
{
    public class Geo : AbstractCompilableElement
    {
        /*
         * Every line of a GEO segment contains the same data, except the first line
         * which also has a name.
         */
        public Geo(
            string name,
            Point firstPoint,
            Point secondPoint,
            string colour,
            List<GeoSegment> additionalSegments,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            Name = name;
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
            Colour = colour;
            AdditionalSegments = additionalSegments;
        }

        public string Name { get; }
        public Point FirstPoint { get; }
        public Point SecondPoint { get; }
        public string Colour { get; }
        public List<GeoSegment> AdditionalSegments { get; }

        public override IEnumerable<ICompilableElement> GetCompilableElements()
        {
            List<ICompilableElement> elements = new List<ICompilableElement>();
            elements.Add(this);
            elements.Concat(this.AdditionalSegments);
            return elements;
        }

        /*
         * Returns the definition lines compile data
         */
        public override string GetCompileData()
        {
            return string.Format(
                "{0} {1} {2} {3}",
                this.Name.PadRight(27, ' '),
                this.FirstPoint.ToString(),
                this.SecondPoint.ToString(),
                this.Colour ?? ""
            ).Trim();
        }
    }
}
