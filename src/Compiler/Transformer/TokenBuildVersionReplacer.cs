using Compiler.Argument;

namespace Compiler.Transformer
{
    public class TokenBuildVersionReplacer: ITokenReplacer
    {
        private readonly CompilerArguments arguments;
        public string Token { get; }

        public TokenBuildVersionReplacer(CompilerArguments arguments, string token)
        {
            this.arguments = arguments;
            this.Token = token;
        }

        public string ReplaceTokens(string data)
        {
            return data.Replace(Token, arguments.BuildVersion);
        }
    }
}