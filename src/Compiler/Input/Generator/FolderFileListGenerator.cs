using System.Collections.Generic;
using System.IO;

namespace Compiler.Input.Generator
{
    public class FolderFileListGenerator: IFileListGenerator
    {
        private readonly string directory;

        public FolderFileListGenerator(string directory)
        {
            this.directory = directory;
        }
        
        public IEnumerable<string> GetPaths()
        {
            return Directory.GetFiles(
                directory,
                "*.*",
                SearchOption.TopDirectoryOnly
            );
        }
    }
}
