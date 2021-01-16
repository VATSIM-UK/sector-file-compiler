using System.Collections.Generic;
using System.Linq;
using Compiler.Input;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Input
{
    public class InputFileListTest
    {
        private readonly AbstractSectorDataFile file1;
        private readonly AbstractSectorDataFile file2;
        private readonly InputFileList fileList;

        public InputFileListTest()
        {
            this.file1 = SectorDataFileFactoryFactory.Make(new List<string>()).Create("test.txt", InputDataType.ESE_AGREEMENTS);
            this.file2 = SectorDataFileFactoryFactory.Make(new List<string>()).Create("test2.txt", InputDataType.ESE_AGREEMENTS);
            this.fileList = new InputFileList();
        }

        [Fact]
        public void TestItAddsFiles()
        {
            this.fileList.Add(file1);
            this.fileList.Add(file2);

            Assert.Equal(2, this.fileList.Count());
            using var enumerator = this.fileList.GetEnumerator();
            enumerator.MoveNext();
            Assert.Same(file1, enumerator.Current);
            enumerator.MoveNext();
            Assert.Same(file2, enumerator.Current);
        }
        
        [Fact]
        public void TestItDoesntAddDuplicateFiles()
        {
            this.fileList.Add(file1);
            this.fileList.Add(file1);
            this.fileList.Add(file1);
            this.fileList.Add(file2);
            this.fileList.Add(file2);
            this.fileList.Add(file2);

            Assert.Equal(2, this.fileList.Count());
            using var enumerator = this.fileList.GetEnumerator();
            enumerator.MoveNext();
            Assert.Same(file1, enumerator.Current);
            enumerator.MoveNext();
            Assert.Same(file2, enumerator.Current);
        }
    }
}
