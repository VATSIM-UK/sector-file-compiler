using System;

namespace Compiler.Model
{
    /*
     * Represents a single segment of an Airway. Although in the UKSF we have an entire file of coordinates
     * that pertain to a single airway, in SCT2 each individual line is in theory a new airway segment, it might
     * just have duplicate names. Therefore this model represents each individual segment.
     */
    public class AirwaySegment : AbstractCompilableElement
    {
        public AirwaySegment(
            string identifier,
            AirwayType type,
            Point startPoint,
            Point endPoint,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) 
            : base(definition, docblock, inlineComment)
        {
            Identifier = identifier;
            Type = type;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public string Identifier { get; }
        public AirwayType Type { get; }
        public Point StartPoint { get; }
        public Point EndPoint { get; }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"{this.Identifier} {this.StartPoint} {this.EndPoint}";
        }
    }
}
