using Compiler.Output;
using Compiler.Argument;
using Compiler.Transformer;

namespace Compiler.Input
{
    class SectionFactory
    {
        private readonly FileIndex files;
        private readonly CompilerArguments arguments;

        public SectionFactory(FileIndex files, CompilerArguments arguments)
        {
            this.files = files;
            this.arguments = arguments;
        }

        public Section Create(OutputSections section)
        {
            return new Section(
                this.files.GetFilesForSection(section),
                TransformerChainFactory.Create(this.arguments, section),
                SectionHeaders.headers.ContainsKey(section) ? SectionHeaders.headers[section] : null
            );
        }
    }
}
