using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Output;

namespace Compiler.Input
{
    public interface IInclusionRule
    {
        /*
         * Returns a list of files that should be included.
         */
        public IEnumerable<AbstractSectorDataFile> GetFilesToInclude();

        /*
         * Returns the output group for the files to include.
         */
        public OutputGroup GetOutputGroup();
    }
}
