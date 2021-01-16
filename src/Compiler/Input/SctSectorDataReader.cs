namespace Compiler.Input
{
    public class SctSectorDataReader : AbstractSectorDataReader
    {
        private static readonly string CommentDelimiter = ";";
        private static readonly string DataDelimiter = " ";

        protected override string GetCommentDelimiter()
        {
            return SctSectorDataReader.CommentDelimiter;
        }

        protected override bool FilterExtraWhitespace()
        {
            return true;
        }

        protected override string GetDataDelimiter()
        {
            return SctSectorDataReader.DataDelimiter;
        }
    }
}
