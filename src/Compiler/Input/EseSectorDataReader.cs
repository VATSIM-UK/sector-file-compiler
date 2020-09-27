using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    public class EseSectorDataReader : AbstractSectorDataReader
    {
        private static readonly string commentDelimiter = ";";
        private static readonly string dataDelimiter = ":";

        protected override string GetCommentDelimiter()
        {
            return EseSectorDataReader.commentDelimiter;
        }

        protected override string GetDataDelimiter()
        {
            return EseSectorDataReader.dataDelimiter;
        }
    }
}
