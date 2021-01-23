using Compiler.Argument;

namespace Compiler.Transformer
{
    public static class TokenBuildVersionReplacerFactory
    {
        public static TokenBuildVersionReplacer Make(CompilerArguments arguments, string token)
        {
            return new TokenBuildVersionReplacer(arguments, token);
        }
    }
}