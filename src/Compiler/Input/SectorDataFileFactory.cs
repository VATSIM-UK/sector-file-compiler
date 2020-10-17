using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;

namespace Compiler.Input
{
    public class SectorDataFileFactory
    {
        public static AbstractSectorDataFile Create(string fullPath, InputDataType inputDataType)
        {
            if (!File.Exists(fullPath))
            {
                throw new ArgumentException(string.Format("File does not exist: {0}", fullPath));
            }

            return inputDataType == InputDataType.FILE_HEADERS
                ? (AbstractSectorDataFile) new HeaderDataFile()
                : new SectorDataFile(fullPath, inputDataType, SectorDataReaderFactory.Create(inputDataType));
        }
    }
}
