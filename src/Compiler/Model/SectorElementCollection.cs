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

        public Dictionary<OutputSections, List<ICompilable>> Compilables { get; } = new Dictionary<OutputSections, List<ICompilable>>();

        public SectorElementCollection()
        {
            foreach (OutputSections section in Enum.GetValues(typeof(OutputSections)))
            {
                Compilables.Add(section, new List<ICompilable>());
            }
        }

        public void Add(Airport airport)
        {
            this.Compilables[OutputSections.SCT_AIRPORT].Add(airport);
            this.Airports.Add(airport);
        }

        public void Add(Airway airway)
        {
            switch (airway.Type)
            {
                case AirwayType.LOW:
                    this.LowAirways.Add(airway);
                    this.Compilables[OutputSections.SCT_LOW_AIRWAY].Add(airway);
                    break;
                case AirwayType.HIGH:
                    this.HighAirways.Add(airway);
                    this.Compilables[OutputSections.SCT_HIGH_AIRWAY].Add(airway);
                    break;
            }
        }

        public void Add(Artcc artcc)
        {
            switch (artcc.Type)
            {
                case ArtccType.REGULAR:
                    this.Artccs.Add(artcc);
                    this.Compilables[OutputSections.SCT_ARTCC].Add(artcc);
                    break;
                case ArtccType.LOW:
                    this.LowArtccs.Add(artcc);
                    this.Compilables[OutputSections.SCT_ARTCC_LOW].Add(artcc);
                    break;
                case ArtccType.HIGH:
                    this.HighArtccs.Add(artcc);
                    this.Compilables[OutputSections.SCT_ARTCC_HIGH].Add(artcc);
                    break;

            }
        }

        public void Add(Colour colour)
        {
            this.Compilables[OutputSections.SCT_COLOUR_DEFS].Add(colour);
            this.Colours.Add(colour);
        }

        public void Add(Fix fix)
        {
            this.Compilables[OutputSections.SCT_FIXES].Add(fix);
            this.Fixes.Add(fix);
        }

        public void Add(Ndb ndb)
        {
            this.Compilables[OutputSections.SCT_NDB].Add(ndb);
            this.Ndbs.Add(ndb);
        }

        public void Add(SidStar sidStar)
        {
            this.Compilables[OutputSections.ESE_SIDSSTARS].Add(sidStar);
            this.SidStars.Add(sidStar);
        }

        public void Add(Vor vor)
        {
            this.Compilables[OutputSections.SCT_VOR].Add(vor);
            this.Vors.Add(vor);
        }

        public void Add(BlankLine blankLine, OutputSections section)
        {
            this.Compilables[section].Add(blankLine);
        }

        public void Add(CommentLine comment, OutputSections section)
        {
            this.Compilables[section].Add(comment);
        }
    }
}
