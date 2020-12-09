namespace Compiler.Input
{
    public class SctSectorDataReader : AbstractSectorDataReader
    {
        private static readonly string commentDelimiter = ";";
        private static readonly string dataDelimiter = " ";

        protected override string GetCommentDelimiter()
        {
            return SctSectorDataReader.commentDelimiter;
        }

        protected override bool FilterExtraWhitespace()
        {
            return true;
        }

        protected override string GetDataDelimiter()
        {
            return SctSectorDataReader.dataDelimiter;
        }
    }
}
