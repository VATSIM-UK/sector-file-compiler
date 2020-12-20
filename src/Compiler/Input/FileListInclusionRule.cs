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
        private readonly string exceptWhereExists;
        private readonly InputDataType inputDataType;
        private readonly OutputGroup outputGroup;

        public FileListInclusionRule(
            IEnumerable<string> fileList,
            bool ignoreMissing,
            string exceptWhereExists,
            InputDataType inputDataType,
            OutputGroup outputGroup
        ) {
            this.fileList = fileList;
            this.ignoreMissing = ignoreMissing;
            this.exceptWhereExists = exceptWhereExists;
            this.inputDataType = inputDataType;
            this.outputGroup = outputGroup;
        }

        public IEnumerable<AbstractSectorDataFile> GetFilesToInclude(SectorDataFileFactory dataFileFactory)
        {
            List<AbstractSectorDataFile> files = new List<AbstractSectorDataFile>();
            foreach (string path in this.fileList)
            {
                if (File.Exists(path))
                {
                    if (this.exceptWhereExists == "" || !File.Exists(exceptWhereExists))
                    {
                        files.Add(dataFileFactory.Create(path, this.inputDataType));
                    }
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
