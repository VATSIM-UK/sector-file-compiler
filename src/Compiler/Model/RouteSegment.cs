﻿using System;

namespace Compiler.Model
{
    public class RouteSegment : AbstractCompilableElement
    {
        public RouteSegment(
            string segmentIdentifier,
            Point start,
            Point end,
            Definition definition,
            Docblock docblock,
            Comment inlineComment,
            string colour = null
        ) : base(definition, docblock, inlineComment)
        {
            SegmentIdentifier = segmentIdentifier;
            Start = start;
            End = end;
            Colour = colour;
        }

        // Need the segment identifier in order to be able to pad accordingly
        public string SegmentIdentifier { get; }
        public Point Start { get; }
        public Point End { get; }
        public string Colour { get; }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            RouteSegment segment = (RouteSegment)obj;
            return this.Start.Equals(segment.Start) &&
                this.End.Equals(segment.End) &&
                (
                    (this.Colour == null && segment.Colour == null) || 
                    (this.Colour != null && segment.Colour != null && this.Colour.Equals(segment.Colour))
                );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return
                $"{"".PadRight(this.SegmentIdentifier.PadRight(26, ' ').Length)} {this.Start} {this.End}{(this.Colour == null ? "" : " " + this.Colour)}";
        }
    }
}
