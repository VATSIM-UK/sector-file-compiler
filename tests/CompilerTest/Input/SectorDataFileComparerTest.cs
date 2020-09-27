using System.Collections.Generic;
using Compiler.Input;
using Xunit;

namespace CompilerTest.Input
{
    public class SectorDataFileComparerTest
    {
        private readonly SectorDataFileComparer comparer;

        public SectorDataFileComparerTest()
        {
            this.comparer = new SectorDataFileComparer();
        }

        public static IEnumerable<object[]> SectorFileData =>
            new List<object[]>
            {
                new object[] { new SectorDataFile("a"), new SectorDataFile("b"), -1 },
                new object[] { new SectorDataFile("b"), new SectorDataFile("a"), 1 },
                new object[] { new SectorDataFile("a"), new SectorDataFile("a"), 0 },
                new object[] { new SectorDataFile("b"), new SectorDataFile("b"), 0 },
                new object[] { new SectorDataFile("abb"), new SectorDataFile("ba"), -1 },
            };

        [Theory]
        [MemberData(nameof(SectorFileData))]
        public void ItComparesSectorDataFiles(AbstractSectorDataFile file1, AbstractSectorDataFile file2, int expected)
        {
            Assert.Equal(expected, this.comparer.Compare(file1, file2));
        }
    }
}
