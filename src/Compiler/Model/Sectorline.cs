using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class Sectorline : AbstractCompilableElement
    {
        public Sectorline(
            string name,
            List<SectorlineDisplayRule> displayRules,
            List<SectorlineCoordinate> coordinates,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment) 
        {
            Name = name;
            DisplayRules = displayRules;
            Coordinates = coordinates;
        }

        public Coordinate Corodinate { get; }
        public string Name { get; }
        public List<SectorlineDisplayRule> DisplayRules { get; }
        public List<SectorlineCoordinate> Coordinates { get; }

        /*
         * This model contains other compilable elements and is itself compilable for the definition
         * line. So return them all.
         */
        public override IEnumerable<ICompilableElement> GetCompilableElements()
        {
            List<ICompilableElement> compilables = new List<ICompilableElement>();
            compilables.Add(this);
            compilables.Concat(this.DisplayRules);
            compilables.Concat(this.Coordinates);
            return compilables;
        }

        public override string GetCompileData()
        {
            return String.Format(
                "SECTORLINE:{0}",
                this.Name
            );
        }
    }
}
