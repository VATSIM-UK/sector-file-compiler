using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Output
{
    public class OutputGroupRepository
    {
        private readonly HashSet<OutputGroup> outputGroups;

        public OutputGroupRepository()
        {
            this.outputGroups = new HashSet<OutputGroup>();
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

        public bool TryGetForDefinitionFile(Definition definition, out OutputGroup group)
        {
            try
            {
                group = this.outputGroups.First(group => group.FileList.Contains(definition.Filename));
                return true;
            } catch (InvalidOperationException)
            {
                group = null;
                return false;
            }
        }

        public int Count()
        {
            return this.outputGroups.Count;
        }
    }
}
