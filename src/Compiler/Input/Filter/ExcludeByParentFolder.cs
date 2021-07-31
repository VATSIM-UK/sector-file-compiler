using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Compiler.Input.Filter
{
    public class ExcludeByParentFolder: IFileFilter
    {
        private readonly IEnumerable<string> parentFolders;

        public ExcludeByParentFolder(IEnumerable<string> parentFolders)
        {
            this.parentFolders = parentFolders;
        }

        public bool Filter(string path)
        {
            return !parentFolders.Contains(Path.GetDirectoryName(path));
        }
    }
}
