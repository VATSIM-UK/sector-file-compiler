using System;
using CompilerCli.Cli;
using Xunit;

namespace CompilerCliTest.Cli
{
    public class AbstractCliArgumentTest
    {
        private AbstractCliArgument cliArgument;

        public AbstractCliArgumentTest()
        {
            cliArgument = new NoWaitCliArgument();
        }

        [Fact]
        public void TestEqualityReturnsFalseNotAbstractArgument()
        {
            Assert.False(cliArgument.Equals(new object()));
        }
        
        [Fact]
        public void TestEqualityReturnsFalseDifferentKeys()
        {
            Assert.False(cliArgument.Equals(new DefaultCliArgument()));
        }
        
        [Fact]
        public void TestEqualityReturnsTrueSameObject()
        {
            Assert.True(cliArgument.Equals(new NoWaitCliArgument()));
        }
        [Fact]
        public void TestHashCodeReturnsHashOfSpecifier()
        {
            Assert.Equal(cliArgument.GetSpecifier().GetHashCode(), cliArgument.GetHashCode());
        }

        [Fact]
        public void TestCompareToReturns1IfObjectNull()
        {
            Assert.Equal(1, cliArgument.CompareTo(null));
        }
        
        [Fact]
        public void TestCompareToReturns1IfObjectNotAbstractArgument()
        {
            Assert.Equal(1, cliArgument.CompareTo(new object()));
        }
        
        [Fact]
        public void TestCompareToReturnsComparisonOfSpecifiers()
        {
            var defaultArgument = new DefaultCliArgument();
            Assert.Equal(
                string.Compare(cliArgument.GetSpecifier(), defaultArgument.GetSpecifier(), StringComparison.Ordinal),
                cliArgument.CompareTo(defaultArgument)
            );
        }
    }
}