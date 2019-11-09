namespace Compiler.Transformer
{
    public class RemoveAllComments : ITransformer
    {
        const char COMMENT_DELIMTER = ';';

        public string Transform(string data)
        {
            if (!data.Contains(RemoveAllComments.COMMENT_DELIMTER))
            {
                return data;
            }

            int commentPos = data.IndexOf(RemoveAllComments.COMMENT_DELIMTER);
            return data.Substring(0, commentPos).Trim();
        }
    }
}
