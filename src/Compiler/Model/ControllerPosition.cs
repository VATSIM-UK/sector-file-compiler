using System.Collections.Generic;

namespace Compiler.Model
{
    public class ControllerPosition : AbstractCompilableElement
    {
        public ControllerPosition(
            string callsign,
            string rtfCallsign,
            string frequency,
            string identifier,
            string middleLetter,
            string prefix,
            string suffix,
            string squawkRangeStart,
            string squawkRangeEnd,
            List<Coordinate> visCentres,
            PositionOrder positionOrder,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base (definition, docblock, inlineComment)
        {
            Callsign = callsign;
            RtfCallsign = rtfCallsign;
            Frequency = frequency;
            Identifier = identifier;
            MiddleLetter = middleLetter;
            Prefix = prefix;
            Suffix = suffix;
            SquawkRangeStart = squawkRangeStart;
            SquawkRangeEnd = squawkRangeEnd;
            VisCentres = visCentres;
            PositionOrder = positionOrder;
        }

        public string Callsign { get; }
        public string RtfCallsign { get; }
        public string Frequency { get; }
        public string Identifier { get; }
        public string MiddleLetter { get; }
        public string Prefix { get; }
        public string Suffix { get; }
        public string SquawkRangeStart { get; }
        public string SquawkRangeEnd { get; }
        public List<Coordinate> VisCentres { get; }
        public PositionOrder PositionOrder { get; }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return
                $"{this.Callsign}:{this.RtfCallsign}:{this.Frequency}:{this.Identifier}:{this.MiddleLetter}:{this.Prefix}:{this.Suffix}:-:-:{this.SquawkRangeStart}:{this.SquawkRangeEnd}{this.CompileVisCenters()}";
        }

        private string CompileVisCenters()
        {
            string compiledString = "";
            foreach (Coordinate coordinate in this.VisCentres)
            {
                compiledString += $":{coordinate.latitude}:{coordinate.longitude}";
            }

            return compiledString;
        }
    }
}
