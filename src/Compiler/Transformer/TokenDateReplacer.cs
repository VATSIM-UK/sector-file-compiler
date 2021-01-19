using System;

namespace Compiler.Transformer
{
    public class TokenDateReplacer: ITokenReplacer
    {
        public string Token { get; }
        public string Format { get; }

        public TokenDateReplacer(string token, string format)
        {
            this.Token = token;
            this.Format = format;
        }
        
        public string ReplaceTokens(string data)
        {
            return data.Replace(Token, DateTime.Now.ToString(Format));
        }
    }
}