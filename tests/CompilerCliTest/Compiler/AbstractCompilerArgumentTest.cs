using System;
using CompilerCli.Compiler;
using Xunit;

namespace CompilerCliTest.Compiler
{
    public class AbstractArgumentTest
    {
        private AbstractCompilerArgument compilerArgument;

        public AbstractArgumentTest()
        {
            compilerArgument = new ConfigFileCompilerArgument();
        }

        [Fact]
        public void TestEqualityReturnsFalseNotAbstractArgument()
        {
            Assert.False(compilerArgument.Equals(new object()));
        }
        
        [Fact]
        public void TestEqualityReturnsFalseDifferentKeys()
        {
            Assert.False(compilerArgument.Equals(new StripCommentsCompilerArgument()));
        }
        
        [Fact]
        public void TestEqualityReturnsTrueSameObject()
        {
            Assert.True(compilerArgument.Equals(new ConfigFileCompilerArgument()));
        }

        [Fact]
        public void TestHashCodeReturnsHashOfSpecifier()
        {
            Assert.Equal(compilerArgument.GetSpecifier().GetHashCode(), compilerArgument.GetHashCode());
        }

        [Fact]
        public void TestCompareToReturns1IfObjectNull()
        {
            Assert.Equal(1, compilerArgument.CompareTo(null));
        }
        
        [Fact]
        public void TestCompareToReturns1IfObjectNotAbstractArgument()
        {
            Assert.Equal(1, compilerArgument.CompareTo(new object()));
        }
        
        [Fact]
        public void TestCompareToReturnsComparisonOfSpecifiers()
        {
            var skipValidationArgument = new SkipValidationCompilerArgument();
            Assert.Equal(
                string.Compare(compilerArgument.GetSpecifier(), skipValidationArgument.GetSpecifier(), StringComparison.Ordinal),
                compilerArgument.CompareTo(skipValidationArgument)
            );
        }
    }
}