using Compiler.Output;

namespace Compiler.Input
{
    class SectionFactory
    {
        private readonly FileIndex files;

        private readonly SectionHeaders headers;

        public SectionFactory(FileIndex files)
        {
            this.files = files;
            this.headers = new SectionHeaders();
        }

        public Section Create(OutputSections section)
        {
            return new Section(
                this.files.GetFilesForSection(section),
                SectionHeaders.headers[section]
            );
        }
    }
}
