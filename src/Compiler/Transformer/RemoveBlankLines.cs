using System.Collections.Generic;

namespace Compiler.Transformer
{
    /**
     * Transformer to remove all lines that are blank and contain no content.
     */
    public class RemoveBlankLines : ITransformer
    {
        public string Transform(string data)
        {
            return data.Trim() == "" ? "" : data;
        }
    }
}
