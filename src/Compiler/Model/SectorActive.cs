using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class SectorActive : AbstractSectorElement, ICompilable
    {
        public SectorActive(
            string airfield,
            string runway,
            string comment
        ) : base(comment) 
        {
            Airfield = airfield;
            Runway = runway;
        }

        public string Airfield { get; }
        public string Runway { get; }

        public string Compile()
        {
            return String.Format(
                "ACTIVE:{0}:{1}{2}\r\n",
                this.Airfield,
                this.Runway,
                this.CompileComment()
            );
        }

        public override bool Equals(object obj)
        {
            return obj is SectorActive &&
                ((SectorActive)obj).Comment == this.Comment &&
                ((SectorActive)obj).Airfield == this.Airfield &&
                ((SectorActive)obj).Runway == this.Runway;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
