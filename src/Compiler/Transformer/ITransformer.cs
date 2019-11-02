using System.Collections.Generic;

namespace Compiler.Transformer
{
    public interface ITransformer
    {
        public List<string> Transform(List<string> lines);
    }
}
