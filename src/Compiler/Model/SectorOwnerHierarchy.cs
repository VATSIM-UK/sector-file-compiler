using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class SectorOwnerHierarchy : AbstractSectorElement, ICompilable
    {
        public SectorOwnerHierarchy(
            List<string> owners,
            string comment
        ) : base(comment) 
        {
            Owners = owners;
        }

        public List<string> Owners { get; }

        public string Compile()
        {
            return String.Format(
                "OWNER:{0}{1}\r\n",
                string.Join(':', this.Owners),
                this.CompileComment()
            );
        }
    }
}
