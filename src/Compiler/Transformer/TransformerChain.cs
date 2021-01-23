using System;
using System.Collections.Generic;
using System.Linq;

namespace Compiler.Transformer
{
    public class TransformerChain
    {
        private readonly List<ITransformer> transformers = new();

        public TransformerChain AddTransformer(ITransformer transformer)
        {
            transformers.Add(transformer);
            return this;
        }

        public List<Type> GetTransformerTypes()
        {
            return transformers.Select(transformer => transformer.GetType()).ToList();
        }

        public string Transform(string data)
        {
            foreach (ITransformer transformer in transformers)
            {
                if ((data = transformer.Transform(data)) == null)
                {
                    return null;
                }
            }

            return data;
        }
    }
}
