using System.Collections.Generic;
using System.Linq;

namespace Compiler.Model
{
    public class RadarHole: AbstractCompilableElement
    {
        public int? PrimaryTop { get; }
        public int? SModeTop { get; }
        public int? CModeTop { get; }
        public List<RadarHoleCoordinate> Coordinates { get; }

        public RadarHole(
            int? primaryTop,
            int? sModeTop,
            int? cModeTop,
            List<RadarHoleCoordinate> coordinates,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            PrimaryTop = primaryTop;
            SModeTop = sModeTop;
            CModeTop = cModeTop;
            Coordinates = coordinates;
        }

        public override IEnumerable<ICompilableElement> GetCompilableElements()
        {
            return new List<ICompilableElement> {this}.Concat(Coordinates);
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"HOLE:{PrimaryTop}:{SModeTop}:{CModeTop}";
        }
    }
}
