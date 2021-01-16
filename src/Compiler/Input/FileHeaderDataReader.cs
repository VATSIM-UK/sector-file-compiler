namespace Compiler.Input
{
    public class FileHeaderDataReader : AbstractSectorDataReader
    {
        private static readonly string CommentDelimiter = ";";
        private static readonly string DataDelimiter = " ";

        protected override string GetCommentDelimiter()
        {
            return FileHeaderDataReader.CommentDelimiter;
        }

        protected override bool FilterExtraWhitespace()
        {
            return false;
        }

        protected override string GetDataDelimiter()
        {
            return FileHeaderDataReader.DataDelimiter;
        }
    }
}
