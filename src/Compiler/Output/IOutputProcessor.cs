using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;

namespace Compiler.Output
{
    public interface IOutputProcessor
    {
        /*
         * Prints a single inline comment
         */
        public void Print(Comment comment);

        /*
         * Prints a Docblock
         */
        public void Print(Docblock docblock);
    }
}
