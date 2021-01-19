using System;
using System.Collections.Generic;
using Xunit;
using Compiler.Transformer;
using Compiler.Argument;
using Compiler.Output;

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
            this.arguments.StripComments = true;
            List<Type> expected = new(new[]
                {
                    typeof(RemoveAllComments),
                    typeof(ReplaceTokens),
                    typeof(TokenBuildVersionReplacer)
                }
            );

            Assert.Equal(
                expected,
                TransformerChainFactory.Create(arguments, OutputSectionKeys.SCT_VOR).GetTransformerTypes()
            );
        }

        [Fact]
        public void TestItDoesntAddCommentStripperOnHeader()
        {
            this.arguments.StripComments = true;
            List<Type> expected = new(new[]
            {
                typeof(ReplaceTokens),
                typeof(TokenBuildVersionReplacer)
            });

            Assert.Equal(
                expected,
                TransformerChainFactory.Create(arguments, OutputSectionKeys.FILE_HEADER).GetTransformerTypes()
            );
        }
    }
}
