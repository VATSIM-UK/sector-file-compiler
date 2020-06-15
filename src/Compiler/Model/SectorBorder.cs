using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class SectorBorder : AbstractSectorElement, ICompilable
    {
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
            return String.Format(
                "BORDER:{0}{1}\r\n",
                string.Join(':', this.BorderLines),
                this.CompileComment()
            );
        }
    }
}
