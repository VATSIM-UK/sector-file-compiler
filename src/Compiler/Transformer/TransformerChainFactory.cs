using Compiler.Argument;
using Compiler.Output;

namespace Compiler.Transformer
{
    public class TransformerChainFactory
    {
        public static TransformerChain Create(CompilerArguments arguments, OutputSections section)
        {
            TransformerChain chain = new TransformerChain();

            if (arguments.StripComments && section != OutputSections.ESE_HEADER)
            {
                chain.AddTransformer(new RemoveAllComments());
            }

            if (arguments.RemoveBlankLines)
            {
                chain.AddTransformer(new RemoveBlankLines());
            }

            chain.AddTransformer(new ReplaceTokens(SystemTokensFactory.GetSystemTokens(arguments)));

            return chain;
        }
    }
}
