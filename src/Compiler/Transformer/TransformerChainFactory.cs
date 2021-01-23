using Compiler.Argument;

namespace Compiler.Transformer
{
    public static class TransformerChainFactory
    {
        public static TransformerChain Create(CompilerArguments arguments)
        {
            return AddRemoveAllComments(
                new TransformerChain(),
                arguments
            ).AddTransformer(ReplaceTokensFactory.Make(arguments));
        }

        private static TransformerChain AddRemoveAllComments(TransformerChain chain, CompilerArguments arguments)
        {
            return arguments.StripComments
                ? chain.AddTransformer(new RemoveAllComments())
                : chain;
        }
    }
}
