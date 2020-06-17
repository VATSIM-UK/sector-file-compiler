using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class SectorDepartureAirports : AbstractSectorElement, ICompilable
    {
        public SectorDepartureAirports(
            List<string> airports,
            string comment
        ) : base(comment) 
        {
            Airports = airports;
        }

        public List<string> Airports { get; }

        public string Compile()
        {
            return String.Format(
                "DEPAPT:{0}{1}\r\n",
                string.Join(":", this.Airports),
                this.CompileComment()
            );
        }
    }
}
