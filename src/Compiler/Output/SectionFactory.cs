using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Compiler.Output;

namespace Compiler.Input
{
    class SectionFactory
    {

        private readonly FileIndexer indexer;

        private readonly SectionHeaders headers;

        public SectionFactory(FileIndexer indexer)
        {
            this.indexer = indexer;
            this.headers = new SectionHeaders();
        }

        public Section Create(OutputSections section)
        {
            return new Section(
                this.indexer.CreateFileListForSection(section),
                this.headers.headers[section]
            );
        }
    }
}
