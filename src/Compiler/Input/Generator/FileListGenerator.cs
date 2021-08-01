using System.Collections.Generic;

namespace Compiler.Input.Generator
{
    public class FileListGenerator: IFileListGenerator
    {
        private IEnumerable<string> paths;

        public FileListGenerator(IEnumerable<string> paths)
        {
            this.paths = paths;
        }

        public IEnumerable<string> GetPaths()
        {
            return paths;
        }
    }
}
