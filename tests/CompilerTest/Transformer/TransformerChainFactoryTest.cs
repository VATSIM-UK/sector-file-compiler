using System;
using System.Collections.Generic;
using Xunit;
using Compiler.Transformer;
using Compiler.Argument;

namespace CompilerTest.Transformer
{
    public class TransformerChainFactoryTest
    {
        private readonly CompilerArguments arguments;

        public TransformerChainFactoryTest()
        {
            arguments = new CompilerArguments();
        }

        [Fact]
        public void TestItAddsCommentStripper()
        {
            arguments.StripComments = true;
            List<Type> expected = new(new[]
                {
                    typeof(RemoveAllComments),
                    typeof(ReplaceTokens)
                }
            );

            Assert.Equal(
                expected,
                TransformerChainFactory.Create(arguments).GetTransformerTypes()
            );
        }

        [Fact]
        public void TestItDoesntAddCommentStripperIfDisabled()
        {
            List<Type> expected = new(new[]
            {
                typeof(ReplaceTokens)
            });

            Assert.Equal(
                expected,
                TransformerChainFactory.Create(arguments).GetTransformerTypes()
            );
        }
    }
}
