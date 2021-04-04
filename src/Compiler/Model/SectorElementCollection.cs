using System.Collections.Generic;

namespace Compiler.Model
{
    public class SectorElementCollection
    {
        public List<SidStar> SidStars { get; } = new();

        public List<Colour> Colours { get; } = new();

        public List<Airport> Airports { get; } = new();

        public List<Fix> Fixes { get; } = new();

        public List<Vor> Vors { get; } = new();

        public List<Ndb> Ndbs { get; } = new();

        public List<ArtccSegment> Artccs { get; } = new();
        public List<ArtccSegment> LowArtccs { get; } = new();
        public List<ArtccSegment> HighArtccs { get; } = new();
        public List<AirwaySegment> LowAirways { get; } = new();
        public List<AirwaySegment> HighAirways { get; } = new();
        public List<SidStarRoute> SidRoutes { get; } = new();
        public List<SidStarRoute> StarRoutes { get; } = new();
        public List<Geo> GeoElements { get; } = new();
        public List<Label> Labels { get; } = new();
        public List<Region> Regions { get; } = new();

        public List<Freetext> Freetext { get; } = new();
        public Info Info { get; private set; }

        public List<ControllerPosition> EsePositions { get; } = new();

        public List<Sectorline> SectorLines { get; } = new();

        public List<CircleSectorline> CircleSectorLines { get; } = new();

        public List<CoordinationPoint> CoordinationPoints { get; } = new();

        public List<Sector> Sectors { get; } = new();

        public List<Runway> Runways { get; } = new();

        public List<ActiveRunway> ActiveRunways { get; } = new();

        public List<Header> FileHeaders { get; } = new();

        public List<RunwayCentreline> RunwayCentrelines { get; } = new();
        
        public List<RunwayCentreline> FixedColourRunwayCentrelines { get; } = new();
        
        public List<GroundNetwork> GroundNetworks { get; } = new();

        public List<Radar> Radars { get; } = new();
        
        public List<RadarHole> RadarHoles { get; } = new();

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
        
        public void Add(RunwayCentreline centreline)
        {
            RunwayCentrelines.Add(centreline);
        }
        
        public void Add(FixedColourRunwayCentreline centreline)
        {
            FixedColourRunwayCentrelines.Add(centreline);
        }

        public void Add(GroundNetwork network)
        {
            GroundNetworks.Add(network);
        }

        public void Add(Radar radar)
        {
            Radars.Add(radar);
        }
        
        public void Add(RadarHole hole)
        {
            RadarHoles.Add(hole);
        }
    }
}
