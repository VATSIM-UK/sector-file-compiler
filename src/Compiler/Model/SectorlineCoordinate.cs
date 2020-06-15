using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class SectorlineCoordinate : AbstractSectorElement, ICompilable
    {
        public SectorlineCoordinate(
            Coordinate corodinate,
            string comment
        ) : base(comment) 
        {
            Corodinate = corodinate;
        }

        public Coordinate Corodinate { get; }

        public string Compile()
        {
            return String.Format(
                "COORD:{0}:{1}{2}\r\n",
                this.Corodinate.latitude,
                this.Corodinate.longitude,
                this.CompileComment()
            );
        }
    }
}
