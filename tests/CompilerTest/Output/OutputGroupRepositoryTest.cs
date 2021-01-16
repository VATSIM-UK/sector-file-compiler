using System.Collections.Generic;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Output
{
    public class OutputGroupRepositoryTest
    {
        private readonly OutputGroupRepository repository;
        private readonly OutputGroup group1;
        private readonly OutputGroup group2;
        private readonly OutputGroup group3;

        public OutputGroupRepositoryTest()
        {
            this.group1 = new OutputGroup("test1");
            this.group2 = new OutputGroup("test2");
            this.group3 = new OutputGroup("test3");
            this.repository = new OutputGroupRepository();
        }

        public void TestItAddsGroups()
        {
            this.repository.AddGroupWithFiles(this.group1, new List<string>{"foo.txt"});
            this.repository.AddGroupWithFiles(this.group2, new List<string>{"bar.txt"});
            this.repository.AddGroupWithFiles(this.group3, new List<string>{"baz.txt"});
            Assert.Equal(3, this.repository.Count());
        }
        
        public void TestItDoesntAddGroupsWithDuplicateKeys()
        {
            this.repository.AddGroupWithFiles(this.group1, new List<string>{"foo.txt"});
            this.repository.AddGroupWithFiles(this.group2, new List<string>{"bar.txt"});
            this.repository.AddGroupWithFiles(this.group1, new List<string>{"baz.txt"});
            Assert.Equal(2, this.repository.Count());
        }

        public void TestItReturnsGroupForDefinition()
        {
            Definition definition = DefinitionFactory.Make();
            this.repository.AddGroupWithFiles(this.group1, new List<string>{"foo.txt"});
            this.repository.AddGroupWithFiles(this.group2, new List<string>{definition.Filename});
            this.repository.AddGroupWithFiles(this.group3, new List<string>{"baz.txt"});

            Assert.Equal(this.group2, this.repository.GetForDefinitionFile(definition));
        }
        
        public void TestItTriesReturnsGroupForDefinition()
        {
            Definition definition = DefinitionFactory.Make();
            this.repository.AddGroupWithFiles(this.group1, new List<string>{"foo.txt"});
            this.repository.AddGroupWithFiles(this.group2, new List<string>{definition.Filename});
            this.repository.AddGroupWithFiles(this.group3, new List<string>{"baz.txt"});

            Assert.True(this.repository.TryGetForDefinitionFile(definition, out OutputGroup group));
            Assert.Equal(this.group2, group);
        }
        
        public void TestItTriesReturnNullIfGroupNotFound()
        {
            Definition definition = DefinitionFactory.Make();
            this.repository.AddGroupWithFiles(this.group1, new List<string>{"foo.txt"});
            this.repository.AddGroupWithFiles(this.group2, new List<string>{definition.Filename});
            this.repository.AddGroupWithFiles(this.group3, new List<string>{"baz.txt"});

            Assert.False(this.repository.TryGetForDefinitionFile(definition, out OutputGroup group));
            Assert.Null(group);
        }
    }
}
