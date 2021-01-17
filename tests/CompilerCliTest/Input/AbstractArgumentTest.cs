using System;
using System.Collections.Generic;
using CompilerCli.Input;
using Xunit;

namespace CompilerCliTest.Input
{
    public class AbstractArgumentTest
    {
        private ConfigFileArgument argument;

        public AbstractArgumentTest()
        {
            argument = new ConfigFileArgument();
        }

        public static IEnumerable<object[]> EqualityData => new List<object[]>
        {
            new object[]
            {
                
            }, // Bad number of segments
            new object[] { new List<string>{
                "\"test label\" N050.57.00.000 N001.21.24.490 red"
            }}, // Bad coordinate
            new object[] { new List<string>{
                "\"test label\"\" N050.57.00.000 N001.21.24.490 red"
            }}, // Too many quotes
            new object[] { new List<string>{
                "abc\"test label\" N050.57.00.000 N001.21.24.490 red"
            }}, // Doesnt start with quotes
        };
        
        [Fact]
        public void TestEqualityReturnsFalseNotAbstractArgument()
        {
            Assert.NotEqual(argument, new object());
        }
        
        [Fact]
        public void TestEqualityReturnsFalseDifferentKeys()
        {
            Assert.NotEqual<AbstractArgument>(argument, new StripCommentsArgument());
        }
        
        [Fact]
        public void TestEqualityReturnsTrueSameObject()
        {
            Assert.Equal(argument, new ConfigFileArgument());
        }

        [Fact]
        public void TestHashCodeReturnsHashOfSpecifier()
        {
            Assert.Equal(argument.GetSpecifier().GetHashCode(), argument.GetHashCode());
        }

        [Fact]
        public void TestCompareToReturns1IfObjectNull()
        {
            Assert.Equal(1, argument.CompareTo(null));
        }
        
        [Fact]
        public void TestCompareToReturns1IfObjectNotAbstractArgument()
        {
            Assert.Equal(1, argument.CompareTo(new object()));
        }
        
        [Fact]
        public void TestCompareToReturnsComparisonOfSpecifiers()
        {
            var skipValidationArgument = new SkipValidationArgument();
            Assert.Equal(
                string.Compare(argument.GetSpecifier(), skipValidationArgument.GetSpecifier(), StringComparison.Ordinal),
                argument.CompareTo(skipValidationArgument)
            );
        }
    }
}