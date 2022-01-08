using System.Collections.Generic;
using System.Linq;

namespace Compiler.Model
{
    public class Sectorline : AbstractCompilableElement
    {
        public Sectorline(
            string name,
            List<SectorlineDisplayRule> displayRules,
            List<SectorlineCoordinate> coordinates,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment) 
        {
            Name = name;
            DisplayRules = displayRules;
            Coordinates = coordinates;
        }
        public string Name { get; }
        public List<SectorlineDisplayRule> DisplayRules { get; }
        public List<SectorlineCoordinate> Coordinates { get; }

        /*
         * This model contains other compilable elements and is itself compilable for the definition
         * line. So return them all.
         */
        public override IEnumerable<ICompilableElement> GetCompilableElements()
        {
            List<ICompilableElement> compilables = new List<ICompilableElement> {this};
            return compilables.Concat(this.DisplayRules)
                .Concat(this.Coordinates);
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"SECTORLINE:{this.Name}";
        }

        public Coordinate Start()
        {
            return Coordinates.First().Coordinate;
        }
        
        public Coordinate End()
        {
            return Coordinates.Last().Coordinate;
        }
    }
}
