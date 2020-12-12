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
            if (!this.outputGroups.Add(group))
            {
                this.outputGroups.First(findGroup => findGroup.Key == group.Key).Merge(group);
            }
        }

        public OutputGroup GetForDefinitionFile(Definition definition)
        {
           return this.outputGroups.First(group => group.FileList.Contains(definition.Filename));
        }
    }
}
