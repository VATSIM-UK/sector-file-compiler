using System.IO;
using System.Collections.Generic;
using Compiler.Exception;
using Compiler.Output;

namespace Compiler.Input
{
    public class FileListInclusionRule : IInclusionRule
    {
        public IEnumerable<string> FileList { get; }
        public bool IgnoreMissing { get; }
        public string ExceptWhereExists { get; }
        public InputDataType InputDataType { get; }
        private readonly OutputGroup outputGroup;

        public FileListInclusionRule(
            IEnumerable<string> fileList,
            bool ignoreMissing,
            string exceptWhereExists,
            InputDataType inputDataType,
            OutputGroup outputGroup
        ) {
            this.FileList = fileList;
            this.IgnoreMissing = ignoreMissing;
            this.ExceptWhereExists = exceptWhereExists;
            this.InputDataType = inputDataType;
            this.outputGroup = outputGroup;
        }

        public IEnumerable<AbstractSectorDataFile> GetFilesToInclude(SectorDataFileFactory dataFileFactory)
        {
            List<AbstractSectorDataFile> files = new List<AbstractSectorDataFile>();
            foreach (string path in FileList)
            {
                if (File.Exists(path))
                {
                    if (this.ExceptWhereExists == "" || !File.Exists(ExceptWhereExists))
                    {
                        files.Add(dataFileFactory.Create(path, this.InputDataType));
                    }
                } else if (!this.IgnoreMissing)
                {
                    throw new InputFileNotFoundException(path);
                }
            }

            return files;
        }

        public OutputGroup GetOutputGroup()
        {
            return this.outputGroup;
        }
    }
}
