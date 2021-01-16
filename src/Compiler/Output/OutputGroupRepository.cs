using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Output
{
    public class OutputGroupRepository
    {
        private readonly SortedDictionary<string, OutputGroup> outputGroups;

        private readonly Dictionary<string, OutputGroup> fileMap;

        public OutputGroupRepository()
        {
            this.outputGroups = new ();
            this.fileMap = new();
        }

        /*
         * Add an OutputGroup
         */
        public void AddGroupWithFiles(OutputGroup group, List<string> files)
        {
            if (!this.outputGroups.ContainsKey(group.Key))
            {
                this.outputGroups[group.Key] = group;
            }

            OutputGroup storedGroup = this.outputGroups[group.Key];
            foreach (string file in files)
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
