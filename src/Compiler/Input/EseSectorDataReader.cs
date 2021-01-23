namespace Compiler.Input
{
    public class EseSectorDataReader : AbstractSectorDataReader
    {
        private static readonly string CommentDelimiter = ";";
        private static readonly string DataDelimiter = ":";

        protected override string GetCommentDelimiter()
        {
            return EseSectorDataReader.CommentDelimiter;
        }

        protected override bool FilterExtraWhitespace()
        {
            return false;
        }

        protected override string GetDataDelimiter()
        {
            return EseSectorDataReader.DataDelimiter;
        }
    }
}
