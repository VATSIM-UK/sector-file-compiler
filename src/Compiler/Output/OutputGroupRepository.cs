using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Output
{
    public class OutputGroupRepository
    {
        private SortedSet<OutputGroup> outputGroups;

        public OutputGroupRepository()
        {
            this.outputGroups = new SortedSet<OutputGroup>();
        }

        /*
         * Add an OutputGroup
         */
        public void Add(OutputGroup group)
        {
            this.outputGroups.Add(group);
        }

        public OutputGroup GetForDefinitionFile(Definition definition)
        {
           return this.outputGroups.First(group => group.FileList.Contains(definition.Filename));
        }
    }
}
