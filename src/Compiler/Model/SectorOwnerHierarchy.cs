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

        public override bool Equals(object obj)
        {
            if (
                !(obj is SectorOwnerHierarchy) ||
                ((SectorOwnerHierarchy)obj).Comment != this.Comment ||
                ((SectorOwnerHierarchy)obj).Owners.Count != this.Owners.Count
            ) {
                return false;
            }

            for (int i = 0; i < this.Owners.Count; i++)
            {
                if (this.Owners[i] != ((SectorOwnerHierarchy)obj).Owners[i])
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
