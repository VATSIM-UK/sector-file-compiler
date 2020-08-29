using Compiler.Argument;
using Compiler.Model;
using Compiler.Output;
using Compiler.Transformer;
using System.IO;

namespace Compiler.Compile
{
    public class SectionCompilerFactory
    {
        private readonly CompilerArguments arguments;
        private readonly SectorElementCollection elements;

        public SectionCompilerFactory(CompilerArguments arguments, SectorElementCollection elements)
        {
            this.arguments = arguments;
            this.elements = elements;
        }

        public SectionCompiler Create(OutputSections section)
        {
            TextWriter outputFile = null;
            if (this.arguments.EseSections.Contains(section)) {
                outputFile = this.arguments.OutFileEse;
            } else if (this.arguments.SctSections.Contains(section)) {
                outputFile = this.arguments.OutFileSct;
            } else {
                outputFile = this.arguments.OutFileRwy;
            }

            return new SectionCompiler(
                section,
                this.elements,
                TransformerChainFactory.Create(this.arguments, section),
                outputFile
            );
        }
    }
}
