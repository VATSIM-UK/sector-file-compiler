using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class Sector : AbstractCompilableElement
    {
        public Sector(
            string name,
            int minimumAltitude,
            int maximumAltitude,
            SectorOwnerHierarchy owners,
            List<SectorAlternateOwnerHierarchy> altOwners,
            List<SectorActive> active,
            List<SectorGuest> guests,
            List<SectorBorder> borders,
            List<SectorArrivalAirports> arrivalAirports,
            List<SectorDepartureAirports> departureAirports,
            Definition initialDefinition,
            Docblock initialDocblock,
            Comment initialInlineComment
        ) : base(initialDefinition, initialDocblock, initialInlineComment) 
        {
            Name = name;
            MinimumAltitude = minimumAltitude;
            MaximumAltitude = maximumAltitude;
            Owners = owners;
            AltOwners = altOwners;
            Active = active;
            Guests = guests;
            Borders = borders;
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

        public List<SectorBorder> Borders { get; }
        public List<SectorArrivalAirports> ArrivalAirports { get; }
        public List<SectorDepartureAirports> DepartureAirports { get; }

        public override IEnumerable<ICompilableElement> GetCompilableElements()
        {
            List<ICompilableElement> elements = new List<ICompilableElement>();
            elements.Add(this);
            elements.Add(this.Owners);
            elements.Concat(this.AltOwners);
            elements.Concat(this.Active);
            elements.Concat(this.Guests);
            elements.Concat(this.Borders);
            elements.Concat(this.ArrivalAirports);
            elements.Concat(this.DepartureAirports);
            return elements;
        }

        public override string GetCompileData()
        {
            return string.Format(
                "SECTOR:{0}:{1}:{2}",
                this.Name,
                this.MinimumAltitude.ToString(),
                this.MaximumAltitude.ToString()
            );
        }
    }
}
