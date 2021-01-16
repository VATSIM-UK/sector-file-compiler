namespace Compiler.Transformer
{
    public class RemoveAllComments : ITransformer
    {
        const char CommentDelimter = ';';

        public string Transform(string data)
        {
            if (!data.Contains(RemoveAllComments.CommentDelimter))
            {
                return data;
            }

            int commentPos = data.IndexOf(RemoveAllComments.CommentDelimter);
            return data.Substring(0, commentPos).Trim();
        }
    }
}
