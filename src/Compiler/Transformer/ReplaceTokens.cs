using System.Collections.Generic;

namespace Compiler.Transformer
{
    /**
     * Replaces all tokens in a given set of lines
     */
    public class ReplaceTokens : ITransformer
    {
        private readonly Dictionary<string, string> tokens;
        public ReplaceTokens(Dictionary<string, string> tokens)
        {
            this.tokens = tokens;
        }

        public string Transform(string data)
        {
            foreach (KeyValuePair<string, string> token in this.tokens)
            {
                data = data.Replace(token.Key, token.Value);
            }

            return data;
        }
    }
}
