using System;

namespace Compiler.Transformer
{
    public class TokenDateReplacer: ITokenReplacer
    {
        private readonly string token;
        private readonly string format;

        public TokenDateReplacer(string token, string format)
        {
            this.token = token;
            this.format = format;
        }
        
        public string ReplaceTokens(string data)
        {
            return data.Replace(token, DateTime.Now.ToString(format));
        }
    }
}