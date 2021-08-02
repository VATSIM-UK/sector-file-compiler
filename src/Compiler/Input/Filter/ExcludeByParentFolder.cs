using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Compiler.Input.Filter
{
    public class ExcludeByParentFolder: IFileFilter
    {
        public IEnumerable<string> ParentFolders { get; }

        public ExcludeByParentFolder(IEnumerable<string> parentFolders)
        {
            ParentFolders = parentFolders;
        }

        public bool Filter(string path)
        {
            return !ParentFolders.Contains(
                new FileInfo(Path.GetDirectoryName(path)).Name
            );
        }
    }
}
