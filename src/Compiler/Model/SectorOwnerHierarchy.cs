using System.Collections.Generic;

namespace Compiler.Model
{
    public class SectorOwnerHierarchy : AbstractCompilableElement
    {
        public SectorOwnerHierarchy(
            List<string> owners,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            Owners = owners;
        }

        public List<string> Owners { get; }

        public override bool Equals(object obj)
        {
            if (
                !(obj is SectorOwnerHierarchy) ||
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

        public override string GetCompileData(SectorElementCollection elements)
        {
            return string.Format(
                "OWNER:{0}",
                string.Join(':', this.Owners)
            );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
