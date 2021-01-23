using System;
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
            Mock<ITransformer> transformer1 = new();
            Mock<ITransformer> transformer2 = new();
            TransformerChain chain = new();
            chain.AddTransformer(transformer1.Object);
            chain.AddTransformer(transformer2.Object);

            string expectedStep1 = new("a");
            string expectedStep2 = new("b");
            string expectedFinal = new("c");

            transformer1.Setup(foo => foo.Transform(expectedStep1)).Returns(expectedStep2);
            transformer2.Setup(foo => foo.Transform(expectedStep2)).Returns(expectedFinal);

            Assert.Equal(expectedFinal, chain.Transform(expectedStep1));
        }
        
        [Fact]
        public void TestItSkipsTransformersIfOneReturnsNull()
        {
            Mock<ITransformer> transformer1 = new();
            Mock<ITransformer> transformer2 = new();
            TransformerChain chain = new();
            chain.AddTransformer(transformer1.Object);
            chain.AddTransformer(transformer2.Object);

            string expectedStep1 = new("a");

            transformer1.Setup(foo => foo.Transform("a")).Returns<string>(null);

            Assert.Null(chain.Transform(expectedStep1));
            transformer2.Verify(foo => foo.Transform(It.IsAny<String>()), Times.Never);
        }
    }
}
