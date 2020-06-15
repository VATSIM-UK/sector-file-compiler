using Xunit;
using Compiler.Model;
using System.Collections.Generic;

namespace CompilerTest.Model
{
    public class SectorOwnerHierarchyTest
    {
        private readonly SectorOwnerHierarchy model;
        private List<string> owners = new List<string>
        {
            "ONE",
            "TWO",
            "THREE"
        };

        public SectorOwnerHierarchyTest()
        {
            this.model = new SectorOwnerHierarchy(
                this.owners,
                "comment"
            );
        }

        [Fact]
        public void TestItSetsOwners()
        {
            Assert.Equal(this.owners, this.model.Owners);
        }

        [Fact]
        public void TestItCompiles()
        {
            Assert.Equal(
                "OWNER:ONE:TWO:THREE ;comment\r\n",
                this.model.Compile()
            );
        }
    }
}