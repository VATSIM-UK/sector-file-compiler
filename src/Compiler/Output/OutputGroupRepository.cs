using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Input;

namespace Compiler.Output
{
    public class OutputGroupRepository
    {
        private SortedSet<OutputGroup> outputGroups;

        public OutputGroupRepository()
        {
            this.outputGroups = new SortedSet<OutputGroup>();
        }

        public void Add(OutputGroup group)
        {
            this.outputGroups.Add(group);
        }
    }
}
