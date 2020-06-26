using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class SectorGuest : AbstractSectorElement, ICompilable
    {
        public SectorGuest(
            string sector,
            string departureAirport,
            string arrivalAirport,
            string comment
        ) : base(comment) 
        {
            Sector = sector;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
        }

        public string Sector { get; }
        public string DepartureAirport { get; }
        public string ArrivalAirport { get; }

        public string Compile()
        {
            return String.Format(
                "GUEST:{0}:{1}:{2}{3}\r\n",
                this.Sector,
                this.DepartureAirport,
                this.ArrivalAirport,
                this.CompileComment()
            );
        }

        public override bool Equals(object obj)
        {
            return obj is SectorGuest &&
                ((SectorGuest)obj).Comment == this.Comment &&
                ((SectorGuest)obj).Sector == this.Sector &&
                ((SectorGuest)obj).ArrivalAirport == this.ArrivalAirport &&
                ((SectorGuest)obj).DepartureAirport == this.DepartureAirport;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
