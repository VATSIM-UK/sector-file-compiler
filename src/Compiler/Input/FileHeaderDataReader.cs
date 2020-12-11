namespace Compiler.Input
{
    public class FileHeaderDataReader : AbstractSectorDataReader
    {
        private static readonly string commentDelimiter = ";";
        private static readonly string dataDelimiter = " ";

        protected override string GetCommentDelimiter()
        {
            return FileHeaderDataReader.commentDelimiter;
        }

        protected override bool FilterExtraWhitespace()
        {
            return false;
        }

        protected override string GetDataDelimiter()
        {
            return FileHeaderDataReader.dataDelimiter;
        }
    }
}
