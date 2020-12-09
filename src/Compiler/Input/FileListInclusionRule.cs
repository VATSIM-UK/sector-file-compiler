using System.IO;
using System.Collections.Generic;
using Compiler.Exception;
using Compiler.Output;

namespace Compiler.Input
{
    public class FileListInclusionRule : IInclusionRule
    {
        private readonly IEnumerable<string> fileList;
        private readonly bool ignoreMissing;
        private readonly InputDataType inputDataType;
        private readonly OutputGroup outputGroup;

        public FileListInclusionRule(
            IEnumerable<string> fileList,
            bool ignoreMissing,
            InputDataType inputDataType,
            OutputGroup outputGroup
        ) {
            this.fileList = fileList;
            this.ignoreMissing = ignoreMissing;
            this.inputDataType = inputDataType;
            this.outputGroup = outputGroup;
        }

        public IEnumerable<AbstractSectorDataFile> GetFilesToInclude()
        {
            List<AbstractSectorDataFile> files = new List<AbstractSectorDataFile>();
            foreach (string path in this.fileList)
            {
                if (File.Exists(path))
                {
                    files.Add(SectorDataFileFactory.Create(path, this.inputDataType));
                } else if (!this.ignoreMissing)
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
