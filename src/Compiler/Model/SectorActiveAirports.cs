using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class SectorActiveAirports : AbstractSectorElement, ICompilable
    {
        public SectorActiveAirports(
            bool departureAirport,
            List<string> airports,
            string comment
        ) : base(comment) 
        {
            DepartureAirport = departureAirport;
            Airports = airports;
        }

        public bool DepartureAirport { get; }
        public List<string> Airports { get; }

        public string Compile()
        {
            return String.Format(
                "{0}:{1}{2}\r\n",
                this.DepartureAirport ? "DEPAPT" : "ARRAPT",
                string.Join(":", this.Airports),
                this.CompileComment()
            );
        }
    }
}
