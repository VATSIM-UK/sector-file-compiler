using Xunit;
using Compiler.Model;
using System.Collections.Generic;

namespace CompilerTest.Model
{
    public class SectorAlternateOwnerHierarchyTest
    {
        private readonly SectorAlternateOwnerHierarchy model;
        private List<string> owners = new List<string>
        {
            "ONE",
            "TWO",
            "THREE"
        };

        public SectorAlternateOwnerHierarchyTest()
        {
            this.model = new SectorAlternateOwnerHierarchy(
                "TEST",
                this.owners,
                "comment"
            );
        }

        [Fact]
        public void TestItSetsName()
        {
            Assert.Equal("TEST", this.model.Name);
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
                "ALTOWNER:TEST:ONE:TWO:THREE ;comment\r\n",
                this.model.Compile()
            );
        }
    }
}