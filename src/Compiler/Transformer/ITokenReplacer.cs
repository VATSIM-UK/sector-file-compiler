namespace Compiler.Transformer
{
    public interface ITokenReplacer
    {
        /**
         * Replaces instances of a given token in a given string
         */
        public string ReplaceTokens(string data);
    }
}