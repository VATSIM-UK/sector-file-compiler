using System.Collections.Generic;

namespace Compiler.Input.Filter
{
    public class ExcludeFileFilter: IncludeFileFilter
    {
        public ExcludeFileFilter(IEnumerable<string> fileNames) : base(fileNames)
        {
        }
        
        public override bool Filter(string path)
        {
            return !base.Filter(path);
        }
    }
}
