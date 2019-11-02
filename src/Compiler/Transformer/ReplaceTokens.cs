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

        public List<string> Transform(List<string> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                foreach(KeyValuePair<string, string> token in this.tokens)
                {
                    lines[i] = lines[i].Replace(token.Key, token.Value);
                }
            }

            return lines;
        }
    }
}
