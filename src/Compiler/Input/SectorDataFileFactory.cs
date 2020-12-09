namespace Compiler.Input
{
    public class SectorDataFileFactory
    {
        public static AbstractSectorDataFile Create(string fullPath, InputDataType inputDataType)
        {
            return inputDataType == InputDataType.FILE_HEADERS
                ? (AbstractSectorDataFile) new HeaderDataFile()
                : new SectorDataFile(fullPath, inputDataType, SectorDataReaderFactory.Create(inputDataType));
        }
    }
}
