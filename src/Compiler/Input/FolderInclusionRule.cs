using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Compiler.Exception;
using Compiler.Output;

namespace Compiler.Input
{
    public class FolderInclusionRule : IInclusionRule
    {
        public string Folder { get; }
        public bool Recursive { get; }
        public InputDataType InputDataType { get; }
        public bool ExcludeList { get; }
        public List<string> IncludeExcludeFiles { get; }
        private readonly OutputGroup outputGroup;

        public FolderInclusionRule(
            string folder,
            bool recursive,
            InputDataType inputDataType,
            OutputGroup outputGroup,
            bool excludeList = true,
            List<string> includeExcludeFiles = null
        ) {
            this.Folder = folder;
            this.Recursive = recursive;
            this.InputDataType = inputDataType;
            this.outputGroup = outputGroup;
            this.ExcludeList = excludeList;
            this.IncludeExcludeFiles = includeExcludeFiles != null 
                ? includeExcludeFiles.Select(file => file.ToLower()).ToList()
                : new List<string>();
        }

        public IEnumerable<AbstractSectorDataFile> GetFilesToInclude(SectorDataFileFactory dataFileFactory)
        {
            if (!Directory.Exists(this.Folder))
            {
                throw new InputDirectoryNotFoundException(Folder);
            }

            string[] allFiles = Directory.GetFiles(
                this.Folder,
                "*.*",
                this.Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly
            );
            Array.Sort(allFiles, StringComparer.InvariantCulture);

            List<AbstractSectorDataFile> files = new List<AbstractSectorDataFile>();
            foreach (string path in allFiles)
            {
                if (this.ShouldInclude(path))
                {
                    files.Add(dataFileFactory.Create(path, this.InputDataType));
                }
            }

            return files;
        }

        private bool ShouldInclude(string path)
        {
            return this.ExcludeList
                ? !this.IncludeExcludeFiles.Contains(Path.GetFileName(path))
                : this.IncludeExcludeFiles.Contains(Path.GetFileName(path));
        }

        public OutputGroup GetOutputGroup()
        {
            return this.outputGroup;
        }
    }
}
