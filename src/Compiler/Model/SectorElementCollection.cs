using System;
using Compiler.Output;
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

        public List<Artcc> Artccs { get; } = new List<Artcc>();
        public List<Artcc> LowArtccs { get; } = new List<Artcc>();
        public List<Artcc> HighArtccs { get; } = new List<Artcc>();
        public List<Airway> LowAirways { get; } = new List<Airway>();
        public List<Airway> HighAirways { get; } = new List<Airway>();
        public List<SidStarRoute> SidRoutes { get; } = new List<SidStarRoute>();
        public List<SidStarRoute> StarRoutes { get; } = new List<SidStarRoute>();
        public List<Geo> GeoElements { get; } = new List<Geo>();
        public List<Label> Labels { get; } = new List<Label>();
        public List<Region> Regions { get; } = new List<Region>();

        public List<Freetext> Freetext { get; } = new List<Freetext>();
        public Info Info { get; private set; }

        public List<ControllerPosition> EsePositions { get; } = new List<ControllerPosition>();

        public Dictionary<OutputSections, Dictionary<Subsections, List<ICompilable>>> Compilables { get; } = new Dictionary<OutputSections, Dictionary<Subsections, List<ICompilable>>>();

        public SectorElementCollection()
        {
            foreach (OutputSections section in Enum.GetValues(typeof(OutputSections)))
            {
                Compilables.Add(section, new Dictionary<Subsections, List<ICompilable>>());

                foreach (Subsections subsection in Enum.GetValues(typeof(Subsections)))
                {
                    Compilables[section].Add(subsection, new List<ICompilable>());
                }
            }
        }

        public void Add(Airport airport)
        {
            this.Compilables[OutputSections.SCT_AIRPORT][Subsections.DEFAULT].Add(airport);
            this.Airports.Add(airport);
        }

        public void Add(Airway airway)
        {
            switch (airway.Type)
            {
                case AirwayType.LOW:
                    this.LowAirways.Add(airway);
                    this.Compilables[OutputSections.SCT_LOW_AIRWAY][Subsections.DEFAULT].Add(airway);
                    break;
                case AirwayType.HIGH:
                    this.HighAirways.Add(airway);
                    this.Compilables[OutputSections.SCT_HIGH_AIRWAY][Subsections.DEFAULT].Add(airway);
                    break;
            }
        }

        public void Add(Artcc artcc)
        {
            switch (artcc.Type)
            {
                case ArtccType.REGULAR:
                    this.Artccs.Add(artcc);
                    this.Compilables[OutputSections.SCT_ARTCC][Subsections.DEFAULT].Add(artcc);
                    break;
                case ArtccType.LOW:
                    this.LowArtccs.Add(artcc);
                    this.Compilables[OutputSections.SCT_ARTCC_LOW][Subsections.DEFAULT].Add(artcc);
                    break;
                case ArtccType.HIGH:
                    this.HighArtccs.Add(artcc);
                    this.Compilables[OutputSections.SCT_ARTCC_HIGH][Subsections.DEFAULT].Add(artcc);
                    break;

            }
        }

        public void Add(Colour colour)
        {
            this.Compilables[OutputSections.SCT_COLOUR_DEFS][Subsections.DEFAULT].Add(colour);
            this.Colours.Add(colour);
        }

        public void Add(Fix fix)
        {
            this.Compilables[OutputSections.SCT_FIXES][Subsections.DEFAULT].Add(fix);
            this.Fixes.Add(fix);
        }

        public void Add(Geo geo)
        {
            this.Compilables[OutputSections.SCT_GEO][Subsections.DEFAULT].Add(geo);
            this.GeoElements.Add(geo);
        }

        public void Add(Label label)
        {
            this.Compilables[OutputSections.SCT_LABELS][Subsections.DEFAULT].Add(label);
            this.Labels.Add(label);
        }

        public void Add(Ndb ndb)
        {
            this.Compilables[OutputSections.SCT_NDB][Subsections.DEFAULT].Add(ndb);
            this.Ndbs.Add(ndb);
        }

        public void Add(Region region)
        {
            this.Compilables[OutputSections.SCT_REGIONS][Subsections.DEFAULT].Add(region);
            this.Regions.Add(region);
        }

        public void Add(SidStar sidStar)
        {
            this.Compilables[OutputSections.ESE_SIDSSTARS][Subsections.DEFAULT].Add(sidStar);
            this.SidStars.Add(sidStar);
        }

        public void Add(SidStarRoute sidStar)
        {
            switch (sidStar.Type)
            {
                case SidStarType.SID:
                    this.SidRoutes.Add(sidStar);
                    this.Compilables[OutputSections.SCT_SID][Subsections.DEFAULT].Add(sidStar);
                    break;
                case SidStarType.STAR:
                    this.StarRoutes.Add(sidStar);
                    this.Compilables[OutputSections.SCT_STAR][Subsections.DEFAULT].Add(sidStar);
                    break;
            }
        }

        public void Add(Vor vor)
        {
            this.Compilables[OutputSections.SCT_VOR][Subsections.DEFAULT].Add(vor);
            this.Vors.Add(vor);
        }

        public void Add(Info info)
        {
            this.Compilables[OutputSections.SCT_INFO][Subsections.DEFAULT].Insert(0, info);
            this.Info = info;
        }

        public void Add(Freetext freetext)
        {
            this.Compilables[OutputSections.ESE_FREETEXT][Subsections.DEFAULT].Add(freetext);
            this.Freetext.Add(freetext);
        }

        public void Add(ControllerPosition esePosition)
        {
            this.Compilables[OutputSections.ESE_POSITIONS][Subsections.DEFAULT].Add(esePosition);
            this.EsePositions.Add(esePosition);
        }

        public void Add(BlankLine blankLine, OutputSections section)
        {
            this.Compilables[section][Subsections.DEFAULT].Add(blankLine);
        }

        public void Add(CommentLine comment, OutputSections section)
        {
            this.Compilables[section][Subsections.DEFAULT].Add(comment);
        }
    }
}
