using System.Collections.Generic;
using Compiler.Argument;

namespace Compiler.Transformer
{
    public static class ReplaceTokensFactory
    {
        public static ReplaceTokens Make(CompilerArguments arguments)
        {
            return new ReplaceTokens(arguments.TokenReplacers);
        }
    }
}