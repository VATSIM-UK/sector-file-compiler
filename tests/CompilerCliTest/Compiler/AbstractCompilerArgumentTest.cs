using System;
using CompilerCli.Compiler;
using Xunit;

namespace CompilerCliTest.Compiler
{
    public class AbstractArgumentTest
    {
        private ConfigFileCompilerArgument compilerArgument;

        public AbstractArgumentTest()
        {
            compilerArgument = new ConfigFileCompilerArgument();
        }

        [Fact]
        public void TestEqualityReturnsFalseNotAbstractArgument()
        {
            Assert.NotEqual(compilerArgument, new object());
        }
        
        [Fact]
        public void TestEqualityReturnsFalseDifferentKeys()
        {
            Assert.NotEqual<AbstractCompilerArgument>(compilerArgument, new StripCommentsCompilerArgument());
        }
        
        [Fact]
        public void TestEqualityReturnsTrueSameObject()
        {
            Assert.Equal(compilerArgument, new ConfigFileCompilerArgument());
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