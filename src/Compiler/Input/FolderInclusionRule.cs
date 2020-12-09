using System.IO;
using System.Collections.Generic;
using Compiler.Exception;
using Compiler.Output;

namespace Compiler.Input
{
    public class FolderInclusionRule : IInclusionRule
    {
        private readonly string folder;
        private readonly bool recursive;
        private readonly InputDataType inputDataType;
        private readonly OutputGroup outputGroup;
        private readonly bool excludeList;
        private readonly List<string> includeExcludeFiles;

        public FolderInclusionRule(
            string folder,
            bool recursive,
            InputDataType inputDataType,
            OutputGroup outputGroup,
            bool excludeList,
            List<string> includeExcludeFiles
        ) {
            this.folder = folder;
            this.recursive = recursive;
            this.inputDataType = inputDataType;
            this.outputGroup = outputGroup;
            this.excludeList = excludeList;
            this.includeExcludeFiles = includeExcludeFiles;
        }

        public FolderInclusionRule(
            string folder,
            bool recursive,
            OutputGroup outputGroup,
            InputDataType inputDataType
        ) {
            this.folder = folder;
            this.recursive = recursive;
            this.outputGroup = outputGroup;
            this.inputDataType = inputDataType;
            this.excludeList = true;
            this.includeExcludeFiles = new List<string>();
        }

        public IEnumerable<AbstractSectorDataFile> GetFilesToInclude()
        {
            if (!Directory.Exists(this.folder))
            {
                throw new InputDirectoryNotFoundException(folder);
            }

            string[] allFiles = Directory.GetFiles(
                this.folder, "*.*",
                this.recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly
            );

            List<AbstractSectorDataFile> files = new List<AbstractSectorDataFile>();
            foreach (string path in allFiles)
            {
                if (this.ShouldInclude(path))
                {
                    files.Add(SectorDataFileFactory.Create(path, this.inputDataType));
                }
            }

            return files;
        }

        private bool ShouldInclude(string path)
        {
            return this.excludeList
                ? !this.includeExcludeFiles.Contains(Path.GetFileName(path))
                : this.includeExcludeFiles.Contains(Path.GetFileName(path));
        }

        public OutputGroup GetOutputGroup()
        {
            return this.outputGroup;
        }
    }
}
