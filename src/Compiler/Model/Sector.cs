using System.Collections.Generic;
using System.Linq;

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
            List<ICompilableElement> elements = new List<ICompilableElement> {this, this.Owners};
            return elements.Concat(this.AltOwners)
                .Concat(this.Active)
                .Concat(this.Guests)
                .Concat(this.Borders)
                .Concat(this.ArrivalAirports)
                .Concat(this.DepartureAirports);
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"SECTOR:{this.Name}:{this.MinimumAltitude.ToString()}:{this.MaximumAltitude.ToString()}";
        }
    }
}
