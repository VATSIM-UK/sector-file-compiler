using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class CircleSectorline : AbstractSectorElement, ICompilable
    {
        public CircleSectorline(
            string name,
            string centrePoint,
            double radius,
            List<SectorlineDisplayRule> displayRules,
            string definitionComment
        ) : base(definitionComment) 
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
            string definitionComment
        ) : base(definitionComment)
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

        public string Compile()
        {
            return String.Format(
                "CIRCLE_SECTORLINE:{0}:{1}:{2}{3}\r\n{4}\r\n",
                this.Name,
                this.CentrePoint == null 
                    ? this.CentreCoordinate.latitude + ":" + this.CentreCoordinate.longitude
                    : this.CentrePoint,
                this.Radius,
                this.CompileComment(),
                this.DisplayRules.Aggregate("", (ruleString, newRule) => ruleString + newRule.Compile())
            );
        }
    }
}
