using System.Collections.Generic;

namespace Compiler.Transformer
{
    public interface ITransformer
    {
        public string Transform(string data);
    }
}
