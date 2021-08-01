using System.Collections.Generic;

namespace Compiler.Input.Generator
{
    public interface IFileListGenerator
    {
        /*
         * Return all the paths to include in the file
         * inclusion rule.
         */
        public IEnumerable<string> GetPaths();
    }
}
