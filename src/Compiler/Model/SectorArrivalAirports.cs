using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class SectorArrivalAirports : AbstractSectorElement, ICompilable
    {
        public SectorArrivalAirports(
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
                "ARRAPT:{0}{1}\r\n",
                string.Join(":", this.Airports),
                this.CompileComment()
            );
        }
    }
}
