using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class SectorGuest : AbstractCompilableElement, ICompilable
    {
        public SectorGuest(
            string controller,
            string departureAirport,
            string arrivalAirport,
            string comment
        ) : base(comment) 
        {
            Controller = controller;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
        }

        public string Controller { get; }
        public string DepartureAirport { get; }
        public string ArrivalAirport { get; }

        public string Compile()
        {
            return String.Format(
                "GUEST:{0}:{1}:{2}{3}\r\n",
                this.Controller,
                this.DepartureAirport,
                this.ArrivalAirport,
                this.CompileComment()
            );
        }

        public override bool Equals(object obj)
        {
            return obj is SectorGuest &&
                ((SectorGuest)obj).Comment == this.Comment &&
                ((SectorGuest)obj).Controller == this.Controller &&
                ((SectorGuest)obj).ArrivalAirport == this.ArrivalAirport &&
                ((SectorGuest)obj).DepartureAirport == this.DepartureAirport;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
