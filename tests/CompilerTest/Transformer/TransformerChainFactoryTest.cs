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
            this.arguments = new CompilerArguments();
        }

        [Fact]
        public void TestItAddsCommentStripper()
        {
            this.arguments.StripComments = true;
            List<Type> expected = new List<Type>(new Type[] { typeof(RemoveAllComments), typeof(ReplaceTokens) });

            Assert.Equal(
                expected,
                TransformerChainFactory.Create(arguments, OutputSections.ESE_PREAMBLE).GetTransformerTypes()
            );
        }

        [Fact]
        public void TestItDoesntAddCommentStripperOnEseHeader()
        {
            this.arguments.StripComments = true;
            List<Type> expected = new List<Type>(new Type[] { typeof(ReplaceTokens) });

            Assert.Equal(
                expected,
                TransformerChainFactory.Create(arguments, OutputSections.ESE_HEADER).GetTransformerTypes()
            );
        }

        [Fact]
        public void TestItAddsBlankLineStripper()
        {
            this.arguments.RemoveBlankLines = true;
            List<Type> expected = new List<Type>(new Type[] { typeof(RemoveBlankLines), typeof(ReplaceTokens) });

            Assert.Equal(
                expected,
                TransformerChainFactory.Create(arguments, OutputSections.ESE_PREAMBLE).GetTransformerTypes()
            );
        }

        [Fact]
        public void TestItAddsMultipleTransformers ()
        {
            this.arguments.RemoveBlankLines = true;
            this.arguments.StripComments = true;
            List<Type> expected = new List<Type>(new Type[] { typeof(RemoveAllComments), typeof(RemoveBlankLines), typeof(ReplaceTokens) });

            Assert.Equal(
                expected,
                TransformerChainFactory.Create(arguments, OutputSections.ESE_PREAMBLE).GetTransformerTypes()
            );
        }
    }
}
