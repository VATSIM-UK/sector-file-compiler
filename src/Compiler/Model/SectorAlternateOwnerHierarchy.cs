using System.Collections.Generic;

namespace Compiler.Model
{
    public class SectorAlternateOwnerHierarchy : AbstractCompilableElement
    {
        public SectorAlternateOwnerHierarchy(
            string name,
            List<string> owners,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            Name = name;
            Owners = owners;
        }

        public string Name { get; }
        public List<string> Owners { get; }

        public override bool Equals(object obj)
        {
            if (
                !(obj is SectorAlternateOwnerHierarchy) ||
                ((SectorAlternateOwnerHierarchy)obj).Name != this.Name ||
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

        public override string GetCompileData(SectorElementCollection elements)
        {
            return $"ALTOWNER:{this.Name}:{string.Join(':', this.Owners)}\r\n";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
