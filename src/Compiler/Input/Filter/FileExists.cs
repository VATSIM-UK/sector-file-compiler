using System.IO;

namespace Compiler.Input.Filter
{
    public class FileExists: IFileFilter
    {
        public bool Filter(string path)
        {
            return File.Exists(path);
        }
    }
}
