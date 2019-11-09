using Compiler.Model;
using Compiler.Transformer;
using Compiler.Output;
using System.IO;

namespace Compiler.Compile
{
    public class SectionCompiler
    {
        private readonly OutputSections section;
        private readonly SectorElementCollection elements;
        private readonly TransformerChain transformers;
        private readonly TextWriter outfile;

        public SectionCompiler(
            OutputSections section,
            SectorElementCollection elements,
            TransformerChain transformers,
            TextWriter outfile
        ) {
            this.section = section;
            this.elements = elements;
            this.transformers = transformers;
            this.outfile = outfile;
        }

        public void Compile()
        {
            this.outfile.Write(SectionHeaderFactory.Create(section).Compile());
            foreach (ICompilable compilable in this.elements.Compilables[this.section])
            {
                this.outfile.Write(this.transformers.Transform(compilable.Compile()));
            }
            this.outfile.Write((new SectionFooter()).Compile());
        }
    }
}
