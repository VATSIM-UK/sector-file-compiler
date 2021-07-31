using System.Text.RegularExpressions;

namespace Compiler.Input.Filter
{
    public class FilePatternFilter: IFileFilter
    {
        public Regex Pattern { get; }

        public FilePatternFilter(Regex pattern)
        {
            Pattern = pattern;
        }
        
        public bool Filter(string path)
        {
            return Pattern.IsMatch(path);
        }
    }
}
