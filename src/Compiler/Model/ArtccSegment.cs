using System;

namespace Compiler.Model
{
    /*
     * Similarly to Airways, the SCT2 format doesn't recognise ARTCCs as a single entity with many points, rather
     * each individually named segments. Therefore this model represents a single oneof these segments.
     */
    public class ArtccSegment : AbstractCompilableElement
    {
        public ArtccSegment(
            string identifier,
            ArtccType type,
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
        public ArtccType Type { get; }
        public Point StartPoint { get; }
        public Point EndPoint { get; }

        public override string GetCompileData()
        {
            return String.Format(
                "{0} {1} {2}",
                this.Identifier,
                this.StartPoint.ToString(),
                this.EndPoint.ToString()
            );
        }
    }
}
