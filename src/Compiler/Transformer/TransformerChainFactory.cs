using Compiler.Argument;
using Compiler.Output;

namespace Compiler.Transformer
{
    public class TransformerChainFactory
    {
        public static TransformerChain Create(CompilerArguments arguments, OutputSectionKeys section)
        {
            TransformerChain chain = new TransformerChain();

            if (arguments.StripComments && section != OutputSectionKeys.FILE_HEADER)
            {
                chain.AddTransformer(new RemoveAllComments());
            }

            chain.AddTransformer(ReplaceTokensFactory.Make(arguments));

            return chain;
        }
    }
}
