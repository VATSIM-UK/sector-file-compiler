using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public Regex IncludePattern { get; }

        public FolderInclusionRule(
            string folder,
            bool recursive,
            InputDataType inputDataType,
            OutputGroup outputGroup,
            bool excludeList = true,
            List<string> includeExcludeFiles = null,
            Regex includePattern = null
        ) {
            Folder = folder;
            Recursive = recursive;
            InputDataType = inputDataType;
            this.outputGroup = outputGroup;
            IncludePattern = includePattern;
            ExcludeList = excludeList;
            IncludeExcludeFiles = includeExcludeFiles != null 
                ? includeExcludeFiles.ToList()
                : new List<string>();
        }

        public IEnumerable<AbstractSectorDataFile> GetFilesToInclude(SectorDataFileFactory dataFileFactory)
        {
            if (!Directory.Exists(Folder))
            {
                throw new InputDirectoryNotFoundException(Folder);
            }

            string[] allFiles = Directory.GetFiles(
                Folder,
                "*.*",
                Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly
            );
            
            // Match on include patterns
            if (IncludePattern != null)
            {
                allFiles = allFiles.Where(file => IncludePattern.IsMatch(file)).ToArray();
            }
            
            Array.Sort(allFiles, StringComparer.InvariantCulture);

            List<AbstractSectorDataFile> files = new List<AbstractSectorDataFile>();
            foreach (string path in allFiles)
            {
                if (ShouldInclude(path))
                {
                    files.Add(dataFileFactory.Create(path, InputDataType));
                }
            }

            return files;
        }

        private bool ShouldInclude(string path)
        {
            return ExcludeList
                ? !IncludeExcludeFiles.Contains(Path.GetFileName(path))
                : IncludeExcludeFiles.Contains(Path.GetFileName(path));
        }

        public OutputGroup GetOutputGroup()
        {
            return outputGroup;
        }
    }
}
