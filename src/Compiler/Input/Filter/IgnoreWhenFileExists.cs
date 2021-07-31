using System.IO;

namespace Compiler.Input.Filter
{
    public class IgnoreWhenFileExists: IFileFilter
    {
        public string FileToCheckAgainst { get; }

        public IgnoreWhenFileExists(string fileToCheckAgainst)
        {
            FileToCheckAgainst = fileToCheckAgainst;
        }

        public bool Filter(string path)
        {
            return !File.Exists(FileToCheckAgainst);
        }
    }
}
