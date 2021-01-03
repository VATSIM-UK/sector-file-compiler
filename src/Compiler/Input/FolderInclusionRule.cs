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
            bool excludeList = true,
            List<string> includeExcludeFiles = null
        ) {
            this.folder = folder;
            this.recursive = recursive;
            this.inputDataType = inputDataType;
            this.outputGroup = outputGroup;
            this.excludeList = excludeList;
            this.includeExcludeFiles = includeExcludeFiles ?? new List<string>();
        }

        public IEnumerable<AbstractSectorDataFile> GetFilesToInclude(SectorDataFileFactory dataFileFactory)
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
                    files.Add(dataFileFactory.Create(path, this.inputDataType));
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
