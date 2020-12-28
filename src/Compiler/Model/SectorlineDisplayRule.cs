using System;

namespace Compiler.Model
{
    public class SectorlineDisplayRule : AbstractCompilableElement
    {
        public SectorlineDisplayRule(
            string controlledSector,
            string compareSectorFirst,
            string compareSectorSecond,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment) 
        {
            ControlledSector = controlledSector;
            CompareSectorFirst = compareSectorFirst;
            CompareSectorSecond = compareSectorSecond;
        }

        public string ControlledSector { get; }
        public string CompareSectorFirst { get; }
        public string CompareSectorSecond { get; }


        public override bool Equals(object obj)
        {
            return (obj is SectorlineDisplayRule rule) &&
                (rule.ControlledSector == this.ControlledSector) &&
                (rule.ControlledSector == this.ControlledSector) &&
                (rule.CompareSectorSecond == this.CompareSectorSecond);
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"DISPLAY:{this.ControlledSector}:{this.CompareSectorFirst}:{this.CompareSectorSecond}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
