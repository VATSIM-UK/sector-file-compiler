using System;
using CompilerCli.Cli;
using Xunit;

namespace CompilerCliTest.Cli
{
    public class AbstractCliArgumentTest
    {
        private NoWaitCliArgument compilerArgument;

        public AbstractCliArgumentTest()
        {
            compilerArgument = new NoWaitCliArgument();
        }

        [Fact]
        public void TestEqualityReturnsFalseNotAbstractArgument()
        {
            Assert.NotEqual(compilerArgument, new object());
        }
        
        [Fact]
        public void TestEqualityReturnsFalseDifferentKeys()
        {
            Assert.NotEqual<AbstractCliArgument>(compilerArgument, new DefaultCliArgument());
        }
        
        [Fact]
        public void TestEqualityReturnsTrueSameObject()
        {
            Assert.Equal(compilerArgument, new NoWaitCliArgument());
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
            var defaultArgument = new DefaultCliArgument();
            Assert.Equal(
                string.Compare(compilerArgument.GetSpecifier(), defaultArgument.GetSpecifier(), StringComparison.Ordinal),
                compilerArgument.CompareTo(defaultArgument)
            );
        }
    }
}