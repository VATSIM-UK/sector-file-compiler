using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class SectorAlternateOwnerHierarchy : AbstractSectorElement, ICompilable
    {
        public SectorAlternateOwnerHierarchy(
            string name,
            List<string> owners,
            string comment
        ) : base(comment) 
        {
            Name = name;
            Owners = owners;
        }

        public string Name { get; }
        public List<string> Owners { get; }

        public string Compile()
        {
            return String.Format(
                "ALTOWNER:{0}:{1}{2}\r\n",
                this.Name,
                string.Join(':', this.Owners),
                this.CompileComment()
            );
        }
    }
}
