using System.Collections.Generic;

namespace Compiler.Model
{
    public class SectorElementCollection
    {
        public List<SidStar> SidStars { get; } = new List<SidStar>();

        public List<Colour> Colours { get; } = new List<Colour>();

        public List<Airport> Airports { get; } = new List<Airport>();

        public List<Fix> Fixes { get; } = new List<Fix>();

        public List<Vor> Vors { get; } = new List<Vor>();

        public List<Ndb> Ndbs { get; } = new List<Ndb>();

        public List<ArtccSegment> Artccs { get; } = new List<ArtccSegment>();
        public List<ArtccSegment> LowArtccs { get; } = new List<ArtccSegment>();
        public List<ArtccSegment> HighArtccs { get; } = new List<ArtccSegment>();
        public List<AirwaySegment> LowAirways { get; } = new List<AirwaySegment>();
        public List<AirwaySegment> HighAirways { get; } = new List<AirwaySegment>();
        public List<SidStarRoute> SidRoutes { get; } = new List<SidStarRoute>();
        public List<SidStarRoute> StarRoutes { get; } = new List<SidStarRoute>();
        public List<Geo> GeoElements { get; } = new List<Geo>();
        public List<Label> Labels { get; } = new List<Label>();
        public List<Region> Regions { get; } = new List<Region>();

        public List<Freetext> Freetext { get; } = new List<Freetext>();
        public Info Info { get; private set; }

        public List<ControllerPosition> EsePositions { get; } = new List<ControllerPosition>();

        public List<Sectorline> SectorLines { get; } = new List<Sectorline>();

        public List<CircleSectorline> CircleSectorLines { get; } = new List<CircleSectorline>();

        public List<CoordinationPoint> CoordinationPoints { get; } = new List<CoordinationPoint>();

        public List<Sector> Sectors { get; } = new List<Sector>();

        public List<Runway> Runways { get; } = new List<Runway>();

        public List<ActiveRunway> ActiveRunways { get; } = new List<ActiveRunway>();

        public List<Header> FileHeaders { get; } = new List<Header>();

        public void Add(Airport airport)
        {
            this.Airports.Add(airport);
        }

        public void Add(AirwaySegment airway)
        {
            switch (airway.Type)
            {
                case AirwayType.LOW:
                    this.LowAirways.Add(airway);
                    break;
                case AirwayType.HIGH:
                    this.HighAirways.Add(airway);
                    break;
            }
        }

        public void Add(ArtccSegment artcc)
        {
            switch (artcc.Type)
            {
                case ArtccType.REGULAR:
                    this.Artccs.Add(artcc);
                    break;
                case ArtccType.LOW:
                    this.LowArtccs.Add(artcc);
                    break;
                case ArtccType.HIGH:
                    this.HighArtccs.Add(artcc);
                    break;

            }
        }

        public void Add(Colour colour)
        {
            this.Colours.Add(colour);
        }

        public void Add(Fix fix)
        {
            this.Fixes.Add(fix);
        }

        public void Add(Geo geo)
        {
            this.GeoElements.Add(geo);
        }

        public void Add(Label label)
        {
            this.Labels.Add(label);
        }

        public void Add(Ndb ndb)
        {
            this.Ndbs.Add(ndb);
        }

        public void Add(Region region)
        {
            this.Regions.Add(region);
        }

        public void Add(SidStar sidStar)
        {
            this.SidStars.Add(sidStar);
        }

        public void Add(Runway runway)
        {
            this.Runways.Add(runway);
        }

        public void Add(ActiveRunway runway)
        {
            this.ActiveRunways.Add(runway);
        }

        public void Add(SidStarRoute sidStar)
        {
            switch (sidStar.Type)
            {
                case SidStarType.SID:
                    this.SidRoutes.Add(sidStar);
                    break;
                case SidStarType.STAR:
                    this.StarRoutes.Add(sidStar);
                    break;
            }
        }

        public void Add(Vor vor)
        {
            this.Vors.Add(vor);
        }

        public void Add(Info info)
        {
            this.Info = info;
        }

        public void Add(Freetext freetext)
        {
            this.Freetext.Add(freetext);
        }

        public void Add(ControllerPosition esePosition)
        {
            this.EsePositions.Add(esePosition);
        }

        public void Add(Sectorline sectorline)
        {
            this.SectorLines.Add(sectorline);
        }

        public void Add(CircleSectorline sectorline)
        {
            this.CircleSectorLines.Add(sectorline);
        }

        public void Add(Sector sector)
        {
            this.Sectors.Add(sector);
        }

        public void Add(CoordinationPoint coordinationPoint)
        {
            this.CoordinationPoints.Add(coordinationPoint);
        }

        public void Add(Header header)
        {
            this.FileHeaders.Add(header);
        }
    }
}
