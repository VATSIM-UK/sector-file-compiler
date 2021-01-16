using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Model;
using Compiler.Output;
using CompilerTest.Bogus.Factory;
using Xunit;

namespace CompilerTest.Output
{
    public class OutputGroupRepositoryTest
    {
        private OutputGroupRepository repository;
        private OutputGroup group1;
        private OutputGroup group2;
        private OutputGroup group3;

        public OutputGroupRepositoryTest()
        {
            this.group1 = new OutputGroup("test1");
            this.group2 = new OutputGroup("test2");
            this.group3 = new OutputGroup("test3");
            this.repository = new OutputGroupRepository();
        }

        public void TestItAddsGroups()
        {
            this.repository.Add(this.group1);
            this.repository.Add(this.group2);
            this.repository.Add(this.group3);
            Assert.Equal(3, this.repository.Count());
        }
        
        public void TestItDoesntAddGroupsWithDuplicateKeys()
        {
            this.repository.Add(this.group1);
            this.repository.Add(this.group2);
            this.repository.Add(this.group1);
            Assert.Equal(2, this.repository.Count());
        }

        public void TestItReturnsGroupForDefinition()
        {
            this.repository.Add(this.group1);
            this.repository.Add(this.group2);
            this.repository.Add(this.group3);
            
            Definition definition = DefinitionFactory.Make();
            this.group2.AddFile(definition.Filename);
            
            Assert.Equal(this.group2, this.repository.GetForDefinitionFile(definition));
        }
        
        public void TestItTriesReturnsGroupForDefinition()
        {
            this.repository.Add(this.group1);
            this.repository.Add(this.group2);
            this.repository.Add(this.group3);
            
            Definition definition = DefinitionFactory.Make();
            this.group2.AddFile(definition.Filename);

            Assert.True(this.repository.TryGetForDefinitionFile(definition, out OutputGroup group));
            Assert.Equal(this.group2, group);
        }
        
        public void TestItTriesReturnNullIfGroupNotFound()
        {
            this.repository.Add(this.group1);
            this.repository.Add(this.group2);
            this.repository.Add(this.group3);
            
            Definition definition = DefinitionFactory.Make();

            Assert.False(this.repository.TryGetForDefinitionFile(definition, out OutputGroup group));
            Assert.Null(group);
        }
    }
}
