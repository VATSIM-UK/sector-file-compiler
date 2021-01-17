using System.Collections.Generic;

namespace Compiler.Transformer
{
    /**
     * Replaces all tokens in a given set of lines
     */
    public class ReplaceTokens : ITransformer
    {
        private readonly IEnumerable<ITokenReplacer> replacers;
        public ReplaceTokens(IEnumerable<ITokenReplacer> replacers)
        {
            this.replacers = replacers;
        }

        public string Transform(string data)
        {
            foreach (var replacer in replacers)
            {
                data = replacer.ReplaceTokens(data);
            }

            return data;
        }
    }
}
