using System.Collections.Generic;
using System.Linq;

namespace Compiler.Model
{
    public class GroundNetworkTaxiway: AbstractCompilableElement
    {
        public string Name { get; }
        public List<GroundNetworkCoordinate> Coordinates { get; }
        public int MaximumSpeed { get; }
        public int? UsageFlag { get; }
        public string GateName { get; }

        public GroundNetworkTaxiway(
            string name,
            int maximumSpeed,
            int? usageFlag,
            string gateName,
            List<GroundNetworkCoordinate> coordinates,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            Name = name;
            Coordinates = coordinates;
            MaximumSpeed = maximumSpeed;
            UsageFlag = usageFlag;
            GateName = gateName;
        }

        public override IEnumerable<ICompilableElement> GetCompilableElements()
        {
            return new List<ICompilableElement> {this}
                .Concat(this.Coordinates);
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"TAXI:{Name}:{MaximumSpeed}:{UsageFlag}:{GateName}";
        }
    }
}