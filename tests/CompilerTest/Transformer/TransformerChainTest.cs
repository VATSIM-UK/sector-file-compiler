using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using Compiler.Transformer;

namespace CompilerTest.Transformer
{
    public class TransformerChainTest
    {
        [Fact]
        public void TestItAppliesAllTransformers()
        {
            Mock<ITransformer> transformer1 = new Mock<ITransformer>();
            Mock<ITransformer> transformer2 = new Mock<ITransformer>();
            TransformerChain chain = new TransformerChain();
            chain.AddTransformer(transformer1.Object);
            chain.AddTransformer(transformer2.Object);

            List<String> expectedStep1 = new List<string>(new string[] { "a", "b" });
            List<String> expectedStep2 = new List<string>(new string[] { "c", "d" });
            List<String> expectedFinal = new List<string>(new string[] { "e", "f" });

            transformer1.Setup(foo => foo.Transform(expectedStep1)).Returns(expectedStep2);
            transformer2.Setup(foo => foo.Transform(expectedStep2)).Returns(expectedFinal);

            Assert.Equal(expectedFinal, chain.Transform(expectedStep1));
        }
    }
}
