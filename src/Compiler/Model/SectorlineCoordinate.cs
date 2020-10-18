using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class SectorlineCoordinate : AbstractCompilableElement
    {
        public SectorlineCoordinate(
            Coordinate coordinate,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment) 
        {
            Coordinate = coordinate;
        }

        public Coordinate Coordinate { get; }

        public override bool Equals(object obj)
        {
            return (obj is SectorlineCoordinate) &&
                (((SectorlineCoordinate)obj).Coordinate.Equals(this.Coordinate));
        }

        public override string GetCompileData()
        {
            return string.Format(
                "COORD:{0}:{1}",
                this.Coordinate.latitude,
                this.Coordinate.longitude
            );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
