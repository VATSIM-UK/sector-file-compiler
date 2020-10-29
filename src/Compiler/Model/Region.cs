using System;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Model
{
    public class Region : AbstractCompilableElement
    {
        public Region(
            string name,
            List<RegionPoint> points,
            Definition definition,
            Docblock docblock,
            Comment inlineComment
        ) : base(definition, docblock, inlineComment)
        {
            Name = name;
            Points = points;
        }

        public string Name { get; }
        public List<RegionPoint> Points { get; }

        public override string GetCompileData()
        {
            return string.Format(
                "REGIONNAME {0}",
                this.Name
            );
        }

        public override IEnumerable<ICompilableElement> GetCompilableElements()
        {
            List<ICompilableElement> elements = new List<ICompilableElement>();
            elements.Add(this);
            elements.Concat(this.Points);
            return elements;
        }
    }
}
