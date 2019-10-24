using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Error
{
    public struct CompilerError
    {
        public CompilerError(
            ErrorType type,
            ErrorCode code,
            string fileName,
            int itemNumber,
            string message
        ) {
            this.type = type;
            this.code = code;
            this.fileName = fileName;
            this.itemNumber = itemNumber;
            this.message = message;
        }

        public readonly ErrorType type;

        public readonly ErrorCode code;

        public readonly string fileName;

        public readonly int itemNumber;

        public readonly string message;
    }
}
