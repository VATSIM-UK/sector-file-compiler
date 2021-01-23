namespace Compiler.Transformer
{
    public interface ITransformer
    {
        /**
         * Transform a line of output data. If null is returned, the line should be discarded.
         */
        public string Transform(string data);
    }
}
