using System;
using System.Collections.Generic;

namespace Compiler.Transformer
{
    public class TransformerChain
    {
        private readonly List<ITransformer> transformers = new List<ITransformer>();

        public void AddTransformer(ITransformer transformer)
        {
            this.transformers.Add(transformer);
        }

        public List<Type> GetTransformerTypes()
        {
            List<Type> types = new List<Type>();
            foreach (ITransformer transformer in this.transformers)
            {
                types.Add(transformer.GetType());
            }

            return types;
        }

        public List<string> Transform(List<string> lines)
        {
            foreach (ITransformer transformer in transformers)
            {
                lines = transformer.Transform(lines);
            }

            return lines;
        }
    }
}
