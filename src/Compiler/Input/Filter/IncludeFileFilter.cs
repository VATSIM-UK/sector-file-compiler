using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Compiler.Input.Filter
{
    public class IncludeFileFilter: IFileFilter
    {
        public IEnumerable<string> FileNames { get; }

        public IncludeFileFilter(IEnumerable<string> fileNames)
        {
            FileNames = fileNames;
        }

        public virtual bool Filter(string path)
        {
            return FileNames.Contains(Path.GetFileName(path));
        }
    }
}
