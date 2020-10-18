using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Model
{
    public class SectorAlternateOwnerHierarchy : AbstractCompilableElement, ICompilable
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

        public override bool Equals(object obj)
        {
            if (
                !(obj is SectorAlternateOwnerHierarchy) ||
                ((SectorAlternateOwnerHierarchy)obj).Name != this.Name ||
                ((SectorAlternateOwnerHierarchy)obj).Comment != this.Comment ||
                ((SectorAlternateOwnerHierarchy)obj).Owners.Count != this.Owners.Count
            )
            {
                return false;
            }

            for (int i = 0; i < this.Owners.Count; i++)
            {
                if (this.Owners[i] != ((SectorAlternateOwnerHierarchy)obj).Owners[i])
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
