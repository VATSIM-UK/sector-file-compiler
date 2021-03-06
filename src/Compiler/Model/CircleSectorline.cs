﻿using System.Collections.Generic;
using System.Linq;

namespace Compiler.Model
{
    public class CircleSectorline : AbstractCompilableElement
    {
        public CircleSectorline(
            string name,
            string centrePoint,
            double radius,
            List<SectorlineDisplayRule> displayRules,
            Definition definition,
            Docblock definitionLineDocblock,
            Comment definitionLineComment
        ) : base(definition, definitionLineDocblock, definitionLineComment)
        {
            Name = name;
            CentrePoint = centrePoint;
            Radius = radius;
            DisplayRules = displayRules;
        }

        public CircleSectorline(
            string name,
            Coordinate centreCoordinate,
            double radius,
            List<SectorlineDisplayRule> displayRules,
            Definition definition,
            Docblock definitionLineDocblock,
            Comment definitionLineComment
        ) : base(definition, definitionLineDocblock, definitionLineComment)
        {
            Name = name;
            CentreCoordinate = centreCoordinate;
            Radius = radius;
            DisplayRules = displayRules;
        }

        public string Name { get; }
        public Coordinate CentreCoordinate { get; }
        public string CentrePoint { get; }
        public double Radius { get; }
        public List<SectorlineDisplayRule> DisplayRules { get; }

        /*
         * This class contains the base definition of the CIRCLE_SECTORLINE plus a number of
         * display rules, so the compilable elements are itself followed by all the display rules.
         */
        public override IEnumerable<ICompilableElement> GetCompilableElements()
        {
            List<ICompilableElement> elements = new List<ICompilableElement> {this};
            return elements.Concat(this.DisplayRules);
        }

        /*
         * Returns the compile data for just the main definition.
         */
        public override string GetCompileData(SectorElementCollection elements)
        {
            return
                $"CIRCLE_SECTORLINE:{this.Name}:{this.CentrePoint ?? this.CentreCoordinate.latitude + ":" + this.CentreCoordinate.longitude}:{this.Radius}";
        }
    }
}
