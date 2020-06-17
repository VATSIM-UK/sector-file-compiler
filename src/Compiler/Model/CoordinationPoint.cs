using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class CoordinationPoint : AbstractSectorElement, ICompilable
    {
        public CoordinationPoint(
            bool isFirCopx,
            string departureAirportOrFixBefore,
            string departureRunway,
            string coordinationFix,
            string arrivalAiportOrFixAfter,
            string arrivalRunway,
            string fromSector,
            string toSector,
            string climbLevel,
            string descendLevel,
            string name,
            string comment
        ) : base(comment) 
        {
            IsFirCopx = isFirCopx;
            DepartureAirportOrFixBefore = departureAirportOrFixBefore;
            DepartureRunway = departureRunway;
            CoordinationFix = coordinationFix;
            ArrivalAiportOrFixAfter = arrivalAiportOrFixAfter;
            ArrivalRunway = arrivalRunway;
            FromSector = fromSector;
            ToSector = toSector;
            ClimbLevel = climbLevel;
            DescendLevel = descendLevel;
            Name = name;
        }

        public bool IsFirCopx { get; }
        public string DepartureAirportOrFixBefore { get; }
        public string DepartureRunway { get; }
        public string CoordinationFix { get; }
        public string ArrivalAiportOrFixAfter { get; }
        public string ArrivalRunway { get; }
        public string FromSector { get; }
        public string ToSector { get; }
        public string ClimbLevel { get; }
        public string DescendLevel { get; }
        public string Name { get; }

        public string Compile()
        {
            return String.Format(
                "{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}:{8}:{9}:{10}{11}\r\n",
                this.IsFirCopx ? "FIR_COPX" : "COPX",
                this.DepartureAirportOrFixBefore,
                this.DepartureRunway,
                this.CoordinationFix,
                this.ArrivalAiportOrFixAfter,
                this.ArrivalRunway,
                this.FromSector,
                this.ToSector,
                this.ClimbLevel,
                this.DescendLevel,
                this.Name,
                this.CompileComment()
            );
        }
    }
}
