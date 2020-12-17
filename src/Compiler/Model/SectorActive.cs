using System;

namespace Compiler.Model
{
    public class SectorActive : AbstractCompilableElement
    {
        public SectorActive(
            string airfield,
            string runway,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment) 
        {
            Airfield = airfield;
            Runway = runway;
        }

        public string Airfield { get; }
        public string Runway { get; }

        public override bool Equals(object obj)
        {
            return obj is SectorActive &&
                ((SectorActive)obj).Airfield == this.Airfield &&
                ((SectorActive)obj).Runway == this.Runway;
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return String.Format(
                "ACTIVE:{0}:{1}",
                this.Airfield,
                this.Runway
            );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
