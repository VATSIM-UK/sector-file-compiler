using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Input;
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
            // If we can't add the group, then it already exists, so just add its files to
            // the existing group
            if (!this.outputGroups.Add(group))
            {
                foreach (string file in group.FileList)
                {
                    this.outputGroups.FirstOrDefault(g => g.Key == group.Key).AddFile(file);
                }
            }
        }

        public OutputGroup GetForDefinitionFile(Definition definition)
        {
           return this.outputGroups.First(group => group.FileList.Contains(definition.Filename));
        }
    }
}
