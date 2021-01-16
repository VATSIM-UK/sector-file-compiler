using Xunit;
using Compiler.Model;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class SectorOwnerHierarchyTest
    {
        private readonly SectorOwnerHierarchy model;
        private List<string> owners = new()
        {
            "ONE",
            "TWO",
            "THREE"
        };

        public SectorOwnerHierarchyTest()
        {
            this.model = new SectorOwnerHierarchy(
                this.owners,
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
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
                "OWNER:ONE:TWO:THREE",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}