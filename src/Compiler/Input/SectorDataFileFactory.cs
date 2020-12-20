namespace Compiler.Input
{
    public class SectorDataFileFactory
    {
        private readonly IInputStreamFactory streamFactory;

        public SectorDataFileFactory(IInputStreamFactory streamFactory)
        {
            this.streamFactory = streamFactory;
        }

        public AbstractSectorDataFile Create(string fullPath, InputDataType inputDataType)
        {
            return inputDataType == InputDataType.FILE_HEADERS
                ? this.CreateHeaderDataFile(fullPath, inputDataType)
                : this.CreateStandardDataFile(fullPath, inputDataType);
        }
        
        private AbstractSectorDataFile CreateStandardDataFile(string fullPath, InputDataType inputDataType)
        {
            return new SectorDataFile(
                fullPath,
                this.streamFactory,
                inputDataType,
                SectorDataReaderFactory.Create(inputDataType)
            );
        }

        private AbstractSectorDataFile CreateHeaderDataFile(string fullPath, InputDataType inputDataType)
        {
            return new HeaderDataFile(
                fullPath,
                this.streamFactory,
                SectorDataReaderFactory.Create(inputDataType)
            );
        }
    }
}
