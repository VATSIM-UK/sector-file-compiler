namespace Compiler.Transformer
{
    public class RemoveAllComments : ITransformer
    {
        const char CommentDelimter = ';';

        public string Transform(string data)
        {
            if (!data.Contains(CommentDelimter))
            {
                return data;
            }

            data = data.Substring(0, data.IndexOf(CommentDelimter)).Trim();
            return data == "" ? null : data;
        }
    }
}
