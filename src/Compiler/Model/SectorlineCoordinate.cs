using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class SectorlineCoordinate : AbstractSectorElement, ICompilable
    {
        public SectorlineCoordinate(
            Coordinate coordinate,
            string comment
        ) : base(comment) 
        {
            Coordinate = coordinate;
        }

        public Coordinate Coordinate { get; }

        public string Compile()
        {
            return String.Format(
                "COORD:{0}:{1}{2}\r\n",
                this.Coordinate.latitude,
                this.Coordinate.longitude,
                this.CompileComment()
            );
        }

        public override bool Equals(object obj)
        {
            return (obj is SectorlineCoordinate) &&
                (((SectorlineCoordinate)obj).Coordinate.Equals(this.Coordinate)) &&
                (((SectorlineCoordinate)obj).Comment == this.Comment);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
