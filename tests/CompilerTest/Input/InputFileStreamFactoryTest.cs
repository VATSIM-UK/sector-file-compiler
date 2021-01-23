using System.IO;
using Compiler.Input;
using Xunit;

namespace CompilerTest.Input
{
    public class InputFileStreamFactoryTest
    {
        [Fact]
        public void TestItReturnsStreamReader()
        {
            InputFileStreamFactory factory = new();
            Assert.IsType<StreamReader>(factory.GetStream("_TestData/InputFileStreamFactory/File1.txt"));
        }
        
        [Fact]
        public void TestStreamReaderReferencesCorrectFile()
        {
            InputFileStreamFactory factory = new();
            TextReader test = factory.GetStream("_TestData/InputFileStreamFactory/File1.txt");
            Assert.Equal("; Test file", test.ReadToEnd());
        }
    }
}
