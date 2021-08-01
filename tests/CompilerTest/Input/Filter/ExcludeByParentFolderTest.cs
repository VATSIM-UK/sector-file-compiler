using System.Collections.Generic;
using Compiler.Input.Filter;
using Xunit;

namespace CompilerTest.Input.Filter
{
    public class ExcludeByParentFolderTest
    {
        [Fact]
        public void ItFiltersOnParentFolderIfMatch()
        {
            Assert.False(
                new ExcludeByParentFolder(new List<string>{"ExcludeByParentFolder"})
                    .Filter("_TestData/ExcludeByParentFolder/Foo.txt")
            );
        }
        
        [Fact]
        public void ItDoesntFilterOnParentFolderIfNotMatch()
        {
            Assert.True(
                new ExcludeByParentFolder(new List<string>{"NotExcludeByParentFolder"})
                    .Filter("_TestData/ExcludeByParentFolder/Foo.txt")
            );
        }
    }
}
