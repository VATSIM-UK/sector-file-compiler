using System.Collections.Generic;

namespace Compiler.Transformer
{
    /**
     * Transformer to remove all lines that are blank and contain no content.
     */
    public class RemoveBlankLines : ITransformer
    {
        public List<string> Transform(List<string> lines)
        {
            lines.RemoveAll(line => line.Trim() == "");
            return lines;
        }
    }
}
