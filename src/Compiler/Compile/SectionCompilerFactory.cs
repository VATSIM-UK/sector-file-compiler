using Compiler.Argument;
using Compiler.Model;
using Compiler.Output;
using Compiler.Transformer;

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
            return new SectionCompiler(
                section,
                this.elements,
                TransformerChainFactory.Create(this.arguments, section),
                this.arguments.EseSections.Contains(section) ? this.arguments.OutFileEse : this.arguments.OutFileSct
            );
        }
    }
}
