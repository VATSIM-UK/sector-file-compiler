using Compiler.Model;
using Compiler.Transformer;
using Compiler.Output;
using System.IO;
using System.Collections.Generic;

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

        /*
         * Loop through all the subsections and the files in each
         * and write them to the output file.
         */
        public void Compile()
        {
            this.outfile.Write(SectionHeaderFactory.Create(section).Compile());
            foreach (KeyValuePair<Subsections, List<ICompilable>> subsection in this.elements.Compilables[this.section])
            {
                foreach (ICompilable compilable in subsection.Value)
                {
                    this.outfile.Write(this.transformers.Transform(compilable.Compile()));
                }
            }
            this.outfile.Write((new SectionFooter()).Compile());
        }
    }
}
