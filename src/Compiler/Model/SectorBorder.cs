using System.Collections.Generic;

namespace Compiler.Model
{
    /*
     * Represents a single BORDER defintion under each SECTOR definition
     */
    public class SectorBorder : AbstractCompilableElement
    {
        public SectorBorder(
            List<string> borderLines,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment) 
        {
            BorderLines = borderLines;
        }

        public List<string> BorderLines { get; }

        public override bool Equals(object obj)
        {
            if (
                !(obj is SectorBorder) ||
                ((SectorBorder)obj).BorderLines.Count != this.BorderLines.Count
            )
            {
                return false;
            }

            for (int i = 0; i < this.BorderLines.Count; i++)
            {
                if (this.BorderLines[i] != ((SectorBorder)obj).BorderLines[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override string GetCompileData(SectorElementCollection elements)
        {
            return string.Format(
                "BORDER:{0}",
                string.Join(':', this.BorderLines)
            );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
