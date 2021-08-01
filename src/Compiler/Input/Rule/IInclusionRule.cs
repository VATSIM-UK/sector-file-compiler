using System.Collections.Generic;
using Compiler.Event;
using Compiler.Output;

namespace Compiler.Input.Rule
{
    public interface IInclusionRule
    {
        /*
         * Returns a list of files that should be included.
         */
        public IEnumerable<AbstractSectorDataFile> GetFilesToInclude(
            SectorDataFileFactory dataFileFactory,
            IEventLogger logger
        );

        /*
         * Returns the output group for the files to include.
         */
        public OutputGroup GetOutputGroup();
    }
}
