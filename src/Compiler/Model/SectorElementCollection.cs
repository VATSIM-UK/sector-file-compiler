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
