using System;
using System.Collections.Generic;
using Xunit;
using Compiler.Transformer;
using Compiler.Argument;

namespace CompilerTest.Transformer
{
    public class TransformerChainFactoryTest
    {
        [Fact]
        public void TestItAddsTransformers()
        {
            List<Type> expected = new(new[]
                {
                    typeof(RemoveAllComments),
                    typeof(ReplaceTokens)
                }
            );

            Assert.Equal(
                expected,
                TransformerChainFactory.Create(CompilerArgumentsFactory.Make()).GetTransformerTypes()
            );
        }
    }
}
