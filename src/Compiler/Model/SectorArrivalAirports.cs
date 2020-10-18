using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class SectorArrivalAirports : AbstractCompilableElement, ICompilable
    {
        public SectorArrivalAirports()
            : base("") 
        {
            this.Airports = new List<string>();
        }

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
            if (this.Airports.Count == 0)
            {
                return "";
            }

            return String.Format(
                "ARRAPT:{0}{1}\r\n",
                string.Join(":", this.Airports),
                this.CompileComment()
            );
        }

        public override bool Equals(object obj)
        {
            if (
                !(obj is SectorArrivalAirports) ||
                ((SectorArrivalAirports)obj).Comment != this.Comment ||
                ((SectorArrivalAirports)obj).Airports.Count != this.Airports.Count
            )
            {
                return false;
            }

            for (int i = 0; i < this.Airports.Count; i++)
            {
                if (this.Airports[i] != ((SectorArrivalAirports)obj).Airports[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
