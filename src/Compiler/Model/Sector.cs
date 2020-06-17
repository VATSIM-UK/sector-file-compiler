using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class Sector : AbstractSectorElement, ICompilable
    {
        public Sector(
            string name,
            int minimumAltitude,
            int maximumAltitude,
            SectorOwnerHierarchy owners,
            List<SectorAlternateOwnerHierarchy> altOwners,
            List<SectorActive> active,
            List<SectorGuest> guests,
            SectorBorder border,
            SectorArrivalAirports arrivalAirports,
            SectorDepartureAirports departureAirports,
            string definitionComment
        ) : base(definitionComment) 
        {
            Name = name;
            MinimumAltitude = minimumAltitude;
            MaximumAltitude = maximumAltitude;
            Owners = owners;
            AltOwners = altOwners;
            Active = active;
            Guests = guests;
            Border = border;
            ArrivalAirports = arrivalAirports;
            DepartureAirports = departureAirports;
        }

        public List<string> BorderLines { get; }
        public string Name { get; }
        public int MinimumAltitude { get; }
        public int MaximumAltitude { get; }
        public SectorOwnerHierarchy Owners { get; }
        public List<SectorAlternateOwnerHierarchy> AltOwners { get; }
        public List<SectorActive> Active { get; }
        public List<SectorGuest> Guests { get; }
        public SectorBorder Border { get; }
        public SectorArrivalAirports ArrivalAirports { get; }
        public SectorDepartureAirports DepartureAirports { get; }

        public string Compile()
        {
            return String.Format(
                "SECTOR:{0}:{1}:{2}{3}\r\n{4}{5}{6}{7}{8}{9}{10}\r\n",
                this.Name,
                this.MinimumAltitude.ToString(),
                this.MaximumAltitude.ToString(),
                this.CompileComment(),
                this.Owners.Compile(),
                this.AltOwners.Aggregate("", (ownerString, newOwner) => ownerString + newOwner.Compile()),
                this.Active.Aggregate("", (activeString, newActive) => activeString + newActive.Compile()),
                this.Guests.Aggregate("", (guestString, newGuest) => guestString + newGuest.Compile()),
                this.Border.Compile(),
                this.ArrivalAirports.Compile(),
                this.DepartureAirports.Compile()
            );
        }
    }
}
