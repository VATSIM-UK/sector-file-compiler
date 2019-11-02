using System;
using System.Collections.Generic;
using System.Text;
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
            List<Type> expected = new List<Type>(new Type[] { typeof(RemoveAllComments) });

            Assert.Equal(
                expected,
                TransformerChainFactory.Create(arguments, OutputSections.ESE_PREAMBLE).GetTransformerTypes()
            );
        }

        [Fact]
        public void TestItDoesntAddCommentStripperOnEseHeader()
        {
            this.arguments.StripComments = true;
            List<Type> expected = new List<Type>();

            Assert.Equal(
                expected,
                TransformerChainFactory.Create(arguments, OutputSections.ESE_HEADER).GetTransformerTypes()
            );
        }

        [Fact]
        public void TestItAddsBlankLineStripper()
        {
            this.arguments.RemoveBlankLines = true;
            List<Type> expected = new List<Type>(new Type[] { typeof(RemoveBlankLines) });

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
            List<Type> expected = new List<Type>(new Type[] { typeof(RemoveAllComments), typeof(RemoveBlankLines) });

            Assert.Equal(
                expected,
                TransformerChainFactory.Create(arguments, OutputSections.ESE_PREAMBLE).GetTransformerTypes()
            );
        }
    }
}
