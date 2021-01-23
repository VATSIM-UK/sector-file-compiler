using Xunit;
using Compiler.Model;
using System.Collections.Generic;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Model
{
    public class SectorAlternateOwnerHierarchyTest
    {
        private readonly SectorAlternateOwnerHierarchy model;
        private readonly List<string> owners = new()
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
                DefinitionFactory.Make(),
                DocblockFactory.Make(),
                CommentFactory.Make()
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
                "ALTOWNER:TEST:ONE:TWO:THREE",
                this.model.GetCompileData(new SectorElementCollection())
            );
        }
    }
}