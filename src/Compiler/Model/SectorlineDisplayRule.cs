using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public class SectorlineDisplayRule : AbstractSectorElement, ICompilable
    {
        public SectorlineDisplayRule(
            string controlledSector,
            string compareSectorFirst,
            string compareSectorSecond,
            string comment
        ) : base(comment) 
        {
            ControlledSector = controlledSector;
            CompareSectorFirst = compareSectorFirst;
            CompareSectorSecond = compareSectorSecond;
        }

        public string ControlledSector { get; }
        public string CompareSectorFirst { get; }
        public string CompareSectorSecond { get; }

        public string Compile()
        {
            return String.Format(
                "DISPLAY:{0}:{1}:{2}{3}\r\n",
                this.ControlledSector,
                this.CompareSectorFirst,
                this.CompareSectorSecond,
                this.CompileComment()
            );
        }

        public override bool Equals(object obj)
        {
            return (obj is SectorlineDisplayRule rule) &&
                (rule.ControlledSector == this.ControlledSector) &&
                (rule.ControlledSector == this.ControlledSector) &&
                (rule.CompareSectorSecond == this.CompareSectorSecond) &&
                (rule.Comment == this.Comment);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
