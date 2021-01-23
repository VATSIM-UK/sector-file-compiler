using Compiler.Argument;

namespace Compiler.Transformer
{
    public static class TransformerChainFactory
    {
        public static TransformerChain Create(CompilerArguments arguments)
        {
            return MakeNewChain()
                .AddTransformer(RemoveAllCommentsFactory.Make(arguments))
                .AddTransformer(ReplaceTokensFactory.Make(arguments));
        }

        private static TransformerChain MakeNewChain()
        {
            return new ();
        }
    }
}
