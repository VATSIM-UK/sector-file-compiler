using System.Collections.Generic;
using System.Linq;

namespace Compiler.Model
{
    public class GroundNetworkRunwayExit: AbstractCompilableElement
    {
        public string Runway { get; }
        public string ExitName { get; }
        public int Direction { get; }
        public List<GroundNetworkCoordinate> Coordinates { get; }
        public int MaximumSpeed { get; }

        public GroundNetworkRunwayExit(
            string runway,
            string exitName,
            int direction,
            int maximumSpeed,
            List<GroundNetworkCoordinate> coordinates,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            Runway = runway;
            ExitName = exitName;
            Direction = direction;
            Coordinates = coordinates;
            MaximumSpeed = maximumSpeed;
        }

        public override IEnumerable<ICompilableElement> GetCompilableElements()
        {
            return new List<ICompilableElement> {this}
                .Concat(this.Coordinates);
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"EXIT:{Runway}:{ExitName}:{Direction}:{MaximumSpeed}";
        }
    }
}