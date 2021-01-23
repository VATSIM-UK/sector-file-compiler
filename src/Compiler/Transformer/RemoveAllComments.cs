using System.Text.RegularExpressions;

namespace Compiler.Transformer
{
    public class RemoveAllComments : ITransformer
    {
        private readonly bool removeComments;
        const char CommentDelimiter = ';';
        private readonly Regex annotationPattern = new(@";\s*@preserveComment(\s|$)");

        public RemoveAllComments(bool removeComments)
        {
            this.removeComments = removeComments;
        }
        
        public string Transform(string data)
        {
            if (!data.Contains(CommentDelimiter))
            {
                return data;
            }

            int commentIndex = GetCommentIndex(data);
            return ShouldPreserveComment(data, commentIndex)
                ? RemoveCommentAnnotation(data, commentIndex)
                : HandleComment(data, commentIndex);
        }

        /**
         * Get the index of any comment
         */
        private int GetCommentIndex(string data)
        {
            return data.IndexOf(CommentDelimiter);
        }

        /**
         * Check if we have the @preserveComment annotation and thus should
         * preserve the comment.
         */
        private bool ShouldPreserveComment(string data, int commentIndex)
        {
            return annotationPattern.IsMatch(data.Substring(commentIndex));
        }

        /**
         * Remove the entire comment, returning null if no data is left.
         * Leave be, if argument not set.
         */
        private string HandleComment(string data, int commentIndex)
        {
            if (!removeComments)
            {
                return data;
            }

            string formatted = GetDataPart(data, commentIndex);
            return formatted.Length == 0 ? null : formatted;
        }

        /**
         * Remove the @preserveComment annotation from the data.
         */
        private string RemoveCommentAnnotation(string data, int commentIndex)
        {
            string dataPart = GetDataPart(data, commentIndex);
            return $"{dataPart}{GetCommentPart(dataPart, ReplaceAnnotationInComment(data, commentIndex))}";
        }

        private string GetDataPart(string data, int commentIndex)
        {
            return data.Substring(0, commentIndex).TrimEnd();
        }
        
        private string ReplaceAnnotationInComment(string data, int commentIndex)
        {
            return annotationPattern.Replace(data.Substring(commentIndex), "").Trim();
        }

        private string GetCommentPart(string dataPart, string replacedAnnotation)
        {
            return dataPart.Length == 0
                ? $"; {replacedAnnotation}".Trim()
                : $" ; {replacedAnnotation}".TrimEnd();
        }
    }
}
