using System.Collections.Generic;
using Compiler.Output;

namespace Compiler.Input
{
    public class FileIndex
    {
        private readonly Dictionary<OutputSections, List<IFileInterface>> files;
        public Dictionary<OutputSections, List<IFileInterface>> Files
        {
            get
            {
                return this.files;
            }
        }

        public FileIndex(Dictionary<OutputSections, List<IFileInterface>> files)
        {
            this.files = files;
        }

        public List<IFileInterface> GetFilesForSection(OutputSections section)
        {
            return this.files.ContainsKey(section) ? this.files[section] : new List<IFileInterface>();
        }
    }
}
