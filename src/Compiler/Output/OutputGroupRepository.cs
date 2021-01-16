using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Output
{
    public class OutputGroupRepository
    {
        private readonly HashSet<OutputGroup> outputGroups;

        private readonly Dictionary<string, OutputGroup> fileMap;

        public OutputGroupRepository()
        {
            this.outputGroups = new HashSet<OutputGroup>();
            this.fileMap = new();
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

            var storedGroup = this.outputGroups.First(storedGroup => group == storedGroup);
            foreach (var file in storedGroup.FileList)
            {
                fileMap[file] = storedGroup;
            }
        }

        public OutputGroup GetForDefinitionFile(Definition definition)
        {
            return fileMap.First(file => file.Key == definition.Filename).Value;
        }

        public bool TryGetForDefinitionFile(Definition definition, out OutputGroup group)
        {
            try
            {
                group = this.GetForDefinitionFile(definition);
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
