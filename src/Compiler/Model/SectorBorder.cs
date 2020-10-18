using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class SectorBorder : AbstractCompilableElement, ICompilable
    {
        public SectorBorder() : base("") 
        {
            this.BorderLines = new List<string>();
        }

        public SectorBorder(
            List<string> borderLines,
            string comment
        ) : base(comment) 
        {
            BorderLines = borderLines;
        }

        public List<string> BorderLines { get; }

        public string Compile()
        {
            if (this.BorderLines.Count == 0)
            {
                return "";
            }

            return String.Format(
                "BORDER:{0}{1}\r\n",
                string.Join(':', this.BorderLines),
                this.CompileComment()
            );
        }

        public override bool Equals(object obj)
        {
            if (
                !(obj is SectorBorder) ||
                ((SectorBorder)obj).Comment != this.Comment ||
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
