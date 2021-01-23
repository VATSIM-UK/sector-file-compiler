using System.Text.RegularExpressions;

namespace Compiler.Transformer
{
    public class RemoveAllComments : ITransformer
    {
        const char CommentDelimter = ';';
        private readonly Regex annotationPattern = new(@";\s*@preserveComment\s");

        public string Transform(string data)
        {
            if (!data.Contains(CommentDelimter))
            {
                return data;
            }

            data = FormatComment(data);
            return data.Length == 0 ? null : data;
        }

        private string FormatComment(string data)
        {
            int commentIndex = data.IndexOf(CommentDelimter);
            return annotationPattern.IsMatch(data.Substring(commentIndex))
                ? RemoveCommentAnnotation(data, commentIndex).Trim()
                : data.Substring(0, commentIndex).Trim();
        }

        private string RemoveCommentAnnotation(string data, int commentIndex)
        {
            return
                $"{data.Substring(0, commentIndex).Trim()} ; {annotationPattern.Replace(data.Substring(commentIndex), " ").Trim()}";
        }
    }
}
