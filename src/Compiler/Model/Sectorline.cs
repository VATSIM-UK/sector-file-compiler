using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class Sectorline : AbstractSectorElement, ICompilable
    {
        public Sectorline(
            string name,
            List<SectorlineDisplayRule> displayRules,
            List<SectorlineCoordinate> coordinates,
            string nameComment
        ) : base(nameComment) 
        {
            Name = name;
            DisplayRules = displayRules;
            Coordinates = coordinates;
        }

        public Coordinate Corodinate { get; }
        public string Name { get; }
        public List<SectorlineDisplayRule> DisplayRules { get; }
        public List<SectorlineCoordinate> Coordinates { get; }

        public string Compile()
        {
            return String.Format(
                "SECTORLINE:{0}{1}\r\n{2}{3}\r\n",
                this.Name,
                this.CompileComment(),
                this.DisplayRules.Aggregate("", (ruleString, newRule) => ruleString + newRule.Compile()),
                this.Coordinates.Aggregate("", (coordString, coordinate) => coordString + coordinate.Compile())
            );
        }
    }
}
